using LtInfo.Common.Views;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class GrantType : IAuditableEntity
    {
        public string AuditDescriptionString => GrantTypeName;
    }
}