/*-----------------------------------------------------------------------
<copyright file="ProjectLocationUpdate.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.DbSpatial;
using Microsoft.SqlServer.Types;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectLocationUpdate : IAuditableEntity, IProjectLocation, IHaveSqlGeometry
    {
        public ProjectLocationUpdate(ProjectUpdateBatch projectUpdateBatch, DbGeometry projectLocationGeometry, string projectLocationNotes) : this(projectUpdateBatch, projectLocationGeometry, ProjectLocationType.Other, string.Empty)//todo: tomk fix this ProjectLocationType and Name to actuals
        {
            ProjectLocationUpdateNotes = projectLocationNotes;
        }

        public string AuditDescriptionString
        {
            get
            {
                return "Shape deleted";
            }
        }

        public DbGeometry ProjectLocationGeometry
        {
            get { return ProjectLocationUpdateGeometry; }
            set { ProjectLocationUpdateGeometry = value; }
        }

        public string ProjectLocationNotes
        {
            get { return ProjectLocationUpdateNotes; }
            set { ProjectLocationUpdateNotes = value; }
        }

        public DbGeometry DbGeometry
        {
            get { return ProjectLocationUpdateGeometry; }
            set { ProjectLocationUpdateGeometry = value; }
        }

        public SqlGeometry SqlGeometry
        {
            get { return ProjectLocationUpdateGeometry.ToSqlGeometry(); }
        }

        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            projectUpdateBatch.ProjectLocationUpdates =
                project.ProjectLocations.Select(
                    projectLocationToClone => new ProjectLocationUpdate(projectUpdateBatch, projectLocationToClone.ProjectLocationGeometry, projectLocationToClone.ProjectLocationNotes)).ToList();
        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectLocation> allProjectLocations)
        {
            var project = projectUpdateBatch.Project;
            var currentProjectLocations = project.ProjectLocations.ToList();
            currentProjectLocations.ForEach(projectLocation =>
            {
                allProjectLocations.Remove(projectLocation);
            });
            currentProjectLocations.Clear();

            if (projectUpdateBatch.ProjectLocationUpdates.Any())
            {
                // Completely rebuild the list
                projectUpdateBatch.ProjectLocationUpdates.ToList().ForEach(x =>
                {
                    var projectLocation = new ProjectLocation(project, x.ProjectLocationUpdateName, x.ProjectLocationUpdateGeometry, x.ProjectLocationType, x.ProjectLocationUpdateNotes);
                    allProjectLocations.Add(projectLocation);
                });
            }
        }
    }
}
