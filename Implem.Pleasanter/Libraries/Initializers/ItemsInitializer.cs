﻿using Implem.DefinitionAccessor;
using Implem.Libraries.DataSources.SqlServer;
using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.DataSources;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Settings;
using Implem.Pleasanter.Models;
using System.Data;
using System.Linq;
namespace Implem.Pleasanter.Libraries.Initializers
{
    public static class ItemsInitializer
    {
        public static void Initialize()
        {
            if (ItemsIsEmpty())
            {
                new SiteCollection().ForEach(siteModel =>
                {
                    if (siteModel.SiteSettings != null)
                    {
                        Rds.ExecuteNonQuery(
                            connectionString: Parameters.Rds.OwnerConnectionString,
                            statements: new SqlStatement[]
                            {
                                Rds.IdentityInsertItems(on: true),
                                Rds.InsertItems(
                                    param: Rds.ItemsParam()
                                        .ReferenceId(siteModel.SiteId)
                                        .ReferenceType("Sites")
                                        .SiteId(siteModel.SiteId)
                                        .Title(siteModel.Title.Value)),
                                Rds.IdentityInsertItems(on: false)
                            });
                    }
                });
                new SiteCollection(tableType: Sqls.TableTypes.Deleted).ForEach(siteModel =>
                {
                    if (siteModel.SiteSettings != null)
                    {
                        Rds.ExecuteNonQuery(
                            statements: new SqlStatement[]
                            {
                                Rds.InsertItems(
                                    tableType: Sqls.TableTypes.Deleted,
                                    param: Rds.ItemsParam()
                                        .ReferenceId(siteModel.SiteId)
                                        .Ver(siteModel.Ver)
                                        .ReferenceType("Sites")
                                        .SiteId(siteModel.SiteId)
                                        .Title(siteModel.Title.Value))
                            });
                    }
                });
                new SiteCollection(tableType: Sqls.TableTypes.History).ForEach(siteModel =>
                {
                    if (siteModel.SiteSettings != null)
                    {
                        Rds.ExecuteNonQuery(
                            statements: new SqlStatement[]
                            {
                                Rds.InsertItems(
                                    tableType: Sqls.TableTypes.History,
                                    param: Rds.ItemsParam()
                                        .ReferenceId(siteModel.SiteId)
                                        .Ver(siteModel.Ver)
                                        .ReferenceType("Sites")
                                        .SiteId(siteModel.SiteId)
                                        .Title(siteModel.Title.Value))
                            });
                    }
                });
                Rds.ExecuteTable(statements: Rds.SelectIssues(
                    tableType: Sqls.TableTypes.Normal,
                    column: Rds.IssuesColumn()
                        .SiteId()
                        .IssueId()
                        .Ver())).AsEnumerable().ForEach(dataRow =>
                    {
                        var ss = new SiteModel().Get(where:
                            Rds.SitesWhere().SiteId(dataRow["SiteId"].ToLong()))?
                                .IssuesSiteSettings();
                        var issueModel = new IssueModel(ss).Get(
                            tableType: Sqls.TableTypes.Normal,
                            where: Rds.IssuesWhere()
                                .SiteId(dataRow["SiteId"].ToLong())
                                .IssueId(dataRow["IssueId"].ToLong())
                                .Ver(dataRow["Ver"].ToInt()));
                        if (ss != null &&
                            issueModel.AccessStatus == Databases.AccessStatuses.Selected)
                        {
                            Rds.ExecuteNonQuery(
                                connectionString: Parameters.Rds.OwnerConnectionString,
                                statements: new SqlStatement[]
                                {
                                    Rds.IdentityInsertItems(on: true),
                                    Rds.InsertItems(
                                        param: Rds.ItemsParam()
                                            .ReferenceId(issueModel.IssueId)
                                            .ReferenceType("Issues")
                                            .SiteId(issueModel.SiteId)
                                            .Title(IssueUtilities.TitleDisplayValue(
                                                ss, issueModel))),
                                    Rds.IdentityInsertItems(on: false)
                                });
                        }
                    });
                Rds.ExecuteTable(statements: Rds.SelectIssues(
                    tableType: Sqls.TableTypes.Deleted,
                    column: Rds.IssuesColumn()
                        .SiteId()
                        .IssueId()
                        .Ver())).AsEnumerable().ForEach(dataRow =>
                    {
                        var ss = new SiteModel().Get(where:
                            Rds.SitesWhere().SiteId(dataRow["SiteId"].ToLong()))?
                                .IssuesSiteSettings();
                        var issueModel = new IssueModel(ss).Get(
                            tableType: Sqls.TableTypes.Deleted,
                            where: Rds.IssuesWhere()
                                .SiteId(dataRow["SiteId"].ToLong())
                                .IssueId(dataRow["IssueId"].ToLong())
                                .Ver(dataRow["Ver"].ToInt()));
                        if (ss != null &&
                            issueModel.AccessStatus == Databases.AccessStatuses.Selected)
                        {
                            Rds.ExecuteNonQuery(
                                statements: new SqlStatement[]
                                {
                                    Rds.InsertItems(
                                        tableType: Sqls.TableTypes.Deleted,
                                        param: Rds.ItemsParam()
                                            .ReferenceId(issueModel.IssueId)
                                            .Ver(issueModel.Ver)
                                            .ReferenceType("Issues")
                                            .SiteId(issueModel.SiteId)
                                            .Title(IssueUtilities.TitleDisplayValue(
                                                ss, issueModel)))
                                });
                        }
                    });
                Rds.ExecuteTable(statements: Rds.SelectResults(
                    tableType: Sqls.TableTypes.Normal,
                    column: Rds.ResultsColumn()
                        .SiteId()
                        .ResultId()
                        .Ver())).AsEnumerable().ForEach(dataRow =>
                    {
                        var ss = new SiteModel().Get(where:
                            Rds.SitesWhere().SiteId(dataRow["SiteId"].ToLong()))?
                                .ResultsSiteSettings();
                        var resultModel = new ResultModel(ss).Get(
                            tableType: Sqls.TableTypes.Normal,
                            where: Rds.ResultsWhere()
                                .SiteId(dataRow["SiteId"].ToLong())
                                .ResultId(dataRow["ResultId"].ToLong())
                                .Ver(dataRow["Ver"].ToInt()));
                        if (ss != null &&
                            resultModel.AccessStatus == Databases.AccessStatuses.Selected)
                        {
                            Rds.ExecuteNonQuery(
                                connectionString: Parameters.Rds.OwnerConnectionString,
                                statements: new SqlStatement[]
                                {
                                    Rds.IdentityInsertItems(on: true),
                                    Rds.InsertItems(
                                        param: Rds.ItemsParam()
                                            .ReferenceId(resultModel.ResultId)
                                            .ReferenceType("Results")
                                            .SiteId(resultModel.SiteId)
                                            .Title(ResultUtilities.TitleDisplayValue(
                                                ss, resultModel))),
                                    Rds.IdentityInsertItems(on: false)
                                });
                        }
                    });
                Rds.ExecuteTable(statements: Rds.SelectResults(
                    tableType: Sqls.TableTypes.Deleted,
                    column: Rds.ResultsColumn()
                        .SiteId()
                        .ResultId()
                        .Ver())).AsEnumerable().ForEach(dataRow =>
                    {
                        var ss = new SiteModel().Get(where:
                            Rds.SitesWhere().SiteId(dataRow["SiteId"].ToLong()))?
                                .ResultsSiteSettings();
                        var resultModel = new ResultModel(ss).Get(
                            tableType: Sqls.TableTypes.Deleted,
                            where: Rds.ResultsWhere()
                                .SiteId(dataRow["SiteId"].ToLong())
                                .ResultId(dataRow["ResultId"].ToLong())
                                .Ver(dataRow["Ver"].ToInt()));
                        if (ss != null &&
                            resultModel.AccessStatus == Databases.AccessStatuses.Selected)
                        {
                            Rds.ExecuteNonQuery(
                                statements: new SqlStatement[]
                                {
                                    Rds.InsertItems(
                                        tableType: Sqls.TableTypes.Deleted,
                                        param: Rds.ItemsParam()
                                            .ReferenceId(resultModel.ResultId)
                                            .Ver(resultModel.Ver)
                                            .ReferenceType("Results")
                                            .SiteId(resultModel.SiteId)
                                            .Title(ResultUtilities.TitleDisplayValue(
                                                ss, resultModel)))
                                });
                        }
                    });
                Rds.ExecuteTable(statements: Rds.SelectWikis(
                    tableType: Sqls.TableTypes.Normal,
                    column: Rds.WikisColumn()
                        .SiteId()
                        .WikiId()
                        .Ver())).AsEnumerable().ForEach(dataRow =>
                    {
                        var ss = new SiteModel().Get(where:
                            Rds.SitesWhere().SiteId(dataRow["SiteId"].ToLong()))?
                                .WikisSiteSettings();
                        var wikiModel = new WikiModel(ss).Get(
                            tableType: Sqls.TableTypes.Normal,
                            where: Rds.WikisWhere()
                                .SiteId(dataRow["SiteId"].ToLong())
                                .WikiId(dataRow["WikiId"].ToLong())
                                .Ver(dataRow["Ver"].ToInt()));
                        if (ss != null &&
                            wikiModel.AccessStatus == Databases.AccessStatuses.Selected)
                        {
                            Rds.ExecuteNonQuery(
                                connectionString: Parameters.Rds.OwnerConnectionString,
                                statements: new SqlStatement[]
                                {
                                    Rds.IdentityInsertItems(on: true),
                                    Rds.InsertItems(
                                        param: Rds.ItemsParam()
                                            .ReferenceId(wikiModel.WikiId)
                                            .ReferenceType("Wikis")
                                            .SiteId(wikiModel.SiteId)
                                            .Title(WikiUtilities.TitleDisplayValue(
                                                ss, wikiModel))),
                                    Rds.IdentityInsertItems(on: false)
                                });
                        }
                    });
                Rds.ExecuteTable(statements: Rds.SelectWikis(
                    tableType: Sqls.TableTypes.Deleted,
                    column: Rds.WikisColumn()
                        .SiteId()
                        .WikiId()
                        .Ver())).AsEnumerable().ForEach(dataRow =>
                    {
                        var ss = new SiteModel().Get(where:
                            Rds.SitesWhere().SiteId(dataRow["SiteId"].ToLong()))?
                                .WikisSiteSettings();
                        var wikiModel = new WikiModel(ss).Get(
                            tableType: Sqls.TableTypes.Deleted,
                            where: Rds.WikisWhere()
                                .SiteId(dataRow["SiteId"].ToLong())
                                .WikiId(dataRow["WikiId"].ToLong())
                                .Ver(dataRow["Ver"].ToInt()));
                        if (ss != null &&
                            wikiModel.AccessStatus == Databases.AccessStatuses.Selected)
                        {
                            Rds.ExecuteNonQuery(
                                statements: new SqlStatement[]
                                {
                                    Rds.InsertItems(
                                        tableType: Sqls.TableTypes.Deleted,
                                        param: Rds.ItemsParam()
                                            .ReferenceId(wikiModel.WikiId)
                                            .Ver(wikiModel.Ver)
                                            .ReferenceType("Wikis")
                                            .SiteId(wikiModel.SiteId)
                                            .Title(WikiUtilities.TitleDisplayValue(
                                                ss, wikiModel)))
                                });
                        }
                    });
            }
        }

        private static bool ItemsIsEmpty()
        {
            return 
                Rds.ExecuteScalar_int(statements:
                    Rds.SelectItems(
                        tableType: Sqls.TableTypes.NormalAndHistory,
                        column: Rds.ItemsColumn().ItemsCount())) == 0 &&
                Rds.ExecuteScalar_int(statements:
                    Rds.SelectItems(
                        tableType: Sqls.TableTypes.Deleted,
                        column: Rds.ItemsColumn().ItemsCount())) == 0;
        }
    }
}
