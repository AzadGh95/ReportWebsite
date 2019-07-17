using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebsite.Enums
{
    public class ReportWebSiteType
    {
        public enum WebSiteType : byte
        {
            Design,
            WebApp,
            Project,
            Application
        }

        public static string ToDisplay(WebSiteType type)
        {
            switch (type)
            {
                case WebSiteType.Design:
                    return "طراحی";
           
                case WebSiteType.WebApp:
                    return "وب سایت";
                case WebSiteType.Project:
                    return "پروژه";
                case WebSiteType.Application:
                    return "اپلیکیشن";
                default:
                    return "هیچی";
            }
        }
    }

}