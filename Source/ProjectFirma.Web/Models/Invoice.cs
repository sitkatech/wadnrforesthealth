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

        //TODO: 10/7/22 TK - probably delete this or move to IPR class
        //public string PurchaseAuthorityDisplay => PurchaseAuthorityIsLandownerCostShareAgreement
        //    ? LandOwnerPurchaseAuthority
        //    : PurchaseAuthority;
    }
}