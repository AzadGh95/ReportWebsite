using ReportWebsite.DataProvider;
using ReportWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportWebsite.Controllers
{
    public class FormsController : Controller
    {
        public WebSiteDP _webSiteDP;
        public ElementDP _elementDP;
        public FormsController()
        {
            _webSiteDP = new WebSiteDP();
        }
        // GET: Forms
        public ActionResult Form(int? siteId)
        {

            if (siteId == null)
            {
                return View(new WebSite());
            }
            ViewBag.siteId = siteId;
            return View(_webSiteDP.GetWebSite(siteId ?? 1));
        }

        [HttpPost]
        public ActionResult Form(WebSite webSite, int? initSiteId)
        {
            if (!ModelState.IsValid)
            {
                return View(webSite);
            }
            if (initSiteId == null)
            {
                // insert into wesite
                var result = _webSiteDP.InsertWebsite(webSite);
                if (!result)
                    return View(webSite);

                return RedirectToAction("Forms");
            }
            else
            {
                // update website
                var result = _webSiteDP.UpdateWebSite(webSite);
                return RedirectToAction("Forms");
            }

        }
        public ActionResult Forms()
        {
            return View(_webSiteDP.GetWebSites());
        }

        [HttpPost]
        public ActionResult DeleteForm(int siteId)
        {
            _webSiteDP.DeleteWebsite(siteId);
            return RedirectToAction("Forms");
        }
    }
}