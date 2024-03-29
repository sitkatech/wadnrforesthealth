using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectRegion : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var regionDisplayString = ViewUtilities.NotFoundString;
                if (DNRUplandRegion is null)
                {
                    var region = HttpRequestStorage.DatabaseEntities.DNRUplandRegions.Find(DNRUplandRegionID);
                    if (region != null)
                    {
                        regionDisplayString = region.DisplayName;
                    }
                }
                else
                {
                    regionDisplayString = DNRUplandRegion.DisplayName;
                }

                var projectDisplayString = ViewUtilities.NotFoundString;
                if (Project is null)
                {
                    var project = HttpRequestStorage.DatabaseEntities.Projects.Find(ProjectID);
                    if (project != null)
                    {
                        projectDisplayString = project.DisplayName;
                    }
                }
                else
                {
                    projectDisplayString = Project.DisplayName;
                }

                return $"{FieldDefinition.DNRUplandRegion.GetFieldDefinitionLabel()}: {regionDisplayString}, {FieldDefinition.Project.GetFieldDefinitionLabel()}: {projectDisplayString}";
            }
        }
    }
}