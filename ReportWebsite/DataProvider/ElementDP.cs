using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReportWebsite.Models;
using ReportWebsite.SqlConnections;

namespace ReportWebsite.DataProvider
{
    public class ElementDP
    {
        public Element GetElement(int id)
        {
            return ElementSqlConnection.SelectElement(id).FirstOrDefault();        }

        public List<Element> GetElements()
        {
            return ElementSqlConnection.SelectElement(null);
        }

        public bool UpdateElement(Element element) {
            return ElementSqlConnection.UpdateElement(element);
        }


    }
}