using ClinicManagementSystem.Services;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using static SessionExtensions;

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
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    HttpContext.Session.SetObjectAsJson(SessionVariables.SessionData, null);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                SetSessionModel setSessionModel = new SetSessionModel();
                if (loginModel != null)
                {
                    ResponseModel responseModel = accountServices.checkLoginCredentials(loginModel);
                    if (responseModel != null && responseModel.Id > 0)
                    {
                        setSessionModel.Firstame = responseModel.Firstame;
                        setSessionModel.Lastname = responseModel.Lastname;
                        setSessionModel.Mobilenumber = responseModel.Mobilenumber;
                        setSessionModel.Email = responseModel.Email;
                        setSessionModel.Address = responseModel.Address;
                        setSessionModel.Officaname = responseModel.Officaname;
                        setSessionModel.City = responseModel.City;
                        setSessionModel.Gender = responseModel.Gender;
                        setSessionModel.Username = responseModel.Username;
                        HttpContext.Session.SetObjectAsJson(SessionVariables.SessionData, setSessionModel);
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
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    return View();
                }
                else
                {
                    TempData["msg"] = "Session Not Found";
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
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
        [HttpGet]
        public IActionResult Sidebar()
        {
            return View();
        }


    }
}