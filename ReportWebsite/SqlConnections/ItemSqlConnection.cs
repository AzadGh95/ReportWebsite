using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System;
using ReportWebsite.Models;
using System.Collections.Generic;
using ReportWebsite.Enums;
using WebSiteType = ReportWebsite.Enums.ReportWebSiteType.WebSiteType;


namespace ReportWebsite.SqlConnections
{
    public static class ItemSqlConnection
    {
        public static List<Item> SelectItem(int? id)
        {
            try
            {
                if (id == null)
                {
                    SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");
                    SqlCommand sda = new SqlCommand("SELECT * FROM Item", con);
                    con.Open();

                    var sqlr = sda.ExecuteReader();
                    var Item = new List<Item>();

                    while (sqlr.Read())
                    {
                        Item.Add((Item)sqlr);
                    }
                    con.Close();
                    return Item.ToList();
                }
                else
                {
                    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ReportWebSite;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Item WHERE [ItemId]= @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();

                    var sqlr = cmd.ExecuteReader();
                    var Item = new List<Item>();

                    while (sqlr.Read())
                    {
                        Item.Add((Item)sqlr);
                    }
                    con.Close();
                    return Item.ToList();
                }
            }
            catch (System.Exception e)
            {
                var m=e.Message;
                throw;
            }
        }
        public static List<Item> SelectItemByType(WebSiteType type)
        {
            try
            {

                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("SELECT * FROM Item WHERE [Type] = @type", con);
                cmd.Parameters.AddWithValue("@type", type);
                con.Open();

                var sqlr = cmd.ExecuteReader();
                var Item = new List<Item>();

                while (sqlr.Read())
                {
                    Item.Add((Item)sqlr);
                }
                con.Close();
                return Item.ToList();

            }
            catch (System.Exception)
            {
                return null;
                throw;
            }
        }
        public static bool InsertItem(Item Item)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Item ([Text],[Type]) " +
                    "values(@text ,@type)", con);

                //cmd.Parameters.AddWithValue("@id", Item.ItemId);
                cmd.Parameters.AddWithValue("@text", Item.Text);
                cmd.Parameters.AddWithValue("@type", Item.Type);

                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
        }
        public static bool UpdateItem(Item Item)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=Restaurant;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE  Item SET [Text]= @text WHERE ([ItemId]= @id )", con);
                cmd.Parameters.AddWithValue("@text", Item.Text);
                cmd.Parameters.AddWithValue("@id", Item.ItemId);
                cmd.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public static bool DeleteItem(int itemId)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE from Item where ([ItemId] = @id) ", con);
                cmd.Parameters.AddWithValue("@id", itemId);

                cmd.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
