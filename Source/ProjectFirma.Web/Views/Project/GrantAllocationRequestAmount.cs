using ProjectFirma.Web.Models;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class GrantAllocationRequestAmount : IGrantAllocationRequestAmount
    {
        public Models.GrantAllocation GrantAllocation { get; }

        public decimal? SecuredAmount { get; set; }
        public decimal? UnsecuredAmount { get; set;  }
        public string DisplayCssClass;

        public GrantAllocationRequestAmount(Models.GrantAllocation grantAllocation, decimal? securedAmount, decimal? unsecuredAmount, string displayCssClass)
        {
            GrantAllocation = grantAllocation;
            SecuredAmount = securedAmount;
            UnsecuredAmount = unsecuredAmount;
            DisplayCssClass = displayCssClass;
        }

        public GrantAllocationRequestAmount(IGrantAllocationRequestAmount grantAllocationRequestAmount)
        {
            GrantAllocation = grantAllocationRequestAmount.GrantAllocation;
            SecuredAmount = grantAllocationRequestAmount.SecuredAmount;
            UnsecuredAmount = grantAllocationRequestAmount.UnsecuredAmount;
        }

        public static GrantAllocationRequestAmount Clone(IGrantAllocationRequestAmount grantAllocationRequestAmountToDiff, string displayCssClass)
        {
            return new GrantAllocationRequestAmount(grantAllocationRequestAmountToDiff.GrantAllocation,
                                                  grantAllocationRequestAmountToDiff.SecuredAmount,
                                                  grantAllocationRequestAmountToDiff.UnsecuredAmount,
                                                  displayCssClass);
        }
    }
}