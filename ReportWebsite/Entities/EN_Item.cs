using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ReportWebsite.Enums.ReportWebSiteType;

namespace ReportWebsite.Entities
{
    public class EN_Item
    {
        public int ItemId { get; set; }
        public string Text { get; set; }
        public WebSiteType Type { get; set; }
    }
}