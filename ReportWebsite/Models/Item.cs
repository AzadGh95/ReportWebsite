using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ReportWebsite.Enums;
using WebSiteType = ReportWebsite.Enums.ReportWebSiteType.WebSiteType;
using System.ComponentModel.DataAnnotations;

namespace ReportWebsite.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        [Required(ErrorMessage ="لطفا توضیحات آیتم را وارد کنید .")]
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
        public static implicit operator Item(Entities.EN_Item model)
        {
            if (model == null) return null;

            return new Item()
            {
                ItemId = model.ItemId,
                Text = model.Text,
                Type = model.Type

            };
        }
        public Entities.EN_Item ToItem()
        {
            return new Entities.EN_Item
            {
                ItemId = ItemId,
                Text = Text,
                Type = Type,
            };
        }
    }
}