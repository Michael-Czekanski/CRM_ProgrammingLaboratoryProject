using CustomerRelationshipManager.DataRepositories;
using CustomerRelationshipManager.Models;
using CustomerRelationshipManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Controllers
{
    [Authorize]
    public class CompaniesController: Controller
    {
        private ICompanyRepository _companyRepository;
        private IDataRepository<BusinessIndustry> _businessIndustryRepository;

        public CompaniesController(ICompanyRepository companyRepository, 
            IDataRepository<BusinessIndustry> businessIndustryRepository)
        {
            _companyRepository = companyRepository;
            _businessIndustryRepository = businessIndustryRepository;
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

        [HttpGet]
        public IActionResult Create()
        {
            CreateCompanyViewModel model = new CreateCompanyViewModel()
            {
                BusinessIndustries = _businessIndustryRepository.GetAll().ToList()
            };

            byte[] userWhoAddedIDBytes;
            int userWhoAddedID;
            if (HttpContext.Session.TryGetValue("UserID", out userWhoAddedIDBytes))
            {
                userWhoAddedID = BitConverter.ToInt32(userWhoAddedIDBytes, 0);
                model.UserWhoAddedID = userWhoAddedID;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateCompanyViewModel createCompanyViewModel)
        {
            if (ModelState.IsValid)
            {
                _companyRepository.Add(createCompanyViewModel);
                return RedirectToAction("Details", new { ID = createCompanyViewModel.ID });
            }
            createCompanyViewModel.BusinessIndustries = _businessIndustryRepository.GetAll().ToList();
            return View(createCompanyViewModel);
        }

        public IActionResult Details(int ID)
        {
            Company company = _companyRepository.Get(ID);
            return View(company);
        }

    }
    
}
