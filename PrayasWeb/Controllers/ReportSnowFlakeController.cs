using Microsoft.AspNetCore.Mvc;
using PrayasDAL;
using PrayasModels;
using ClosedXML.Excel;
using System.Collections;
using DocumentFormat.OpenXml.Wordprocessing;

namespace PrayasWeb.Controllers
{
    public class ReportSnowFlakeController : Controller
    {

        SnflakeRpt dal = new SnflakeRpt();
        List<SnowFlakeRpt> snflakeslist = new List<SnowFlakeRpt>();


        [HttpGet]
        public IActionResult Index()
        {
            snflakeslist = dal.BindZone();
            //snflakeslist = dal.SFRpt(Zone);
            ViewBag.list = snflakeslist;
            //return View(snflakeslist);
            return View();
            
        }

        [HttpGet]
        public IActionResult Submit()
        {
            ViewBag.Name = HttpContext.Session.GetString("username");

            snflakeslist = dal.BindZone();
            ////snflakeslist = dal.SFRpt();
            TempData["List"] = snflakeslist;
            return View(snflakeslist);
        }

        [HttpPost]
        public IActionResult Submit(string Zone)
        {
            if(ModelState.IsValid)
            {
                var items = dal.SFRpt(Zone);
                TempData["List"] = snflakeslist;
                ViewBag.Data = items;
                ViewData["Selected"] = Zone;


                //using (XLWorkbook wb = new XLWorkbook())
                //{
                //    wb.Worksheets.Add(Common.ToDataTable(snflakeslist.ToList()));
                //    using (MemoryStream stream = new MemoryStream())
                //    {
                //        wb.SaveAs(stream);
                //        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "customer.xlsx");
                //    }
                //}

            }
            return View();
        }

        [HttpPost]
        public IActionResult Excel() 
        {
            //if (ViewBag.Data! = null)
            //{

            //}

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(Common.ToDataTable(ViewBag.Data));
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "customer.xlsx");
                }
            }

        }

    }
}
