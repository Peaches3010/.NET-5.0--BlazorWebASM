using System;
using System.ComponentModel.DataAnnotations;
using Todolist.Share.Enum;

namespace TodoList.Share
{
   public class TodoTaskDto
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? AssigneeId { get; set; }

        public string AssigneeName { set; get; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Please select your task priority")]
        public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
}
