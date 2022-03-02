using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ApprovalUtilities.Utilities;
using LtInfo.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Program;
using ProjectFirma.Web.Views.Shared;
using EditViewData = ProjectFirma.Web.Views.Program.EditViewData;
using EditViewModel = ProjectFirma.Web.Views.Program.EditViewModel;
using Index = ProjectFirma.Web.Views.Program.Index;
using IndexViewData = ProjectFirma.Web.Views.Program.IndexViewData;

namespace ProjectFirma.Web.Controllers
{
    public class ProgramController : FirmaBaseController
    {


        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult DeleteProgramDocument(FileResourcePrimaryKey fileResourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(fileResourcePrimaryKey.PrimaryKeyValue);
            return ViewProgramDocument(fileResourcePrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewProgramDocument(FileResource fileResource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this Program Document '{fileResource.OriginalCompleteFileName}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteProgramDocument(FileResourcePrimaryKey fileResourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var document = fileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewProgramDocument(document, viewModel);
            }

            document.ProgramsWhereYouAreTheProgramFileResource.ForEach(x => x.ProgramFileResource = null);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            document.Delete(HttpRequestStorage.DatabaseEntities);
            var message = $"Program Document '{document.OriginalCompleteFileName}' successfully deleted.";
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }


        [ProgramViewFeature]
        public ViewResult Detail(ProgramPrimaryKey programPrimaryKey)
        {
            var program = programPrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, program);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [ProgramViewFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.ProgramsList);
            var viewData = new IndexViewData(CurrentPerson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }


        [ProgramViewFeature]
        public GridJsonNetJObjectResult<Program> IndexGridJsonData()
        {
            var gridSpec = new ProgramGridSpec(CurrentPerson, null);
            var programs = HttpRequestStorage.DatabaseEntities.Programs.ToList().OrderBy(x => x.ProgramName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Program>(programs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [ProgramViewFeature]
        public GridJsonNetJObjectResult<Program> ProgramGridJsonData(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var gridSpec = new ProgramGridSpec(CurrentPerson, organization);
            var programs = organization.Programs.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Program>(programs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult NewProgram(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var viewModel = new Views.Program.EditViewModel { IsActive = true, OrganizationID = organization.OrganizationID};
            return ViewEdit(viewModel, null, organization);
        }


        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewProgram(OrganizationPrimaryKey organizationPrimaryKey, Views.Program.EditViewModel viewModel)
        {
            var organization = organizationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, null, organization);
            }
            
            var program = new Program(organization, true, DateTime.Now, CurrentPerson, false);
            viewModel.UpdateModel(program, CurrentPerson, true);
            HttpRequestStorage.DatabaseEntities.Programs.Add(program);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            if (viewModel.ProgramFileResourceData != null)
            {

                program.ProgramFileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.ProgramFileResourceData, CurrentPerson);
            }

            SetMessageForDisplay($"{FieldDefinition.Program.GetFieldDefinitionLabel()} {program.DisplayName} successfully created.");
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult New()
        {
            var viewModel = new Views.Program.EditViewModel { IsActive = true};
            return ViewEdit(viewModel, null, null);
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(Views.Program.EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, null, null);
            }

            var organizationID = viewModel.OrganizationID.GetValueOrDefault();

            var organization = HttpRequestStorage.DatabaseEntities.Organizations.Single(x =>
                x.OrganizationID == organizationID);

            var program = new Program(organization, true, DateTime.Now, CurrentPerson, false);
            viewModel.UpdateModel(program, CurrentPerson, true);
            HttpRequestStorage.DatabaseEntities.Programs.Add(program);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            if (viewModel.ProgramFileResourceData != null)
            {

                program.ProgramFileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.ProgramFileResourceData, CurrentPerson);
            }
            SetMessageForDisplay($"{FieldDefinition.Program.GetFieldDefinitionLabel()} {program.DisplayName} successfully created.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(Views.Program.EditViewModel viewModel, Person currentPrimaryContactPerson, Organization organization)
        {
            var organizationsAsSelectListItems = HttpRequestStorage.DatabaseEntities.Organizations
                .Where(x => !string.Equals(x.OrganizationName, Organization.OrganizationUnknown))
                .OrderBy(x => x.OrganizationName)
                .ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture),
                    x => x.OrganizationName);
            var activePeople = HttpRequestStorage.DatabaseEntities.People.GetActivePeople().Where(x => x.IsFullUser()).ToList();
            if (currentPrimaryContactPerson != null && !activePeople.Contains(currentPrimaryContactPerson))
            {
                activePeople.Add(currentPrimaryContactPerson);
            }
            var people = activePeople.OrderBy(x => x.FullNameLastFirst).ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                x => x.FullNameFirstLastAndOrg);
            bool isSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(CurrentPerson);
            var viewData = new Views.Program.EditViewData(organizationsAsSelectListItems, people, isSitkaAdmin, organization);
            return RazorPartialView<Views.Program.Edit, Views.Program.EditViewData, Views.Program.EditViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult Edit(ProgramPrimaryKey programPrimaryKey)
        {
            var program = programPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(program);
            return ViewEdit(viewModel, program.ProgramPrimaryContactPerson, null);
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(ProgramPrimaryKey programPrimaryKey, EditViewModel viewModel)
        {
            var program = programPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, program.ProgramPrimaryContactPerson, null);
            }
            viewModel.UpdateModel(program, CurrentPerson, false);
            if (viewModel.ProgramFileResourceData != null)
            {
                var currentAgreementFileResource = program.ProgramFileResource;
                program.ProgramFileResource = null;
                // Delete old Agreement file, if present
                if (currentAgreementFileResource != null)
                {
                    HttpRequestStorage.DatabaseEntities.SaveChanges();
                    HttpRequestStorage.DatabaseEntities.FileResources.DeleteFileResource(currentAgreementFileResource);
                }
                program.ProgramFileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.ProgramFileResourceData, CurrentPerson);
            }
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult DeleteProgram(ProgramPrimaryKey programPrimaryKey)
        {
            var program = programPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(program.ProgramID);
            return ViewDeleteProgram(program, viewModel);
        }

        private PartialViewResult ViewDeleteProgram(Program program, ConfirmDialogFormViewModel viewModel)
        {
            string optionalProjectCountString = program.ProjectPrograms.Any() ? $"will delete {program.ProjectPrograms.Count} Projects and" : string.Empty;
            string projectNumberAndDurationWarning = $"<b>This {optionalProjectCountString} may take several minutes to complete.</b>";
            // For now, only show duration warning when ProjectCount > 0. Request by DAL.
            projectNumberAndDurationWarning = program.ProjectPrograms.Any() ? projectNumberAndDurationWarning : string.Empty;
            var confirmMessage = $"Are you sure you want to delete Program \"{program.ProgramNameDisplay}\" with Parent Organization {program.Organization.DisplayName}? <br/><br/>{projectNumberAndDurationWarning}";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteProgram(ProgramPrimaryKey programPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var program = programPrimaryKey.EntityObject;

            string programName = program.ProgramNameDisplay;
            string parentOrganizationName = program.Organization.DisplayName;
            string projectsDeletedCountString = program.ProjectPrograms.Any() ? $"{program.ProjectPrograms.Count} Projects deleted." : string.Empty;

            if (!ModelState.IsValid)
            {
                return ViewDeleteProgram(program, viewModel);
            }
            var message = $"Program \"{programName}\" with Parent Organization {parentOrganizationName} successfully deleted. {projectsDeletedCountString}";
            program.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);

            return new ModalDialogFormJsonResult();
        }



        private PartialViewResult ViewEditProgramPeople(EditProgramPeopleViewModel viewModel, Person currentPrimaryContactPerson)
        {


            var activePeople = HttpRequestStorage.DatabaseEntities.People.GetActivePeople().Where(x => x.IsFullUser()).ToList();
            if (currentPrimaryContactPerson != null && !activePeople.Contains(currentPrimaryContactPerson))
            {
                activePeople.Add(currentPrimaryContactPerson);
            }
            var people = activePeople.OrderBy(x => x.FullNameLastFirst).ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                x => x.FullNameFirstLastAndOrg);

            var viewData = new EditProgramPeopleViewData(people);
            return RazorPartialView<EditProgramPeople, EditProgramPeopleViewData, EditProgramPeopleViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult EditProgramPeople(ProgramPrimaryKey programPrimaryKey)
        {
            var program = programPrimaryKey.EntityObject;
            var viewModel = new EditProgramPeopleViewModel(program);
            return ViewEditProgramPeople(viewModel, program.ProgramPrimaryContactPerson);
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditProgramPeople(ProgramPrimaryKey programPrimaryKey, EditProgramPeopleViewModel viewModel)
        {

            var program = programPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditProgramPeople(viewModel, program.ProgramPrimaryContactPerson);
            }
            viewModel.UpdateModel(program, CurrentPerson);

            return new ModalDialogFormJsonResult();
        }


    }
}