using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todolist.Share.Enum;

namespace TodoList.Share
{
    public class TodoTaskUpdateRequest
    {
        public string Name { get; set; }

        public Priority Priority { get; set; }
    }
}
