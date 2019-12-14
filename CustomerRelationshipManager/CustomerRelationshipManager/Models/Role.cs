using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Models
{
    public enum RoleEnum
    {
        Admin, Moderator, Normal
    }
    public class Role
    {
        public RoleEnum ID { get; set; }
        [Required]
        public string Name { get; set; }

        public IEnumerable<User> UsersWithThisRole { get; set; }
    }
}
