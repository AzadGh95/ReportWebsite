using ReportWebsite.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteType = ReportWebsite.Enums.ReportWebSiteType.WebSiteType;


namespace ReportWebsite.UtilitySystem
{
    public class Links
    {
        public static string Form(WebSiteType type , int? siteId=null)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return urlHelper.Action("Form", "Forms", new { type = type  , siteId = siteId });
        }
        public static string Forms(WebSiteType type)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return urlHelper.Action("Forms", "Forms", new { type = type });
        }
        public static string Items(WebSiteType type)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return urlHelper.Action("Items", "Items", new { type = type });
        }
    }
}