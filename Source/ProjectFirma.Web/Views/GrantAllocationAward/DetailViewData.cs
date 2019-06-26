/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class DetailViewData : FirmaViewData
    {
        public string BackButtonUrl { get;  }
        public string BackButtonText { get; }
        public string EditGrantAllocationAwardUrl { get; }
        public Models.GrantAllocationAward GrantAllocationAward { get;  }
        public bool UserHasEditGrantAllocationAwardPermissions { get;  }

        public string EditContractorInvoiceUrl { get;}

        public SuppliesLineItemGridSpec SuppliesLineItemGridSpec { get; }
        public string SuppliesLineItemGridName { get; }
        public string SuppliesLineItemGridDataUrl { get; }

        public PersonnelAndBenefitsLineItemGridSpec PersonnelAndBenefitsLineItemGridSpec { get; }
        public string PersonnelAndBenefitsLineItemGridName { get; }
        public string PersonnelAndBenefitsLineItemGridDataUrl { get; }

        public TravelLineItemGridSpec TravelLineItemGridSpec { get; }
        public string TravelLineItemGridName { get; }
        public string TravelLineItemGridDataUrl { get; }

        public LandownerCostShareLineItemGridSpec LandownerCostShareLineItemGridSpec { get; }
        public string LandownerCostShareLineItemGridName { get; }
        public string LandownerCostShareLineItemGridDataUrl { get; }

        public ContractorInvoiceItemGridSpec ContractorInvoiceItemGridSpec { get; }
        public string ContractorInvoiceItemGridName { get; }
        public string ContractorInvoiceItemGridDataUrl { get; }


        public DetailViewData(Person currentPerson, 
                              Models.GrantAllocationAward grantAllocationAward, 
                              string backButtonUrl, 
                              string backButtonText, 
                              SuppliesLineItemGridSpec suppliesLineItemGridSpec,
                              PersonnelAndBenefitsLineItemGridSpec personnelAndBenefitsLineItemGridSpec,
                              TravelLineItemGridSpec travelLineItemGridSpec,
                              LandownerCostShareLineItemGridSpec landownerCostShareLineItemGridSpec,
                              ContractorInvoiceItemGridSpec contractorInvoiceItemGridSpec
                              ) : base(currentPerson)
        {
            PageTitle = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()}: {grantAllocationAward.GrantAllocationAwardName}";
            BreadCrumbTitle = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()} Detail";

            BackButtonUrl = backButtonUrl;
            BackButtonText = backButtonText;

            GrantAllocationAward = grantAllocationAward;
            UserHasEditGrantAllocationAwardPermissions = new GrantAllocationAwardEditAsAdminFeature().HasPermissionByPerson(currentPerson);

            EditGrantAllocationAwardUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(x => x.Edit(grantAllocationAward.PrimaryKey));
            EditContractorInvoiceUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(x => x.EditContractorInvoice(grantAllocationAward.PrimaryKey));

            SuppliesLineItemGridName = "grantAllocationAwardSuppliesLineItemGridName";
            SuppliesLineItemGridDataUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(x => x.SuppliesLineItemGridJsonData(grantAllocationAward.PrimaryKey));
            SuppliesLineItemGridSpec = suppliesLineItemGridSpec;

            PersonnelAndBenefitsLineItemGridName = "grantAllocationAwardPersonnelAndBenefitsLineItemGridName";
            PersonnelAndBenefitsLineItemGridDataUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(x => x.PersonnelAndBenefitsLineItemGridJsonData(grantAllocationAward.PrimaryKey));
            PersonnelAndBenefitsLineItemGridSpec = personnelAndBenefitsLineItemGridSpec;

            TravelLineItemGridName = "grantAllocationAwardTravelLineItemGridName";
            TravelLineItemGridDataUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(x => x.TravelLineItemGridJsonData(grantAllocationAward.PrimaryKey));
            TravelLineItemGridSpec = travelLineItemGridSpec;

            LandownerCostShareLineItemGridName = "grantAllocationAwardLandownerCostShareLineItemGridName";
            LandownerCostShareLineItemGridDataUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(x => x.LandownerCostShareLineItemGridJsonData(grantAllocationAward));
            LandownerCostShareLineItemGridSpec = landownerCostShareLineItemGridSpec;

            ContractorInvoiceItemGridName = "grantAllocationAwardContractorInvoiceItemGridName";
            ContractorInvoiceItemGridDataUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(x => x.ContractorInvoiceItemGridJsonData(grantAllocationAward.PrimaryKey));
            ContractorInvoiceItemGridSpec = contractorInvoiceItemGridSpec;


        }

        
    }
}
