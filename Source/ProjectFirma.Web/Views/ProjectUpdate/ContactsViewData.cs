using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Shared.ProjectPerson;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class ContactsViewData : ProjectUpdateViewData
    {
        public readonly string RefreshUrl;
        public readonly string DiffUrl;
        public readonly SectionCommentsViewData SectionCommentsViewData;
        public readonly EditPeopleViewData EditPeopleViewData;
        public readonly ProjectPeopleDetailViewData ProjectPeopleDetailViewData;

        public ContactsViewData(Person currentPerson, ProjectUpdateBatch projectUpdateBatch, UpdateStatus updateStatus, EditPeopleViewData editPeopleViewData, ContactsValidationResult organizationsValidationResult, ProjectPeopleDetailViewData projectPeopleDetailViewData) : base(
            currentPerson, projectUpdateBatch, updateStatus, organizationsValidationResult.GetWarningMessages(), ProjectUpdateSection.Contacts.ProjectUpdateSectionDisplayName)
        {
            EditPeopleViewData = editPeopleViewData;
            ProjectPeopleDetailViewData = projectPeopleDetailViewData;
            SectionCommentsViewData =
                new SectionCommentsViewData(projectUpdateBatch.ContactsComment, projectUpdateBatch.IsReturned);
            RefreshUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.RefreshContacts(projectUpdateBatch.Project));
            DiffUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.DiffContacts(projectUpdateBatch.Project));
        }
    }
}