using ProjectFirma.Web.Models;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class FundSourceAllocationRequestAmount : IFundSourceAllocationRequestAmount
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

        public FundSourceAllocationRequestAmount(Models.FundSourceAllocation fundSourceAllocation, decimal? totalAmount, decimal? matchAmount, decimal? payAmount,string displayCssClass)
        {
            FundSourceAllocation = fundSourceAllocation;
            TotalAmount = totalAmount;
            DisplayCssClass = displayCssClass;
            MatchAmount = matchAmount;
            PayAmount = payAmount;
        }

        public FundSourceAllocationRequestAmount(IFundSourceAllocationRequestAmount fundSourceAllocationRequestAmount)
        {
            FundSourceAllocation = fundSourceAllocationRequestAmount.FundSourceAllocation;
            TotalAmount = fundSourceAllocationRequestAmount.TotalAmount;
            MatchAmount = fundSourceAllocationRequestAmount.MatchAmount;
            PayAmount = fundSourceAllocationRequestAmount.PayAmount;
        }

        public static FundSourceAllocationRequestAmount Clone(IFundSourceAllocationRequestAmount fundSourceAllocationRequestAmountToDiff, string displayCssClass)
        {
            return new FundSourceAllocationRequestAmount(fundSourceAllocationRequestAmountToDiff.FundSourceAllocation,
                                                  fundSourceAllocationRequestAmountToDiff.TotalAmount,
                                                  fundSourceAllocationRequestAmountToDiff.MatchAmount,
                                                  fundSourceAllocationRequestAmountToDiff.PayAmount,
                                                  displayCssClass);
        }
    }
}