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
                if (loginModel != null && loginModel.Username != null && loginModel.Password != null)
                {
                    ResponseModel responseModel = accountServices.checkLoginCredentials(loginModel);
                    if (responseModel.DocId != 0 && responseModel.AadharCardNumber != 0)
                    {
                        setSessionModel.DocId = responseModel.DocId;
                        setSessionModel.Firstname = responseModel.FirstName;
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
            AllPatientModelVM allPatientModel = new AllPatientModelVM();
            IAccountServices accountServices = new AccountServices();
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    int DocId = sessionModel.DocId;
                    allPatientModel.patientModelList = accountServices.GetAllPatientListDataForToday(DocId);
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
            return View(allPatientModel);
        }

        [HttpPost]
        public IActionResult DeletePatient(int DocId, int RecordId, int PatientId)
        {
            DeletePatientModel deletePatientModel = new DeletePatientModel();
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    deletePatientModel.DocId = DocId;
                    deletePatientModel.RecordId = RecordId;
                    deletePatientModel.PatientId = PatientId;                   
                    int data = accountServices.deletePatientRecord(deletePatientModel);
                    if (data != 1)
                    {
                        TempData["msg"] = "Patient Record Deleted UnSuccesfull";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["msg"] = "Patient Record Deleted Succesfull";
                        return RedirectToAction("Index", "Home");
                    }
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

        [HttpGet]
        public IActionResult getAllPatientsCount(int DocId)
        {
            DashboardAllPatientsCount dashboardAllPatientsCount = new DashboardAllPatientsCount();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null)
            {
                dashboardAllPatientsCount = accountServices.getTotalPatientsCount(DocId);
            }
            else
            {
                return Json(null);
            }
            return Json(dashboardAllPatientsCount);
        }

        [HttpGet]
        public IActionResult getAllNewPatientsCount(int DocId) 
        {
            DashboardNewPatientsCount dashboardNewPatientsCount = new DashboardNewPatientsCount();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null)
            {
                dashboardNewPatientsCount = accountServices.getAllNewPatientsCount(DocId);
            }
            else
            {
                return Json(null);
            }
            return Json(dashboardNewPatientsCount);
        }

        [HttpGet]
        public IActionResult getAllAppointmentCount(int DocId) 
        {
            DashboardAllAppointmentCount dashboardAllAppointmentCount = new DashboardAllAppointmentCount();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null)
            {
                dashboardAllAppointmentCount = accountServices.getAllAppointmentCountForDashboard(DocId);
            }
            else
            {
                return Json(null);
            }
            return Json(dashboardAllAppointmentCount);
        }

        [HttpGet]
        public IActionResult getNewAppointmentCount(int DocId)
        {
            DashboardNewAppointmentCount dashboardNewAppointmentCount = new DashboardNewAppointmentCount();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null)
            {
                dashboardNewAppointmentCount = accountServices.getNewAppointmentCountForDashboard(DocId);
            }
            else
            {
                return Json(null);
            }
            return Json(dashboardNewAppointmentCount);
        }

        [HttpGet]
        public IActionResult getNewPrescriptionCount(int DocId)
        {
            DashboardNewPriscriptionCount dashboardNewPriscriptionCount = new DashboardNewPriscriptionCount();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null)
            {
                dashboardNewPriscriptionCount = accountServices.getDashboardNewPriscriptionCount(DocId);
            }
            else
            {
                return Json(null);
            }
            return Json(dashboardNewPriscriptionCount);
        }

        [HttpGet]
        public IActionResult getAllPrescriptionCount(int DocId)
        {
            DashboardAllPriscriptionCount dashboardAllPriscriptionCount = new DashboardAllPriscriptionCount();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null)
            {
                dashboardAllPriscriptionCount = accountServices.getAllPrescriptionCountForDashboard(DocId);
            }
            else
            {
                return Json(null);
            }
            return Json(dashboardAllPriscriptionCount);
        }

        [HttpGet]
        public IActionResult getAllPaymentCountForToday(int DocId) 
        {
            DashboardAllPaymentCountForToday dashboardAllPaymentCountForToday = new DashboardAllPaymentCountForToday();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null) 
            {
                dashboardAllPaymentCountForToday = accountServices.GetAllPaymentCountForToday(DocId);
            }
            else
            {
                return Json(null);
            }
            return Json(dashboardAllPaymentCountForToday);
        }

        [HttpGet]
        public IActionResult getAllPaymentsCount(int DocId) 
        {
            DashboardAllPaymentsCount dashboardAllPaymentsCount = new DashboardAllPaymentsCount();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null)
            {
                dashboardAllPaymentsCount = accountServices.getAllPaymentsCount(DocId);
            }
            else 
            {
                return Json(null);
            }
            return Json(dashboardAllPaymentsCount);
        }

       
    }
}