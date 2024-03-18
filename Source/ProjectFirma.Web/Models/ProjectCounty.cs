using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectCounty: IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var countyDisplayString = ViewUtilities.NotFoundString;
                if (County is null)
                {
                    var county = HttpRequestStorage.DatabaseEntities.Counties.Find(CountyID);
                    if (county != null)
                    {
                        countyDisplayString = county.DisplayName;
                    }
                }else
                {
                    countyDisplayString = County.DisplayName;
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

                return $"{FieldDefinition.County.GetFieldDefinitionLabel()}: {countyDisplayString}, {FieldDefinition.Project.GetFieldDefinitionLabel()}: {projectDisplayString}";
            }
        }
    }
}