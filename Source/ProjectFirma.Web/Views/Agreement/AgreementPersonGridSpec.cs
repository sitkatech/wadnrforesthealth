using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Agreement
{
    public class AgreementPersonGridSpec : GridSpec<Models.AgreementPerson>
    {
        public AgreementPersonGridSpec(Models.Person currentPerson)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()}";


            var userHasEditPermissions = new AgreementEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            if (userHasEditPermissions)
            {
                Add("Edit", x => AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditUrl(), "Edit this Agreement Contact")), 30, AgGridColumnFilterType.None);
            }

            var userHasDeletePermissions = new AgreementDeleteFeature().HasPermissionByPerson(currentPerson);
            if (userHasDeletePermissions)
            {
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, AgGridColumnFilterType.None);
            }
            Add("First Name", x => x.Person.GetFirstNameAsUrl(), 125, AgGridColumnFilterType.Text);
            Add("Last Name", x => x.Person.GetLastNameAsUrl(), 125, AgGridColumnFilterType.Text);
            Add("Agreement Role", x => x.AgreementPersonRole.AgreementPersonRoleDisplayName, 125, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), x => x.Person.Organization.GetDisplayNameWithoutAbbreviationAsUrl(), 250, AgGridColumnFilterType.Html);
           
        }
    }
}