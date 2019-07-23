using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System;
using ReportWebsite.Models;
using System.Collections.Generic;
using static ReportWebsite.Enums.ReportWebSiteType;

namespace ReportWebsite.SqlConnections
{
    public static class WebSiteSqlConnection
    {
        public static List<WebSite> SelectWebSiteByType(WebSiteType type)
        {
            try
            {

                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");
                SqlCommand sda = new SqlCommand("SELECT * FROM WebSite WHERE [Type] = @type ORDER BY [CreateDate] DESC", con);
                sda.Parameters.AddWithValue("@type", type);

                con.Open();

                var sqlr = sda.ExecuteReader();
                var website = new List<WebSite>();

                while (sqlr.Read())
                {
                    website.Add((WebSite)sqlr);
                }
                con.Close();
                return website.ToList();
            }
            catch (System.Exception e)
            {
                var message = e.Message;
                throw;
            }
        }
        public static List<WebSite> SelectWebSite(int? id)
        {
            try
            {
                if (id == null)
                {

                    SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");
                    SqlCommand sda = new SqlCommand("SELECT * FROM WebSite", con);
                    con.Open();

                    var sqlr = sda.ExecuteReader();
                    var website = new List<WebSite>();

                    while (sqlr.Read())
                    {
                        website.Add((WebSite)sqlr);
                    }
                    con.Close();
                    return website.ToList();
                }
                else
                {
                    SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("SELECT * FROM WebSite WHERE  [SiteId] = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();

                    var sqlr = cmd.ExecuteReader();
                    var website = new List<WebSite>();

                    while (sqlr.Read())
                    {
                        website.Add((WebSite)sqlr);
                    }
                    con.Close();
                    return website;
                }
            }
            catch (System.Exception e)
            {
                var message = e.Message;
                throw;
            }
        }
        public static bool InsertWebSite(WebSite webSite)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO WebSite ([Name],[CreateDate],[Admin],[Type]) " +
                    "values (@name,@createdate,@admin,@type )", con);

                cmd.Parameters.AddWithValue("@name", webSite.Name);
                cmd.Parameters.AddWithValue("@createdate", webSite.CreateDate);
                cmd.Parameters.AddWithValue("@admin", webSite.Admin);
                cmd.Parameters.AddWithValue("@type", webSite.Type);

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
        public static bool DeleteWebSite(int siteId)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE from WebSite where ([SiteId] = @id) ", con);
                cmd.Parameters.AddWithValue("@id", siteId);

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
        public static bool UpdateWebSite(WebSite website)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE  WebSite SET [Name]= @name , [Admin]= @admin ,[CreateDate] = @createdate WHERE ([SiteId]= @siteid)", con);
                cmd.Parameters.AddWithValue("@name", website.Name);
                cmd.Parameters.AddWithValue("@createdate", website.CreateDate);
                cmd.Parameters.AddWithValue("@admin", website.Admin);
                cmd.Parameters.AddWithValue("@siteid", website.SiteId);
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
        public static int SelectLastIndex()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");
                SqlCommand sda = new SqlCommand("SELECT MAX(SiteId) FROM WebSite  ", con);
                con.Open();

                var sqlr = sda.ExecuteReader();
                //var r = int.Parse(sqlr["SiteId"].ToString());
                int columnValue = 0;

                while (sqlr.Read())
                {
                    string column = sqlr[0].ToString();
                    columnValue = Convert.ToInt32(sqlr[0]);
                }
                con.Close();
                return columnValue;

            }
            catch (System.Exception e)
            {
                var message = e.Message;
                throw;
            }
        }
        public static int Count(WebSiteType type)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");
                SqlCommand sda = new SqlCommand("SELECT COUNT(SiteId) FROM WebSite  WHERE [Type] = @type" , con);
                sda.Parameters.AddWithValue("@type", type);
                con.Open();
                var sqlr = sda.ExecuteReader();
                int columnValue = 0;
                while (sqlr.Read())
                {
                    string column = sqlr[0].ToString();
                    columnValue = Convert.ToInt32(sqlr[0]);
                }
                con.Close();
                return columnValue;

            }
            catch (System.Exception e)
            {
                var message = e.Message;
                throw;
            }
        }
    }
}