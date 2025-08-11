using System.Collections.Generic;
using System.Web;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.FundSource
{
    public class FundSourceCreateHelper
    {
        private static readonly string ProjectTypeSelectionUrl =
            SitkaRoute<FundSourceController>.BuildUrlFromExpression(x => x.New());

        private static string GetAddNewFundSourceButtonText() =>
            $"{BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-plus")} Add {Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()}";

        public static HtmlString AddFundSourceButton()
        {
            return ModalDialogFormHelper.ModalDialogFormLink(GetAddNewFundSourceButtonText(), ProjectTypeSelectionUrl,
                $"Add a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}", 700, "Save", "Cancel",
                new List<string> { "btn", "btn-firma" }, null, null);
        }

    }
}