using Microsoft.AspNetCore.Mvc;
using PrayasBAL;
using PrayasModels;
using System.Data;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace PrayasWeb.Controllers
{
    public class LoginController : Controller
    {

        DataSet ds = new DataSet();
        LoginModel lm = new LoginModel();
        LoginBaL LC = new LoginBaL();

        

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string Username, string Password) 
        {
            try
            {
                if(ModelState.IsValid)
                {
                    lm.Username= Username;
                    lm.Password= Password;

                    DataSet ds = LC.BFILLogin(lm);

                    string Ecode = "";
                    string EMsg = "";

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 1)
                        {
                            Ecode = ds.Tables[0].Rows[0]["EmployeeID"].ToString();
                            Ecode = Ecode.Substring(0, 3);
                            EMsg = ds.Tables[0].Rows[1]["EmployeeID"].ToString();

                            ViewBag.Message = "Invalid Login" + EMsg;

                            
                            
                        }

                

                        //Application["UserID"] = ds.Tables[0].Rows[0]["EmployeeID"].ToString();
                        //Application["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
                        //Application["DesigName"] = ds.Tables[0].Rows[0]["DesigName"].ToString();
                        //Application["UserRole"] = ds.Tables[0].Rows[0]["LoginType"].ToString();

                        if (Ecode != "E20")
                        {
                           
                            var empidID = ds.Tables[0].Rows[0]["EmployeeID"].ToString();
                            HttpContext.Session.SetString("userid", empidID);

                            var DesigName = ds.Tables[0].Rows[0]["DESIGNATION_NAME"].ToString();
                            HttpContext.Session.SetString("Designation", DesigName);

                            HttpContext.Session.SetString("username", Username);

                            //return RedirectToAction("Index", "SnowFlakeData");
                            return RedirectToAction("Index", "Home");
                            //return RedirectToAction("Index", "InstantCBRpt");
                            //return RedirectToAction("Submit", "ReportSnowFlake");
                            //return RedirectToAction("Index", "BIreportsdownload");

                            


                        }
                    }
                    else
                    {
                        //ShowMessage("ErrorCode2:" + EMsg);
                        ViewBag.Message = "Invalid Login" + EMsg;

                    }
                }
            }
            catch (Exception ex )
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }
    }
}
