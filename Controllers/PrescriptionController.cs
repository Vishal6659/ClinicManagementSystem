using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClinicManagementSystem.Controllers
{
    public class PrescriptionController : Controller
    {
        IPrescriptionServices prescriptionServices = new PrescriptionServices();
        [HttpGet]
        public IActionResult AllPrescriptions()
        {
            AllPrescriptionModelVM allPrescriptionModelVM = new AllPrescriptionModelVM();
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    int DocId = sessionModel.DocId;
                    allPrescriptionModelVM.allPrescriptionModelList = prescriptionServices.allPrescriptionLists(DocId);

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
            return View(allPrescriptionModelVM);
        }
        [HttpGet]
        public IActionResult NewPrescription() 
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
        public IActionResult NewPrescription(NewPrescriptionModel newPrescriptionModel) 
        {
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null) 
                {
                    newPrescriptionModel.DocId = sessionModel.DocId;
                    if (newPrescriptionModel != null)
                    {
                        int success = prescriptionServices.addNewPrescriptionData(newPrescriptionModel);
                        if (success != 0)
                        {
                            TempData["msg"] = "New Prescription Succesfully Created";
                        }
                        else
                        {
                            TempData["msg"] = "New Prescription not Inserted Created";
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
            return RedirectToAction("AllPrescriptions", "Prescription");
        }

        [HttpPost]
        public IActionResult DeletePrescription(int DocId, int RecordId, int PatientId)
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
                    int data = prescriptionServices.deletePrescriptionRecord(deletePatientModel);
                    if (data != 1)
                    {
                        TempData["msg"] = "Prescription Record not Deleted Succesfull";
                        return RedirectToAction("AllPrescriptions", "Prescription");
                    }
                    else
                    {
                        TempData["msg"] = "Prescription Record Deleted Succesfull";
                        return RedirectToAction("AllPrescriptions", "Prescription");
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
        public IActionResult getAllPatientsName(int DocId) 
        {
            ResponseListModel responseListModel = new ResponseListModel();
            List<AllPatientsNamesDetails> allPatientsNamesDetails = new List<AllPatientsNamesDetails>();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null)
            {
                if (DocId != 0)
                {
                    responseListModel.data = JsonConvert.SerializeObject( prescriptionServices.allPatientsNames(DocId));                    
                    allPatientsNamesDetails = JsonConvert.DeserializeObject<List<AllPatientsNamesDetails>>(responseListModel.data.ToString().Trim());
                }
                else 
                {
                    return Json(null);
                }
                return Json(allPatientsNamesDetails);
            }
            else 
            {
                return Json(null);
            }            
        }

        [HttpGet]
        public IActionResult getAllDrugName(int DocId) 
        {
            ResponseListModel responseListModel = new ResponseListModel();
            List<AllDrugNamesDetails> allDrugNamesDetails = new List<AllDrugNamesDetails>();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null) 
            {
                if (DocId != 0) 
                {
                    responseListModel.data = JsonConvert.SerializeObject(prescriptionServices.allDrugNames(DocId));
                    allDrugNamesDetails = JsonConvert.DeserializeObject<List<AllDrugNamesDetails>>(responseListModel.data.ToString().Trim());
                }
                else
                {
                    return Json(null);
                }
                return Json(allDrugNamesDetails);
            }
            else
            {
                return Json(null);
            }
        }

        [HttpGet]
        public IActionResult getAllTestName(int DocId) 
        {
            ResponseListModel responseListModel = new ResponseListModel();
            List<AllTestNamesDetails> allTestNamesDetails = new List<AllTestNamesDetails>();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null) 
            {
                if (DocId != 0) 
                {
                    responseListModel.data = JsonConvert.SerializeObject(prescriptionServices.allTestNames(DocId));
                    allTestNamesDetails = JsonConvert.DeserializeObject<List<AllTestNamesDetails>>(responseListModel.data.ToString().Trim());
                }
                else
                {
                    return Json(null);
                }
                return Json(allTestNamesDetails);
            }
            else
            {
                return Json(null);
            }
        }
        [HttpGet]
        public IActionResult getAllPastRecordsOfPatientOnId(int DocId, int PatientId) 
        {
            ResponseListModel responseListModel = new ResponseListModel();
            List<AllPastPrescriptionDataModel> allPastRecordsOfPatientList = new List<AllPastPrescriptionDataModel>();
            GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
            if (sessionModel != null) 
            {
                if (DocId != 0)
                {
                    responseListModel.data = JsonConvert.SerializeObject(prescriptionServices.allPastRecordsOfPatient(DocId, PatientId));
                    allPastRecordsOfPatientList = JsonConvert.DeserializeObject<List<AllPastPrescriptionDataModel>>(responseListModel.data.ToString().Trim());
                }
                else 
                {
                    return Json(null);
                }
                return Json(allPastRecordsOfPatientList);
            }
            else
            {
                return Json(null);
            }
        }
    }
}
