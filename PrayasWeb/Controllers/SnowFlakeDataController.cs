using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrayasDAL;
using PrayasModels;

namespace PrayasWeb.Controllers
{
    public class SnowFlakeDataController : Controller
    {

        List<BindDropdown> zonelist = new List<BindDropdown>();
        BindZoneDal BindZone = new BindZoneDal();

        [HttpGet]
        public ActionResult Index()
        {

            zonelist = BindZone.BindZone();
            ViewBag.list = zonelist;
            //return View(zonelist);
            ViewBag.Name = HttpContext.Session.GetString("username");

            return View();
        
        }

        //[HttpPost]
        //public ActionResult Submit(IFormCollection formcollection)
        //{
        //    ViewBag.SelectValue = formcollection["Zone"].ToString();
        //    return RedirectToAction("Index");
        //}


        [HttpPost]
        public ActionResult Submit(string Zone)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Zoncevalue = "Selected Zone : " + Zone;
                zonelist = BindZone.BindZone();
                ViewBag.list = zonelist;

                
            }
            return View("Index");
            //return RedirectToAction("Index");
        }
    }
}
