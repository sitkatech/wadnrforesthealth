using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Invoice;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class GrantAllocationBudgetLineItemGridSpec : GridSpec<Models.GrantAllocationBudgetLineItem>
    {
        public GrantAllocationBudgetLineItemGridSpec(Models.Person currentPerson, Models.GrantAllocation grantAllocation)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.GrantAllocationBudgetLineItem.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.GrantAllocationBudgetLineItem.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;


            var userHasEditPermissions = new GrantAllocationBudgetLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            if (userHasEditPermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditGrantAllocationBudgetLineItemUrl(), $"Edit this {Models.FieldDefinition.GrantAllocationBudgetLineItem.GetFieldDefinitionLabel()}")), 30, DhtmlxGridColumnFilterType.None);

                var createNewGrantAllocationBudgetLineItemUrl = SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.NewGrantAllocationBudgetLineItem(grantAllocation.PrimaryKey));
                CreateEntityModalDialogForm = new ModalDialogForm(createNewGrantAllocationBudgetLineItemUrl, $"Create a new {Models.FieldDefinition.GrantAllocationBudgetLineItem.GetFieldDefinitionLabel()}");
            }

            var userHasDeletePermissions = new GrantAllocationBudgetLineItemDeleteAsAdminFeature().HasPermissionByPerson(currentPerson);
            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteGrantAllocationBudgetLineItemUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }

            Add("Cost Type", x => x.CostType.CostTypeDescription, 125, DhtmlxGridColumnFilterType.Text);
            Add("Amount", x => x.GrantAllocationBudgetLineItemAmount, 125, DhtmlxGridColumnFormatType.Currency, DhtmlxGridColumnAggregationType.Total);
            Add("Note", x => x.GrantAllocationBudgetLineItemNote, 650, DhtmlxGridColumnFilterType.Text);
           
        }
    }
}