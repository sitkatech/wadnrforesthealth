using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.ExcelWorkbookUtilities;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Models.ApiJson;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Agreement;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class AgreementController : FirmaBaseController
    {
        [HttpGet]
        [AgreementCreateFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditAgreementViewModel();
            return ViewEdit(viewModel, EditAgreementType.NewAgreement);
        }

        [HttpPost]
        [AgreementCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditAgreementViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, EditAgreementType.NewAgreement);
            }
           
            var agreementOrganization = HttpRequestStorage.DatabaseEntities.Organizations.Single(g => g.OrganizationID == viewModel.OrganizationID);
            var agreementType = HttpRequestStorage.DatabaseEntities.AgreementTypes.Single(at => at.AgreementTypeID == viewModel.AgreementTypeID);
            var agreement = Agreement.CreateNewBlank(agreementType, agreementOrganization);
            viewModel.UpdateModel(agreement, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditAgreementViewModel viewModel, EditAgreementType editAgreementType)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();
            var agreementTypes = HttpRequestStorage.DatabaseEntities.AgreementTypes.OrderBy(x => x.AgreementTypeName);
            var agreementStatuses = HttpRequestStorage.DatabaseEntities.AgreementStatuses;
            var grants = HttpRequestStorage.DatabaseEntities.Grants.OrderBy(x => x.FundSourceName);

            var viewData = new EditAgreementViewData(editAgreementType,
                organizations,
                grants,
                agreementTypes,
                agreementStatuses
            );
            return RazorPartialView<EditAgreement, EditAgreementViewData, EditAgreementViewModel>(viewData, viewModel);
        }

        [AgreementsViewFullListFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullAgreementList);
            var agreements = HttpRequestStorage.DatabaseEntities.Agreements.ToList();
            var viewData = new AgreementIndexViewData(CurrentPerson, firmaPage, agreements.Any(x => x.AgreementFileResourceID.HasValue));
            return RazorView<AgreementIndex, AgreementIndexViewData>(viewData);
        }

        [AgreementsViewFeature]
        public ViewResult AgreementDetail(AgreementPrimaryKey agreementPrimaryKey)
        {
            var userHasAgreementAdminPermissions = new AgreementEditAsAdminFeature().HasPermissionByPerson(CurrentPerson);
            var agreement = agreementPrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, agreement, userHasAgreementAdminPermissions);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [AgreementsViewFullListFeature]
        public GridJsonNetJObjectResult<Agreement> AgreementGridJsonData()
        {
            var agreements = HttpRequestStorage.DatabaseEntities.Agreements.ToList();
            var gridSpec = new AgreementGridSpec(CurrentPerson, agreements.Any(x => x.AgreementFileResourceID.HasValue), true, true);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Agreement>(agreements, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [AgreementsViewFullListFeature]
        public ExcelResult AgreementsExcelDownload()
        {
            var agreements = HttpRequestStorage.DatabaseEntities.Agreements.ToList();
            var workbookTitle = FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized();
            return AgreementsExcelDownloadImpl(agreements, workbookTitle);
        }

        public static ExcelResult AgreementsExcelDownloadImpl(List<Agreement> agreements,  string workbookTitle)
        {
            var workSheets = new List<IExcelWorkbookSheetDescriptor>();

            // Agreements
            var agreementExcelSpec = new AgreementExcelSpec();
            var agreementWorkSheet = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()}", agreementExcelSpec, agreements);
            workSheets.Add(agreementWorkSheet);

            // Overall excel file
            var wbm = new ExcelWorkbookMaker(workSheets);
            var excelWorkbook = wbm.ToXLWorkbook();

            return new ExcelResult(excelWorkbook, workbookTitle);
        }


        [HttpGet]
        [AgreementDeleteFeature]
        public PartialViewResult DeleteAgreement(AgreementPrimaryKey agreementPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(agreementPrimaryKey.PrimaryKeyValue);
            return ViewDeleteAgreement(agreementPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteAgreement(Agreement agreement, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.Agreement.GetFieldDefinitionLabel()} '{agreement.AgreementTitle}'?";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [AgreementDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteAgreement(AgreementPrimaryKey agreementPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var agreement = agreementPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteAgreement(agreement, viewModel);
            }

            var message = $"{FieldDefinition.Agreement.GetFieldDefinitionLabel()} \"{agreement.AgreementTitle}\" successfully deleted.";

            agreement.DeleteFull(HttpRequestStorage.DatabaseEntities);

            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [AgreementDeleteFeature]
        public PartialViewResult DeleteAgreementPerson(AgreementPersonPrimaryKey agreementPersonPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(agreementPersonPrimaryKey.PrimaryKeyValue);
            return ViewDeleteAgreementPerson(agreementPersonPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteAgreementPerson(AgreementPerson agreementPerson, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to remove this {FieldDefinition.Agreement.GetFieldDefinitionLabel()} Contact '{agreementPerson.Person.FullNameFirstLastAndOrg}' from this {FieldDefinition.Agreement.GetFieldDefinitionLabel()}?";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [AgreementDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteAgreementPerson(AgreementPersonPrimaryKey agreementPersonPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var agreementPerson = agreementPersonPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteAgreementPerson(agreementPerson, viewModel);
            }

            var message = $"{FieldDefinition.Agreement.GetFieldDefinitionLabel()} Contact '{agreementPerson.Person.FullNameFirstLastAndOrg}' successfully removed from this {FieldDefinition.Agreement.GetFieldDefinitionLabel()}.";

            agreementPerson.DeleteFull(HttpRequestStorage.DatabaseEntities);

            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [AgreementEditAsAdminFeature]
        public PartialViewResult EditAgreementPerson(AgreementPersonPrimaryKey agreementPersonPrimaryKey)
        {
            var agreementPerson = agreementPersonPrimaryKey.EntityObject;
            var viewModel = new EditAgreementPersonViewModel(agreementPerson);
            return ViewEditAgreementPerson(viewModel);
        }

        [HttpPost]
        [AgreementEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditAgreementPerson(AgreementPersonPrimaryKey agreementPersonPrimaryKey, EditAgreementPersonViewModel viewModel)
        {
            var agreementPerson = agreementPersonPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditAgreementPerson(viewModel);
            }
            viewModel.UpdateModel(agreementPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditAgreementPerson(EditAgreementPersonViewModel viewModel)
        {
            var agreementPersonRoles = AgreementPersonRole.All.ToSelectListWithEmptyFirstRow(k => k.AgreementPersonRoleID.ToString(), v => v.AgreementPersonRoleDisplayName);
            var allPeople = HttpRequestStorage.DatabaseEntities.People.GetActivePeople();
            if (!allPeople.Contains(CurrentPerson))
            {
                allPeople.Add(CurrentPerson);
            }

            var contacts = allPeople.OrderBy(x => x.LastName)
                .ToSelectListWithEmptyFirstRow(k => k.PersonID.ToString(), v => v.FullNameFirstLastAndOrg);

            var viewData = new EditAgreementPersonViewData(agreementPersonRoles, contacts);
            return RazorPartialView<EditAgreementPerson, EditAgreementPersonViewData, EditAgreementPersonViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [AgreementEditAsAdminFeature]
        public PartialViewResult NewAgreementPerson(AgreementPrimaryKey agreementPrimaryKey)
        {
            var agreementID = agreementPrimaryKey.EntityObject.AgreementID;
            var viewModel = new EditAgreementPersonViewModel(agreementID);
            return ViewEditAgreementPerson(viewModel);
        }

        [HttpPost]
        [AgreementEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewAgreementPerson(AgreementPrimaryKey agreementPrimaryKey, EditAgreementPersonViewModel viewModel)
        {
            var agreementID = agreementPrimaryKey.EntityObject.AgreementID;
            if (!ModelState.IsValid)
            {
                return ViewEditAgreementPerson(viewModel);
            }

            var agreementPerson = new AgreementPerson(agreementID, viewModel.PersonID,
                viewModel.AgreementPersonRoleID);
            viewModel.UpdateModel(agreementPerson);
            HttpRequestStorage.DatabaseEntities.AgreementPeople.Add(agreementPerson);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"Agreement Contact '{agreementPerson.Person.FullNameFirstLastAndOrg}' successfully added to this agreement.");

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [AgreementEditAsAdminFeature]
        public PartialViewResult EditAgreementGrantAllocations(AgreementPrimaryKey agreementPrimaryKey)
        {
            var agreement = agreementPrimaryKey.EntityObject;
            var viewModel = new EditAgreementGrantAllocationsViewModel(agreement);
            return ViewEditAgreementGrantAllocations(viewModel);
        }

        private PartialViewResult ViewEditAgreementGrantAllocations(EditAgreementGrantAllocationsViewModel viewModel)
        {

            var viewData = new EditAgreementGrantAllocationsViewData();
            return RazorPartialView<EditAgreementGrantAllocations, EditAgreementGrantAllocationsViewData, EditAgreementGrantAllocationsViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [AgreementEditAsAdminFeature]
        public PartialViewResult EditAgreementGrantAllocationRelationships(AgreementPrimaryKey agreementPrimaryKey)
        {
            var agreementId = agreementPrimaryKey.EntityObject.AgreementID;
            var agreement = HttpRequestStorage.DatabaseEntities.Agreements.FirstOrDefault(ag => ag.AgreementID == agreementId);
            Check.EnsureNotNull(agreement);

            var viewModel = new EditAgreementGrantAllocationsViewModel(agreement);
            return ViewEditAgreementGrantAllocations(viewModel);
        }

        [HttpPost]
        [AgreementEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditAgreementGrantAllocationRelationships(AgreementPrimaryKey agreementPrimaryKey, EditAgreementGrantAllocationsViewModel viewModel)
        {
            // Find relevant agreement
            var agreement = agreementPrimaryKey.EntityObject;
            Check.EnsureNotNull(agreement);

            if (!ModelState.IsValid)
            {
                return ViewEditAgreementGrantAllocations(viewModel);
            }
            var grantAllocationsCurrentlyOnAgreement = agreement.AgreementGrantAllocations.Select(x => x.GrantAllocationID).ToList();
            var grantAllocationsFromPost = viewModel.GrantAllocationJsons.Select(x => x.GrantAllocationID).ToList();
            var countAdded = grantAllocationsFromPost.Except(grantAllocationsCurrentlyOnAgreement).Count();
            var countDeleted = grantAllocationsCurrentlyOnAgreement.Except(grantAllocationsFromPost).Count();
             
            viewModel.UpdateModel(agreement);

            HttpRequestStorage.DatabaseEntities.SaveChanges();
            var finalMessage = MakeAddDeleteSuccessMessage(countAdded, countDeleted, agreement, FieldDefinition.GrantAllocation.FieldDefinitionDisplayName, FieldDefinition.GrantAllocation.GetFieldDefinitionLabelPluralized());
            SetMessageForDisplay(finalMessage);
            return new ModalDialogFormJsonResult();
        }

        private static string MakeAddDeleteSuccessMessage(int countAdded, int countDeleted, Agreement agreement, string singularTypeName, string pluralTypeName)
        {
            var addedTypeString = countAdded == 1 ? singularTypeName : pluralTypeName;
            var deletedTypeString = countDeleted == 1 ? singularTypeName : pluralTypeName;
            var addedMessage = countAdded == 0
                ? string.Empty
                : $"{countAdded} {addedTypeString} successfully added to Agreement \"{agreement.AgreementTitle}\".";
            var deletedMessage = countDeleted == 0
                ? string.Empty
                : $"{countDeleted} {deletedTypeString} successfully removed from Agreement \"{agreement.AgreementTitle}\".";

            var finalMessage = $"{addedMessage} {System.Environment.NewLine} {deletedMessage}";
            return finalMessage;
        }

        [AgreementsViewFeature]
        public GridJsonNetJObjectResult<AgreementPerson> AgreementPersonGridJsonData(AgreementPrimaryKey agreementPrimaryKey)
        {
            var agreementID = agreementPrimaryKey.EntityObject.AgreementID;
            var gridSpec = new AgreementPersonGridSpec(CurrentPerson);
            var agreement = HttpRequestStorage.DatabaseEntities.Agreements.FirstOrDefault(x => x.AgreementID == agreementID);
            var agreementPeople = agreement.AgreementPeople.OrderBy(x => x.Person.LastName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<AgreementPerson>(agreementPeople, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [AgreementEditAsAdminFeature]
        public PartialViewResult Edit(AgreementPrimaryKey agreementPrimaryKey)
        {
            var agreement = agreementPrimaryKey.EntityObject;
            var viewModel = new EditAgreementViewModel(agreement);
            return ViewEdit(viewModel, EditAgreementType.ExistingAgreement);
        }

        [HttpPost]
        [AgreementEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(AgreementPrimaryKey agreementPrimaryKey, EditAgreementViewModel viewModel)
        {
            var agreement = agreementPrimaryKey.EntityObject;
            Check.Require(agreement.AgreementID == viewModel.AgreementID, "URL AgreementID does not match Form AgreementID. Should not happen.");
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, EditAgreementType.ExistingAgreement);
            }
            viewModel.UpdateModel(agreement, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }


        #region WADNR Grant JSON API

        [AgreementsViewJsonApiFeature]
        public JsonNetJArrayResult AgreementJsonApi()
        {
            var agreements = HttpRequestStorage.DatabaseEntities.Agreements.ToList(); 
            var jsonApiAgreements = AgreementApiJson.MakeAgreementApiJsonsFromAgreements(agreements, false);
            return new JsonNetJArrayResult(jsonApiAgreements);
        }

        #endregion

        [HttpGet]
        [AgreementEditAsAdminFeature]
        public PartialViewResult EditAgreementProjects(AgreementPrimaryKey agreementPrimaryKey)
        {
            var agreement = agreementPrimaryKey.EntityObject;
            Check.EnsureNotNull(agreement);

            var viewModel = new EditAgreementProjectsViewModel(agreement);
            return ViewEditAgreementProjects(viewModel);
        }

        [HttpPost]
        [AgreementEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditAgreementProjects(AgreementPrimaryKey agreementPrimaryKey, EditAgreementProjectsViewModel viewModel)
        {
            var agreement = agreementPrimaryKey.EntityObject;
            Check.EnsureNotNull(agreement);

            if (!ModelState.IsValid)
            {
                return ViewEditAgreementProjects(viewModel);
            }
            var projectsCurrentlyOnAgreement = agreement.AgreementProjects.Select(x => x.ProjectID).ToList();
            var projectsFromPost = viewModel.ProjectJsons.Select(x => x.ProjectID).ToList();
            var countAdded = projectsFromPost.Except(projectsCurrentlyOnAgreement).Count();
            var countDeleted = projectsCurrentlyOnAgreement.Except(projectsFromPost).Count();

            viewModel.UpdateModel(agreement);

            HttpRequestStorage.DatabaseEntities.SaveChanges();
            var finalMessage = MakeAddDeleteSuccessMessage(countAdded, countDeleted, agreement, FieldDefinition.Project.GetFieldDefinitionLabel(), FieldDefinition.Project.GetFieldDefinitionLabelPluralized());
            SetMessageForDisplay(finalMessage);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditAgreementProjects(EditAgreementProjectsViewModel viewModel)
        {
            var viewData = new EditAgreementProjectsViewData();
            return RazorPartialView<EditAgreementProjects, EditAgreementProjectsViewData, EditAgreementProjectsViewModel>(viewData, viewModel);
        }
    }
}