﻿using Implem.Pleasanter.Libraries.Html;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Security;
namespace Implem.Pleasanter.Libraries.HtmlParts
{
    public static class HtmlHeaders
    {
        public static HtmlBuilder Header(
            this HtmlBuilder hb,
            Permissions.Types pt,
            long siteId,
            string referenceType,
            bool allowAccess,
            bool useNavigationMenu,
            bool useSearch)
        {
            return hb.Header(id: "Header", action: () => hb
                .H(number: 2, id: "Logo", action: () => hb
                    .A(
                        attributes: new HtmlAttributes().Href(Locations.Top()),
                        action: () => hb
                            .Img(
                                id: "CorpLogo",
                                src: Locations.Images("logo-corp.png"))
                            .Span(id: "ProductLogo", action: () => hb
                                .Text(text: Displays.ProductName()))))
                .NavigationMenu(
                    pt: pt,
                    siteId: siteId,
                    referenceType: referenceType,
                    allowAccess: allowAccess,
                    useNavigationMenu: useNavigationMenu,
                    useSearch: useSearch));

        }
    }
}