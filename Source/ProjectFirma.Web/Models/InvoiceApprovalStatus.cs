using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Models
{
    public partial class InvoiceApprovalStatus : IAuditableEntity
    {
        public string AuditDescriptionString => InvoiceApprovalStatusName;
    }
}