﻿using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;

namespace ProjectFirma.Web.Models
{
    public partial class AgreementProject : IAuditableEntity
    {
        public string AuditDescriptionString => $"AgreementProjectID:{AgreementProjectID} AgreementID:{AgreementID} ProjectID:{ProjectID}";
    }
}