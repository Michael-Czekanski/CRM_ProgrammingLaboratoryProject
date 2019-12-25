using CustomerRelationshipManager.CustomAttributes;
using CustomerRelationshipManager.DataRepositories;
using CustomerRelationshipManager.Helpers;
using CustomerRelationshipManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Controllers
{
    [Authorize("Admin", "Moderator", "Normal")]
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
            IEnumerable <BusinessNote> model = _businessNoteRepository.GetAll().Where(b => b.IsDeleted == false).ToList();
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

            int userWhoAddedID;
            if (UserManagementHelper.TryGetLoggedUserID(HttpContext, out userWhoAddedID))
            {
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

        [HttpPost]
        public IActionResult Edit(BusinessNote businessNote)
        {
            if(ModelState.IsValid)
            {
                _businessNoteRepository.Edit(businessNote);
                return RedirectToAction("all");
            }

            return View(businessNote);
        }

        public IActionResult Delete(int ID)
        {
            BusinessNote businessNoteToDelete = _businessNoteRepository.Get(ID);
            businessNoteToDelete.IsDeleted = true;
            _businessNoteRepository.Edit(businessNoteToDelete);
            return RedirectToAction("all");
        }

    }
}
