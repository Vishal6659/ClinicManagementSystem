using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class PatientController : Controller
    {
        IPatientServices patientServices = new PatientServices();
        [HttpGet]
        public IActionResult AllPatients()
        {
            return View();
        }
        [HttpGet]
        public IActionResult NewPatient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewPatient(NewPatient newPatient)
        {
            try
            {
                if (newPatient != null)
                {
                    int success = patientServices.AddNewPatient(newPatient);
                    if (success != 0)
                    {
                        TempData["msg"] = "New Patient Succesfully Inserted";
                        return RedirectToAction("AllPatients", "Patient");
                    }
                    else
                    {
                        TempData["msg"] = "New Patient not Inserted Succesfully ";
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
