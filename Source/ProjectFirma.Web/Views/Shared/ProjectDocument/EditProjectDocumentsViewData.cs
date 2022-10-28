using LtInfo.Common.Mvc;
using ProjectFirma.Web.Views.Agreement;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjectFirma.Web.Views.Shared.ProjectDocument
{
    public class EditProjectDocumentsViewData
    {
        public IEnumerable<SelectListItem> ProjectDocumentTypesList { get; set; }

        public EditProjectDocumentsViewData(IEnumerable<Models.ProjectDocumentType> projectDocumentTypesList)
        {
            ProjectDocumentTypesList =
                projectDocumentTypesList.ToSelectListWithEmptyFirstRow( x=>x.ProjectDocumentTypeID.ToString(),  x=> x.ProjectDocumentTypeDisplayName);
        }
    }
}
