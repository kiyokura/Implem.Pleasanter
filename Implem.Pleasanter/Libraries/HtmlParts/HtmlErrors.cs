﻿using Implem.Pleasanter.Libraries.Html;
using Implem.Pleasanter.Libraries.Models;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Libraries.Server;
using System.Web;
using System.Web.Mvc;
namespace Implem.Pleasanter.Libraries.HtmlParts
{
    public static class HtmlErrors
    {
        public static string Get()
        {
            var error = HttpContext.Current.Session["error"] as ExceptionContext;
            var hb = new HtmlBuilder();
            return hb.Template(
                pt: Permissions.Types.NotSet,
                verType: Versions.VerTypes.Latest,
                methodType: Pleasanter.Models.BaseModel.MethodTypes.NotSet,
                allowAccess: true,
                title: Displays.Error(),
                action: () => hb
                    .Section(css: "error-page", action: () => hb
                        .H(number: 1, css: "error-page-title", action: () => hb
                            .Text(Displays.ExceptionTitle()))
                        .P(css: "error-page-body", action: () => hb
                            .Text(Displays.ExceptionBody()))
                        .P(css: "error-page-message", action: () => hb
                            .Text(error.Exception.Message))
                        .Details(error))).ToString();
        }

        private static HtmlBuilder Details(this HtmlBuilder hb, ExceptionContext error)
        {
            return HttpContext.Current.Request.UserHostAddress == "::1" && Sessions.Developer()
                ? hb
                    .P(css: "error-page-action", action: () => hb
                        .Em(action: () => hb
                            .Text(error.RouteData.Values["controller"].ToString()))
                        .Em(action: () => hb
                            .Text(error.RouteData.Values["action"].ToString()))
                        .Em(action: () => hb
                            .Text(error.Exception.TargetSite.ToString()))
                        .Em(action: () => hb
                            .Text(error.Exception.HResult.ToString())))
                    .P(css: "error-page-stacktrace", action: () => hb
                        .Text(error.Exception.StackTrace))
                : hb;
        }
    }
}