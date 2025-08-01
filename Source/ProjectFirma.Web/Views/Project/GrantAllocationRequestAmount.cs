using ProjectFirma.Web.Models;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class GrantAllocationRequestAmount : IGrantAllocationRequestAmount
    {
        public Models.FundSourceAllocation FundSourceAllocation { get; }

        public decimal? TotalAmount { get; }
        public decimal? MatchAmount { get; }
        public decimal? PayAmount { get; }
        public bool IsMatchAndPayRelevant
        {
            get { return true; }
        }
        public string DisplayCssClass;

        public GrantAllocationRequestAmount(Models.FundSourceAllocation fundSourceAllocation, decimal? totalAmount, decimal? matchAmount, decimal? payAmount,string displayCssClass)
        {
            FundSourceAllocation = fundSourceAllocation;
            TotalAmount = totalAmount;
            DisplayCssClass = displayCssClass;
            MatchAmount = matchAmount;
            PayAmount = payAmount;
        }

        public GrantAllocationRequestAmount(IGrantAllocationRequestAmount grantAllocationRequestAmount)
        {
            FundSourceAllocation = grantAllocationRequestAmount.FundSourceAllocation;
            TotalAmount = grantAllocationRequestAmount.TotalAmount;
            MatchAmount = grantAllocationRequestAmount.MatchAmount;
            PayAmount = grantAllocationRequestAmount.PayAmount;
        }

        public static GrantAllocationRequestAmount Clone(IGrantAllocationRequestAmount grantAllocationRequestAmountToDiff, string displayCssClass)
        {
            return new GrantAllocationRequestAmount(grantAllocationRequestAmountToDiff.FundSourceAllocation,
                                                  grantAllocationRequestAmountToDiff.TotalAmount,
                                                  grantAllocationRequestAmountToDiff.MatchAmount,
                                                  grantAllocationRequestAmountToDiff.PayAmount,
                                                  displayCssClass);
        }
    }
}