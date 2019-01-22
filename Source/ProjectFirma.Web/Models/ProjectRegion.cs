using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectRegion : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var region = Region != null ? Region.DisplayName : ViewUtilities.NotFoundString;
                var project = Project != null ? Project.DisplayName : ViewUtilities.NotFoundString;
                return $"Region: {region}, {FieldDefinition.Project.GetFieldDefinitionLabel()} Update: {project}";
            }
        }
    }
}