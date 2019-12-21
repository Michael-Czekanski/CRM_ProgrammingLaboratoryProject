using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerRelationshipManager.Models
{
    public class Company
    {
        public int ID { get; set; }
        [ForeignKey("BusinessIndustry"), Display(Name="Business Industry")]
        public int BusinessIndustryID { get; set; }
        [ForeignKey("UserWhoAdded")]
        public int UserWhoAddedID { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, RegularExpression("[0-9]{10}")]
        public string NIP { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        public BusinessIndustry BusinessIndustry { get; set; }
        public User UserWhoAdded { get; set; }
        public IEnumerable<BusinessNote> BusinessNotes { get; set; }
        public IEnumerable<ContactPerson> ContactPeople { get; set; }

    }
}
