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
    public static class ElementSqlConnection
    {
        public static List<Element> SelectElement(int? elementId)
        {
            try
            {
                if (elementId == null)
                {
                    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ReportWebSite;Integrated Security=True");
                    SqlCommand sda = new SqlCommand("SELECT * FROM Element", con);
                    con.Open();

                    var sqlr = sda.ExecuteReader();
                    var Element = new List<Element>();

                    while (sqlr.Read())
                    {
                        Element.Add((Element)sqlr);
                    }
                    con.Close();
                    return Element.ToList();
                }
                else
                {
                    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ReportWebSite;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Element WHERE [ElementId]= @id", con);
                    cmd.Parameters.AddWithValue("@id", elementId);
                    con.Open();

                    var sqlr = cmd.ExecuteReader();
                    var Element = new List<Element>();

                    while (sqlr.Read())
                    {
                        Element.Add((Element)sqlr);
                    }
                    con.Close();
                    return Element.ToList();
                }
            }
            catch (System.Exception e)
            {
                throw;
            }
        }
        public static List<Element> SelectElementBySite(int siteId)
        {
            try
            {

                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ReportWebSite;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("SELECT * FROM Element WHERE [SiteId] = @siteId", con);
                cmd.Parameters.AddWithValue("@siteId", siteId);
                con.Open();

                var sqlr = cmd.ExecuteReader();
                var Element = new List<Element>();

                while (sqlr.Read())
                {
                    Element.Add((Element)sqlr);
                }
                con.Close();
                return Element.ToList();

            }
            catch (System.Exception)
            {
                return null;
                throw;
            }
        }
        public static bool InsertElement(Element element,int siteId)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Element ([Status],[Value],[SiteId],[ItemText],[ItemId]) " +
                    "values(@status,@value,@siteId,@itemtext , @itemid )", con);

                cmd.Parameters.AddWithValue("@status", element.Status);
                cmd.Parameters.AddWithValue("@value", element.Value);
                cmd.Parameters.AddWithValue("@siteId", siteId);
                cmd.Parameters.AddWithValue("@itemtext", element.ItemText);
                cmd.Parameters.AddWithValue("@itemid", element.ItemId);


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
        public static bool UpdateElement(Element element)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ReportWebSite;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE  Element SET [Status]= @status , [Value]= @value WHERE ([ElementId]= @id AND [SiteId]= @siteid)", con);
                cmd.Parameters.AddWithValue("@status", element.Status);
                cmd.Parameters.AddWithValue("@value", element.Value);
                cmd.Parameters.AddWithValue("@id", element.ElementId);
                cmd.Parameters.AddWithValue("@siteid", element.SiteId);
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
        public static bool DeleteElement(int siteId)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE from Element where ([SiteId] = @id) ", con);
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

        public static bool DeleteElementByItemId(int itemId)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE from Element where ([ItemId] = @id) ", con);
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
