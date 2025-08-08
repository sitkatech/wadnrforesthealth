using LtInfo.Common.Views;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceType : IAuditableEntity
    {
        public string AuditDescriptionString => FundSourceTypeName;
    }
}