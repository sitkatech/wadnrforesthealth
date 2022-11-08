/*-----------------------------------------------------------------------
<copyright file="ProjectLocation.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Data.Entity.Spatial;
using LtInfo.Common.DbSpatial;
using Microsoft.SqlServer.Types;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectLocation : IAuditableEntity, IProjectLocation, IHaveSqlGeometry
    {
        public ProjectLocation(Project project, string projectlocationName, DbGeometry projectLocationGeometry, ProjectLocationType projectLocationType, string projectLocationNotes)
        {
            ProjectLocationNotes = projectLocationNotes;
            ProjectLocationName = projectlocationName;
            ProjectLocationGeometry = projectLocationGeometry;
            ProjectLocationTypeID = projectLocationType.ProjectLocationTypeID;
            ProjectID = project.PrimaryKey;
        }

        public ProjectLocation(Project project, string projectlocationName, DbGeometry projectLocationGeometry, int projectLocationTypeID, string projectLocationNotes)
        {
            ProjectLocationNotes = projectLocationNotes;
            ProjectLocationName = projectlocationName;
            ProjectLocationGeometry = projectLocationGeometry;
            ProjectLocationTypeID = projectLocationTypeID;
            ProjectID = project.PrimaryKey;
        }

        public ProjectLocation(Project project, int projectLocationID, string projectlocationName, DbGeometry projectLocationGeometry, int projectLocationTypeID, string projectLocationNotes)
        {
            ProjectLocationID = projectLocationID;
            ProjectLocationNotes = projectLocationNotes;
            ProjectLocationName = projectlocationName;
            ProjectLocationGeometry = projectLocationGeometry;
            ProjectLocationTypeID = projectLocationTypeID;
            ProjectID = project.PrimaryKey;
        }

        public string AuditDescriptionString
        {
            get
            {
                return "Shape deleted";
            }
        }

        public DbGeometry DbGeometry
        {
            get { return ProjectLocationGeometry; }
            set { ProjectLocationGeometry = value; }
        }

        public SqlGeometry SqlGeometry
        {
            get { return ProjectLocationGeometry.ToSqlGeometry(); }
        }
    }
}
