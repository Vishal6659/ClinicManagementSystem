using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class AppointmentController : Controller
    {
        IAppointmentService appointmentService = new AppointmentService();
        public IActionResult AllAppointments()
        {
            AllAppointmentModelVM allAppointmentModelVM = new AllAppointmentModelVM();
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    int DocId = sessionModel.DocId;
                    allAppointmentModelVM.allAppointmentModelsList = appointmentService.GetAllAppointmentList(DocId);
                }
                else
                {
                    TempData["msg"] = "Session Not Found";
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {

            }
            return View(allAppointmentModelVM);
        }

        [HttpGet]
        public IActionResult NewAppointment() 
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

        public IActionResult NewAppointment(NewAppointment newAppointment)
        {
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    newAppointment.DocID = sessionModel.DocId;
                    if (newAppointment != null)
                    {
                        int success = appointmentService.AddNewAppointment(newAppointment);
                        if (success != 0)
                        {
                            TempData["msg"] = "New Appointment Succesfully Inserted";
                        }
                        else
                        {
                            TempData["msg"] = "New Appointment not Inserted Succesfully ";
                            return View();
                        }
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
            return RedirectToAction("AllAppointments", "Appointment");
        }

    }

   
}
