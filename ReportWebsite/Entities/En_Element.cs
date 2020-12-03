using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebsite.Entities
{
    public class En_Element
    {

        public int ElementId { get; set; }
        public bool Status { get; set; }
        public string Value { get; set; }
        public string ItemText { get; set; }
        public int ItemId { get; set; }
        public int SiteId { get; set; }

    }
}