using Dualp.Common.Types;
using ReportWebsite.Models;
using ReportWebsite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace ReportWebsite.Plugins
{
    public class WebSiteDataProvider
    {
        private readonly IWebsiteRepository _websiteRepository;

        public WebSiteDataProvider(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }
        ResultActivity InsertWebsite(WebSite webSite)
        {
            try
            {
                using (var ts = new TransactionScope(TransactionScopeOption.Required,
                 TransactionScopeAsyncFlowOption.Enabled))
                {
                    Entities.EN_WebSite eN_WebSite= webSite.ToWebSite();
                    var result =  _websiteRepository.Insert(eN_WebSite);
                   
                    ts.Complete();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}