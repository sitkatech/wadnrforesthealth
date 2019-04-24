using ProjectFirma.Web.Models;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class GrantAllocationRequestAmount : IGrantAllocationRequestAmount
    {
        public Models.GrantAllocation GrantAllocation { get; }

        public decimal? TotalAmount { get; }
        public string DisplayCssClass;

        public GrantAllocationRequestAmount(Models.GrantAllocation grantAllocation, decimal? totalAmount, string displayCssClass)
        {
            GrantAllocation = grantAllocation;
            TotalAmount = totalAmount;
            DisplayCssClass = displayCssClass;
        }

        public GrantAllocationRequestAmount(IGrantAllocationRequestAmount grantAllocationRequestAmount)
        {
            GrantAllocation = grantAllocationRequestAmount.GrantAllocation;
            TotalAmount = grantAllocationRequestAmount.TotalAmount;
        }

        public static GrantAllocationRequestAmount Clone(IGrantAllocationRequestAmount grantAllocationRequestAmountToDiff, string displayCssClass)
        {
            return new GrantAllocationRequestAmount(grantAllocationRequestAmountToDiff.GrantAllocation,
                                                  grantAllocationRequestAmountToDiff.TotalAmount,
                                                  displayCssClass);
        }
    }
}