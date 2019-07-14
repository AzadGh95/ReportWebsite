using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ReportWebsite.SqlConnections;

namespace ReportWebsite.Models
{
    public class WebSite
    {
        public int SiteId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Admin { get; set; }
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
                Name = sql["Name"].ToString(),
                Admin = sql["Admin"].ToString(),
                CreateDate = DateTime.Parse(sql["CreateDate"].ToString()),
            };
        }
    }
}