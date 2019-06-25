using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Shared;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Models;
using ProjectFirma.Web.Views.GrantAllocationAwardLandownerCostShareLineItem;

namespace ProjectFirma.Web.Controllers
{
    public class GrantAllocationAwardLandownerCostShareLineItemController : FirmaBaseController
    {

        [HttpGet]
        [GrantAllocationAwardLandownerCostShareLineItemViewFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [GrantAllocationAwardLandownerCostShareLineItemViewFeature]
        public GridJsonNetJObjectResult<GrantAllocationAwardLandownerCostShareLineItem> IndexGridJsonData()
        {
            var gridSpec = new LandownerCostShareIndexGridSpec(CurrentPerson);
            var landownerCostShareLineItems = HttpRequestStorage.DatabaseEntities.GrantAllocationAwardLandownerCostShareLineItems.OrderBy(x => x.Project.ProjectName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocationAwardLandownerCostShareLineItem>(landownerCostShareLineItems, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}