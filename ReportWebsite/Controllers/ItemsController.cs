using ReportWebsite.DataProvider;
using ReportWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Items(byte? type = null)
        {
            if (type == null)
            {
                return View(_itemDP.GetItems());
            }
            else
            {
                return View(_itemDP.GetItemsByType(type??0));
            }
        }
    }
}