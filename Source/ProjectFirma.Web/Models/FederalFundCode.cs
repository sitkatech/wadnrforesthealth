using LtInfo.Common.Views;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class FederalFundCode : IAuditableEntity
    {
        public string AuditDescriptionString => FederalFundCodeAbbrev;
    }
}