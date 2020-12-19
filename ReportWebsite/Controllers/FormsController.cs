using Dualp.Common.Logger;
using ReportWebsite.DataProvider;
using ReportWebsite.Models;
using ReportWebsite.Plugins;
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
        //public WebSiteDP _webSiteDP;
        // public ElementDP _elementDP;
        //  public ItemDP _itemDP;

        public WebSiteDataProvider _webSiteDataProvider;
        public ItemDataProvider _itemDataProvider;
        public FormsController()
        {
            //  _webSiteDP = new WebSiteDP();
            //    _itemDP = new ItemDP();
            //    _elementDP = new ElementDP();
            _webSiteDataProvider = new WebSiteDataProvider();
            _itemDataProvider = new ItemDataProvider();
        }
        // GET: Forms
        //[Authorize(Roles="Admin , SuperAdmin")]
        [Authorize]
        public ActionResult Form(WebSiteType type, int? siteId)
        {

            //var Items = _itemDP.GetItemsByType(type);
            var Items = _itemDataProvider.GetItems(type);
            ViewBag.Items = Items;
            ViewBag.Type = type;
            ViewBag.siteId = siteId ?? 0;

            if (siteId == null)
            {
                var webSiteModel = new WebSite();
                foreach (var item in Items)
                {
                    webSiteModel.Elements.Add(new Element
                    {
                        ItemId = item.ItemId,
                        ItemText = item.Text,
                        Status = true,
                        Value = "",
                    });
                }

                return View(webSiteModel);
            }
            else
            {

                ViewBag.siteId = siteId;
                List<ViewEditModel> ViewEditModels = new List<ViewEditModel>() { };
                List<Element> TempElements = new List<Element>() { };
                // var webSiteModel = _webSiteDP.GetWebSite(siteId ?? 1);
                var webSiteModel = _webSiteDataProvider.GetWebSite(siteId ?? 1);
                if (Items.Count() >= 0)
                {
                    foreach (var item in Items)
                    {
                        var elementModel = webSiteModel.Elements.FirstOrDefault(x => x.ItemId == item.ItemId);
                        if (elementModel != null)
                        {
                            TempElements.Add(new Element
                            {
                                ItemId = elementModel.ItemId,
                                ElementId = elementModel.ElementId,
                                ItemText = elementModel.ItemText,
                                SiteId = elementModel.SiteId,
                                Status = elementModel.Status,
                                Value = elementModel.Value
                            });
                        }
                    }
                    webSiteModel.Elements = TempElements;
                }
                return View(webSiteModel);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Form(WebSite webSite, WebSiteType type, int? initSiteId)
        {
            ViewBag.Type = type;
            //if (!ModelState.IsValid)
            //{
            //    return View(webSite);
            //}
            if (initSiteId == 0)
            {
                // .....insert into wesite

                //var result = _webSiteDP.InsertWebsite(webSite);
                var result = _webSiteDataProvider.InsertWebsite(webSite);
                if (!result)
                    return View(webSite);

                return RedirectToAction("Forms", new { type = type });
            }
            else
            {
                // .....update website

                webSite.SiteId = initSiteId ?? 0;
                //var result = _webSiteDP.UpdateWebSite(webSite);
                var result = _webSiteDataProvider.UpdateWebSite(webSite, initSiteId ?? 0);
                if (result)
                {
                    ModelState.AddModelError("LockError",
                 "" + " < i class='fa fa-like'></i>" +
                 "عملیات ثبت با موفقیت انجام شد."
                 );
                    return RedirectToAction("Forms", new { type = type });
                }
                ModelState.AddModelError("LockError",
                  "" + " < i class='fa fa-warning'></i>" +
                  "خطا در ذخیره سازی !"
                  );
                return RedirectToAction("Forms", new { type = type });
                //todo : نمایش پیغام خطا
            }

        }

        [Authorize]
        public ActionResult Forms(WebSiteType type)
        {
            ViewBag.Type = type;
            //return View(_webSiteDP.GetWebSites(type));
            return View(_webSiteDataProvider.GetWebSites(type));
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public ActionResult DeleteForm(int siteId, WebSiteType type)
        {
            //_webSiteDP.DeleteWebsite(siteId);
            _webSiteDataProvider.DeleteWebSite(siteId);
            return RedirectToAction("Forms", new { type = type });
        }
        //[HttpPost]
        //public PartialViewResult FormInfo(int siteId)
        //{
        //    var model = _webSiteDP.GetWebSite(siteId);
        //    return PartialView(model);
        //}

        [Authorize]
        [HttpPost]
        public JsonResult FormInfo(int? siteId)
        {
            try
            {
                if (siteId == null)
                    return Json(new Tuple<bool, string>(false, "فرمی بااین مشخصات یافت نشد"));

                //var model = _webSiteDP.GetWebSite(siteId ?? 0);
                var model = _webSiteDataProvider.GetWebSite(siteId ?? 0);
                if (model == null)
                    return Json(new Tuple<bool, string>(false, "فرمی بااین مشخصات یافت نشد"));

                string html = $@"  
        <div class='card'>
                <div>
                    <div class='col-12' style='background-color: #e4cace;'>
                        <div class='card-header'  style='background-color: #e4cace;' >
                            <h4 class='card-title' id='heading-buttons2'>{model.Name} </h4>
                            <a class='heading-elements-toggle'><i class='fa fa-ellipsis-v font-medium-3'></i></a>
                            <div class='heading-elements'>
                                    <i class='fa fa-user'   ></i><small> {model.Admin}</small>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div style='margin: 28px; margin-top: 0;' class='list-group'>";
                foreach (var m in model.Elements)
                {

                    html += $@"
                                             <a style=' padding: 0.75rem 0.75rem; ' href='javascript:void(0)' class='list-group-item'>
                        <div class='media'>
                            <div class='media-left pr-1' >
                                        {(m.Status ? "<i  style='color:#6e3941;'class='fa fa-check'></i>" : "<i  style='color:#6e3941;'class='fa fa-times'></i>")}
                            </div>
                            <div class='media-body w-100'>
                                <h6 class='media-heading rose-mb-0' style='color:#6e3941;'>{m.ItemText}</h6>
                                <strong class='font-small-2 rose-mb-0 text-muted'> {m.Value}</strong>
                            </div>
                        </div>
                    </a>


";
                }
                html += $@"</div></div> ";
                return Json(new Tuple<bool, string>(true, html));

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                return Json(new Tuple<bool, string>(false, "مشاهده اطلاعات با خطا مواجه شد."));
            }
        }


    }
}