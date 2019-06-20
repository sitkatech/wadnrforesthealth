using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantAllocationAwardContractorInvoiceModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.DeleteContractorInvoice(UrlTemplate.Parameter1Int)));
        public static string GetDeleteContractorInvoiceUrl(this GrantAllocationAwardContractorInvoice contractorInvoice)
        {
            return DeleteUrlTemplate.ParameterReplace(contractorInvoice.GrantAllocationAwardContractorInvoiceID);
        }


        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.EditContractorInvoiceItem(UrlTemplate.Parameter1Int)));
        public static string GetEditContractorInvoiceUrl(this GrantAllocationAwardContractorInvoice contractorInvoice)
        {
            return EditUrlTemplate.ParameterReplace(contractorInvoice.GrantAllocationAwardContractorInvoiceID);
        }

    }
}