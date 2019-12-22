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

        [HttpGet]
        public IActionResult Create(int CompanyID)
        {
            if (CompanyID <= 0)
            {
                return RedirectToAction("all", "companies");
            }
            BusinessNote model = new BusinessNote();
            model.CompanyID = CompanyID;
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
        public IActionResult Create(BusinessNote businessNote)
        {
            if(ModelState.IsValid)
            {
                _businessNoteRepository.Add(businessNote);
                return RedirectToAction("details", new { ID = businessNote.ID });
            }

            return View(businessNote);
        }

        [HttpGet]
        public IActionResult Edit(int ID)
        {
            BusinessNote model = _businessNoteRepository.Get(ID);
            return View(model);
        }
    }
}
