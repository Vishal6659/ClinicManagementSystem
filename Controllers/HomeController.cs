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
                        TempData["msg"] = "Login Succesfull";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["msg"] = "Login Unsuccesfull";
                        return View();
                    }
                }
                else 
                {
                    TempData["msg"] = "Please enter the Login Credentials";
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Index()
        {            
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
                    int data = accountServices.insertRegistrationData(registrationModel);
                    if (data != 1)
                    {
                        TempData["msg"] = "Registration Succesfull";
                        return RedirectToAction("Login", "Home");
                    }
                    else 
                    {
                        TempData["msg"] = "Registration UnSuccesfull";
                        return View();
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
        }                    
    }
}