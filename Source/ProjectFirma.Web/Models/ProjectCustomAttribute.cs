﻿using System.Collections.Generic;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectCustomAttribute : IAuditableEntity, IProjectCustomAttribute
    {
        public string AuditDescriptionString
        {
            get
            {
                var project = Project ?? HttpRequestStorage.DatabaseEntities.Projects.GetProject(ProjectID);
                var projectCustomAttributeType = ProjectCustomAttributeType ??
                                                 HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeTypes
                                                     .GetProjectCustomAttributeType(ProjectCustomAttributeTypeID);
                return
                    $"Custom Attribute (type: {projectCustomAttributeType?.ProjectCustomAttributeTypeName ?? "<Type Not Found>"}, " +
                    $"Project: {project?.ProjectName ?? "<Project Not Found>"})";
            }
        }

        public IEnumerable<IProjectCustomAttributeValue> Values
        {
            get => ProjectCustomAttributeValues;
            set => ProjectCustomAttributeValues = (ICollection<ProjectCustomAttributeValue>)value;
        }
    }
}
