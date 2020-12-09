using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReportWebsite.Entities
{
    public class En_Element
    {
        [Key]
        public int ElementId { get; set; }
        public bool Status { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(300)]
        public string Value { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(300)]
        public string ItemText { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public EN_Item Items { get; set; }


        public int SiteId { get; set; }
        [ForeignKey("SiteId")]
        public EN_WebSite WebSites { get; set; }


    }
}