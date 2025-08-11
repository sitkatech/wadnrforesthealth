/*-----------------------------------------------------------------------
<copyright file="ProjectUpdate.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;
using ProjectFirma.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectUpdate : IProject
    {
        public int ProjectTypeID => ProjectUpdateBatch.Project.ProjectTypeID;
        public int EntityID => ProjectUpdateID;
        public string DisplayName => ProjectUpdateBatch.Project.DisplayName;

        public string PercentageMatchFormatted => PercentageMatch.HasValue ? $"{PercentageMatch}%" : String.Empty;

        public decimal GetTotalAmount()
        {
            return ProjectUpdateBatch.ProjectFundSourceAllocationRequestUpdates.Any()
                ? ProjectUpdateBatch.ProjectFundSourceAllocationRequestUpdates.Sum(x => x.TotalAmount.GetValueOrDefault())
                : 0;
        }

        public ProjectUpdate(ProjectUpdateBatch projectUpdateBatch) : this(projectUpdateBatch, projectUpdateBatch.Project.ProjectStage, 
                                                                           projectUpdateBatch.Project.ProjectLocationSimpleType)
        {
            this.FocusAreaID = projectUpdateBatch.Project.FocusAreaID;
            this.ProjectDescription = projectUpdateBatch.Project.ProjectDescription;
            
            var project = projectUpdateBatch.Project;
            LoadUpdateFromProject(project);
            LoadSimpleLocationFromProject(project);
            LoadProgramsFromProject(project);
        }

        public void LoadProgramsFromProject(Project project)
        {
            foreach (var projectProgram in project.ProjectPrograms)
            {
                var projectUpdateProgramTemp = new ProjectUpdateProgram(projectProgram.Program, this.ProjectUpdateBatch);
                this.ProjectUpdateBatch.ProjectUpdatePrograms.Add(projectUpdateProgramTemp);
            }
        }

        public void LoadUpdateFromProject(Project project)
        {
            ProjectDescription = project.ProjectDescription;
            ProjectStageID = project.ProjectStageID;
            PlannedDate = project.PlannedDate;
            ExpirationDate = project.ExpirationDate;
            CompletionDate = project.CompletionDate;
            FocusAreaID = project.FocusAreaID;
            EstimatedTotalCost = project.EstimatedTotalCost;
            PercentageMatch = project.PercentageMatch;
            ProjectFundingSourceNotes = project.ProjectFundingSourceNotes;
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
            project.ExpirationDate = ExpirationDate;
            project.CompletionDate = CompletionDate;
            project.EstimatedTotalCost = EstimatedTotalCost;
            project.ProjectFundingSourceNotes = ProjectFundingSourceNotes;
            project.FocusAreaID = FocusAreaID;
            project.PercentageMatch = PercentageMatch;

        }

        public void CommitSimpleLocationToProject(Project project)
        {
            project.ProjectLocationPoint = ProjectLocationPoint;
            project.ProjectLocationNotes = ProjectLocationNotes;
            project.ProjectLocationSimpleTypeID = ProjectLocationSimpleTypeID;
        }

        public bool HasProjectLocationPoint => ProjectLocationPoint != null;

        public bool HasProjectLocationDetail => AllDetailedLocationsToGeoJsonFeatureCollection().Features.Any();


        public IEnumerable<IProjectLocation> GetProjectLocationDetails()
        {
            return ProjectUpdateBatch.ProjectLocationUpdates.ToList();
        }

        public DbGeometry GetDefaultBoundingBox()
        {
            return ProjectUpdateBatch.Project.GetDefaultBoundingBox();
        }

        public IEnumerable<DNRUplandRegion> GetProjectRegions()
        {
            return ProjectUpdateBatch.ProjectRegionUpdates.Select(x => x.DNRUplandRegion);
        }

        public IEnumerable<County> GetProjectCounties()
        {
            return ProjectUpdateBatch.ProjectCountyUpdates.Select(x => x.County);
        }

        public IEnumerable<PriorityLandscape> GetProjectPriorityLandscapes()
        {
            return ProjectUpdateBatch.ProjectPriorityLandscapeUpdates.Select(x => x.PriorityLandscape);
        }

        public FeatureCollection AllDetailedLocationsToGeoJsonFeatureCollection()
        {
            return ProjectUpdateBatch.ProjectLocationUpdates.ToGeoJsonFeatureCollection();
        }

        public FeatureCollection DetailedLocationsByTypeToGeoJsonFeatureCollection(
            ProjectLocationType projectLocationType)
        {
            return ProjectUpdateBatch.ProjectLocationUpdates.Where(x => x.ProjectLocationTypeID == projectLocationType.ProjectLocationTypeID)
                .ToGeoJsonFeatureCollection();
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

        public Person GetPrimaryContact()
        {
            var primaryContact = this.ProjectUpdateBatch.ProjectPersonUpdates.SingleOrDefault(pp => pp.ProjectPersonRelationshipTypeID == ProjectPersonRelationshipType.PrimaryContact.ProjectPersonRelationshipTypeID);
            if (primaryContact != null)
            {
                return primaryContact.Person;
            }

            return GetPrimaryContactOrganization()?.PrimaryContactPerson;
        }

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
            return PlannedDate?.Year;
        }

        public int? GetCompletionYear()
        {
            return CompletionDate?.Year;
        }
        public string GetCompletionDateFormatted()
        {
            return CompletionDate?.ToShortDateString();
        }

        public string GetExpirationDateFormatted()
        {
            return ExpirationDate?.ToShortDateString();
        }

    }
}
