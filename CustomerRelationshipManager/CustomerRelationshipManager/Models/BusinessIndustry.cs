using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Models
{
    public enum BusinessIndustryEnum
    {
        Aerospace,
        Transport,
        Computer,
        Telecommunication,
        Agriculture,
        Construction,
        Education,
        Pharmaceutical,
        Food,
        HealthCare,
        Hospitality,
        Entertainment,
        NewsMedia,
        Energy,
        Manufacturing,
        Music,
        Mining,
        WorldwideWeb,
        Electronics
    }
    public class BusinessIndustry
    {
        public BusinessIndustryEnum ID { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public IEnumerable<Company> Companies { get; set; }

    }
}
