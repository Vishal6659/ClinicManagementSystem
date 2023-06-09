﻿using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{   
    public class DrugController : Controller
    {
        IDrugSevices drugSevices = new DrugSevices();
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
                            TempData["msg"] = "New Drug not Inserted Succesfully ";
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
    }
}
