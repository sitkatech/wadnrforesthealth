﻿using System.Collections.Generic;
using ProjectFirma.Web.Models;
using ProjectFirmaModels.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Can generate reports on Projects using existing report templates")]
    public class ReportTemplateGenerateReportsFeature : FirmaFeature
    {
        public ReportTemplateGenerateReportsFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.ProjectSteward }) { }
    }
}