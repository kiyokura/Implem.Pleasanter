﻿using Implem.DefinitionAccessor;
using Implem.Libraries.Classes;
using Implem.Libraries.DataSources.SqlServer;
using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.Converts;
using Implem.Pleasanter.Libraries.DataSources;
using Implem.Pleasanter.Libraries.DataTypes;
using Implem.Pleasanter.Libraries.General;
using Implem.Pleasanter.Libraries.Html;
using Implem.Pleasanter.Libraries.HtmlParts;
using Implem.Pleasanter.Libraries.Models;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Libraries.Server;
using Implem.Pleasanter.Libraries.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace Implem.Pleasanter.Models
{
    public class ItemModel : BaseModel
    {
        public long ReferenceId = 0;
        public string ReferenceType = string.Empty;
        public long SiteId = 0;
        public string Title = string.Empty;
        public SiteModel Site = null;
        public long SavedReferenceId = 0;
        public string SavedReferenceType = string.Empty;
        public long SavedSiteId = 0;
        public string SavedTitle = string.Empty;
        public SiteModel SavedSite = null;
        public bool ReferenceId_Updated { get { return ReferenceId != SavedReferenceId; } }
        public bool ReferenceType_Updated { get { return ReferenceType != SavedReferenceType && ReferenceType != null; } }
        public bool SiteId_Updated { get { return SiteId != SavedSiteId; } }
        public bool Title_Updated { get { return Title != SavedTitle && Title != null; } }

        public ItemModel(
            SiteSettings ss,
            Permissions.Types pt,
            DataRow dataRow)
        {
            OnConstructing();
            SiteSettings = ss;
            Set(dataRow);
            OnConstructed();
        }

        private void OnConstructing()
        {
        }

        private void OnConstructed()
        {
        }

        public void ClearSessions()
        {
        }

        public ItemModel Get(
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal,
            SqlColumnCollection column = null,
            SqlJoinCollection join = null,
            SqlWhereCollection where = null,
            SqlOrderByCollection orderBy = null,
            SqlParamCollection param = null,
            bool distinct = false,
            int top = 0)
        {
            Set(Rds.ExecuteTable(statements: Rds.SelectItems(
                tableType: tableType,
                column: column ?? Rds.ItemsDefaultColumns(),
                join: join ??  Rds.ItemsJoinDefault(),
                where: where ?? Rds.ItemsWhereDefault(this),
                orderBy: orderBy ?? null,
                param: param ?? null,
                distinct: distinct,
                top: top)));
            return this;
        }

        public string Index()
        {
            if (ReferenceId == 0)
            {
                return SiteUtilities.SiteTop();
            }
            SetSite();
            ViewModes.Set(Site.SiteId);
            switch (Site.ReferenceType)
            {
                case "Sites": return SiteUtilities.SiteMenu(Site);
                case "Issues": return IssueUtilities.Index(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType);
                case "Results": return ResultUtilities.Index(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string IndexJson()
        {
            SetSite();
            ViewModes.Set(Site.SiteId);
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.IndexJson(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType);
                case "Results": return ResultUtilities.IndexJson(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string Gantt()
        {
            SetSite();
            ViewModes.Set(Site.SiteId);
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.Gantt(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string GanttJson()
        {
            SetSite();
            ViewModes.Set(Site.SiteId);
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.GanttJson(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string BurnDown()
        {
            SetSite();
            ViewModes.Set(Site.SiteId);
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.BurnDown(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string BurnDownJson()
        {
            SetSite();
            ViewModes.Set(Site.SiteId);
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.BurnDownJson(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string BurnDownRecordDetailsJson()
        {
            SetSite();
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities
                    .BurnDownRecordDetails(Site.IssuesSiteSettings());
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string TimeSeries()
        {
            SetSite();
            ViewModes.Set(Site.SiteId);
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.TimeSeries(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType);
                case "Results": return ResultUtilities.TimeSeries(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string TimeSeriesJson()
        {
            SetSite();
            ViewModes.Set(Site.SiteId);
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.TimeSeriesJson(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType);
                case "Results": return ResultUtilities.TimeSeriesJson(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string Kamban()
        {
            SetSite();
            ViewModes.Set(Site.SiteId);
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.Kamban(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType);
                case "Results": return ResultUtilities.Kamban(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string KambanJson()
        {
            SetSite();
            ViewModes.Set(Site.SiteId);
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.KambanJson(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType);
                case "Results": return ResultUtilities.KambanJson(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string New()
        {
            SetSite(siteOnly: true);
            switch (Site.ReferenceType)
            {
                case "Sites": return SiteUtilities.EditorNew(
                    ReferenceId != 0
                        ? Site.PermissionType
                        : Permissions.Types.Manager,
                    ReferenceId);
                case "Issues": return IssueUtilities.EditorNew(Site);
                case "Results": return ResultUtilities.EditorNew(Site);
                case "Wikis": return WikiUtilities.EditorNew(Site);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string NewJson()
        {
            return new ResponseCollection()
                .ReplaceAll("#MainContainer", New())
                .WindowScrollTop()
                .FocusMainForm()
                .ClearFormData()
                .PushState("Edit", Locations.Get("Items", ReferenceId.ToString(), "New"))
                .ToJson();
        }

        public string Editor()
        {
            SetSite();
            switch (ReferenceType)
            {
                case "Sites": return SiteUtilities.Editor(ReferenceId, clearSessions: true);
                case "Issues": return IssueUtilities.Editor(Site, ReferenceId, clearSessions: true);
                case "Results": return ResultUtilities.Editor(Site, ReferenceId, clearSessions: true);
                case "Wikis": return WikiUtilities.Editor(Site, ReferenceId, clearSessions: true);
                default: return new HtmlBuilder().NotFoundTemplate().ToString();
            }
        }

        public string Import()
        {
            SetSite();
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.Import(siteModel: Site);
                case "Results": return ResultUtilities.Import(siteModel: Site);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public ResponseFile Export()
        {
            SetSite();
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.Export(
                    Site.IssuesSiteSettings(),
                    Site.PermissionType,
                    siteModel: Site);
                case "Results": return ResultUtilities.Export(
                    Site.ResultsSiteSettings(),
                    Site.PermissionType,
                    siteModel: Site);
                default: return null;
            }
        }

        public string SearchDropDown()
        {
            SetSite();
            var ss = Site.SiteSettings;
            ss.InheritPermission = ss.SiteId;
            ss.Init();
            var controlId = Forms.Data("DropDownSearchTarget");
            var column = ss.Columns.FirstOrDefault(o =>
                controlId.EndsWith(ss.ReferenceType + "_" + o.ColumnName));
            ss.SetChoiceHash(
                targetColumn: column,
                searchIndexes: Forms.Data("DropDownSearchText").SearchIndexes());
            return new ResponseCollection()
                .ReplaceAll(
                    "#DropDownSearchResults",
                    new HtmlBuilder().Selectable(
                        controlId: "DropDownSearchResults",
                        listItemCollection: column?.EditChoices()))
                .ClearFormData("DropDownSearchResults")
                .ToJson();
        }

        public string SelectSearchDropDown()
        {
            SetSite();
            var ss = Site.SiteSettings;
            ss.InheritPermission = ss.SiteId;
            ss.Init();
            var controlId = Forms.Data("DropDownSearchTarget");
            var column = ss.Columns.FirstOrDefault(o =>
                controlId.EndsWith(ss.ReferenceType + "_" + o.ColumnName));
            ss.SetChoiceHash(
                targetColumn: column,
                searchIndexes: Forms.Data("DropDownSearchText").SearchIndexes());
            var selected = Forms.List("DropDownSearchResults", ';');
            var multiple = Forms.Bool("DropDownSearchMultiple");
            if (multiple)
            {
                return SelectSearchDropDownResponse(controlId, column, selected, multiple);
            }
            else if (selected.Count() != 1)
            {
                return new ResponseCollection()
                    .Message(Messages.SelectOne())
                    .ToJson();
            }
            else
            {
                return SelectSearchDropDownResponse(controlId, column, selected, multiple);
            }
        }

        private static string SelectSearchDropDownResponse(
            string controlId, Column column, List<string> selected, bool multiple)
        {
            var optionCollection = column.EditChoices()?
                .Where(o => selected.Contains(o.Key))
                .ToDictionary(o => o.Key, o => o.Value);
            return optionCollection?.Any() == true
                ? new ResponseCollection()
                    .CloseDialog()
                    .Html("#" + controlId, new HtmlBuilder()
                        .OptionCollection(
                            optionCollection: optionCollection,
                            selectedValue: multiple
                                ? selected.ToJson()
                                : selected.FirstOrDefault(),
                            multiple: multiple))
                    .Invoke("setDropDownSearch")
                    .ToJson()
                : new ResponseCollection()
                    .Message(Messages.NotFound())
                    .ToJson();
        }

        public string GridRows()
        {
            SetSite();
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.GridRows(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType,
                    offset: DataViewGrid.Offset());
                case "Results": return ResultUtilities.GridRows(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType,
                    offset: DataViewGrid.Offset());
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string Create()
        {
            SetSite();
            switch (Site.ReferenceType)
            {
                case "Sites": return SiteUtilities.Create(
                    pt: Site.SiteId != 0
                        ? Site.PermissionType
                        : Permissions.Types.Manager,
                    parentId: Site.SiteId,
                    inheritPermission: Site.InheritPermission);
                case "Issues": return IssueUtilities.Create(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType);
                case "Results": return ResultUtilities.Create(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string Update()
        {
            SetSite();
            switch (ReferenceType)
            {
                case "Sites": return SiteUtilities
                    .Update(Site.SitesSiteSettings(), Site.PermissionType, ReferenceId);
                case "Issues": return IssueUtilities
                    .Update(Site.IssuesSiteSettings(), Site.PermissionType, ReferenceId);
                case "Results": return ResultUtilities
                    .Update(Site.ResultsSiteSettings(), Site.PermissionType, ReferenceId);
                case "Wikis": return WikiUtilities
                    .Update(Site.WikisSiteSettings(), Site.PermissionType, ReferenceId);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string DeleteComment()
        {
            SetSite();
            switch (ReferenceType)
            {
                case "Sites": return SiteUtilities
                    .Update(Site.SitesSiteSettings(), Site.PermissionType, ReferenceId);
                case "Issues": return IssueUtilities
                    .Update(Site.IssuesSiteSettings(), Site.PermissionType, ReferenceId);
                case "Results": return ResultUtilities
                    .Update(Site.ResultsSiteSettings(), Site.PermissionType, ReferenceId);
                case "Wikis": return WikiUtilities
                    .Update(Site.WikisSiteSettings(), Site.PermissionType, ReferenceId);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string Copy()
        {
            SetSite();
            switch (ReferenceType)
            {
                case "Sites": return SiteUtilities.Copy(Site);
                case "Issues": return IssueUtilities.Copy(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType,
                    issueId: ReferenceId);
                case "Results": return ResultUtilities.Copy(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType,
                    resultId: ReferenceId);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string MoveTargets()
        {
            SetSite();
            return new ResponseCollection().Html("#MoveTargets", new HtmlBuilder()
                .OptionCollection(
                    optionCollection: MoveTargets(
                        Rds.ExecuteTable(statements: new SqlStatement(
                            commandText: Def.Sql.MoveTarget,
                            param: Rds.SitesParam()
                                .TenantId(Sessions.TenantId())
                                .ReferenceType(Site.ReferenceType)
                                .Permissions_PermissionType(
                                    Permissions.Types.Update.ToInt().ToString())))
                                        .AsEnumerable()), 
                    selectedValue: Site.SiteId.ToString())).ToJson();
        }

        private Dictionary<string, ControlData> MoveTargets(IEnumerable<DataRow> siteCollection)
        {
            var moveTargets = new Dictionary<string, ControlData>();
            siteCollection
                .Where(o => o["ReferenceType"].ToString() == Site.ReferenceType)
                .ForEach(dataRow =>
                {
                    var current = dataRow;
                    var titles = new List<string>() { current["Title"].ToString() };
                    while(siteCollection.Any(o =>
                        o["SiteId"].ToLong() == current["ParentId"].ToLong()))
                        {
                            current = siteCollection.First(o =>
                                o["SiteId"].ToLong() == current["ParentId"].ToLong());
                            titles.Insert(0, current["Title"].ToString());
                        }
                    moveTargets.Add(
                        dataRow["SiteId"].ToString(),
                        new ControlData(titles.Join(" / ")));
                });
            return moveTargets;
        }

        public string Move()
        {
            SetSite();
            switch (ReferenceType)
            {
                case "Issues": return IssueUtilities.Move(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType,
                    issueId: ReferenceId);
                case "Results": return ResultUtilities.Move(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType,
                    resultId: ReferenceId);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string BulkMove()
        {
            SetSite();
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.BulkMove(
                    Site.IssuesSiteSettings(), Site.PermissionType);
                case "Results": return ResultUtilities.BulkMove(
                    Site.ResultsSiteSettings(), Site.PermissionType);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string Delete()
        {
            SetSite();
            switch (ReferenceType)
            {
                case "Sites": return SiteUtilities.Delete(siteId: ReferenceId);
                case "Issues": return IssueUtilities.Delete(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType,
                    issueId: ReferenceId);
                case "Results": return ResultUtilities.Delete(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType,
                    resultId: ReferenceId);
                case "Wikis": return WikiUtilities.Delete(
                    ss: Site.WikisSiteSettings(),
                    pt: Site.PermissionType,
                    wikiId: ReferenceId);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string BulkDelete()
        {
            SetSite();
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.BulkDelete(
                    Site.PermissionType, Site.IssuesSiteSettings());
                case "Results": return ResultUtilities.BulkDelete(
                    Site.PermissionType, Site.ResultsSiteSettings());
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string Restore(long referenceId)
        {
            ReferenceId = referenceId;
            Get(Sqls.TableTypes.Deleted, where: Rds.ItemsWhere().ReferenceId(ReferenceId));
            SetSite();
            switch (ReferenceType)
            {
                case "Sites": return SiteUtilities.Restore(siteId: ReferenceId);
                case "Issues": return IssueUtilities.Restore(issueId: ReferenceId);
                case "Results": return ResultUtilities.Restore(resultId: ReferenceId);
                case "Wikis": return WikiUtilities.Restore(wikiId: ReferenceId);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string EditSeparateSettings()
        {
            SetSite();
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.EditSeparateSettings(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType,
                    issueId: ReferenceId);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string Separate()
        {
            SetSite();
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.Separate(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType,
                    issueId: ReferenceId);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string Histories()
        {
            SetSite();
            switch (ReferenceType)
            {
                case "Sites": return SiteUtilities.Histories(siteModel: Site);
                case "Issues": return IssueUtilities.Histories(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType,
                    issueId: ReferenceId);
                case "Results": return ResultUtilities.Histories(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType,
                    resultId: ReferenceId);
                case "Wikis": return WikiUtilities.Histories(
                    ss: Site.WikisSiteSettings(),
                    pt: Site.PermissionType,
                    wikiId: ReferenceId);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string History()
        {
            SetSite();
            switch (ReferenceType)
            {
                case "Sites": return SiteUtilities.History(siteModel: Site);
                case "Issues": return IssueUtilities.History(
                    ss: Site.IssuesSiteSettings(),
                    pt: Site.PermissionType,
                    issueId: ReferenceId);
                case "Results": return ResultUtilities.History(
                    ss: Site.ResultsSiteSettings(),
                    pt: Site.PermissionType,
                    resultId: ReferenceId);
                case "Wikis": return WikiUtilities.History(
                    ss: Site.WikisSiteSettings(),
                    pt: Site.PermissionType,
                    wikiId: ReferenceId);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string EditorJson()
        {
            SetSite();
            switch (ReferenceType)
            {
                case "Sites": return SiteUtilities.EditorJson(ReferenceId);
                case "Issues": return IssueUtilities.EditorJson(
                    Site.IssuesSiteSettings(),
                    Site.PermissionType,
                    ReferenceId);
                case "Results": return ResultUtilities.EditorJson(
                    Site.ResultsSiteSettings(),
                    Site.PermissionType,
                    ReferenceId);
                case "Wikis": return WikiUtilities.EditorJson(
                    Site.WikisSiteSettings(),
                    Site.PermissionType,
                    ReferenceId);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string UpdateByKamban()
        {
            SetSite();
            switch (Site.ReferenceType)
            {
                case "Issues": return IssueUtilities.UpdateByKamban(Site);
                case "Results": return ResultUtilities.UpdateByKamban(Site);
                default: return Messages.ResponseNotFound().ToJson();
            }
        }

        public string SynchronizeSummaries()
        {
            SetSite();
            return Site.SynchronizeSummaries();
        }

        public string SynchronizeFormulas()
        {
            SetSite();
            return Site.SynchronizeFormulas();
        }

        private void SetSite(bool siteOnly = false)
        {
            Site = GetSite(siteOnly);
        }

        public SiteModel GetSite(bool siteOnly = false)
        {
            return siteOnly
                ? new SiteModel(ReferenceId)
                : new SiteModel(ReferenceType == "Sites" ? ReferenceId : SiteId);
        }

        private void SetBySession()
        {
        }

        private void Set(DataTable dataTable)
        {
            switch (dataTable.Rows.Count)
            {
                case 1: Set(dataTable.Rows[0]); break;
                case 0: AccessStatus = Databases.AccessStatuses.NotFound; break;
                default: AccessStatus = Databases.AccessStatuses.Overlap; break;
            }
        }

        private void Set(DataRow dataRow)
        {
            AccessStatus = Databases.AccessStatuses.Selected;
            foreach(DataColumn dataColumn in dataRow.Table.Columns)
            {
                var name = dataColumn.ColumnName;
                switch(name)
                {
                    case "ReferenceId": if (dataRow[name] != DBNull.Value) { ReferenceId = dataRow[name].ToLong(); SavedReferenceId = ReferenceId; } break;
                    case "Ver": Ver = dataRow[name].ToInt(); SavedVer = Ver; break;
                    case "ReferenceType": ReferenceType = dataRow[name].ToString(); SavedReferenceType = ReferenceType; break;
                    case "SiteId": SiteId = dataRow[name].ToLong(); SavedSiteId = SiteId; break;
                    case "Title": Title = dataRow[name].ToString(); SavedTitle = Title; break;
                    case "Comments": Comments = dataRow["Comments"].ToString().Deserialize<Comments>() ?? new Comments(); SavedComments = Comments.ToJson(); break;
                    case "Creator": Creator = SiteInfo.User(dataRow.Int(name)); SavedCreator = Creator.Id; break;
                    case "Updator": Updator = SiteInfo.User(dataRow.Int(name)); SavedUpdator = Updator.Id; break;
                    case "CreatedTime": CreatedTime = new Time(dataRow, "CreatedTime"); SavedCreatedTime = CreatedTime.Value; break;
                    case "UpdatedTime": UpdatedTime = new Time(dataRow, "UpdatedTime"); Timestamp = dataRow.Field<DateTime>("UpdatedTime").ToString("yyyy/M/d H:m:s.fff"); SavedUpdatedTime = UpdatedTime.Value; break;
                    case "IsHistory": VerType = dataRow[name].ToBool() ? Versions.VerTypes.History : Versions.VerTypes.Latest; break;
                }
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public ItemModel()
        {
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public ItemModel(long referenceId)
        {
            OnConstructing();
            ReferenceId = referenceId;
            Get();
            OnConstructed();
        }
    }
}
