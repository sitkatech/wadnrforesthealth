using System.Collections.Generic;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Grant
{
    public class GrantDetailsFileDetailsViewData
    {
        public GrantDetailsFileDetailsViewData(Models.Grant grant, Person currentPerson)
        {
            Grant = grant;
            CurrentPerson = currentPerson;

            //NewGrantFilesUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(c => c.NewGrantFiles(grant));
            //DeleteGrantFileUrlTemplate = new UrlTemplate<int>(
            //    SitkaRoute<GrantController>.BuildUrlFromExpression(c => c.DeleteGrantFile(UrlTemplate.Parameter1Int)));

            GrantFileEditAsAdminFeature = new GrantEditAsAdminFeature();
        }

        public List<EntityDocument> Documents { get; }
        public string AddDocumentUrl { get; }
        public string ProjectName { get; }
        public bool CanEditDocuments { get; }
        public Models.FieldDefinition FieldDefinition { get; }

        public GrantDetailsFileDetailsViewData(List<EntityDocument> documents, string addDocumentUrl, string projectName, bool canEditDocuments, Models.FieldDefinition fieldDefinition)
        {
            Documents = documents;
            AddDocumentUrl = addDocumentUrl;
            ProjectName = projectName;
            CanEditDocuments = canEditDocuments;
            ShowDownload = true;
            FieldDefinition = fieldDefinition;
        }
        
        public bool ShowDownload { get; }

        public Models.Grant Grant { get; set; }
        public string NewGrantFilesUrl { get; set; }
        public UrlTemplate<int> EditProjectDocumentUrlTemplate { get; set; }
        public UrlTemplate<int> DeleteGrantFileUrlTemplate { get; set; }
        public Person CurrentPerson { get; set; }
        public GrantEditAsAdminFeature GrantFileEditAsAdminFeature { get; set; }
    }
}