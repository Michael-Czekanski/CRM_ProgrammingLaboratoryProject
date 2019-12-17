using CustomerRelationshipManager.DataRepositories;
using CustomerRelationshipManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Controllers
{
    public class UserManagementController: Controller
    {
        private IDataRepository<User> _userRepository;

        public UserManagementController(IDataRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public ViewResult All()
        {
            return View(_userRepository.GetAll());
        }

        public ViewResult Details(int? ID)
        {
            return View(_userRepository.Get(ID ?? 1));
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                _userRepository.Add(user);
                return RedirectToAction("Details", new { ID = user.ID });
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int ID)
        {
            User user = _userRepository.Get(ID);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User model)
        {
            if(ModelState.IsValid)
            {
                User user = _userRepository.Get(model.ID);
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.DateOfBirth = model.DateOfBirth;
                user.Login = model.Login;
                user.PasswordSHA256 = model.PasswordSHA256;
                user.RoleID = model.RoleID;

                _userRepository.Edit(user);
                return RedirectToAction("All");

            }


            return View();
        }

        public IActionResult Delete(int ID)
        {
            _userRepository.Delete(ID);
            return RedirectToAction("All");
        }
    }
}
