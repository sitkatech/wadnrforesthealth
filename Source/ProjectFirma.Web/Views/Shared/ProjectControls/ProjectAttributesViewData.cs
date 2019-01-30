using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectControls
{
    public class ProjectAttributesViewData
    {
        public IEnumerable<Models.ProjectCustomAttributeType> ProjectCustomAttributeTypes { get; }
        public IList<ProjectCustomAttributeSimple> ProjectCustomAttributeSimples { get; }
        public bool InDiffMode { get; }

        public ProjectAttributesViewData( IEnumerable<Models.ProjectCustomAttributeType> projectCustomAttributeTypes, IList<ProjectCustomAttributeSimple> projectCustomAttributeSimples, bool inDiffMode)
        {
            ProjectCustomAttributeTypes = projectCustomAttributeTypes;
            ProjectCustomAttributeSimples = projectCustomAttributeSimples;
            InDiffMode = inDiffMode;
        }

        public string GetClassStringLabel()
        {
            return InDiffMode ? "col-sm-4" : "col-sm-3";
        }

        public string GetClassStringDisplay()
        {
            return InDiffMode ? "col-sm-8" : "col-sm-9";
        }
    }
}