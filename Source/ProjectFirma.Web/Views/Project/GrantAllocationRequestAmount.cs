using ProjectFirma.Web.Models;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class GrantAllocationRequestAmount : IGrantAllocationRequestAmount
    {
        public Models.GrantAllocation GrantAllocation { get; }

        public decimal? TotalAmount { get; }
        public decimal? MatchAmount { get; }
        public decimal? PayAmount { get; }
        public bool IsMatchAndPayRelevant
        {
            get { return true; }
        }
        public string DisplayCssClass;

        public GrantAllocationRequestAmount(Models.GrantAllocation grantAllocation, decimal? totalAmount, decimal? matchAmount, decimal? payAmount,string displayCssClass)
        {
            GrantAllocation = grantAllocation;
            TotalAmount = totalAmount;
            DisplayCssClass = displayCssClass;
            MatchAmount = matchAmount;
            PayAmount = payAmount;
        }

        public GrantAllocationRequestAmount(IGrantAllocationRequestAmount grantAllocationRequestAmount)
        {
            GrantAllocation = grantAllocationRequestAmount.GrantAllocation;
            TotalAmount = grantAllocationRequestAmount.TotalAmount;
            MatchAmount = grantAllocationRequestAmount.MatchAmount;
            PayAmount = grantAllocationRequestAmount.PayAmount;
        }

        public static GrantAllocationRequestAmount Clone(IGrantAllocationRequestAmount grantAllocationRequestAmountToDiff, string displayCssClass)
        {
            return new GrantAllocationRequestAmount(grantAllocationRequestAmountToDiff.GrantAllocation,
                                                  grantAllocationRequestAmountToDiff.TotalAmount,
                                                  grantAllocationRequestAmountToDiff.MatchAmount,
                                                  grantAllocationRequestAmountToDiff.PayAmount,
                                                  displayCssClass);
        }
    }
}