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
            try
            {
                var result = WebSiteSqlConnection.UpdateWebSite(WebSite);
                if (!result)
                    return result;

                foreach (var element in ElementSqlConnection.SelectElementBySite(WebSite.SiteId))
                {
                    result = ElementSqlConnection.UpdateElement(element);
                    if (!result)
                        return result;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public bool DeleteWebsite(int siteId)
        {
            try
            {
                var result = WebSiteSqlConnection.DeleteWebSite(siteId);
                if (!result)
                    return result;
                result = ElementSqlConnection.DeleteElement(siteId);
                if (!result)
                    return result;

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }


        }
    }
}