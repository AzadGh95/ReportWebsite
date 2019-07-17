using ReportWebsite.DataProvider;
using ReportWebsite.Enums;
using ReportWebsite.Models;
using ReportWebsite.UtilitySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteType = ReportWebsite.Enums.ReportWebSiteType.WebSiteType;
namespace ReportWebsite.Controllers
{
    public class ItemsController : Controller
    {
        public ItemDP _itemDP;
        public ItemsController()
        {
            _itemDP = new ItemDP();
        }

        // GET: Items
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(Item model)
        {
            var result = _itemDP.Insert(model);
            //if (!result)
            //    return result;
            return RedirectToAction("Items", new { type = model.ItemId });

        }
        public PartialViewResult Item(int? itemId = null)
        {
            if (itemId == null)
            {
                return PartialView(new Item());
            }
            else
            {
                return PartialView(_itemDP.GetItem(itemId ?? 0));
            }



        }
        public ActionResult Items(WebSiteType type)
        {
            ViewBag.Type = type;
            return View(_itemDP.GetItemsByType(type));

        }

        [HttpPost]
        public ActionResult DeleteItem(int itemId , WebSiteType type)
        {
            var result = _itemDP.DeleteItem(itemId);
            return RedirectToAction("Items", new {type = type });
        }

    }
}