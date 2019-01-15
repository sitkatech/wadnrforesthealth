/*-----------------------------------------------------------------------
<copyright file="TreatmentActivityGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;


namespace ProjectFirma.Web.Views.Project
{
    public class TreatmentActivityGridSpec : GridSpec<Models.TreatmentActivity>
    {
        public TreatmentActivityGridSpec(Person currentPerson)
        {


            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteTreatmentActivityUrl(), new FirmaAdminFeature().HasPermissionByPerson(currentPerson), true), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditTreatmentActivityUrl())), 30, DhtmlxGridColumnFilterType.None);

            Add("Contact", a => a.GetContactText(), 100, DhtmlxGridColumnFilterType.Html);
            Add("Status", a => a.GetStatusDisplayName(), 75, DhtmlxGridColumnFilterType.Text);
            Add("Start Date", a => a.TreatmentActivityStartDate, 125, DhtmlxGridColumnFormatType.Date);
            Add("End Date", a => a.TreatmentActivityEndDate, 125, DhtmlxGridColumnFormatType.Date);
            Add("Footprint Acres", a => a.TreatmentActivityFootprintAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Chipping Acres", a => a.TreatmentActivityChippingAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Pruning Acres", a => a.TreatmentActivityPruningAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Thinning Acres", a => a.TreatmentActivityThinningAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Mastication Acres", a => a.TreatmentActivityMasticationAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Grazing Acres", a => a.TreatmentActivityGrazingAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Lop And Scatter Acres", a => a.TreatmentActivityLopAndScatterAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Biomass Removal Acres", a => a.TreatmentActivityBiomassRemovalAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Hand Pile Acres", a => a.TreatmentActivityHandPileAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Broadcat Burn Acres", a => a.TreatmentActivityBroadcastBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Hand Pile Burn Acres", a => a.TreatmentActivityHandPileBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Machine Burn Acres", a => a.TreatmentActivityMachinePileBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            Add("Other Acres", a => a.TreatmentActivityOtherTreatmentAcres, 75, DhtmlxGridColumnFormatType.Decimal);

            //Add(Models.FieldDefinition.Project.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.DisplayName), 350, DhtmlxGridColumnFilterType.Html);
            //Add("Project Name", a => a.GetProjectName(), 200, DhtmlxGridColumnFilterType.Text);
            //Add("Focus Area", a => a.GetProjectFocusAreaName(), 100, DhtmlxGridColumnFilterType.Text);
            //Add("Contact", a => a.GetContactName(), 75, DhtmlxGridColumnFilterType.Text);
            //Add("Status", a => a.GetStatusDisplayName(), 75, DhtmlxGridColumnFilterType.Text);
            //Add("Start Date", a => a.TreatmentActivityStartDate, 75, DhtmlxGridColumnFormatType.Date);
            //Add("End Date", a => a.TreatmentActivityEndDate, 75, DhtmlxGridColumnFormatType.Date);
            //Add("Footprint Acres", a => a.TreatmentActivityFootprintAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Chipping Acres", a => a.TreatmentActivityChippingAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Pruning Acres", a => a.TreatmentActivityPruningAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Thinning Acres", a => a.TreatmentActivityThinningAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Mastication Acres", a => a.TreatmentActivityMasticationAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Grazing Acres", a => a.TreatmentActivityGrazingAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Lop And Scatter Acres", a => a.TreatmentActivityLopAndScatterAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Biomass Removal Acres", a => a.TreatmentActivityBiomassRemovalAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Hand Pile Acres", a => a.TreatmentActivityHandPileAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Broadcat Burn Acres", a => a.TreatmentActivityBroadcastBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Hand Pile Burn Acres", a => a.TreatmentActivityHandPileBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Machine Burn Acres", a => a.TreatmentActivityMachinePileBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal);
            //Add("Other Acres", a => a.TreatmentActivityOtherTreatmentAcres, 75, DhtmlxGridColumnFormatType.Decimal);

        }
    }
}
