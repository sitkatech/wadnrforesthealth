using ProjectFirma.Web.Models;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class GrantAllocationRequestAmount : IGrantAllocationRequestAmount
    {
        public Models.GrantAllocation GrantAllocation { get; }

        //TODO SecuredAmount and UnsecuredAmount are going to be removed
        public decimal? SecuredAmount { get; set; }
        public decimal? UnsecuredAmount { get; set;  }
        public decimal? TotalAmount { get; }
        public string DisplayCssClass;

        public GrantAllocationRequestAmount(Models.GrantAllocation grantAllocation, decimal? securedAmount, decimal? unsecuredAmount,  decimal? totalAmount, string displayCssClass)
        {
            GrantAllocation = grantAllocation;
            SecuredAmount = securedAmount;
            UnsecuredAmount = unsecuredAmount;
            TotalAmount = totalAmount;
            DisplayCssClass = displayCssClass;
        }

        public GrantAllocationRequestAmount(IGrantAllocationRequestAmount grantAllocationRequestAmount)
        {
            GrantAllocation = grantAllocationRequestAmount.GrantAllocation;
            SecuredAmount = grantAllocationRequestAmount.SecuredAmount;
            UnsecuredAmount = grantAllocationRequestAmount.UnsecuredAmount;
            TotalAmount = grantAllocationRequestAmount.TotalAmount;
        }

        public static GrantAllocationRequestAmount Clone(IGrantAllocationRequestAmount grantAllocationRequestAmountToDiff, string displayCssClass)
        {
            return new GrantAllocationRequestAmount(grantAllocationRequestAmountToDiff.GrantAllocation,
                                                  grantAllocationRequestAmountToDiff.SecuredAmount,
                                                  grantAllocationRequestAmountToDiff.UnsecuredAmount,
                                                  grantAllocationRequestAmountToDiff.TotalAmount,
                                                  displayCssClass);
        }
    }
}