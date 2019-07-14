using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System;
using ReportWebsite.Models;
using System.Collections.Generic;

namespace ReportWebsite.SqlConnections
{
    public static class WebSiteSqlConnection
    {
        public static List<WebSite> SelectWebSite(int? id)
        {
            try
            {
                if (id == null)
                {

                    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ReportWebSite;Integrated Security=True");
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
                    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ReportWebSite;Integrated Security=True");
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
            catch (System.Exception)
            {
                throw;
            }
        }
        public static bool InsertWebSite(WebSite webSite)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;InitialCatalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO WebSite ([Name],[CreateDate],[Admin]) " +
                    "values(@name,@createdate,@admin )", con);

                cmd.Parameters.AddWithValue("@name", webSite.Name);
                cmd.Parameters.AddWithValue("@createdate", webSite.CreateDate);
                cmd.Parameters.AddWithValue("@admin", webSite.Admin);

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
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public static bool UpdateWebSite(WebSite website)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Restaurant;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE  WebSite SET [Name]= @name , [Admin]= @admin WHERE ([SiteId]= @siteid)", con);
                cmd.Parameters.AddWithValue("@name", website.Name);
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
    }
}