using Dualp.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ReportWebsite.Enums.ReportWebSiteType;

namespace ReportWebsite.Repositories
{
    public interface IWebsiteRepository : IDisposable
    {
        ResultActivity Delete(int Id);
        ResultActivity Insert(Entities.EN_WebSite WebSite);
        ResultActivity Edit(Entities.EN_WebSite WebSite , int id);
        Entities.EN_WebSite GetWebsite(int id);
        List<Entities.EN_WebSite> GetWebSites(WebSiteType type);
        int CountWeb(WebSiteType webSiteType);
    }
}
