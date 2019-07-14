using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace ReportWebsite.Models
{
    public class WebSite
    {
        public int SiteId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Admin { get; set; }

        public static implicit operator WebSite(SqlDataReader sql)
        {
            return new WebSite
            {
                Name = sql["Name"].ToString(),
                Admin = sql["Admin"].ToString(),
                CreateDate = DateTime.Parse(sql["CreateDate"].ToString()),
            };
        }
    }
}