using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Share
{
   public class LoginResponse
    {
        public bool Successfull { get; set; }

        public string Error { get; set; }

        public string Token { get; set; }
    }
}
