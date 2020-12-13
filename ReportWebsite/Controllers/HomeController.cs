using ReportWebsite.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  ReportWebsite.SqlConnections;
using static ReportWebsite.Enums.ReportWebSiteType;
using ReportWebsite.Plugins;

namespace ReportWebsite.Controllers
{
    public class HomeController : Controller
    {

        public WebSiteDataProvider _websiteDataProvider;

        public HomeController()
        {
            _websiteDataProvider = new WebSiteDataProvider();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public JsonResult CountWebSite(WebSiteType type)
        {
           return Json(_websiteDataProvider.Count(type));
            //return Json(WebSiteSqlConnection.Count(type), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult SliderHomePartial()
        {
            return PartialView();
        }
    }
}