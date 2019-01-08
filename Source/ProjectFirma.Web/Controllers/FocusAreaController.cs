using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.FocusArea;
using Index = ProjectFirma.Web.Views.FocusArea.Index;
using IndexGridSpec = ProjectFirma.Web.Views.FocusArea.IndexGridSpec;
using IndexViewData = ProjectFirma.Web.Views.FocusArea.IndexViewData;

namespace ProjectFirma.Web.Controllers
{
    public class FocusAreaController : FirmaBaseController
    {
        [FocusAreaViewFeature]
        public ViewResult Index()
        {
            var layerGeoJsons = new List<LayerGeoJson>();
            
            var mapInitJson = new MapInitJson("focusAreaIndex", 10, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox());

            var firmaPage = Models.FirmaPage.GetFirmaPageByPageType(FirmaPageType.FocusAreasList);
            var viewData = new IndexViewData(CurrentPerson, mapInitJson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [FocusAreaViewFeature]
        public GridJsonNetJObjectResult<FocusArea> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var focusAreas = HttpRequestStorage.DatabaseEntities.FocusAreas.OrderBy(x => x.FocusAreaName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FocusArea>(focusAreas, gridSpec);
            return gridJsonNetJObjectResult;
        }

        public ViewResult Detail(FocusAreaPrimaryKey focusAreaPrimaryKey)
        {
            throw new System.NotImplementedException();
        }

        [HttpGet]
        [FocusAreaManageFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditViewModel();
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [FocusAreaManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            var focusArea = new FocusArea(string.Empty, ModelObjectHelpers.NotYetAssignedID);
            viewModel.UpdateModel(focusArea);
            HttpRequestStorage.DatabaseEntities.AllFocusAreas.Add(focusArea);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"FocusArea {focusArea.DisplayName} successfully created.");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var focusAreaStatusAsSelectListItems =
                FocusAreaStatus.All.ToSelectListWithEmptyFirstRow(v => v.FocusAreaStatusID.ToString(), m => m.FocusAreaStatusDisplayName);
            var isSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(CurrentPerson);
            var viewData = new EditViewData(focusAreaStatusAsSelectListItems, isSitkaAdmin);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

    }
}