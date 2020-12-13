using Dualp.Common.Logger;
using Dualp.Common.Types;
using ReportWebsite.Models;
using ReportWebsite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using static ReportWebsite.Enums.ReportWebSiteType;

namespace ReportWebsite.Plugins
{
    public class WebSiteDataProvider
    {
        private readonly IWebsiteRepository _websiteRepository;

        public WebSiteDataProvider()
        {
            _websiteRepository = new WebsiteRepository();
        }

        public WebSiteDataProvider(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }
        public ResultActivity InsertWebsite(WebSite webSite)
        {
            try
            {
                using (var ts = new TransactionScope(TransactionScopeOption.Required,
                 TransactionScopeAsyncFlowOption.Enabled))
                {
                    Entities.EN_WebSite eN_WebSite = webSite.ToWebSite();
                    var result = _websiteRepository.Insert(eN_WebSite);

                    ts.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }
        public ResultActivity UpdateWebSite(WebSite webSite, int siteId)
        {
            try
            {
                var websiteModel = webSite.ToWebSite();
                var result = _websiteRepository.Edit(websiteModel, siteId);
                return result;
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }
        public int Count(WebSiteType type)
        {
            return _websiteRepository.CountWeb(type);
        }
        public List<WebSite> GetWebSites(WebSiteType type)
        {
            try
            {
                return _websiteRepository.GetWebSites(type)?.Select(i => (WebSite)i).ToList();
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public WebSite GetWebSite(int id)
        {
            return _websiteRepository.GetWebsite(id);
        }

        public ResultActivity DeleteWebSite(int id)
        {
            var result = _websiteRepository.Delete(id);
            return result;
        }
    }
}