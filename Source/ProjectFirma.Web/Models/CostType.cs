using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class CostType : IAuditableEntity
    {
        public string AuditDescriptionString => $"{CostTypeDisplayName}";

        public static List<CostType> GetLineItemCostTypes()
        {
            return All.Where(ct => ct.IsValidInvoiceLineItemCostType).ToList();
        }
    }
}