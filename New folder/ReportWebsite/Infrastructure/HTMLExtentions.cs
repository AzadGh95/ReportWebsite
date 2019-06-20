using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportWebsite.Infrastructure
{
    public static class HTMLExtentions
    {
        public static MvcHtmlString RenderBroadcast(this  System.Web.Mvc.HtmlHelper htmlHelper ,string title,params Tuple<string,string>[] links)
        {
            var html = string.Empty;
            if (!string.IsNullOrEmpty(title))
                html += $@"<h3 class='content-header-title mb-0'>{title}</h3>";

            html += $@"<div class='row breadcrumbs-top'>
        <div class='breadcrumb-wrapper col-12'>
            <ol class='breadcrumb'>
              {CreateItemsBrodcast(links)}
               
                {(string.IsNullOrEmpty(title)?string.Empty:$@"<li class='breadcrumb-item active'>{title} </li>")}
            </ol>
        </div>
    </div>";

            return new MvcHtmlString(html);


        }

        private static string CreateItemsBrodcast(Tuple<string, string>[] links)
        {
            string html = string.Empty;
            if (links != null)
            {
                foreach (var link in links)
                {
                    html += $@"<li class='breadcrumb-item'>
                    <a href='{link.Item1}'>{link.Item2}</a>
                </li>";
                }
            }
            return html;
        }
    }
}