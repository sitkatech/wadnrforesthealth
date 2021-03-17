namespace ProjectFirma.Web.Models
{
    public interface IGrantAllocationRequestAmount
    {
        GrantAllocation GrantAllocation { get; }
        decimal? TotalAmount { get; }
        decimal? MatchAmount { get; }
        decimal? PayAmount { get; }
        bool IsMatchAndPayRelevant { get; }
    }
}