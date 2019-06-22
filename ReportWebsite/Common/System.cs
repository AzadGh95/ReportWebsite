using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebsite.Common
{
    public class System
    {
        public static string UserName
        {
            get { return HttpContext.Current.Session["UserName"].ToString(); }
            set
            {
                if (HttpContext.Current.Session["UserName"] != null)
                    HttpContext.Current.Session["UserName"] = value;
                HttpContext.Current.Session.Add("UserName", value);
            }
        }
        public static string ConnectionString { get; set; }
    }
}