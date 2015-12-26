using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ToDoList
{
    [DataContract]
    public class ToDoListStatus
    {
        [DataMember(Name = "finnished")]
        public int finnished { get; set; }

        [DataMember(Name = "unFinnished")]
        public int unFinnished { get; set; }
    }
}
