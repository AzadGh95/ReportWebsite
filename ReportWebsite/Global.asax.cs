using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ReportWebsite
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var persianCulture = new PersianCulture();
            persianCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            persianCulture.DateTimeFormat.LongDatePattern = "dddd d MMMM yyyy";
            persianCulture.DateTimeFormat.AMDesignator = "صبح";
            persianCulture.DateTimeFormat.PMDesignator = "عصر";
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }

    }
}
