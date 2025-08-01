using System.Collections.Generic;
using System.Web;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.Grant
{
    public class GrantCreateHelper
    {
        private static readonly string ProjectTypeSelectionUrl =
            SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.New());

        private static string GetAddNewGrantButtonText() =>
            $"{BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-plus")} Add {Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}";

        public static HtmlString AddGrantButton()
        {
            return ModalDialogFormHelper.ModalDialogFormLink(GetAddNewGrantButtonText(), ProjectTypeSelectionUrl,
                $"Add a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}", 700, "Save", "Cancel",
                new List<string> { "btn", "btn-firma" }, null, null);
        }

    }
}