using ReportWebsite.DataProvider;
using ReportWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ReportWebsite.Enums.ReportWebSiteType;

namespace ReportWebsite.Controllers
{
    public class FormsController : Controller
    {
        public WebSiteDP _webSiteDP;
        public ElementDP _elementDP;
        public ItemDP _itemDP;
        public FormsController()
        {
            _webSiteDP = new WebSiteDP();
            _itemDP = new ItemDP();
        }
        // GET: Forms
        public ActionResult Form(WebSiteType type, int? siteId)
        {
            var Items = _itemDP.GetItemsByType(type);
            ViewBag.Items = Items;
            ViewBag.Type = type;
            if (siteId == null)
            {
                return View(new WebSite());
            }
            ViewBag.siteId = siteId;
            return View(_webSiteDP.GetWebSite(siteId ?? 1));
        }

        [HttpPost]
        public ActionResult Form(WebSite webSite, WebSiteType type, int? initSiteId)
        {
            ViewBag.Type =type;
            if (!ModelState.IsValid)
            {
                //return View(webSite);
            }
            if (initSiteId == null)
            {
                // insert into wesite
                var result = _webSiteDP.InsertWebsite(webSite);
                if (!result)
                    return View(webSite);

                return RedirectToAction("Forms", new { type = type });
            }
            else
            {
                // update website
                var result = _webSiteDP.UpdateWebSite(webSite);
                return RedirectToAction("Forms", new { type =type});
            }

        }
        public ActionResult Forms(WebSiteType type)
        {
            ViewBag.Type = type;
            return View(_webSiteDP.GetWebSites());
        }

        [HttpPost]
        public ActionResult DeleteForm(int siteId, WebSiteType type)
        {
            _webSiteDP.DeleteWebsite(siteId);
            return RedirectToAction("Forms", new { type = type });
        }
        [HttpPost]
        public PartialViewResult FormInfo()
        {
            return PartialView();
        }

    }
}