﻿using Implem.Pleasanter.Libraries.General;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Libraries.Settings;
namespace Implem.Pleasanter.Models
{
    public static class WikiValidators
    {
        public static Error.Types OnCreating(
            SiteSettings ss, Permissions.Types pt, WikiModel wikiModel)
        {
            if (!pt.CanCreate())
            {
                return Error.Types.HasNotPermission;
            }
            foreach(var controlId in Forms.Keys())
            {
                switch (controlId)
                {
                    case "Wikis_Title":
                        if (!ss.GetColumn("Title").CanCreate(pt))
                        {
                            return Error.Types.InvalidRequest;
                        }
                        break;
                    case "Wikis_Body":
                        if (!ss.GetColumn("Body").CanCreate(pt))
                        {
                            return Error.Types.InvalidRequest;
                        }
                        break;
                    case "Wikis_Timestamp":
                        if (!ss.GetColumn("Timestamp").CanCreate(pt))
                        {
                            return Error.Types.InvalidRequest;
                        }
                        break;
                }
            }
            return Error.Types.None;
        }

        public static Error.Types OnUpdating(
            SiteSettings ss, Permissions.Types pt, WikiModel wikiModel)
        {
            if (!pt.CanUpdate())
            {
                return Error.Types.HasNotPermission;
            }
            foreach(var controlId in Forms.Keys())
            {
                switch (controlId)
                {
                    case "Wikis_Title":
                        if (!ss.GetColumn("Title").CanUpdate(pt))
                        {
                            return Error.Types.InvalidRequest;
                        }
                        break;
                    case "Wikis_Body":
                        if (!ss.GetColumn("Body").CanUpdate(pt))
                        {
                            return Error.Types.InvalidRequest;
                        }
                        break;
                    case "Wikis_Timestamp":
                        if (!ss.GetColumn("Timestamp").CanUpdate(pt))
                        {
                            return Error.Types.InvalidRequest;
                        }
                        break;
                }
            }
            return Error.Types.None;
        }

        public static Error.Types OnDeleting(
            SiteSettings ss, Permissions.Types pt, WikiModel wikiModel)
        {
            if (!pt.CanDelete())
            {
                return Error.Types.HasNotPermission;
            }
            return Error.Types.None;
        }

        public static Error.Types OnRestoring()
        {
            if (!Permissions.Admins().CanEditTenant())
            {
                return Error.Types.HasNotPermission;
            }
            return Error.Types.None;
        }
    }
}
