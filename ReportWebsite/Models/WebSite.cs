﻿using System;
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

        [Display(Name = "تاریخ تحویل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
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

        public string UserSite { get; set; }
        public string UserSuper { get; set; }
        public string PasswordSite { get; set; }
        public string PasswordSuper { get; set; }
        public string Description { get; set; }






        public static implicit operator WebSite(SqlDataReader sql)
        {
            return new WebSite
            {

                SiteId = int.Parse(sql["SiteId"].ToString()),
                Name = sql["Name"].ToString(),
                Admin = sql["Admin"].ToString(),
                UserSite = sql["UserSite"].ToString(),
                UserSuper = sql["UserSuper"].ToString(),
                PasswordSite = sql["PasswordSite"].ToString(),
                PasswordSuper = sql["PasswordSuper"].ToString(),
                Description = sql["Description"].ToString(),
                CreateDate = DateTime.Parse(sql["CreateDate"].ToString()),
            };
        }
    }
}