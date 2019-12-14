using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerRelationshipManager.Models
{
    public class ContactPerson
    {
        public int ID { get; set; }
        [ForeignKey("Company"), Required]
        public int CompanyID { get; set; }
        [ForeignKey("UserWhoAdded")]
        public int UserWhoAddedID { get; set; }
        [Required(ErrorMessage = "Name is required"), MaxLength(50), RegularExpression("[A-Z][a-z]+", ErrorMessage = "Invalid name format")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required"), MaxLength(20), RegularExpression("[A-Z][a-z]+", ErrorMessage = "Invalid surname format")]
        public string Surname { get; set; }
        [Required, Display(Name = "Office Email"), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string TelephoneNum { get; set; }
        public string Position { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        public Company Company { get; set; }
        public User UserWhoAdded { get; set; }

    }
}
