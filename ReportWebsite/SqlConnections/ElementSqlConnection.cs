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
    public class ElementSqlConnection
    {
        public List<Element> SelectElement()
        {
            try
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
                return Element;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public bool InsertElement(Element element)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;InitialCatalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd2 = new SqlCommand("INSERT INTO Element ([Status],[Value],[SiteId]) " +
                    "values(@status,@value,@siteId )", con);

                cmd2.Parameters.AddWithValue("@status", element.Status);
                cmd2.Parameters.AddWithValue("@value", element.Value);
                cmd2.Parameters.AddWithValue("@siteId", element.SiteId);

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
        public bool DeleteElement()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=ReportWebSite;Integrated Security=True");

                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE from Element where ([ElementId] = @id) ", con);
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