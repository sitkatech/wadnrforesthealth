using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.Shared.ProjectDocument
{
    public class NewProjectDocumentViewData
    {
        public IEnumerable<SelectListItem> ProjectDocumentTypesList { get; set; }

        public IEnumerable<SelectListItem> ProjectDocumentTypesOtherList { get; set; }

        public NewProjectDocumentViewData(IEnumerable<Models.ProjectDocumentType> projectDocumentTypesList)
        {
            ProjectDocumentTypesList =
                projectDocumentTypesList.Where(x => !x.ProjectDocumentParentTypeID.HasValue)
                    .ToSelectListWithEmptyFirstRow(x => x.ProjectDocumentTypeName);

            ProjectDocumentTypesOtherList = projectDocumentTypesList
                .Where(x => x.ProjectDocumentParentTypeID == 7)
                .ToSelectListWithEmptyFirstRow(x => x.ProjectDocumentTypeName);
        }
    }
}
