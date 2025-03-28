﻿using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Grant Modification Note Internal")]
    public class GrantModificationNoteInternalEditAsAdminFeature : FirmaFeature
    {
        public GrantModificationNoteInternalEditAsAdminFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
        }
    }
}