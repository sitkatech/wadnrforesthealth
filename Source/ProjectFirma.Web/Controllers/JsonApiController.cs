using System.Web.Mvc;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.JsonApiManagement;

namespace ProjectFirma.Web.Controllers
{
    public class JsonApiManagementController : FirmaBaseController
    {
        [ViewJsonApiLandingPageFeature]
        public ViewResult JsonApiLandingPage()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.TagList);
            var viewData = new Views.JsonApiManagement.JsonApiLandingPageViewData(CurrentPerson, firmaPage);
            return RazorView<JsonApiLandingPage, JsonApiLandingPageViewData>(viewData);
        }
    }
}