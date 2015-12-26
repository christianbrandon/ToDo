using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ToDoList
{
    [DataContract]
    public class ToDoShort
    {
        [DataMember(Name = "toDoListName", IsRequired = true)]
        public string ToDoListName { get; set; }

        [DataMember(Name = "tasks", IsRequired = true)]
        public string Tasks { get; set; }
    }
}
