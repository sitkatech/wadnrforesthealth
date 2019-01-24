using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.TreatmentActivity
{
    public class TreatmentActivityIndexGridSpec : GridSpec<Models.TreatmentActivity>
    {
        public TreatmentActivityIndexGridSpec(Person currentPerson)
        {

            //Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteTreatmentActivityUrl(), new FirmaAdminFeature().HasPermissionByPerson(currentPerson), true), 30, DhtmlxGridColumnFilterType.None);
            //Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditTreatmentActivityUrl())), 30, DhtmlxGridColumnFilterType.None);

            Add("Project Name", a => UrlTemplate.MakeHrefString(a.GetProjectDetailUrl(), a.GetProjectName()), 150, DhtmlxGridColumnFilterType.Html);
            Add("Focus Area", a => a.GetFocusAreaText(), 150, DhtmlxGridColumnFilterType.Html);
            Add("Region", a => a.GetProjectRegions(), 150, DhtmlxGridColumnFilterType.Text);
            Add("Contact", a => a.GetContactText(), 125, DhtmlxGridColumnFilterType.Text);
            Add("Status", a => a.GetStatusDisplayName(), 75, DhtmlxGridColumnFilterType.Text);
            Add("Program Index", a => a.GetProgramIndexName(), 75, DhtmlxGridColumnFilterType.Text);
            Add("Project Code", a => a.GetProjectCodeName(), 75, DhtmlxGridColumnFilterType.Text);
            Add("Start Date", a => a.TreatmentActivityStartDate, 125, DhtmlxGridColumnFormatType.Date);
            Add("End Date", a => a.TreatmentActivityEndDate, 125, DhtmlxGridColumnFormatType.Date);
            Add("Footprint Acres", a => a.TreatmentActivityFootprintAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Chipping Acres", a => a.TreatmentActivityChippingAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Pruning Acres", a => a.TreatmentActivityPruningAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Thinning Acres", a => a.TreatmentActivityThinningAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Mastication Acres", a => a.TreatmentActivityMasticationAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Grazing Acres", a => a.TreatmentActivityGrazingAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Lop And Scatter Acres", a => a.TreatmentActivityLopAndScatterAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Biomass Removal Acres", a => a.TreatmentActivityBiomassRemovalAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Hand Pile Acres", a => a.TreatmentActivityHandPileAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Broadcat Burn Acres", a => a.TreatmentActivityBroadcastBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Hand Pile Burn Acres", a => a.TreatmentActivityHandPileBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Machine Burn Acres", a => a.TreatmentActivityMachinePileBurnAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);
            Add("Other Acres", a => a.TreatmentActivityOtherTreatmentAcres, 75, DhtmlxGridColumnFormatType.Decimal, DhtmlxGridColumnAggregationType.Total);

        }


    }
}