 using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Todolist.Share.Enum;

namespace Todolist.API.Entities
{
    public class ToDoTask
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        public Guid? AssigneeId { get; set; }

        public int MyProperty { get; set; }

        public DateTime CreatedDate { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        [ForeignKey("AssigneeeId")]
        public User Assignee { get; set; }

    }
}
