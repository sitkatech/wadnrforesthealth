using System;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.GisProjectBulkUpdate;
using ProjectFirma.Web.Views.ProjectCreate;

namespace ProjectFirma.Web.Controllers
{
    public class GisProjectBulkUpdateController : FirmaBaseController
    {
        [HttpGet]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        public PartialViewResult SourceOrganizationSelection()
        {
            var gisSourceOrganizations = HttpRequestStorage.DatabaseEntities.GisUploadSourceOrganizations.ToList();
            var viewData = new SourceOrganizationTypeSelectionViewData(gisSourceOrganizations);
            var viewModel = new SourceOrganizationTypeSelectionViewModel();
            return RazorPartialView<SourceOrganizationTypeSelection, SourceOrganizationTypeSelectionViewData, SourceOrganizationTypeSelectionViewModel>(viewData, viewModel);
        }


        [HttpPost]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult SourceOrganizationSelection(SourceOrganizationTypeSelectionViewModel viewModel)
        {
            var gisSourceOrganizations = HttpRequestStorage.DatabaseEntities.GisUploadSourceOrganizations.ToList();
            var viewData = new SourceOrganizationTypeSelectionViewData(gisSourceOrganizations);

            if (!ModelState.IsValid)
            {
                return RazorPartialView<SourceOrganizationTypeSelection, SourceOrganizationTypeSelectionViewData, SourceOrganizationTypeSelectionViewModel>(viewData, viewModel);
            }

            var gisAttempt = new GisUploadAttempt(viewModel.SourceOrganizationID.Value, CurrentPerson.PersonID, DateTime.Now);

            HttpRequestStorage.DatabaseEntities.GisUploadAttempts.Add(gisAttempt);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            return new ModalDialogFormJsonResult(
                        SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(x => x.InstructionsGisImport(gisAttempt.GisUploadAttemptID)));

        }

        private static GisImportSectionStatus GetGisImportSectionStatus(GisUploadAttempt gisUploadAttempt)
        {
            return new GisImportSectionStatus(gisUploadAttempt);
        }


        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        public ActionResult InstructionsGisImport(int gisUploadAttemptID)
        {
            var firmaPageType = FirmaPageType.ToType(FirmaPageTypeEnum.GisUploadAttemptInstructions);
            var firmaPage = FirmaPage.GetFirmaPageByPageType(firmaPageType);
                var gisUploadAttempt = HttpRequestStorage.DatabaseEntities.GisUploadAttempts.GetGisUploadAttempt(gisUploadAttemptID);
                var gisImportSectionStatus = GetGisImportSectionStatus(gisUploadAttempt);
                var viewData = new InstructionsGisImportViewData(CurrentPerson, gisUploadAttempt, gisImportSectionStatus, firmaPage, false);

                return RazorView<InstructionsGisImport, InstructionsGisImportViewData>(viewData);

        }

    }
}