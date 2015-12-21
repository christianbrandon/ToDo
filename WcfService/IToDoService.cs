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
            UriTemplate = "todo/{name}")]
        [Description("Gets ToDoList by name.")]
        List<ToDo> GetToDoListByName(string name);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/{Id}")]
        [Description("Deletes a Todo task by Id.")]
        HttpStatusCode DeleteToDo(string Id);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/{Id}")]
        [Description("Toggles Finnished status of a ToDo.")]
        HttpStatusCode ChangeFinnishedStatus(string Id);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo")]
        [Description("Adds a todo.")]
        HttpStatusCode AddToDo(ToDo toDo);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/{name}/finnished")]
        [Description("Gets a ToDoList by name with tasks that are finnished.")]
        List<ToDo> GetFinnishedToDos(string name);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo")]
        [Description("Updates a todo.")]
        HttpStatusCode UpdateToDo(ToDo toDo);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "todo/{name}/sortedbyfinnished")]
        [Description("Get a list of ToDos todo followed by a list of finnished.")]
        List<ToDo> GetSortedByFinnished(string name);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "todo/{name}/orderbydeadline")]
        [Description("Get a list of ToDos ordered by deadline.")]
        List<ToDo> OrderByDeadline(string name);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/addseveral")]
        [Description("Adds several tasks using ',' as a delimiter in Tasks property.")]
        HttpStatusCode AddSeveralTasks(ToDos toDos);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/setestimate/{ESTIMATE}/{ID}")]
        [Description("Toggles Finnished status of a ToDo.")]
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
            UriTemplate = "todo/{name}/numbertodos")]
        [Description("Returns a string with information about the numbers of finnished and unfinnished tasks.")]
        string NumberOfTdos(string name);
    }
}
