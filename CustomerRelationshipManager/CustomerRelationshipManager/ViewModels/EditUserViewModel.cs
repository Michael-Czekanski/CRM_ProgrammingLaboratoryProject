using CustomerRelationshipManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.ViewModels
{
    public class EditUserViewModel
    {
        public int ID { get; set; }
        [Required, Display(Name = "Role")]
        public RoleEnum? RoleID { get; set; }
        [Required(ErrorMessage = "Name is required"), MaxLength(20),
            RegularExpression("[A-Z][a-z]+", ErrorMessage = "Invalid name format")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required"), MaxLength(50),
            RegularExpression("[A-Z][a-z]+", ErrorMessage = "Invalid surname format")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Date of birth is required"), DataType(DataType.Date), Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

    }
}
