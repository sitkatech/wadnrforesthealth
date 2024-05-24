/*-----------------------------------------------------------------------
<copyright file="FirmaWebConfiguration.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web.Configuration;
using LtInfo.Common;

namespace ProjectFirma.Web.Common
{
    public class FirmaWebConfiguration : SitkaWebConfiguration
    {
        public static readonly int MaximumAllowedUploadFileSize = Int32.Parse(SitkaConfiguration.GetRequiredAppSetting("MaximumAllowedUploadFileSize"));
        public static readonly int MaximumAllowedUploadImageSize = Int32.Parse(SitkaConfiguration.GetRequiredAppSetting("MaximumAllowedUploadImageSize"));
        public static readonly string DatabaseName = SitkaConfiguration.GetRequiredAppSetting("DatabaseName");
        public static readonly string DatabaseConnectionString = SitkaConfiguration.GetRequiredAppSetting("DatabaseConnectionString");
        public static readonly string RecaptchaValidatorUrl = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("RecaptchaValidatorUrl");
        public static readonly string SitkaSupportEmail = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("SitkaSupportEmail");
        public static readonly string WadnrSupportEmail = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("WadnrSupportEmail");
        public static readonly string WebsiteDisplayName = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("WebsiteDisplayName");
        public static readonly string DoNotReplyEmail = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("DoNotReplyEmail");
        public static readonly string Ogr2OgrExecutable = SitkaConfiguration.GetRequiredAppSetting("Ogr2OgrExecutable");
        public static readonly string OgrInfoExecutable = SitkaConfiguration.GetRequiredAppSetting("OgrInfoExecutable");
        public static readonly int DefaultSupportPersonID = Int32.Parse(SitkaConfiguration.GetRequiredAppSetting("DefaultSupportPersonID"));

        public static readonly TimeSpan HttpRuntimeExecutionTimeout = ((HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime")).ExecutionTimeout;

        public static readonly FirmaEnvironment FirmaEnvironment = FirmaEnvironment.MakeFirmaEnvironment(SitkaConfiguration.GetRequiredAppSetting("FirmaEnvironment"));

        public static readonly Uri SAWEndPoint = new Uri(SitkaConfiguration.GetRequiredAppSetting("SAWEndPoint"));
        public static readonly Uri ADFSEndPoint = new Uri(SitkaConfiguration.GetRequiredAppSetting("ADFSEndPoint"));

        public static readonly string CanonicalHostName = SitkaConfiguration.GetRequiredAppSetting("CanonicalHostName");

        public static readonly string WebMapServiceUrl = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("WebMapServiceUrl");

        public static readonly string ProjectCodeJsonApiBaseUrl = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("ProjectCodeJsonApiBaseUrl");
        public static readonly string ProgramIndexJsonApiBaseUrl = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("ProgramIndexJsonApiBaseUrl");
        public static readonly string VendorJsonApiBaseUrl = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("VendorJsonApiBaseUrl");
        public static readonly string GrantExpendituresJsonApiBaseUrl = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("GrantExpendituresJsonApiBaseUrl");
        public static readonly string DataImportAuthUrl = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("DataImportAuthUrl");
        public static readonly string DataImportAuthUsername = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("DataImportAuthUsername");
        public static readonly string DataImportAuthPassword = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("DataImportAuthPassword");

        public static readonly string ShapeFileRootDirectory = SitkaConfiguration.GetRequiredAppSetting("ShapeFileRootDirectory");

        public static readonly string LastLoadDateUrl = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("LastLoadDateUrl");
        public static readonly string ArcGisAuthUrl = SitkaConfiguration.GetRequiredAppSetting("ArcGisAuthUrl");
        public static readonly string ArcGisLoaDataEasternUrl = SitkaConfiguration.GetRequiredAppSetting("ArcGisLoaDataEasternUrl");
        public static readonly string ArcGisLoaDataWesternUrl = SitkaConfiguration.GetRequiredAppSetting("ArcGisLoaDataWesternUrl");
        public static readonly string ArcGisClientId = SitkaConfiguration.GetRequiredAppSetting("ArcGisClientId");
        public static readonly string ArcGisClientSecret = SitkaConfiguration.GetRequiredAppSetting("ArcGisClientSecret");

        public static readonly string ArcGisUsfsDataUrl = SitkaConfiguration.GetRequiredAppSetting("ArcGisUsfsDataUrl");

        public static readonly string MapBoxApiKey = SitkaConfiguration.GetRequiredAppSetting("MapBoxApiKey");

        public static readonly int GeoSpatialReferenceID = 4326;

        public static string GetCanonicalHost(string hostName, bool useApproximateMatch)
        {
            return CanonicalHostName;
        }

        public static List<string> GetWmsLayerNames()
        {
            return new List<string> { GetDNRUplandRegionWmsLayerName(), GetPriorityLandscapeWmsLayerName() };
        }

        public static string GetDNRUplandRegionWmsLayerName()
        {
            return "WADNRForestHealth:DNRUplandRegion"; //todo: move region layer name to web config
        }

        public static string GetPriorityLandscapeWmsLayerName()
        {
            return "WADNRForestHealth:PriorityLandscape";//todo: move priorityLandscape layer name to web config
        }

        public static string GetAllProjectLocationsSimpleWmsLayerName()
        {
            return "WADNRForestHealth:ProjectLocationSimple";//todo: move layer name to web config
        }

        public static string GetAllProjectLocationsDetailedWmsLayerName()
        {
            return "WADNRForestHealth:ProjectLocationDetailed";//todo: move layer name to web config
        }

        public static string GetAllProjectTreatmentAreasWmsLayerName()
        {
            return "WADNRForestHealth:ProjectTreatmentArea";//todo: move layer name to web config
        }

        public static string GetCountyWmsLayerName()
        {

            return "WADNRForestHealth:County";//todo: move layer name to web config
        }

        public static string GetWashingtonLegislativeDistrictWmsLayerName()
        {
            return "WADNRForestHealth:WashingtonLegislativeDistrict";//todo: move layer name to web config
        }

        public static string GetPriorityRankingWmsLayerName()
        {
            return "WADNRForestHealth:PriorityRanking";//todo: move layer name to web config
        }

        public static string GetDualBenefitPrioritizationWmsLayerName()
        {
            return "WADNRForestHealth:DualBenefitPrioritization";//todo: move layer name to web config
        }

        public static string GetFindYourForesterWmsLayerName()
        {
            return "WADNRForestHealth:FindYourForester"; //todo: move layer name to web config
        }
    }
}
