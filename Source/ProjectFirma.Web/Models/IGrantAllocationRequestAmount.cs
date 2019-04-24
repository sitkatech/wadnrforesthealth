namespace ProjectFirma.Web.Models
{
    public interface IGrantAllocationRequestAmount
    {
        GrantAllocation GrantAllocation { get; }
        decimal? TotalAmount { get; }
    }
}