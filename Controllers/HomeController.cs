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
                    if (responseModel.DocId != null && responseModel.AadharCardNumber != null)
                    {
                        setSessionModel.DocId = responseModel.DocId;
                        setSessionModel.Firstame = responseModel.FirstName;
                        setSessionModel.Lastname = responseModel.LastName;
                        setSessionModel.Mobilenumber = Convert.ToInt64(responseModel.PhoneNumber);
                        setSessionModel.Email = responseModel.EmailAddress;
                        setSessionModel.Address = responseModel.Address;
                        setSessionModel.Gender = responseModel.Gender;
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
                        TempData["msg"] = "Registration UnSuccesfull";                        
                    }
                    else
                    {
                        TempData["msg"] = "Registration Succesfull";                        
                    }
                    return RedirectToAction("Login", "Home");
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