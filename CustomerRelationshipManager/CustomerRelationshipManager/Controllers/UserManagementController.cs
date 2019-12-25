using CustomerRelationshipManager.DataRepositories;
using CustomerRelationshipManager.Helpers;
using CustomerRelationshipManager.Models;
using CustomerRelationshipManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Controllers
{
    [CustomAttributes.Authorize("Admin", "Moderator")]
    public class UserManagementController: Controller
    {
        private IUserRepository _userRepository;

        public UserManagementController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        

        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        public ViewResult All()
        {
            return View(_userRepository.GetAll());
        }

        public IActionResult Details(int? ID)
        {
            User userDetailsWithEmptyNavProp = _userRepository.Get(ID ?? 1);

            int loggedInUserID = -1;
            UserManagementHelper.TryGetLoggedUserID(HttpContext, out loggedInUserID);
            
            if(loggedInUserID != userDetailsWithEmptyNavProp.ID)
            {
                switch (userDetailsWithEmptyNavProp.RoleID)
                {
                    case RoleEnum.Admin:
                        if (!HttpContext.User.HasClaim(ClaimTypes.Role, RoleEnum.Admin.ToString()))
                        {
                            return RedirectToAction("NoPermission", "Dashboard");
                        }
                        break;
                    case RoleEnum.Moderator:
                        if (!HttpContext.User.HasClaim(ClaimTypes.Role, RoleEnum.Admin.ToString()))
                        {
                            return RedirectToAction("NoPermission", "Dashboard");
                        }
                        break;
                }
            }
            
            User userDetailsWithFilledNavProp = _userRepository.GetAllAddedByUser(userDetailsWithEmptyNavProp);
            return View(userDetailsWithFilledNavProp);
        }

        [HttpGet, CustomAttributes.Authorize("Admin")]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost, CustomAttributes.Authorize("Admin")]
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
        public IActionResult Edit(int ID)
        {
            User userToEdit = _userRepository.Get(ID);

            byte[] loggedInUserIDBytes;
            int loggedInUserID = -1;
            if (HttpContext.Session.TryGetValue("UserID", out loggedInUserIDBytes))
            {
                loggedInUserID = BitConverter.ToInt32(loggedInUserIDBytes, 0);
            }

            // Allow user to edit himself without checking his role
            if(loggedInUserID != userToEdit.ID)
            {
                switch (userToEdit.RoleID)
                {
                    case RoleEnum.Admin:
                        // You can't edit admin
                        return RedirectToAction("NoPermission", "Dashboard");
                    case RoleEnum.Moderator:
                        // If you are not admin, you can't edit moderator
                        if (!User.HasClaim(ClaimTypes.Role, RoleEnum.Admin.ToString()))
                        {
                            return RedirectToAction("NoPermission", "Dashboard");
                        }
                        break;
                    case RoleEnum.Normal:
                        // If you are not admin or moderator, you can't edit normal user
                        if ((!User.HasClaim(ClaimTypes.Role, RoleEnum.Admin.ToString()))
                            && (!User.HasClaim(ClaimTypes.Role, RoleEnum.Moderator.ToString())))
                        {
                            return RedirectToAction("NoPermission", "Dashboard");
                        }
                        break;
                }
            }
            
            EditUserViewModel model = new EditUserViewModel()
            {
                ID = userToEdit.ID,
                Name = userToEdit.Name,
                Surname = userToEdit.Surname,
                DateOfBirth = userToEdit.DateOfBirth,
                RoleID = userToEdit.RoleID,
                IsDeleted = userToEdit.IsDeleted
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                User userToEdit = _userRepository.Get(model.ID);

                byte[] loggedInUserIDBytes;
                int loggedInUserID = -1;
                if (HttpContext.Session.TryGetValue("UserID", out loggedInUserIDBytes))
                {
                    loggedInUserID = BitConverter.ToInt32(loggedInUserIDBytes, 0);
                }

                // Allow user to edit himself without checking his role
                if (loggedInUserID != userToEdit.ID)
                {
                    switch (userToEdit.RoleID)
                    {
                        case RoleEnum.Admin:
                            // You can't edit admin
                            return RedirectToAction("NoPermission", "Dashboard");
                        case RoleEnum.Moderator:
                            // If you are not admin, you can't edit moderator
                            if (!User.HasClaim(ClaimTypes.Role, RoleEnum.Admin.ToString()))
                            {
                                return RedirectToAction("NoPermission", "Dashboard");
                            }
                            break;
                        case RoleEnum.Normal:
                            // If you are not admin or moderator, you can't edit normal user
                            if ((!User.HasClaim(ClaimTypes.Role, RoleEnum.Admin.ToString()))
                                && (!User.HasClaim(ClaimTypes.Role, RoleEnum.Moderator.ToString())))
                            {
                                return RedirectToAction("NoPermission", "Dashboard");
                            }
                            break;
                    }
                }
                    
                userToEdit.Name = model.Name;
                userToEdit.Surname = model.Surname;
                userToEdit.DateOfBirth = model.DateOfBirth;
                if (User.HasClaim(ClaimTypes.Role, RoleEnum.Admin.ToString()))
                {
                    userToEdit.RoleID = model.RoleID;
                    userToEdit.IsDeleted = model.IsDeleted;
                }
                

                _userRepository.Edit(userToEdit);
                return RedirectToAction("All");

            }


            return View();
        }

        [CustomAttributes.Authorize("Admin")]
        public IActionResult Delete(int ID)
        {
            User userToDelete = _userRepository.Get(ID);
            if(userToDelete.RoleID == RoleEnum.Admin)
            {
                // You can't delete admin
                return RedirectToAction("NoPermission", "Dashboard");
            }
            _userRepository.Delete(ID);
            return RedirectToAction("All");
        }
    }
}
