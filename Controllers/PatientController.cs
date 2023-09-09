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
            AllPatientModelVM allPatientModel = new AllPatientModelVM();             
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    int DocId = sessionModel.DocId;
                    allPatientModel.patientModelList = patientServices.GetAllPatientListData(DocId);
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
        [HttpGet]
        public IActionResult NewPatient()
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
        [HttpPost]
        public IActionResult NewPatient(NewPatient newPatient)
        {
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    Random random = new Random();
                    int num = random.Next(1, 999999);
                    newPatient.PatientID = num;
                    newPatient.DocID = sessionModel.DocId;
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
            return View(newPatient);
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
                    int data = patientServices.deletePatientRecord(deletePatientModel);
                    if (data != 1)
                    {
                        TempData["msg"] = "Patient Record Deleted UnSuccesfull";
                        return RedirectToAction("AllPatients", "Patient");
                    }
                    else
                    {
                        TempData["msg"] = "Patient Record Deleted Succesfull";
                        return RedirectToAction("AllPatients", "Patient");
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
    }
}
