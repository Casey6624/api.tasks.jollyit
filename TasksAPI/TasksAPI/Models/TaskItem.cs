using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TasksAPI.Models
{
    public class TaskItem
    {
        // [key] from ComponentModel.DataAnnotations is needed as .Net assumes PK is "Id"
        [Key]
        public long taskID { get; set; }
        public string assignedTo { get; set; }
        public int priority { get; set; }
        public string taskTitle { get; set; }
        public string taskDescription { get; set; }
        public string taskTime { get; set; }
        public int status { get; set; }
    }
}
