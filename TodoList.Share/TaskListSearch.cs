using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todolist.Share.Enum;
using TodoList.Share.SeedWork;

namespace TodoList.Share
{
     public class TaskListSearch : PagingParameters
    {
            public string Name { get; set; }

            public Guid? AssigneeId { get; set; }

            public Priority? Priority { get; set; }
        
    }
}
