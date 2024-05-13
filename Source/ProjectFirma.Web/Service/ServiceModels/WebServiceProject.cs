﻿/*-----------------------------------------------------------------------
<copyright file="WebServiceProject.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using System.Runtime.Serialization;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.Models;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Service.ServiceModels
{
    [DataContract]
    public class WebServiceProject : SimpleModelObject
    {
        public WebServiceProject(Project project)
        {
            ProjectID = project.ProjectID;
            ProjectName = project.ProjectName;
            TaxonomyTrunk = project.ProjectType.TaxonomyBranch.TaxonomyTrunk.TaxonomyTrunkName;
            TaxonomyBranch = project.ProjectType.TaxonomyBranch.TaxonomyBranchName;
            ProjectType = project.ProjectType.ProjectTypeName;
            ProjectDescription = project.ProjectDescription;

            PlanningStartDate = project.PlannedDate;
            ImplementationStartDate = project.GetImplementationStartYear();
            EndDate = project.GetCompletionYear();
            Stage = project.ProjectStage.ProjectStageDisplayName;

            Latitude = project.ProjectLocationPointLatitude;
            Longitude = project.ProjectLocationPointLongitude;

            Datum = "WGS84";
            
            ProjectState = project.ProjectLocationStateProvince;
            ProjectDetailUrl = project.GetDetailUrl();
            ProjectFactSheetUrl = project.GetFactSheetUrl();
        }

        [DataMember] public int ProjectID { get; set; }
        [DataMember] public string ProjectName { get; set; }
        [DataMember] public string TaxonomyTrunk { get; set; }
        [DataMember] public string TaxonomyBranch { get; set; }
        [DataMember] public string ProjectType { get; set; }
        [DataMember] public string ProjectDescription { get; set; }

        [DataMember] public DateTime? PlanningStartDate { get; set; }
        [DataMember] public int? ImplementationStartDate { get; set; }
        [DataMember] public int? EndDate { get; set; }        
        [DataMember] public string Stage { get; set; }

        [DataMember] public double? Latitude { get; set; }
        [DataMember] public double? Longitude { get; set; }
        [DataMember] public string Datum { get; set; }
        [DataMember] public string ProjectState { get; set; }

        [DataMember] public string ProjectDetailUrl { get; set; }
        [DataMember] public string ProjectFactSheetUrl { get; set; }

        public static List<WebServiceProject> GetProject(int projectID)
        {
            var project = HttpRequestStorage.DatabaseEntities.Projects.GetProject(projectID);
            return new List<WebServiceProject> {new WebServiceProject(project)};
        }

        public static List<WebServiceProject> GetProjects()
        {
            var projects =
                HttpRequestStorage.DatabaseEntities.Projects
                    .Where(x => x.ProjectStageID != ProjectStage.Cancelled.ProjectStageID)
                    .ToList();                    
            return
                projects
                    .Select(x => new WebServiceProject(x))
                    .OrderBy(x => x.ProjectID)
                    .ToList();
        }

        public static List<WebServiceProject> GetProjectsByOrganization(int organizationID)
        {            
            var projectIDs = HttpRequestStorage.DatabaseEntities.ProjectOrganizations.Where(x => x.OrganizationID == organizationID).Select(x => x.ProjectID).ToList();
            var projects = HttpRequestStorage.DatabaseEntities.Projects.Where(x => projectIDs.Contains(x.ProjectID)).ToList();

            return projects
                .Select(x => new WebServiceProject(x))
                    .OrderBy(x => x.ProjectID)
                    .ToList();
        }
    }

    public class WebServiceProjectGridSpec : GridSpec<WebServiceProject>
    {
        public WebServiceProjectGridSpec()
        {
            Add("ProjectID", x => x.ProjectID, 0);
            Add("ProjectName", x => x.ProjectName, 0);
            Add("TaxonomyTrunk", x => x.TaxonomyTrunk, 0);
            Add("TaxonomyBranch", x => x.TaxonomyBranch, 0);
            Add("ProjectType", x => x.ProjectType, 0);
            Add("Stage", x => x.Stage, 0);
            Add("ProjectDescription", x => x.ProjectDescription, 0);
            Add("PlanningStartDate", x => x.PlanningStartDate, 0);
            Add("ImplementationStartDate", x => x.ImplementationStartDate, 0);
            Add("EndDate", x => x.EndDate, 0);
            Add("Latitude", x => x.Latitude, 0);
            Add("Longitude", x => x.Longitude, 0);
            Add("Datum", x => x.Datum, 0);
            Add("ProjectState", x => x.ProjectState, 0);
            Add("ProjectDetailUrl", x => x.ProjectDetailUrl, 0);
            Add("ProjectFactSheetUrl", x => x.ProjectFactSheetUrl, 0);
        }
    }
}
