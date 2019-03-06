using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Models
{
    public partial class Invoice : IAuditableEntity
    {
        public string InvoiceDateDisplay => InvoiceDate.ToShortDateString();
        public string AuditDescriptionString => InvoiceID.ToString();
    }
}