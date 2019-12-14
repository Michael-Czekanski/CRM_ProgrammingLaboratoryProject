using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Models
{
    public class BusinessIndustry
    {
        public int ID { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public IEnumerable<Company> Companies { get; set; }

    }
}
