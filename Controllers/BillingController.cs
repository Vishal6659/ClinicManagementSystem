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
                            TempData["msg"] = "New Prescription Succesfully Created";
                        }
                        else
                        {
                            TempData["msg"] = "New Prescription not Inserted Created ";
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
    }
}
