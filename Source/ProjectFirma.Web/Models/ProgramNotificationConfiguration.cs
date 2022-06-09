using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class ProgramNotificationConfiguration : IAuditableEntity
    {
        public string AuditDescriptionString => $"ProgramNotificationConfigurationID: {ProgramNotificationConfigurationID}";





    }
}