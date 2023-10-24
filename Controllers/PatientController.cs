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
                            TempData["msg"] = "New Patient not Inserted Succesfully";
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
        public IActionResult UpdateRowData(int recordId,int docId,int patientId,string newFirstName, string newLastName, string newAge, string newMobileNumber, string newGender, string newPresentComplaint, string newPastHistory, string newFamilyHistory, string newPresentMedication, string newPhysicalNature, string newMentalNature)
        {
            EditPatientDataModel editPatientDataModel = new EditPatientDataModel();
            editPatientDataModel.RecordId = recordId;
            editPatientDataModel.DocId = docId;
            editPatientDataModel.PatientId = patientId;
            editPatientDataModel.FirstName = newFirstName;
            editPatientDataModel.LastName = newLastName;
            editPatientDataModel.Age = newAge;
            editPatientDataModel.Phone = newMobileNumber;
            editPatientDataModel.Gender = newGender;
            editPatientDataModel.PresentComplaint = newPresentComplaint;
            editPatientDataModel.PastHistory = newPastHistory;
            editPatientDataModel.FamilyHistory = newFamilyHistory;
            editPatientDataModel.PresentMedication = newPresentMedication;
            editPatientDataModel.PhysicalNature = newPhysicalNature;
            editPatientDataModel.MentalNature = newMentalNature;
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    int success = patientServices.updateRowData(editPatientDataModel);
                    if (success != 0)
                    {
                        TempData["msg"] = "Row Updated Succesfully";
                        return RedirectToAction("AllPatients", "Patient");
                    }
                    else
                    {
                        TempData["msg"] = "Row Not Updated Succesfully";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return RedirectToAction("AllPatients", "Patient");
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

        [HttpGet]
        public IActionResult ViewRowData(int DocId, int RecordId)
        {
            try
            {
                ViewPatientDataModel viewPatientDataModel = patientServices.getDataToView(DocId, RecordId);
                if (viewPatientDataModel != null)
                {
                    return Json(viewPatientDataModel);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }
    }
}
