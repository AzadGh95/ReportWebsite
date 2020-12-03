using Dualp.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportWebsite.Repositories
{
    interface IWebsiteRepository :IDisposable
    {
        ResultActivity Delete(int Id);
        ResultActivity Insert(Entities.EN_WebSite WebSite);
        ResultActivity Edit(Entities.EN_WebSite WebSite);
        Entities.EN_WebSite GetElement();
        List<Entities.EN_WebSite> GetElements();
    }
}
