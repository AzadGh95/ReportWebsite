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
    public class WebSiteSqlConnection
    {
        public List<WebSite> SelectWebSite()
        {
            try
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
                return website;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public bool InsertWebSite()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;InitialCatalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd2 = new SqlCommand("INSERT INTO WebSite ([Name],[CreateDate],[Amin]) " +
                    "values(@name,@createdate,@admin )", con);

                cmd2.Parameters.AddWithValue("@name", true);
                cmd2.Parameters.AddWithValue("@createdate", "مقدار تست ");
                cmd2.Parameters.AddWithValue("@admin", 1);

                cmd2.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool DeleteWebSite()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE from WebSite where ([SiteId] = @id) ", con);
                cmd.Parameters.AddWithValue("@id", 1);

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