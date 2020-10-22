using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ReportWebsite.UtilitySystem.HtmlHelper
{
   public static  class ExtentionHtmlHelper
    {
        public static MvcHtmlString StyleSheetPartial(this System.Web.Mvc.HtmlHelper htmlHelper, string nameScript,
            string linkScripts)
        {
            htmlHelper.ViewContext.HttpContext.Items["_stylesheet_" + nameScript] = linkScripts;
            return MvcHtmlString.Empty;
        }

        public static IHtmlString RenderStyleSheetPartial(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            foreach (var key in htmlHelper.ViewContext.HttpContext.Items.Keys)
            {
                if (!key.ToString().StartsWith("_stylesheet_")) continue;
                var template = (string)htmlHelper.ViewContext.HttpContext.Items[key];
                if (template != null) htmlHelper.ViewContext.Writer.Write(template);
            }

            return MvcHtmlString.Empty;
        }
        public static MvcHtmlString ScriptPartial(this System.Web.Mvc.HtmlHelper htmlHelper, string nameScript,
            string linkScripts)
        {
            htmlHelper.ViewContext.HttpContext.Items["_script_" + nameScript] = linkScripts;
            return MvcHtmlString.Empty;
        }
        public static IHtmlString RenderScriptsPartial(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            foreach (var key in htmlHelper.ViewContext.HttpContext.Items.Keys)
            {
                if (!key.ToString().StartsWith("_script_")) continue;
                var template = (string)htmlHelper.ViewContext.HttpContext.Items[key];
                if (template != null) htmlHelper.ViewContext.Writer.Write(template);
            }

            return MvcHtmlString.Empty;
        }

        public static IHtmlString RenderBreadcrumbs(this System.Web.Mvc.HtmlHelper htmlHelper,
            params string[] breadcrumbs)
        {
            string html = $@"
                                 <div class='row breadcrumbs-top'>
                                        <div class='breadcrumb-wrapper col-12'>
                                            <ol class='breadcrumb'>
                                                {createBreadcrumbs(breadcrumbs)}
                                            </ol>
                                        </div>
                                    </div>

            ";
            return new HtmlString(html);
        }
 public static IHtmlString DisplayUnitProduct(this System.Web.Mvc.HtmlHelper htmlHelper,
            string sellUnit,float? amount,string detailUnit)
        {
            string html =sellUnit;
            if (!string.IsNullOrEmpty(detailUnit))
                html += $" معادل {amount} {detailUnit}";
            return new HtmlString(html);
        }

        public static IHtmlString RenderLoadModalInDiv(this System.Web.Mvc.HtmlHelper htmlHelper,string elementButton,string panelload,string dataIdname,string nameModal,string action,string controller,string nameparameters)
        {
            UrlHelper url=new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string html = $@"

                                        $(document).on('click','{elementButton}',function() {{
 var id = $(this).data('{dataIdname}');
                                                var block_ele = $(this).closest('.card');
                                            var isblock;
                                            // Block Element
                                            block_ele.block({{
                                                message: '<div class=""ft-refresh-cw icon-spin font-medium-2""></div>',
                                                //timeout: 3000, //unblock after 2 seconds
                                                overlayCSS: {{
                                                    backgroundColor: '#FFF',
                                                    cursor: 'wait',
                                                }},
                                                css: {{
                                                    border: 0,
                                                    padding: 0,
                                                    backgroundColor: 'none'
                                                }},
                                                onBlock: function () {{
                                                   
                                                        $('{panelload}').load(
                                                        '{url.Action(action,controller)}?{nameparameters}=' +
                                                        id,
                                                    function() {{
                                                        $('{nameModal}').modal('show');
                                                        block_ele.unblock();
                                                    }});

                                                }}
                                            }});
                                            }});";

            return new HtmlString(html);
        }

        private static object createBreadcrumbs(string[] breadcrumbs)
        {
            string html = string.Empty;
            for (int i = 0; i < breadcrumbs.Length; i++)
            {

                    html += $@"<li class='breadcrumb-item {(i==breadcrumbs.Length-1?"active":string.Empty)}'>
                    {breadcrumbs[i]}
                </li>";

            }

            return html;
        }
        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }

//        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };
//
//        public static string GetEnumDescription<TEnum>(TEnum value)
//        {
//            FieldInfo fi = value.GetType().GetField(value.ToString());
//
//            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
//
//            if ((attributes != null) && (attributes.Length > 0))
//                return attributes[0].Description;
//            else
//                return value.ToString();
//        }
//
//        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
//        {
//            return EnumDropDownListFor(htmlHelper, expression, null);
//        }
//        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
//        {
//            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
//            Type enumType = GetNonNullableModelType(metadata);
//            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();
//
//            IEnumerable<SelectListItem> items = from value in values
//                select new SelectListItem
//                {
//                    Text = GetEnumDescription(value),
//                    Value = value.ToString(),
//                    Selected = value.Equals(metadata.Model)
//                };
//
//            // If the enum is nullable, add an 'empty' item to the collection
//            if (metadata.IsNullableValueType)
//                items = SingleEmptyItem.Concat(items);
//
//            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
//        }
    }
}
