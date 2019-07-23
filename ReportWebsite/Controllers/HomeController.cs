using ReportWebsite.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  ReportWebsite.SqlConnections;
using static ReportWebsite.Enums.ReportWebSiteType;
namespace ReportWebsite.Controllers
{
    public class HomeController : Controller
    {
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
            return Json(WebSiteSqlConnection.Count(type), JsonRequestBehavior.AllowGet);
        }
    }
}