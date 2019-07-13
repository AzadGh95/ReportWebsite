﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace ReportWebsite.Models
{
    public class Element
    {
        public int ElementId { get; set; }
        public bool Status { get; set; }
        public string Value { get; set; }
        public int SiteId { get; set; }

        public static implicit operator Element(SqlDataReader sql)
        {
            return new Element
            {
                Value = sql["Value"].ToString(),
                Status = bool.Parse(sql["Status"].ToString()),
                SiteId = int.Parse(sql["SiteId"].ToString()),

            };
        }
    }
}