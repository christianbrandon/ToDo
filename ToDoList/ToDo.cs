using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    [DataContract]
    public class ToDo
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }

        [DataMember(Name = "description", IsRequired = true)]
        public string Description { get; set; }

        [DataMember(Name = "finnished", IsRequired = true)]
        public bool Finnished { get; set; }

        [DataMember(Name = "createdDate", IsRequired = true)]
        public DateTime CreatedDate { get; set; }

        [DataMember(Name = "deadLine")]
        public DateTime DeadLine { get; set; }

        [DataMember(Name = "estimationTime")]
        public int EstimationTime { get; set; }
    }
}
