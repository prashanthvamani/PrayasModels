using Microsoft.AspNetCore.Mvc;
using PrayasWeb.Models;
using System.Diagnostics;
using System.Data;
using PrayasBAL;
using PrayasModels;

namespace PrayasWeb.Controllers
{
    public class HomeController : Controller
    {
        DataSet ds = new DataSet();
        DDLRptBAL RptBAL = new DDLRptBAL();

        List<BIReportsListModel> BIReportsList = new List<BIReportsListModel>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        

        public IActionResult Index()
        {

            ViewData["Message"] = HttpContext.Session.GetString("username");

            ViewData["ID"] = HttpContext.Session.GetString("userid");

            ViewData["desig"] = HttpContext.Session.GetString("Designation");



            BIReportsList = RptBAL.bindrpt();
            //return RedirectToAction("Index","InstantCBRpt");
            TempData["List"] = BIReportsList;

            return View();
        }


    }
}
