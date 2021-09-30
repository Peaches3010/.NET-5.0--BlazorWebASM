using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Todolist.API.Entities
{
    public class User : IdentityUser<Guid>
    {
        [MaxLength(200)]
        public string LastName { get; set; }

        [MaxLength(200)]
        public string FirstName { get; set; }
    }
}
