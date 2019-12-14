using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerRelationshipManager.Models
{
    public class BusinessNote
    {
        public int ID { get; set; }
        [Required, ForeignKey("Company")]
        public int CompanyID { get; set; }
        [ForeignKey("UserWhoAdded")]
        public int UserWhoAddedID { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        public Company Company { get; set; }
        public User UserWhoAdded { get; set; }

    }
}
