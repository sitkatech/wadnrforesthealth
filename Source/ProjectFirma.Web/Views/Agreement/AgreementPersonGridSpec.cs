using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
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


            //var userHasCreatePermissions = new GrantCreateFeature().HasPermissionByPerson(currentPerson);
            //if (userHasCreatePermissions)
            //{
            //    var contentUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(t => t.New());
            //    CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, "Create a new Agreement");
            //}

            var userHasDeletePermissions = new AgreementDeleteFeature().HasPermissionByPerson(currentPerson);
            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true), 30, DhtmlxGridColumnFilterType.None);
            }
            Add("First Name", x => x.Person.GetFirstNameAsUrl(), 70, DhtmlxGridColumnFilterType.Text);
            Add("Last Name", x => x.Person.GetLastNameAsUrl(), 100, DhtmlxGridColumnFilterType.Text);
            Add("Agreement Role", x => x.AgreementPersonRole.AgreementPersonRoleDisplayName, 100, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), x => x.Person.Organization.GetDisplayNameWithoutAbbreviationAsUrl(), 200, DhtmlxGridColumnFilterType.Html);
           
        }
    }
}