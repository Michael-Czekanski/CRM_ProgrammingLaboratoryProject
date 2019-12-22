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
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                string token = tokenProvider.ProvideToken(user);
                HttpContext.Session.Set("JWT", Encoding.ASCII.GetBytes(token));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
