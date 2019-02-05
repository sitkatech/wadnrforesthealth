using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectLocationTypeJson
    {
        public ProjectLocationTypeJson(int projectLocationTypeId, string projectLocationTypeName, string projectLocationTypeDisplayName)
        {
            ProjectLocationTypeID = projectLocationTypeId;
            ProjectLocationTypeName = projectLocationTypeName;
            ProjectLocationTypeDisplayName = projectLocationTypeDisplayName;
        }

        public ProjectLocationTypeJson(ProjectLocationType projectLocationType)
        {
            ProjectLocationTypeID = projectLocationType.ProjectLocationTypeID;
            ProjectLocationTypeName = projectLocationType.ProjectLocationTypeName;
            ProjectLocationTypeDisplayName = projectLocationType.ProjectLocationTypeDisplayName;
        }

        public int ProjectLocationTypeID { get; }
        public string ProjectLocationTypeName { get; }
        public string ProjectLocationTypeDisplayName { get; }
    }

    public partial class ProjectLocationType
    {
        public static List<ProjectLocationTypeJson> GetAllProjectLocationTypeJsons()
        {
            return ProjectLocationType.All.Select(x => new ProjectLocationTypeJson(x)).ToList();
        }
    }
}