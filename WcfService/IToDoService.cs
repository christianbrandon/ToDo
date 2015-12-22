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
            UriTemplate = "{toDoListName}")]
        [Description("Get todo list by name.")]
        List<ToDo> GetToDoListByName(string toDoListName);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{toDoListName}/{toDoTaskId}")]
        [Description("Delete a todo task.")]
        HttpStatusCode DeleteToDo(string toDoListName, string toDoTaskId);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{toDoListName}/{toDoTaskId}/finnishedstatus")]
        [Description("Toggle finnished status of a todo task. If false sets true and if true sets false.")]
        HttpStatusCode ChangeFinnishedStatus(string toDoListName, string toDoTaskId);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todolist/todo")]
        [Description("Add a todo task.")]
        HttpStatusCode AddToDo(ToDo toDo);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{toDoListName}/finnished")]
        [Description("Get finnished tasks withint a list")]
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
        UriTemplate = "{toDoListname}/sortedbyfinnished")]
        [Description("Get a todo list which is sorted by finnished tasks. Finnished tasks first then not finnished.")]
        List<ToDo> GetSortedByFinnished(string toDoListName);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "{toDoListName}/orderbydeadline")]
        [Description("Get a todo list by name which is ordered by deadline.")]
        List<ToDo> OrderByDeadline(string toDoListName);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/addseveral")]
        [Description("Add several tasks in a given todo list, using ',' as a delimiter in tasks property of the JSON.")]
        HttpStatusCode AddSeveralTasks(ToDoSeveral toDos);

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
            UriTemplate = "{toDoListName}/status")]
        [Description("Get a JSON with finnished(int) and unFinnished(int) todo tasks in a given todo list.")]
        ToDoStatusTracker GetToDoListStatus(string toDoListName);
    }
}
