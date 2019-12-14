using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Models
{
    public class User
    {
        public int ID { get; set; }
        [ForeignKey("Role"), Required]
        public RoleEnum RoleID { get; set; }
        [Required(ErrorMessage = "Name is required"), MaxLength(20), 
            RegularExpression("[A-Z][a-z]+", ErrorMessage = "Invalid name format")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required"), MaxLength(50), 
            RegularExpression("[A-Z][a-z]+", ErrorMessage = "Invalid surname format")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Login is required"), MaxLength(30)]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password)]
        public string PasswordSHA256 { get; set; }
        [Required(ErrorMessage = "Date of birth is required"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        public Role Role { get; set; }
        public IEnumerable<BusinessNote> BusinessNotes { get; set; }
        public IEnumerable<ContactPerson> AddedContactPeople { get; set; }
        public IEnumerable<Company> AddedCompanies { get; set; }

    }
}
