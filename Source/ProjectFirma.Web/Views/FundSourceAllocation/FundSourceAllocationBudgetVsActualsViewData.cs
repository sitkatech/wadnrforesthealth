using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public class FundSourceAllocationBudgetVsActualsViewData : FirmaViewData
    {
        public FundSourceAllocationBudgetVsActualsGridSpec FundSourceAllocationBudgetVsActualsGridSpec { get; }
        public string FundSourceAllocationBudgetVsActualsGridDataUrl { get; }

        public readonly string FundSourceAllocationBudgetVsActualsGridName = "fundSourceAllocationBudgetVsActualGrid";


        public FundSourceAllocationBudgetVsActualsViewData(Person currentPerson, Models.FundSourceAllocation fundSourceAllocation) : base(currentPerson)
        {
            FundSourceAllocationBudgetVsActualsGridSpec = new FundSourceAllocationBudgetVsActualsGridSpec(currentPerson);
            FundSourceAllocationBudgetVsActualsGridDataUrl = SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(gac => gac.FundSourceAllocationBudgetVsActualsGridJsonData(fundSourceAllocation));

        }
    }
}