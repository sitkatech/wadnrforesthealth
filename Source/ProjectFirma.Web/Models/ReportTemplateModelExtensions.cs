using System.Web;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirmaModels.Models;

namespace ProjectFirma.Web.Models
{
    public static class ReportTemplateModelExtensions
    {
        public static HtmlString DownloadTemplateLink(this ReportTemplate reportTemplate)
        {
            var linkText = reportTemplate.FileResource.OriginalBaseFilename +
                           reportTemplate.FileResource.OriginalFileExtension;
            return UrlTemplate.MakeHrefString(reportTemplate.FileResource.FileResourceUrl, linkText);
        }

        public static string DownloadTemplateLinkForAgGrid(this ReportTemplate reportTemplate)
        {
            var linkText = reportTemplate.FileResource.OriginalBaseFilename +
                           reportTemplate.FileResource.OriginalFileExtension;
            return new HtmlLinkObject( linkText,reportTemplate.FileResource.FileResourceUrl).ToJsonObjectForAgGrid();
        }

        public static string FileResourceName(this ReportTemplate reportTemplate)
        {
            return $"{reportTemplate.FileResource.OriginalBaseFilename}{reportTemplate.FileResource.OriginalFileExtension}";
        }

    }
}