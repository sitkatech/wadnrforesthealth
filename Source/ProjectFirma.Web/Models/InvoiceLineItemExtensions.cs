using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class InvoiceLineItemExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<InvoiceController>.BuildUrlFromExpression(t => t.DeleteInvoiceLineItem(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this InvoiceLineItem invoiceLineItem)
        {
            return DeleteUrlTemplate.ParameterReplace(invoiceLineItem.PrimaryKey);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<InvoiceController>.BuildUrlFromExpression(t => t.EditInvoiceLineItem(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this InvoiceLineItem invoiceLineItem)
        {
            return EditUrlTemplate.ParameterReplace(invoiceLineItem.PrimaryKey);
        }

    }
}