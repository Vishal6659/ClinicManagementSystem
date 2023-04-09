using ClinicManagementSystem.Services;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClinicManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IAccountServices accountServices = new AccountServices();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                if (loginModel != null)
                {
                    bool data = accountServices.checkLoginCredentials(loginModel);
                    if (data != false)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegistrationModel registrationModel)
        {
            try
            {
                if (registrationModel != null)
                {
                    bool data = accountServices.insertRegistrationData(registrationModel);
                    if (data != false)
                    {
                        return RedirectToAction("Login", "Home");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
      
    }
}