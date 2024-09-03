using Microsoft.AspNetCore.Mvc;
using PrayasDAL;
using PrayasModels;

namespace PrayasWeb.Controllers
{
    public class InstantCBRptController : Controller
    {
        InstanceCBrpt cBrptDal = new InstanceCBrpt();

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("username");

            ViewData["ID"] = HttpContext.Session.GetString("userid");

            ViewData["desig"] = HttpContext.Session.GetString("Designation");

            List<InstanceCBRptModel> list = new List<InstanceCBRptModel>();

            list = cBrptDal.UpdateList();
            ViewBag.List = list;
            return View();
        }
    }
}
