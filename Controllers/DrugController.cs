using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ClinicManagementSystem.Controllers
{   
    public class DrugController : Controller
    {
        IDrugSevices drugSevices = new DrugSevices();
        [HttpGet]
        public IActionResult AllDrugs()
        {
            AllDrugModelVM allDrugModel = new AllDrugModelVM();
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    int DocId = sessionModel.DocId;
                    allDrugModel.allDrugModelsList = drugSevices.GetAllDrugListData(DocId);
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
            return View(allDrugModel);
        }

        [HttpGet]
        public IActionResult NewDrug() 
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
        public IActionResult NewDrug(NewDrug newDrug) 
        {
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    newDrug.DocID = sessionModel.DocId;
                    if (newDrug != null)
                    {
                        int success = drugSevices.AddNewDrug(newDrug);
                        if (success != 0)
                        {
                            TempData["msg"] = "New Drug Succesfully Inserted";
                        }
                        else 
                        {
                            TempData["msg"] = "New Drug not Inserted Succesfully";
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
            return RedirectToAction("AllDrugs", "Drug");
        }       

        [HttpPost]
        public IActionResult DeleteDrug(int DocId, int RecordId)
        {            
            DeleteDrugModel deleteDrugModel = new DeleteDrugModel();
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    deleteDrugModel.DocId = DocId;
                    deleteDrugModel.RecordId = RecordId;
                    int data = drugSevices.deleteDrugRecord(deleteDrugModel);
                    if (data != 1)
                    {
                        TempData["msg"] = "Drug Record not Deleted Succesfull";
                        return RedirectToAction("AllDrugs", "Drug");
                    }
                    else
                    {
                        TempData["msg"] = "Drug Record Deleted Succesfully";
                        return RedirectToAction("AllDrugs", "Drug");
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
                ViewRowDrugData viewRowDrugData = drugSevices.getDataToView(DocId, RecordId);
                if (viewRowDrugData != null)
                {
                    return Json(viewRowDrugData);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateRowData(int recordId, int docId, string newDrugName, string newDrugDescription, string newGenericName) 
        {
            EditDrugModel editDrugModel = new EditDrugModel();
            editDrugModel.DocID = docId;
            editDrugModel.RecordID = recordId;
            editDrugModel.NewDrugName = newDrugName;
            editDrugModel.NewDrugDescription = newDrugDescription;
            editDrugModel.NewGenericName = newGenericName;
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null) 
                {
                    int success = drugSevices.updateRowData(editDrugModel);
                    if (success != 0)
                    {
                        TempData["msg"] = "Row Updated Succesfully";
                        return RedirectToAction("AllDrugs", "Drug");
                    }
                    else
                    {
                        TempData["msg"] = "Row Not Updated Succesfully";
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
            return RedirectToAction("AllDrugs", "Drug");
        }
    }
}
