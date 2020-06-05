using System.Collections.Generic;
using System.Web;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public class GisProjectBulkUpdateHelper
    {
        private static readonly string GisProjectBulkUploadUrl =
            SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(x => x.SourceOrganizationSelection());

        private static string GetGisBulkUploadButtonText =>
            $"Import/Update {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} from GIS Upload";


        private static readonly string GisProjectBulkUploadContinueButtonText =
            $"Continue {BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-chevron-right")}";

        public static HtmlString GisProjectBulkUploadButton()
        {
            return ModalDialogFormHelper.ModalDialogFormLink(GetGisBulkUploadButtonText, GisProjectBulkUploadUrl,
                GetGisBulkUploadButtonText, 700, GisProjectBulkUploadContinueButtonText, "Cancel",
                new List<string> { "btn", "btn-firma", "addSomePadding" }, null, null);
        }

    }
}