﻿using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a {0}", FieldDefinitionEnum.GrantAllocation)]
    public class GrantAllocationCreateFeature : FirmaFeature
    {
        public GrantAllocationCreateFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanManageGrantsAndAgreements })
        {
            
        }
    }
}