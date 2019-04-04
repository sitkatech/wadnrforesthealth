using ProjectFirma.Web.Models;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class FundingSourceRequestAmount : IFundingSourceRequestAmount
    {
        public Models.FundingSource FundingSource { get; }
        public Models.GrantAllocation GrantAllocation { get; }
        public int FundingSourceID => FundingSource?.FundingSourceID ?? ModelObjectHelpers.NotYetAssignedID;

        public decimal? SecuredAmount { get; set; }
        public decimal? UnsecuredAmount { get; set;  }
        public string DisplayCssClass;

        public FundingSourceRequestAmount(Models.FundingSource fundingSource, Models.GrantAllocation grantAllocation, decimal? securedAmount, decimal? unsecuredAmount, string displayCssClass)
        {
            FundingSource = fundingSource;
            GrantAllocation = grantAllocation;
            SecuredAmount = securedAmount;
            UnsecuredAmount = unsecuredAmount;
            DisplayCssClass = displayCssClass;
        }

        public FundingSourceRequestAmount(IFundingSourceRequestAmount fundingSourceRequestAmount)
        {
            FundingSource = fundingSourceRequestAmount.FundingSource;
            GrantAllocation = fundingSourceRequestAmount.GrantAllocation;
            SecuredAmount = fundingSourceRequestAmount.SecuredAmount;
            UnsecuredAmount = fundingSourceRequestAmount.UnsecuredAmount;
        }

        public static FundingSourceRequestAmount Clone(IFundingSourceRequestAmount fundingSourceRequestAmountToDiff, string displayCssClass)
        {
            return new FundingSourceRequestAmount(fundingSourceRequestAmountToDiff.FundingSource,
                                                  fundingSourceRequestAmountToDiff.GrantAllocation,
                                                  fundingSourceRequestAmountToDiff.SecuredAmount,
                                                  fundingSourceRequestAmountToDiff.UnsecuredAmount,
                                                  displayCssClass);
        }
    }
}