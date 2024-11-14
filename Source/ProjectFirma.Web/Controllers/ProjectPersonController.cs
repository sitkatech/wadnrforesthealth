using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Shared.ProjectPerson;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectPersonController : FirmaBaseController
    {
        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult EditPeople(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new EditPeopleViewModel(project.ProjectPeople.OrderBy(x => x.Person.FullNameFirstLast).ToList());
            return ViewEditPeople(viewModel, project);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditPeople(ProjectPrimaryKey projectPrimaryKey, EditPeopleViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditPeople(viewModel, project);
            }

            HttpRequestStorage.DatabaseEntities.ProjectPeople.Load();
            var projectPeople = HttpRequestStorage.DatabaseEntities.ProjectPeople.Local;

            viewModel.UpdateModel(project, projectPeople);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditPeople(EditPeopleViewModel viewModel, Project project)
        {
            var allPeople = HttpRequestStorage.DatabaseEntities.People.GetActivePeople();
            if (!allPeople.Contains(CurrentPerson))
            {
                allPeople.Add(CurrentPerson);
            }

            var allRelationshipTypes = ProjectPersonRelationshipType.All;

            var viewData = new EditPeopleViewData(allPeople, allRelationshipTypes, CurrentPerson, project.IsPrivateLandownerImported(), false);
            return RazorPartialView<EditPeople, EditPeopleViewData, EditPeopleViewModel>(viewData, viewModel);
        }
    }
}