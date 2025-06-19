/*-----------------------------------------------------------------------
<copyright file="MultiTenantHelpers.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.Spatial;
using System.Linq;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Common
{
    public static class MultiTenantHelpers
    {
        private static readonly EnglishPluralizationService PluralizationService = new EnglishPluralizationService();

        public static string GetTaxonomySystemName()
        {
            return FieldDefinition.TaxonomySystemName.GetFieldDefinitionLabel();
        }

        public static string GetProjectTypeDisplayNameForProject()
        {
            return FieldDefinition.ProjectTypeDisplayNameForProject.GetFieldDefinitionLabel();
        }

        public static string GetTenantDisplayName()
        {
            return "Washington Dept. of Natural Resources";
        }

        public static string GetTenantName()
        {
            return "WashingtonDepartmentOfNaturalResources";
        }

        public static string GetToolDisplayName()
        {
            return "Forest Health Tracker";
        }

        public static string GetTenantSquareLogoUrl()
        {
            return GetSystemAttribute().SquareLogoFileResource != null
                ? GetSystemAttribute().SquareLogoFileResource.FileResourceUrl
                : "/Content/img/ProjectFirma_Logo_Square.png";
        }

        public static string GetTenantBannerLogoUrl()
        {
            return GetSystemAttribute().BannerLogoFileResource != null
                ? GetSystemAttribute().BannerLogoFileResource.FileResourceUrl
                : "/Content/img/ProjectFirma_Logo_2016_FNL.width-600.png";
        }

        public static DbGeometry GetDefaultBoundingBox()
        {
            return GetSystemAttribute().DefaultBoundingBox;
        }

        private static SystemAttribute GetSystemAttribute()
        {
            return HttpRequestStorage.DatabaseEntities.SystemAttributes.Single();
        }

        public static int GetMinimumYear()
        {
            return GetSystemAttribute().MinimumYear;
        }

        public static string GetTenantRecaptchaPrivateKey()
        {
            return GetSystemAttribute().RecaptchaPrivateKey;
        }

        public static string GetTenantRecaptchaPublicKey()
        {
            return GetSystemAttribute().RecaptchaPublicKey;
        }

        public static string GetSocrataAppToken()
        {
            return GetSystemAttribute().SocrataAppToken;
        }

        public static List<ITaxonomyTier> GetTopLevelTaxonomyTiers()
        {
            var taxonomyLevel = GetTaxonomyLevel();
            if (taxonomyLevel == TaxonomyLevel.Trunk)
            {
                return new List<ITaxonomyTier>(HttpRequestStorage.DatabaseEntities.TaxonomyTrunks.ToList());
            }

            if (taxonomyLevel == TaxonomyLevel.Branch)
            {
                return new List<ITaxonomyTier>(HttpRequestStorage.DatabaseEntities.TaxonomyBranches.ToList());
            }

            return new List<ITaxonomyTier>();
        }

        public static TaxonomyLevel GetTaxonomyLevel()
        {
            return GetSystemAttribute().TaxonomyLevel;
        }

        public static bool IsTaxonomyLevelTrunk()
        {
            return GetTaxonomyLevel() == TaxonomyLevel.Trunk;
        }

        public static bool IsTaxonomyLevelBranch()
        {
            return GetTaxonomyLevel() == TaxonomyLevel.Branch;
        }

        public static bool IsTaxonomyLevelLeaf()
        {
            return GetTaxonomyLevel() == TaxonomyLevel.Leaf;
        }

        public static bool HasCanStewardProjectsOrganizationRelationship()
        {
            return GetCanStewardProjectsOrganizationRelationship() != null;
        }

        public static RelationshipType GetCanStewardProjectsOrganizationRelationship()
        {
            return HttpRequestStorage.DatabaseEntities.RelationshipTypes.SingleOrDefault(x => x.CanStewardProjects);
        }

        public static RelationshipType GetRelationshipTypeToReportInAccomplishmentsDashboard()
        {
            return HttpRequestStorage.DatabaseEntities.RelationshipTypes.SingleOrDefault(x =>
                x.ReportInAccomplishmentsDashboard);
        }

        public static RelationshipType GetIsPrimaryContactOrganizationRelationship()
        {
            return HttpRequestStorage.DatabaseEntities.RelationshipTypes.SingleOrDefault(x => x.IsPrimaryContact);
        }

        public static bool ShowApplicationsToThePublic()
        {
            return GetSystemAttribute().ShowApplicationsToThePublic;
        }

        public static bool ShowLeadImplementerLogoOnFactSheet()
        {
            return GetSystemAttribute().ShowLeadImplementerLogoOnFactSheet;
        }

        public static List<ClassificationSystem> GetClassificationSystems()
        {
            return HttpRequestStorage.DatabaseEntities.ClassificationSystems.ToList();
        }

        public static List<CustomPage> GetAllCustomPages()
        {
            return HttpRequestStorage.DatabaseEntities.CustomPages.ToList();
        }

        public static List<CustomPage> GetCustomPagesByNavigationSection(CustomPageNavigationSectionEnum customPageNavigationSectionEnumValue)
        {
            return HttpRequestStorage.DatabaseEntities.CustomPages.Where(x => x.CustomPageNavigationSectionID == (int)customPageNavigationSectionEnumValue).ToList();
        }

        public static ProjectUpdateConfiguration GetProjectUpdateConfiguration()
        {
            return HttpRequestStorage.DatabaseEntities.ProjectUpdateConfigurations.Single();
        }

        public static DateTime GetStartDayOfReportingYear()
        {
            return new DateTime(1990, 1, 1);
        }

        public static bool UseFiscalYears()
        {
            return false;
        }

        public static string FormatReportingYear(int reportingYear)
        {
            if (UseFiscalYears())
            {
                return $"FY{reportingYear}";
            }
            return reportingYear.ToString();
        }


        public static ProjectStewardshipAreaType GetProjectStewardshipAreaType()
        {
            return GetSystemAttribute().ProjectStewardshipAreaType;
        }
    }
}
