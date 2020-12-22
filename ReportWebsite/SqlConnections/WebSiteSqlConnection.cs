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
        public const string ConnectionString = "Data Source=.;Initial Catalog=ReportWebSiteDB;Integrated Security=True";

        public static bool CheckDatabase(string name = "ReportWebSiteDB")
        {
            try
            {
                var result = false;
                string query = $@"SELECT * FROM sys.databases WHERE name = '{name}'";

                using (var sqlconn = new SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=True"))
                {
                    var sqlcomm = new SqlCommand(query, sqlconn);
                    if (sqlconn.State != ConnectionState.Open)
                    {
                        sqlconn.Open();

                    }


                    var ready = sqlcomm.ExecuteReader();
                    while (ready.Read())
                    {
                        result = true;
                    }

                    if (sqlconn.State == ConnectionState.Open)
                    {
                        sqlconn.Close();

                    }

                    return result;

                }

            }
            catch (Exception exc)
            {
                return false;
                
            }
        }

        public static bool CreateDB()
        {
            try
            {
                if (CheckDatabase())
                    return true;

                string src = "Create database 'ReportWebSiteDB'";
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=True");
                SqlCommand sda = new SqlCommand(src, con);
                con.Open();
                int exists = sda.ExecuteNonQuery();
                if ((con.State == ConnectionState.Open))
                {
                    con.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public static bool CreateTableWebSite()
        {
            try
            {
                var src = "If not exists (select name from sysobjects where name = 'WebSite') " +
                    "CREATE TABLE WebSite(SiteId int , Name char(50),CreateDate datetime," +
                    "Admin char(50),Type char(10))";

                SqlConnection con = new SqlConnection(ConnectionString);
                using (SqlCommand command = new SqlCommand(src, con))
                    command.ExecuteNonQuery();
                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        }
        public static List<WebSite> SelectWebSiteByType(WebSiteType type)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConnectionString);
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

                    SqlConnection con = new SqlConnection(ConnectionString);
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
                    SqlConnection con = new SqlConnection(ConnectionString);
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
                SqlConnection con = new SqlConnection(ConnectionString);

                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO WebSite " +
                    "([Name],[CreateDate],[Admin],[Type],[UserSuper],[PasswordSuper],[UserSite],[PasswordSite],[Description]) " +
                    "values (@name,@createdate,@admin,@type,@usersuper,@passwordsuper,@usersite,@passwordsite,@description )", con);

                cmd.Parameters.AddWithValue("@name", webSite.Name);
                cmd.Parameters.AddWithValue("@createdate", webSite.CreateDate);
                cmd.Parameters.AddWithValue("@admin", webSite.Admin);
                cmd.Parameters.AddWithValue("@type", webSite.Type);
                cmd.Parameters.AddWithValue("@usersite", webSite.UserSite);
                cmd.Parameters.AddWithValue("@usersuper", webSite.UserSuper);
                cmd.Parameters.AddWithValue("@passwordsite", webSite.PasswordSite);
                cmd.Parameters.AddWithValue("@passwordsuper", webSite.PasswordSuper);
                cmd.Parameters.AddWithValue("@description", webSite.Description);

                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;
                throw;
            }
        }
        public static bool DeleteWebSite(int siteId)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);

                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE from WebSite where ([SiteId] = @id) ", con);
                cmd.Parameters.AddWithValue("@id", siteId);

                cmd.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static bool UpdateWebSite(WebSite website)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE  WebSite SET [Name]= @name ," +
                    " [Admin]= @admin ,[CreateDate] = @createdate , [UserSite]=@usersite,[UserSuper]=@usersuper"  +
                    ", [PasswordSite]=@passwordsite , [PasswordSuper]=@passwordsuper ,[Description]=@description " +
                    " WHERE ([SiteId]= @siteid)", con);
                cmd.Parameters.AddWithValue("@name", website.Name);
                cmd.Parameters.AddWithValue("@createdate", website.CreateDate);
                cmd.Parameters.AddWithValue("@admin", website.Admin);
                cmd.Parameters.AddWithValue("@siteid", website.SiteId);
                cmd.Parameters.AddWithValue("@usersite", website.UserSite);
                cmd.Parameters.AddWithValue("@usersuper", website.UserSuper);
                cmd.Parameters.AddWithValue("@passwordsite", website.PasswordSite);
                cmd.Parameters.AddWithValue("@passwordsuper", website.PasswordSuper);
                cmd.Parameters.AddWithValue("@description", website.Description);
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
                SqlConnection con = new SqlConnection(ConnectionString);
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

                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand sda = new SqlCommand("SELECT COUNT(SiteId) FROM WebSite  WHERE [Type] = @type", con);
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