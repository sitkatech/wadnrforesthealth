/*-----------------------------------------------------------------------
<copyright file="TreatmentActivityProjectDetailGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Web;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;


namespace ProjectFirma.Web.Views.TreatmentActivity
{
    public class TreatmentActivityProjectDetailGridSpec : GridSpec<Models.TreatmentActivity>
    {
        public TreatmentActivityProjectDetailGridSpec(Person currentPerson)
        {
            bool userHasEditTreatmentPermissions = new FirmaAdminFeature().HasPermissionByPerson(currentPerson);
            int buttonGridWidth = 30;
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteTreatmentActivityUrl(), userHasEditTreatmentPermissions, true, true), buttonGridWidth, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditTreatmentActivityUrl()), userHasEditTreatmentPermissions), buttonGridWidth, DhtmlxGridColumnFilterType.None);

            Add("Contact", a => a.GetContactText(), 125, DhtmlxGridColumnFilterType.Html);
            Add("Status", a => a.GetStatusDisplayName(), 75, DhtmlxGridColumnFilterType.Text);
            Add("Program Index", a => a.GetProgramIndexName(), 75, DhtmlxGridColumnFilterType.Text);
            Add("Project Code", a => a.GetProjectCodeName(), 75, DhtmlxGridColumnFilterType.Text);
            Add("Start Date", a => a.TreatmentActivityStartDate, 125, DhtmlxGridColumnFormatType.Date);
            Add("End Date", a => a.TreatmentActivityEndDate, 125, DhtmlxGridColumnFormatType.Date);
            Add("Footprint Acres", a => a.TreatmentActivityFootprintAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Chipping Acres", a => a.TreatmentActivityChippingAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Pruning Acres", a => a.TreatmentActivityPruningAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Thinning Acres", a => a.TreatmentActivityThinningAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Slash Acres", a => a.TreatmentActivitySlashAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Mastication Acres", a => a.TreatmentActivityMasticationAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Grazing Acres", a => a.TreatmentActivityGrazingAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Lop And Scatter Acres", a => a.TreatmentActivityLopAndScatterAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Biomass Removal Acres", a => a.TreatmentActivityBiomassRemovalAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Hand Pile Acres", a => a.TreatmentActivityHandPileAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Broadcat Burn Acres", a => a.TreatmentActivityBroadcastBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Hand Pile Burn Acres", a => a.TreatmentActivityHandPileBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Machine Burn Acres", a => a.TreatmentActivityMachinePileBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Other Acres", a => a.TreatmentActivityOtherTreatmentAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Notes", a => a.TreatmentActivityNotes, 200, DhtmlxGridColumnFilterType.Text);

        }
    }
}
