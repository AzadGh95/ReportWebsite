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
using System.ComponentModel.DataAnnotations;

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
        [UIHint("LocalDatetime")]
        public DateTime CreateDate { get; set; }
        public string Admin { get; set; }
        public WebSiteType Type { get; set; }
    

        private List<Element> _elements;
        private bool _get;
        public List<Element> Elements
        {
            get
            {
                if (!_get && _elements == null)
                {
                    _elements = ElementSqlConnection.SelectElementBySite(SiteId).ToList();
                    _get = true;
                }

                return _elements;
            }
            set
            {
                _elements = value;
                _get = true;
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