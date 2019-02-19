using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Shared.ProjectCostShare
{
    public class ProjectCostShareViewData
    {
        public List<HtmlString> CostShareAgreementLinks = new List<HtmlString>();

        public ProjectCostShareViewData(Models.Project project, Person currentPerson)
        {
            var blankCostShareLink = SitkaRoute<PdfFormController>.BuildLinkFromExpression(c => c.BlankCostShareAgreementPdf(), $"Blank Cost Share Agreement").ToHTMLFormattedString();
            CostShareAgreementLinks.Add(blankCostShareLink);

            foreach (Models.ProjectPerson landowner in project.ProjectPeople.Where(x => x.ProjectPersonRelationshipType.ToEnum == ProjectPersonRelationshipTypeEnum.Landowner))
            {
                var urlString = SitkaRoute<PdfFormController>.BuildLinkFromExpression(c => c.CostShareAgreementPdf(landowner.PrimaryKey), $"Cost Share Agreement for {landowner.Person.FullNameFirstLast}").ToHTMLFormattedString();
                CostShareAgreementLinks.Add(urlString);
            }
        }

        public ProjectCostShareViewData(Models.Project project, Person currentPerson, bool showNewButton) : this(project, currentPerson)
        {
            //UserHasProjectManagePermissions = UserHasProjectManagePermissions && showNewButton;
        }

        public ProjectCostShareViewData(List<EntityDocument> documents, string addDocumentUrl, string projectName, bool canEditDocuments)
        {
            //Documents = documents;
            //AddDocumentUrl = addDocumentUrl;
            //ProjectName = projectName;
            //CanEditDocuments = canEditDocuments;
            //ShowDownload = true;
        }

        public ProjectCostShareViewData(List<EntityDocument> documents, string addDocumentUrl, string projectName,
            bool canEditDocuments, bool showDownload) : this(documents,addDocumentUrl,projectName,canEditDocuments)
        {
            //ShowDownload = showDownload;
        }


    }
}
