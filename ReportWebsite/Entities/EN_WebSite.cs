using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static ReportWebsite.Enums.ReportWebSiteType;

namespace ReportWebsite.Entities
{
    public class EN_WebSite
    {
        [Key]
        public int SiteId { get; set; }

        [StringLength(60)]
        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string Admin { get; set; }
        public WebSiteType Type { get; set; }

        [StringLength(50)]
        public string UserSite { get; set; }

        [StringLength(50)]
        public string UserSuper { get; set; }
        [Column(TypeName = "Nvarchar")]
        [StringLength(100)]
        public string PasswordSite { get; set; }
        [Column(TypeName = "Nvarchar")]
        [StringLength(100)]
        public string PasswordSuper { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(500)]
        public string Description { get; set; }
    }
}