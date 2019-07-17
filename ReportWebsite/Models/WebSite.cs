using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using ReportWebsite.SqlConnections;
using ReportWebsite.Enums;
using WebSiteType = ReportWebsite.Enums.ReportWebSiteType.WebSiteType;


namespace ReportWebsite.Models
{
    public class WebSite
    {
        public WebSite()
        {
            CreateDate = DateTime.UtcNow;
        }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Admin { get; set; }
        public WebSiteType Type { get; set; }
        public List<Element> Elements
        {
            get
            {
                return ElementSqlConnection.SelectElement(null);
            }
            set
            {
                Elements = value;
            }
        }

        public static implicit operator WebSite(SqlDataReader sql)
        {
            return new WebSite
            {

                SiteId = int.Parse(sql["SiteId"].ToString()),
                Name = sql["Name"].ToString(),
                Admin = sql["Admin"].ToString(),
                CreateDate = DateTime.Parse(sql["CreateDate"].ToString()),
            };
        }
    }
}