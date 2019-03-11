using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Models
{
    public partial class Invoice : IAuditableEntity
    {

        public const string LandOwnerPurchaseAuthority = "Landowner Cost-Share Agreement";

        public string InvoiceDateDisplay => InvoiceDate.ToShortDateString();
        public string AuditDescriptionString => InvoiceIdentifyingName;

        public string PurchaseAuthorityDisplay => PurchaseAuthorityIsLandownerCostShareAgreement
            ? LandOwnerPurchaseAuthority
            : PurchaseAuthority;
    }
}