using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectCounty: IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var county = County != null ? County.DisplayName : ViewUtilities.NotFoundString;
                var project = Project != null ? Project.DisplayName : ViewUtilities.NotFoundString;
                return $"County: {county}, {FieldDefinition.Project.GetFieldDefinitionLabel()} Update: {project}";
            }
        }
    }
}