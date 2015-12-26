using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ToDoList;
using System.Net;
using System.ComponentModel;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IToDoService" in both code and config file together.
    [ServiceContract]
    public interface IToDoService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todolist/{todolistname}")]
        [Description("Get todo list by name.")]
        List<ToDo> GetToDoListByName(string toDoListName);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todolist/{todolistname}/{todotaskid}")]
        [Description("Delete a todo task.")]
        HttpStatusCode DeleteToDo(string toDoListName, string toDoTaskId);

        [OperationContract]
        [WebInvoke(Method = "PATCH",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todolist/{todolistname}/{todotaskid}")]
        [Description("Toggles finnished status of a todo task. If false then sets to true. If true sets to false.")]
        HttpStatusCode ChangeFinnishedStatus(string toDoListName, string toDoTaskId);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todolist/{newtodolistname}")]
        [Description("Add a new todo list with todo tasks.")]
        List<ToDo> AddToDoList(string newToDoListName, List<ToDoMinified> toDoList);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todolist/{todolistname}/")]
        [Description("Add a todo task.")]
        ToDo AddToDoTask(string toDoListName, ToDoMinified toDo);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todolist/{todolistname}/finnishedtasks")]
        [Description("Get finnished tasks withint a todo list")]
        List<ToDo> GetFinnishedToDos(string toDoListName);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo")]
        [Description("Update a todo task.")]
        HttpStatusCode UpdateToDo(ToDo toDo);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "todolist/{todolistname}/todotasks?sort=finnished")]
        [Description("Get a todo list which is sorted by finnished tasks. Finnished tasks first then not finnished.")]
        List<ToDo> GetSortedByFinnished(string toDoListName);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "todolist/{todolistname}/todotasks?sort=deadline")]
        [Description("Get a todo list by name with tasks that is ordered by deadline.")]
        List<ToDo> OrderByDeadline(string toDoListName);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/addseveral")]
        [Description("Add several tasks in a given todo list, using ',' as a delimiter in tasks property of the JSON.")]
        HttpStatusCode AddSeveralTasks(ToDoShort toDos);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/setestimate/{ESTIMATE}/{ID}")]
        [Description("Set estimate on a todo task.")]
        HttpStatusCode SetEstimate(string estimate, string Id);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/addtoestimate/{ESTIMATE}/{ID}")]
        [Description("Toggles Finnished status of a ToDo.")]
        HttpStatusCode AddToEstimate(string estimate, string Id);

        //[OperationContract]               // Kontrakt för den bortkommenterade metoden.
        //[WebInvoke(Method = "GET",
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "todo/eta/{name}")]
        //[Description("Get a list of ToDos ordered by deadline.")]
        //List<ToDo> GetETA(string name);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todolist/{todolistname}/status")]
        [Description("Get a JSON with finnished(int) and unFinnished(int) todo tasks in a given todo list.")]
        ToDoListStatus GetToDoListStatus(string toDoListName);
    }
}
