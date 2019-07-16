using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ReportWebsite.Enums;

namespace ReportWebsite.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Text { get; set; }
        public WebSiteType Type { get; set; }

        public static implicit operator Item(SqlDataReader sql)
        {
            var _type =int.Parse(sql["Type"].ToString());

            return new Item
            {
                ItemId = int.Parse(sql["ItemId"].ToString()),
                Text = sql["Text"].ToString(),
                Type = _type == 0 ? WebSiteType.Design :
                            _type == 1 ? WebSiteType.WebApp :
                            _type == 2 ? WebSiteType.Project : 
                            WebSiteType.Application,
            };
        }
    }
}