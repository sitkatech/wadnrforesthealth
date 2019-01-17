/*-----------------------------------------------------------------------
<copyright file="ProjectUpdate.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
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
using System.Data.Entity.Spatial;
using System.Linq;
using ProjectFirma.Web.Common;
using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectUpdate : IProject
    {
        public int EntityID => ProjectUpdateID;
        public string DisplayName => ProjectUpdateBatch.Project.DisplayName;

        public decimal? UnfundedNeed => EstimatedTotalCost - GetSecuredFunding();

        public decimal GetSecuredFunding()
        {
            return ProjectUpdateBatch.ProjectFundingSourceRequestUpdates.Any()
                ? ProjectUpdateBatch.ProjectFundingSourceRequestUpdates.Sum(x => x.SecuredAmount.GetValueOrDefault())
                : 0;
        }

        public ProjectUpdate(ProjectUpdateBatch projectUpdateBatch) : this(projectUpdateBatch, projectUpdateBatch.Project.ProjectStage, projectUpdateBatch.Project.ProjectDescription, projectUpdateBatch.Project.ProjectLocationSimpleType)
        {
            var project = projectUpdateBatch.Project;
            LoadUpdateFromProject(project);
            LoadSimpleLocationFromProject(project);            
        }

        public void LoadUpdateFromProject(Project project)
        {
            ProjectDescription = project.ProjectDescription;
            ProjectStageID = project.ProjectStageID;
            PlannedDate = project.PlannedDate;
            ApprovalStartDate = project.ApprovalStartDate;
            CompletionDate = project.CompletionDate;
            FocusAreaID = project.FocusAreaID;
            EstimatedTotalCost = project.EstimatedTotalCost;
        }

        public void LoadSimpleLocationFromProject(Project project)
        {
            ProjectLocationPoint = project.ProjectLocationPoint;
            ProjectLocationNotes = project.ProjectLocationNotes;
            ProjectLocationSimpleTypeID = project.ProjectLocationSimpleTypeID;
        }

        public void CommitChangesToProject(Project project)
        {
            project.ProjectDescription = ProjectDescription;
            project.ProjectStageID = ProjectStageID;
            project.PlannedDate = PlannedDate;
            project.ApprovalStartDate = ApprovalStartDate;
            project.CompletionDate = CompletionDate;
            project.FocusAreaID = FocusAreaID;
            project.EstimatedTotalCost = EstimatedTotalCost;
            project.PrimaryContactPersonID = PrimaryContactPersonID;
        }

        public void CommitSimpleLocationToProject(Project project)
        {
            project.ProjectLocationPoint = ProjectLocationPoint;
            project.ProjectLocationNotes = ProjectLocationNotes;
            project.ProjectLocationSimpleTypeID = ProjectLocationSimpleTypeID;
        }

        public bool HasProjectLocationPoint => ProjectLocationPoint != null;

        public bool HasProjectLocationDetail => DetailedLocationToGeoJsonFeatureCollection().Features.Any();

        public IEnumerable<IProjectCustomAttribute> ProjectCustomAttributes
        {
            get => ProjectUpdateBatch.ProjectCustomAttributeUpdates;
            set => ProjectUpdateBatch.ProjectCustomAttributeUpdates = (ICollection<ProjectCustomAttributeUpdate>) value;
        }

        public IEnumerable<IProjectLocation> GetProjectLocationDetails()
        {
            return ProjectUpdateBatch.ProjectLocationUpdates.ToList();
        }

        public DbGeometry GetDefaultBoundingBox()
        {
            return ProjectUpdateBatch.Project.GetDefaultBoundingBox();
        }

        public IEnumerable<GeospatialArea> GetProjectGeospatialAreas()
        {
            return ProjectUpdateBatch.ProjectGeospatialAreaUpdates.Select(x => x.GeospatialArea);
        }

        public FeatureCollection DetailedLocationToGeoJsonFeatureCollection()
        {
            return ProjectUpdateBatch.ProjectLocationUpdates.ToGeoJsonFeatureCollection();
        }

        public FeatureCollection SimpleLocationToGeoJsonFeatureCollection(bool addProjectProperties)
        {
            var featureCollection = new FeatureCollection();

            if (ProjectLocationSimpleType == ProjectLocationSimpleType.PointOnMap && ProjectLocationPoint != null)
            {
                featureCollection.Features.Add(DbGeometryToGeoJsonHelper.FromDbGeometry(ProjectLocationPoint));
            }
            return featureCollection;
        }
        
        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var projectUpdate = new ProjectUpdate(projectUpdateBatch);
            HttpRequestStorage.DatabaseEntities.ProjectUpdates.Add(projectUpdate);
        }

        public static void CommitToProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var projectUpdate = new ProjectUpdate(projectUpdateBatch);
            HttpRequestStorage.DatabaseEntities.ProjectUpdates.Add(projectUpdate);
        }

        public Person GetPrimaryContact() => PrimaryContactPerson ?? GetPrimaryContactOrganization()?.PrimaryContactPerson;

        public Organization GetPrimaryContactOrganization()
        {
            return ProjectUpdateBatch.ProjectOrganizationUpdates.SingleOrDefault(x => x.RelationshipType.IsPrimaryContact)?.Organization;
        }

        public string GetPlannedDate()
        {
            return PlannedDate?.ToShortDateString();
        }

        public int? GetImplementationStartYear()
        {
            return ApprovalStartDate?.Year;
        }

        public int? GetCompletionYear()
        {
            return CompletionDate?.Year;
        }
        public string GetCompletionDateFormatted()
        {
            return CompletionDate?.ToShortDateString();
        }

        public string GetApprovalStartDateFormatted()
        {
            return ApprovalStartDate?.ToShortDateString();
        }
    }
}
