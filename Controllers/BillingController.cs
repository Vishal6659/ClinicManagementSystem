using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClinicManagementSystem.Controllers
{
    public class BillingController : Controller
    {
        IBillingService billingService = new BillingService();
        public IActionResult AllBillings()
        {

            AllBillingModelVM allBillingModelVM = new AllBillingModelVM();
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    int DocId = sessionModel.DocId;
                    allBillingModelVM.allBillingModelsList = billingService.allBillingList(DocId);
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
            return View(allBillingModelVM);
        }

        [HttpGet]
        public IActionResult NewBilling() 
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
        public IActionResult NewBilling(NewBillingModel newBillingModel) 
        {
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null) 
                {
                    newBillingModel.DocId = sessionModel.DocId;
                    if (newBillingModel != null)
                    {
                        int success = billingService.addNewBillingData(newBillingModel);
                        if (success != 0)
                        {
                            TempData["msg"] = "New Billing Created Succesfully";
                        }
                        else
                        {
                            TempData["msg"] = "New Billing not Inserted Succesfully";
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
            return RedirectToAction("AllBillings", "Billing");
        }

        [HttpPost]
        public IActionResult UpdateRowData(int DocId, int RecordId, int PatientId, string NewPatientName, string NewPaymentMode, string NewAmount, string NewPaymentStatus) 
        {
            UpdateNewBillingModel updateNewBillingModel = new UpdateNewBillingModel();
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    updateNewBillingModel.DocId = DocId;
                    updateNewBillingModel.RecordId= RecordId;
                    updateNewBillingModel.PatientId = PatientId;
                    updateNewBillingModel.NewPatientName = NewPatientName;
                    updateNewBillingModel.NewPaymentMode = NewPaymentMode;
                    updateNewBillingModel.NewPaymentStatus = NewPaymentStatus;
                    updateNewBillingModel.NewAmount = NewAmount;
                    if (updateNewBillingModel != null)
                    {
                        int success = billingService.updateRowData(updateNewBillingModel);
                        if (success != 0)
                        {
                            TempData["msg"] = "Row Updated Succesfully";
                            return RedirectToAction("AllBillings", "Billing");
                        }
                        else
                        {
                            TempData["msg"] = "Row Not Updated Succesfully";
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
            return RedirectToAction("AllBillings", "Billing");
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
                    responseListModel.data = JsonConvert.SerializeObject(billingService.allPatientsNames(DocId));
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

        [HttpPost]
        public IActionResult DeleteBilling(int DocId, int RecordId, int PatientId)
        {
            DeleteBillingModel deleteBillingModel = new DeleteBillingModel();
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    deleteBillingModel.DocId = DocId;
                    deleteBillingModel.RecordId = RecordId;
                    deleteBillingModel.PatientId = PatientId;
                    int data = billingService.deleteBillingRecord(deleteBillingModel);
                    if (data != 1)
                    {
                        TempData["msg"] = "Billing Record not Deleted Succesfully";
                        return RedirectToAction("AllBillings", "Billing");
                    }
                    else
                    {
                        TempData["msg"] = "Billing Record Deleted Succesfully";
                        return RedirectToAction("AllBillings", "Billing");
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
        public IActionResult ViewRowData(int DocId, int RecordId, int PatientId)
        {
            try
            {
                ViewBillingDataModel viewBillingDataModel = billingService.getDataToView(DocId, RecordId, PatientId);
                if (viewBillingDataModel != null)
                {
                    return Json(viewBillingDataModel);
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
