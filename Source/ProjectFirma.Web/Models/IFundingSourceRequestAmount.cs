namespace ProjectFirma.Web.Models
{
    public interface IFundingSourceRequestAmount
    {
        FundingSource FundingSource { get; }
        GrantAllocation GrantAllocation { get; }
        decimal? SecuredAmount { get; }
        decimal? UnsecuredAmount { get; }
    }
}