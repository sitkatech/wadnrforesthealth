/*-----------------------------------------------------------------------
<copyright file="OrganizationModelExtensions.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using ProjectFirma.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.GeoJson;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Models
{
    public static class OrganizationModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(t => t.DeleteOrganization(UrlTemplate.Parameter1Int)));
        public const int WadnrID = 4704;

        public static string GetDeleteUrl(this Organization organization)
        {
            return DeleteUrlTemplate.ParameterReplace(organization.OrganizationID);
        }

        public static HtmlString GetDisplayNameAsUrl(this Organization organization)
        {          
            return organization != null ? UrlTemplate.MakeHrefString(organization.GetDetailUrl(), organization.DisplayName) : new HtmlString(null);
        }

        public static string GetDisplayNameAsAgGridLink(this Organization organization)
        {

            return (organization != null ? new HtmlLinkObject(organization.DisplayName, organization.GetDetailUrl()) : new HtmlLinkObject(string.Empty,string.Empty)).ToJsonObjectForAgGrid();
        }

        public static HtmlString GetDisplayNameAsUrlBlankTarget(this Organization organization)
        {
            return organization != null ? UrlTemplate.MakeHrefStringBlankTarget(organization.GetDetailUrl(), organization.DisplayName) : new HtmlString(null);
        }

        public static HtmlString GetDisplayNameWithoutAbbreviationAsUrl(this Organization organization)
        {
            return organization != null
                ? UrlTemplate.MakeHrefString(organization.GetDetailUrl(), organization.DisplayNameWithoutAbbreviation)
                : new HtmlString(null);
        }

        public static string GetDisplayNameWithoutAbbreviationAsAgGridLinkJson(this Organization organization)
        {
            return organization != null
                ? new HtmlLinkObject(organization.DisplayNameWithoutAbbreviation, organization.GetDetailUrl()).ToJsonObjectForAgGrid()
                : new HtmlLinkObject(string.Empty, string.Empty).ToJsonObjectForAgGrid();
        }

        public static HtmlString GetShortNameAsUrl(this Organization organization)
        {          
            return organization != null ? UrlTemplate.MakeHrefString(organization.GetDetailUrl(), organization.OrganizationShortName ?? organization.OrganizationName) : new HtmlString(null);
        }
        public static string GetShortNameAsAgGridLinkJson(this Organization organization)
        {
            return (organization != null ? new HtmlLinkObject(organization.OrganizationShortName ?? organization.OrganizationName,organization.GetDetailUrl() ) : new HtmlLinkObject(string.Empty,string.Empty)).ToJsonObjectForAgGrid();
        }

        public static readonly UrlTemplate<int> SummaryUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Organization organization)
        {
            return organization == null ? "" : SummaryUrlTemplate.ParameterReplace(organization.OrganizationID);
        }

        public static List<LayerGeoJson> GetBoundaryLayerGeoJson(this IEnumerable<Organization> organizations)
        {
            var organizationsToShow =
                organizations?.Where(x => x.OrganizationBoundary != null && x.OrganizationType != null)
                    .ToList();
            if (organizationsToShow == null || !organizationsToShow.Any())
            {
                return new List<LayerGeoJson>();
            }


            return organizationsToShow.GroupBy(x => x.OrganizationType, (organizationType, organizationList) =>
            {
                return new LayerGeoJson(
                    $"{organizationType.OrganizationTypeName} {FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}",
                    new FeatureCollection(organizationList.Select(organization =>
                    {
                        var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(organization.OrganizationBoundary);
                        feature.Properties.Add("Hover Name", UrlTemplate.MakeHrefString(organization.GetDetailUrl(), organization.OrganizationShortName).ToHtmlString());
                        feature.Properties.Add(organizationType.OrganizationTypeName, UrlTemplate.MakeHrefString(organization.GetDetailUrl(), organization.OrganizationName).ToHtmlString());
                        return feature;
                    }).ToList()),
                    organizationType.LegendColor, 1,
                    organizationType.ShowOnProjectMaps ? LayerInitialVisibility.Show : LayerInitialVisibility.Hide);
            }).ToList();
        }

        public static List<Project> GetAllAssociatedProjectsForOrgVisibleToUser(this Organization organization, Person currentPerson)
        {
            var allAsociatedProjects = organization.GrantAllocations.SelectMany(x => x.ProjectGrantAllocationRequests).Select(x => x.Project)
                .Union(organization.GrantAllocations.SelectMany(x => x.ProjectGrantAllocationExpenditures)
                    .Select(x => x.Project))
                .Union(organization.ProjectOrganizations.Select(x => x.Project))
                .ToList();
            var projectViewFeature = new ProjectViewFeature();
            return allAsociatedProjects.Where(p => projectViewFeature.HasPermission(currentPerson, p).HasPermission).ToList();
        }

        public static  List<Project> GetAllActiveProjectsAndProposals(this Organization organization, Person currentPerson)
        {
            return organization.GetAllAssociatedProjectsForOrgVisibleToUser(currentPerson).GetActiveProjectsAndProposalsVisibleToUser(currentPerson);
        }

        public static List<Project> GetAllActiveProjects(this Organization organization, Person currentPerson)
        {
            return organization.GetAllAssociatedProjectsForOrgVisibleToUser(currentPerson).GetActiveProjectsVisibleToUser(currentPerson);
        }

        public static List<Project> GetProposalsVisibleToUser(this Organization organization, Person currentPerson)
        {
            return organization.GetAllAssociatedProjectsForOrgVisibleToUser(currentPerson).GetProposalsVisibleToUser(currentPerson);
        }

        public static List<Project> GetAllPendingProjects(this Organization organization, Person currentPerson)
        {
            return organization.GetAllAssociatedProjectsForOrgVisibleToUser(currentPerson).GetPendingProjectsVisibleToUser(currentPerson);
        }

        public static List<Project> GetAllActiveProjectsAndProposalsWhereOrganizationIsStewardOrPrimaryContact(this Organization organization, Person currentPerson)
        {
            var allActiveProjectsAndProposals = organization.GetAllAssociatedProjectsForOrgVisibleToUser(currentPerson).GetActiveProjectsAndProposalsVisibleToUser(currentPerson);

            if (MultiTenantHelpers.HasCanStewardProjectsOrganizationRelationship())
            {
                return allActiveProjectsAndProposals.Where(x => x.GetCanStewardProjectsOrganization() == organization).ToList();
            }

            return allActiveProjectsAndProposals.Where(x => x.GetPrimaryContactOrganization() == organization).ToList();
        }
    }
}
