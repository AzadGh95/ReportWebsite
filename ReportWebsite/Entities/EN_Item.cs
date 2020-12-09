using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static ReportWebsite.Enums.ReportWebSiteType;

namespace ReportWebsite.Entities
{
    public class EN_Item
    {
        [Key]
        public int ItemId { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(600)]
        public string Text { get; set; }
        public WebSiteType Type { get; set; }

    }
}