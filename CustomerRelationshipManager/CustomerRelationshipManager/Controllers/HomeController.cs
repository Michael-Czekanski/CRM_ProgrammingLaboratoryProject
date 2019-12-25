using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomerRelationshipManager.Models;
using Microsoft.AspNetCore.Authorization;
using CustomerRelationshipManager.ViewModels;
using CustomerRelationshipManager.DataRepositories;
using System.Text;
using CustomerRelationshipManager.Helpers;

namespace CustomerRelationshipManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserRepository _userRepository;
        private readonly ITokenProvider tokenProvider;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository, ITokenProvider tokenProvider)
        {
            _logger = logger;
            this._userRepository = userRepository;
            this.tokenProvider = tokenProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _userRepository.Authenticate(model.Login, model.Password);
                if (user == null)
                {
                    return new ObjectResult("Username or password is incorrect");
                }
                else if(user.IsDeleted == true)
                {
                    return new ObjectResult("Username or password is incorrect");
                }
                string token = tokenProvider.ProvideToken(user);
                HttpContext.Session.Set("JWT", Encoding.ASCII.GetBytes(token));
                HttpContext.Session.Set("UserID", BitConverter.GetBytes(user.ID)); 
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if(ModelState.IsValid)
            {
                if(_userRepository.GetAll().Where(u => u.Login == model.Login).FirstOrDefault() != null)
                {
                    return new ObjectResult("There is user with that login");
                }
                _userRepository.Add(model);
                return RedirectToAction("signin");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccountDetails()
        {
            if(User.Identity.IsAuthenticated)
            {
                int loggedInUserID;
                UserManagementHelper.TryGetLoggedUserID(HttpContext, out loggedInUserID);

                User user = _userRepository.Get(loggedInUserID);
                _userRepository.GetAllAddedByUser(user);
                return View(user);
            }
            return RedirectToAction("SignIn", "Home");
        }
    }
}
