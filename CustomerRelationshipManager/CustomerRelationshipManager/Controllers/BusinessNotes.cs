using CustomerRelationshipManager.DataRepositories;
using CustomerRelationshipManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Controllers
{
    public class BusinessNotes: Controller
    {
        private readonly IBusinessNoteRepository _businessNoteRepository;

        public BusinessNotes(IBusinessNoteRepository businessNoteRepository)
        {
            _businessNoteRepository = businessNoteRepository;
        }

        public IActionResult Index()
        {
            return RedirectToAction("all");
        }

        public IActionResult All()
        {
            IEnumerable <BusinessNote> model = _businessNoteRepository.GetAll().ToList();
            foreach(BusinessNote businessNote in model)
            {
                _businessNoteRepository.FillCompanyNavProperty(businessNote);
            }
            return View(model);
        }

        public IActionResult Details(int ID)
        {
            BusinessNote model = _businessNoteRepository.Get(ID);
            _businessNoteRepository.FillCompanyNavProperty(model);
            return View(model);
        }
    }
}
