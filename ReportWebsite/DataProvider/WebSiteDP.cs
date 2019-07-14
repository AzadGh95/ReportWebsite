using ReportWebsite.Models;
using ReportWebsite.SqlConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebsite.DataProvider
{
    public class WebSiteDP
    {
        public WebSite GetWebSite(int id)
        {
            return WebSiteSqlConnection.SelectWebSite(id).FirstOrDefault();
        }

        public List<WebSite> GetWebSites()
        {
            return WebSiteSqlConnection.SelectWebSite(null);
        }

        public bool UpdateWebSite(WebSite WebSite)
        {
            return WebSiteSqlConnection.UpdateWebSite(WebSite);
        }
    }
}