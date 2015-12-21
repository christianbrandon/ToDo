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
        [Description("Get todo list by name.")]
        List<ToDo> GetToDoListByName(string name);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/{Id}")]
        [Description("Delete a todo task by Id.")]
        HttpStatusCode DeleteToDo(string Id);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/{Id}")]
        [Description("Toggle finnished status of a todo task. If false sets true and if true sets false.")]
        HttpStatusCode ChangeFinnishedStatus(string Id);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo")]
        [Description("Add a todo task.")]
        HttpStatusCode AddToDo(ToDo toDo);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "todo/{name}/finnished")]
        [Description("Get a todo list by name with tasks that are finnished.")]
        List<ToDo> GetFinnishedToDos(string name);

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
        UriTemplate = "todo/{name}/sortedbyfinnished")]
        [Description("Get a todo list which is sorted by finnished tasks. Finnished tasks first then not finnished.")]
        List<ToDo> GetSortedByFinnished(string name);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "todo/{name}/orderbydeadline")]
        [Description("Get a todo list by name which is ordered by deadline.")]
        List<ToDo> OrderByDeadline(string name);

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
            UriTemplate = "todo/{name}/numbertodos")]
        [Description("Returns a string with information about the numbers of finnished and unfinnished tasks.")]
        string NumberOfTodos(string name);
    }
}
