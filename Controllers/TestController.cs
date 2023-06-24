using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class TestController : Controller
    {
        ITestServices testServices = new TestServices();
        public IActionResult NewTest()
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
        [HttpGet]
        public IActionResult AllTests()
        {
            AllTestModelVM allTestModel = new AllTestModelVM();
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null)
                {
                    int DocId = sessionModel.DocId;
                    allTestModel.allTestModelsList = testServices.GetAllTestList(DocId);
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
            return View(allTestModel);
        }

        [HttpPost]
        public IActionResult NewTest(NewTest newTest) 
        {
            try
            {
                GetSessionModel sessionModel = HttpContext.Session.GetObjectFromJson<GetSessionModel>(SessionVariables.SessionData);
                if (sessionModel != null) 
                {
                    newTest.DocID = sessionModel.DocId;
                    if (newTest != null)
                    {
                        int success = testServices.AddTest(newTest);
                        if (success != 0)
                        {
                            TempData["msg"] = "New Test Succesfully Inserted";
                        }
                        else 
                        {
                            TempData["msg"] = "New Test not Inserted Succesfully ";
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
            return RedirectToAction("AllTests", "Test");
        }

    } 
    
}

