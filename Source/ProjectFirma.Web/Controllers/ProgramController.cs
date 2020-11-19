using System.Linq;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Program;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class ProgramController : FirmaBaseController
    {


        [ProgramViewFeature]
        public ViewResult Detail(ProgramPrimaryKey programPrimaryKey)
        {
            var program = programPrimaryKey.EntityObject;
            var viewData = new Views.Program.DetailViewData(CurrentPerson, program);
            return RazorView<Views.Program.Detail, Views.Program.DetailViewData>(viewData);
        }

        [ProgramViewFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.OrganizationsList);
            var viewData = new IndexViewData(CurrentPerson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }


        [ProgramViewFeature]
        public GridJsonNetJObjectResult<Program> IndexGridJsonData()
        {
            var hasDeleteProgramPermission = new ProgramManageFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new IndexGridSpec(CurrentPerson, hasDeleteProgramPermission);
            var programs = HttpRequestStorage.DatabaseEntities.Programs.ToList().OrderBy(x => x.ProgramName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Program>(programs, gridSpec);
            return gridJsonNetJObjectResult;
        }


        [HttpGet]
        [OrganizationManageFeature]
        public PartialViewResult DeleteProgram(ProgramPrimaryKey programPrimaryKey)
        {
            var program = programPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(program.ProgramID);
            return ViewDeleteProgram(program, viewModel);
        }

        private PartialViewResult ViewDeleteProgram(Program program, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "Are you sure you want to delete this Program?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [OrganizationManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteProgram(ProgramPrimaryKey programPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var program = programPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteProgram(program, viewModel);
            }
            var message = $"Program \"{program.ProgramName}\" successfully deleted.";
            program.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);

            return new ModalDialogFormJsonResult();
        }
    }
}