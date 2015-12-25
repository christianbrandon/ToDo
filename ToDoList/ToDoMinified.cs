using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ToDoList
{
    [DataContract]
    public class ToDoMinified
    {
        [DataMember (Name = "taskDescription")]
        public string TaskDescription { get; set; }

        [DataMember (Name = "finnished")]
        public bool Finnished { get; set; }

        [DataMember (Name = "deadLine")]
        public DateTime? DeadLine { get; set; }

        [DataMember (Name = "esimationTime")]
        public int? EstimationTime { get; set; }
    }
}
