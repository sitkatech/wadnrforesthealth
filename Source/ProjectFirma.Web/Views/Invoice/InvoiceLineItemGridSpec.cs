using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Invoice
{
    public class InvoiceLineItemGridSpec : GridSpec<Models.InvoiceLineItem>
    {
        public InvoiceLineItemGridSpec(Models.Person currentPerson)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()}";


            var userHasEditPermissions = new InvoiceLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            if (userHasEditPermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditUrl(), "Edit this Invoice Line Item")), 30, DhtmlxGridColumnFilterType.None);
            }

            var userHasDeletePermissions = new InvoiceLineItemDeleteFeature().HasPermissionByPerson(currentPerson);
            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }
            Add("Cost Type", x => x.CostType.CostTypeDescription, 125, DhtmlxGridColumnFilterType.Text);
            Add("Grant Allocation", x => x.GrantAllocation.GrantAllocationName, 125, DhtmlxGridColumnFilterType.Text);
            Add("Amount", x => x.InvoiceLineItemAmount, 70, DhtmlxGridColumnFormatType.Currency, DhtmlxGridColumnAggregationType.Total);
            Add("Note", x => x.InvoiceLineItemNote, 250, DhtmlxGridColumnFilterType.Text);
           
        }
    }
}