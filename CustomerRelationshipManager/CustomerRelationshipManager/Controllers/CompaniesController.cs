using CustomerRelationshipManager.DataRepositories;
using CustomerRelationshipManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Controllers
{
    public class CompaniesController: Controller
    {
        private IDataRepository<Company> _companyRepository;

        public CompaniesController(IDataRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IActionResult All()
        {
            return View();
        }

    }
    
}
