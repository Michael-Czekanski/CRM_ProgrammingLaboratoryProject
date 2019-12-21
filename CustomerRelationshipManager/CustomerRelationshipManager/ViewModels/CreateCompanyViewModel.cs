using CustomerRelationshipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.ViewModels
{
    public class CreateCompanyViewModel: Company
    {
        public IEnumerable<BusinessIndustry> BusinessIndustries { get; set; }
    }
}
