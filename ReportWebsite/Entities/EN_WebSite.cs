using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ReportWebsite.Enums.ReportWebSiteType;

namespace ReportWebsite.Entities
{
    public class EN_WebSite
    {
        public int SiteId { get; set; }
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }
        public string Admin { get; set; }
        public WebSiteType Type { get; set; }

        public string UserSite { get; set; }
        public string UserSuper { get; set; }
        public string PasswordSite { get; set; }
        public string PasswordSuper { get; set; }
        public string Description { get; set; }
                         }
}