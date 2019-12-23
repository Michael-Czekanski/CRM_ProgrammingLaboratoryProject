using CustomerRelationshipManager.DataRepositories;
using CustomerRelationshipManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Controllers
{
    public class ContactPeopleController: Controller
    {
        private readonly IContactPersonRepository _contactPersonRepository;
        public ContactPeopleController(IContactPersonRepository contactPersonRepository)
        {
            _contactPersonRepository = contactPersonRepository;
        }


        public IActionResult Index()
        {
            return RedirectToAction("all");
        }

        public IActionResult All()
        {
            IEnumerable<ContactPerson> model = _contactPersonRepository.GetAll().ToList();
            foreach (ContactPerson contactPerson in model)
            {
                _contactPersonRepository.FillCompanyNavProperty(contactPerson);
            }
            return View(model);
        }


        public IActionResult Details(int ID)
        {
            ContactPerson model = _contactPersonRepository.Get(ID);
            _contactPersonRepository.FillCompanyNavProperty(model);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create(int CompanyID)
        {
            if(CompanyID <= 0)
            {
                return RedirectToAction("all", "companies");
            }
            ContactPerson model = new ContactPerson();
            model.CompanyID = CompanyID;
            byte[] userWhoAddedIDBytes;
            int userWhoAddedID;
            if (HttpContext.Session.TryGetValue("UserID", out userWhoAddedIDBytes))
            {
                userWhoAddedID = BitConverter.ToInt32(userWhoAddedIDBytes, 0);
                model.UserWhoAddedID = userWhoAddedID;
            }
            else
            {
                return RedirectToAction("signin", "home");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ContactPerson model)
        {
            if(ModelState.IsValid)
            {
                _contactPersonRepository.Add(model);
                return RedirectToAction("details", new { ID = model.ID });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int ID)
        {
            ContactPerson model = _contactPersonRepository.Get(ID);
            return View(model);
        }

    }
}
