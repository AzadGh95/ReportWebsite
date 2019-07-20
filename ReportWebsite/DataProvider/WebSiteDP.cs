using ReportWebsite.Models;
using ReportWebsite.SqlConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ReportWebsite.Enums.ReportWebSiteType;

namespace ReportWebsite.DataProvider
{
    public class WebSiteDP
    {
        public WebSiteDP() { }

        public bool InsertWebsite(WebSite webSite)
        {
            try
            {
                var result = WebSiteSqlConnection.InsertWebSite(webSite);
                if (!result)
                    return result;
                var SiteId = WebSiteSqlConnection.SelectLastIndex();

                foreach (var element in webSite.Elements)
                {
                    element.SiteId = SiteId;
                    result = ElementSqlConnection.InsertElement(element , SiteId);
                    if (!result)
                        return result;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
            }

        }

        public WebSite GetWebSite(int id)
        {
            return WebSiteSqlConnection.SelectWebSite(id).FirstOrDefault();
        }

        public List<WebSite> GetWebSites()
        {
            return WebSiteSqlConnection.SelectWebSite(null);
        }
        public List<WebSite> GetWebSites(WebSiteType type)
        {
            return WebSiteSqlConnection.SelectWebSiteByType(type);
        }
        public bool UpdateWebSite(WebSite WebSite)
        {
            try
            {
                var result = WebSiteSqlConnection.UpdateWebSite(WebSite);
                if (!result)
                    return result;

                result = ElementSqlConnection.DeleteElement(WebSite.SiteId);
                if (!result)
                    return result;

                foreach (var element in WebSite.Elements)
                {
                    result = ElementSqlConnection.InsertElement(element , WebSite.SiteId);
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

                var result = ElementSqlConnection.DeleteElement(siteId);
                if (!result)
                    return result;
                result = WebSiteSqlConnection.DeleteWebSite(siteId);
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