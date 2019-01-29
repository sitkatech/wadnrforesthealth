using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectControls
{
    public class ProjectAttributesViewData
    {
        public IEnumerable<Models.ProjectCustomAttributeType> ProjectCustomAttributeTypes { get; }
        public IList<ProjectCustomAttributeSimple> ProjectCustomAttributeSimples { get; }

        public ProjectAttributesViewData( IEnumerable<Models.ProjectCustomAttributeType> projectCustomAttributeTypes, IList<ProjectCustomAttributeSimple> projectCustomAttributeSimples)
        {
            ProjectCustomAttributeTypes = projectCustomAttributeTypes;
            ProjectCustomAttributeSimples = projectCustomAttributeSimples;
        }
    }
}