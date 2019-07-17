using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReportWebsite.Enums;
using ReportWebsite.Models;
using ReportWebsite.SqlConnections;
using WebSiteType = ReportWebsite.Enums.ReportWebSiteType.WebSiteType;


namespace ReportWebsite.DataProvider
{
    public class ItemDP
    {
        public Item GetItem(int id)
        {
            return ItemSqlConnection.SelectItem(id).FirstOrDefault();
        }

        public List<Item> GetItems()
        {
            return ItemSqlConnection.SelectItem(null);
        }
        public List<Item> GetItemsByType(WebSiteType type)
        {
            return ItemSqlConnection.SelectItemByType(type);
        }
        public bool UpdateItem(Item Item)
        {
            return ItemSqlConnection.UpdateItem(Item);
        }
        public bool Insert(Item item)
        {
            return ItemSqlConnection.InsertItem(item);
        }

    }
}