namespace ProjectFirma.Web.Models
{
    public interface IGrantAllocationRequestAmount
    {
        FundSourceAllocation FundSourceAllocation { get; }
        decimal? TotalAmount { get; }
        decimal? MatchAmount { get; }
        decimal? PayAmount { get; }
        bool IsMatchAndPayRelevant { get; }
    }
}