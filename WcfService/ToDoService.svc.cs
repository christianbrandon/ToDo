using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using Repository;
using ToDoList;
using System.ServiceModel.Web;
using System.Net;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ToDoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ToDoService.svc or ToDoService.svc.cs at the Solution Explorer and start debugging.
    public class ToDoService : IToDoService
    {

        private DAL repo;

        public ToDoService()
        {
            string connection = @"Data Source = ALEXEJ-L450; Initial Catalog = DB_ToDoList; User ID = RestFullUser; Password = RestFull123";
            repo = new DAL(connection);
        }

        /// <summary>
        /// Get todo list by name.
        /// </summary>
        /// <param name="toDoListName">todo list name.</param>
        /// <returns>Todo tasks found in the list specified by name parameter. Throws 404 if not found</returns>
        public List<ToDo> GetToDoListByName(string toDoListName)
        {
            List<ToDo> toDoList = this.repo.GetToDoListByName(toDoListName);

            if (!toDoList.Any())
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }

            return toDoList;
        }

        /// <summary>
        /// Delete todo task in a todo list.
        /// </summary>
        /// <param name="toDoListName">The name of the list.</param>
        /// <param name="toDoTaskId">The id of the task to delete.</param>
        /// <returns>404 if list or task within the list not found. Otherwise 200.</returns>
        public HttpStatusCode DeleteToDo(string toDoListName, string toDoTaskId)
        {
            int parsedId = int.Parse(toDoTaskId);

            List<ToDo> toDoList = repo.GetToDoListByName(toDoListName);

            if (toDoList == null)
            {
                return HttpStatusCode.NotFound;
            }

            ToDo toDoTaskToDelete = toDoList.FirstOrDefault(toDotask => toDotask.Id == parsedId);

            if (toDoTaskToDelete == null)
            {
                return HttpStatusCode.NotFound;
            }

            this.repo.DeleteToDo(toDoTaskToDelete.Id);

            return HttpStatusCode.OK;
        }

        /// <summary>
        /// Change status of a todo task in a todo list.
        /// </summary>
        /// <param name="toDoListName">The name of the todo list.</param>
        /// <param name="toDoTaskId">The id of the todo task.</param>
        /// <returns>200 OK if succefull otherwise 404 if list is not found or if id is not within the list.</returns>
        public HttpStatusCode ChangeFinnishedStatus(string toDoListName, string toDoTaskId)
        {
            int parsedId = int.Parse(toDoTaskId);

            List<ToDo> toDoList = this.repo.GetToDoListByName(toDoListName);

            if (toDoList == null)
            {
                return HttpStatusCode.NotFound;
            }

            ToDo toDoTask = toDoList.FirstOrDefault(task => task.Id == parsedId);
            
            if (toDoTask == null)
            {
                return HttpStatusCode.NotFound;
            }

            toDoTask.Finnished = (toDoTask.Finnished) ? false : true;

            this.repo.UpdateToDo(toDoTask);

            return HttpStatusCode.OK;
            
        }

        /// <summary>
        /// Adds a ToDo.
        /// </summary>
        /// <param name="toDo">ToDo JSON object.</param>
        /// <returns>HTTP OK if success, else HTTP NotAcceptable</returns>
        public HttpStatusCode AddToDo(ToDo toDo)
        {

            if (toDo.Name.Length >= 6 && Regex.IsMatch(toDo.Name, @"^[a-zA-Z]+$"))
            // Check length and that the name uses only letters.
            {
                this.repo.AddToDo(toDo);
                return HttpStatusCode.Created;
            }
            else
            {
                return HttpStatusCode.NotAcceptable;
            }
        }

        /// <summary>
        /// Returns ToDo tasks that are finnished.
        /// </summary>
        /// <param name="name">The name of the ToDo List.</param>
        /// <returns>List with ToDo tasks that are finnished. If ToDoList is not found HTTP NotFound</returns>
        public List<ToDo> GetFinnishedToDos(string toDoListName)
        {
            List<ToDo> toDoList = this.repo.GetToDoListByName(toDoListName);

            if (!toDoList.Any())
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }

            List<ToDo> finnishedToDos = new List<ToDo>();

            foreach (var toDo in toDoList)
            {
                if (toDo.Finnished)
                {
                    finnishedToDos.Add(toDo);
                }
            }

            return finnishedToDos;
        }

        /// <summary>
        /// Updated a ToDo task.
        /// </summary>
        /// <param name="toDo">ToDo object</param>
        /// <returns>HTTP OK sucess. HTTP NotFound if task can't be found</returns>
        public HttpStatusCode UpdateToDo(ToDo toDo)
        {
            if (this.repo.GetToDoById(toDo.Id) == null)
            {
                return HttpStatusCode.NotFound;
            }

            this.repo.UpdateToDo(toDo);

            return HttpStatusCode.OK;
        }

        /// <summary>
        /// Returns a list of finnished and unfinnished ToDos.
        /// </summary>
        /// <param name="toDoListName">The name of the ToDolist</param>
        /// <returns>A sorted list of Finnished and unfinnished ToDos in the given list</returns>
        public List<ToDo> GetSortedByFinnished(string toDoListName)
        {
            List<ToDo> toDoList = this.repo.GetToDoListByName(toDoListName);


            if (!toDoList.Any())
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }

            List<ToDo> toDos = new List<ToDo>();

            foreach (var toDo in toDoList)
            {
                if (toDo.Finnished)
                {
                    toDos.Add(toDo);
                }
            }

            foreach (var toDo in toDoList)
            {
                if (!toDo.Finnished)
                {
                    toDos.Add(toDo);
                }
            }

            return toDos;
        }

        /// <summary>
        /// Returns ordered ToDoList by deadline
        /// </summary>
        /// <param name="toDoListName">The name of the ToDoList</param>
        /// <returns>Ordered ToDoList by deadline</returns>

        public List<ToDo> OrderByDeadline(string toDoListName)
        {
            List<ToDo> toDoList = this.repo.GetToDoListByName(toDoListName);

            if (!toDoList.Any())
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }

            List<ToDo> orderByDeadline = new List<ToDo>();

            foreach (var toDo in toDoList)
            {
                if (!toDo.Finnished)
                {
                    orderByDeadline.Add(toDo);
                }

            }

            foreach (var toDo in orderByDeadline)
            {
                orderByDeadline.Sort((dl1, dl2) => DateTime.Compare(dl1.DeadLine, dl2.DeadLine));
            }

            return orderByDeadline;
        }

        /// <summary>
        /// Adds several ToDo tasks.
        /// </summary>
        /// <param name="toDos">List of ToDos.</param>
        /// <returns>HTTP Status based on if succesfull or not.</returns>
        public HttpStatusCode AddSeveralTasks(ToDoSeveral toDos)
        {

            if (toDos.Tasks.IndexOf(',') == -1)
            {
                return HttpStatusCode.NotAcceptable;
            }

            string[] tasks = toDos.Tasks.Split(',');

            foreach (var task in tasks)
            {
                ToDo toDo = new ToDo();
                toDo.Description = task.Trim(' ');
                toDo.Name = toDos.ToDoListName;
                toDo.CreatedDate = DateTime.Now;
                toDo.DeadLine = DateTime.Now;
                toDo.EstimationTime = 0;
                toDo.Finnished = false;

                this.AddToDo(toDo);
            }

            return HttpStatusCode.Created;
        }

        /// <summary>
        /// Overwrites EstimatedTime for ToDoList by ID
        /// </summary>
        /// <param name="estimate">Estimated time in minutes</param>
        /// <param name="Id">ID of the ToDoList</param>
        /// <returns>HTTP Status based on if succesfull or not.</returns>
        public HttpStatusCode SetEstimate(string estimate, string Id)
        {
            int parsedId = int.Parse(Id);
            int parsedEstimate = int.Parse(estimate);

            ToDo toDo = this.repo.GetToDoById(parsedId);

            if (this.repo.GetToDoById(toDo.Id) == null)
            {
                return HttpStatusCode.NotFound;
            }

            toDo.EstimationTime = parsedEstimate;

            return HttpStatusCode.OK;
        }

        /// <summary>
        /// Adds to current EstimatedTime for ToDoList by ID
        /// </summary>
        /// <param name="estimate">Estimated time in minutes</param>
        /// <param name="Id">ID of the ToDoList</param>
        /// <returns>HTTP Status based on if succesfull or not.</returns>
        public HttpStatusCode AddToEstimate(string estimate, string Id)
        {
            int parsedId = int.Parse(Id);
            int parsedEstimate = int.Parse(estimate);

            ToDo toDo = this.repo.GetToDoById(parsedId);

            if (this.repo.GetToDoById(toDo.Id) == null)
            {
                return HttpStatusCode.NotFound;
            }

            toDo.EstimationTime = toDo.EstimationTime + parsedEstimate;

            return HttpStatusCode.OK;
        }

        /// <summary>
        /// Get finnished status of todo tasks in a todo list.
        /// </summary>
        /// <param name="toDoListName">The name of the todo list.</param>
        /// <returns>JSON with finnished and unfinnished property.</returns>
        public ToDoStatusTracker GetToDoListStatus(string toDoListName)
        {
            List<ToDo> toDoList = repo.GetToDoListByName(toDoListName);

            if (toDoList == null)
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }

            ToDoStatusTracker statusTracker = new ToDoStatusTracker();
            
            foreach (var toDo in toDoList)
            {
                if (toDo.Finnished)
                {
                    statusTracker.finnished++;
                }
                else if (!toDo.Finnished)
                {
                    statusTracker.unFinnished++;
                }
            }

            return statusTracker;
        }


        /// <summary>
        /// Add a new todo list.
        /// </summary>
        /// <param name="toDoList">List of todo tasks.</param>
        /// <returns>HTTP 409 if the todo list already exists.
        /// HTTP 400 if ToDo.Name property is not equal in all of the objects within the list.
        /// HTTP 201 on success.</returns>
        public HttpStatusCode AddToDoList(List<ToDo> toDoList)
        {
            if (toDoList == null)
            {
                return HttpStatusCode.BadRequest;
            }

            
            ToDo toDoTask = toDoList.First();

            string toDoListName = toDoTask.Name;

            //Check to see if the .Name property of the ToDo objects in the list are equal.
            foreach (var toDoTaskName in toDoList)
            {
                if (toDoTaskName.Name.ToLower() != toDoListName.ToLower())
                {
                    return HttpStatusCode.BadRequest;
                }
            }

            bool alreadyExists = this.repo.GetToDoListByName(toDoListName).Any();
            
            if (alreadyExists)
            {
                return HttpStatusCode.Conflict;
            }

            foreach (var toDo in toDoList)
            {
                this.repo.AddToDo(toDo);
            }

            return HttpStatusCode.Created;
        }

        //public DateTime GetETA(string toDoName) // Denna kod är ett WIP vad gäller att hämta och konvertera alla EstimatedTime till en klump, och sen lägga det på dagens datum för att hitta en ETA.
        //{
        //    DateTime ETA = new DateTime();
        //    int tempETA;

        //    List<ToDo> toDoLoop = repo.GetToDoListByName(toDoName);

        //    foreach (var item in toDoLoop)
        //    {
        //        tempETA += item.EstimationTime;
        //    }

        //    tempETA = tempETA / 60;
        //    ETA.Hour + ETA.Hour + tempETA;
        //}
    }
}
