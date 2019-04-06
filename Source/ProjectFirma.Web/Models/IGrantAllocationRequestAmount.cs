namespace ProjectFirma.Web.Models
{
    public interface IGrantAllocationRequestAmount
    {
        GrantAllocation GrantAllocation { get; }
        decimal? SecuredAmount { get; }
        decimal? UnsecuredAmount { get; }
    }
}