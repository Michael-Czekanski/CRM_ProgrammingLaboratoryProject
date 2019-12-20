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
        private ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IActionResult All()
        {
            IEnumerable<Company> companies = _companyRepository.GetAll().ToList();
            foreach(Company company in companies)
            {
                _companyRepository.FillBusinessIndustryNavProperty(company);
            }
            return View(companies);
        }

        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

    }
    
}
