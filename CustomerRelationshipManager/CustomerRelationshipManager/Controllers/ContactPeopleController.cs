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
        
    }
}
