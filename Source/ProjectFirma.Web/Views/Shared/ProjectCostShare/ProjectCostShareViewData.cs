using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            // Only offer links if user is authorized to see them
            if (new CostSharePDFFilledInFeature().HasPermissionByPerson(currentPerson))
            {
                foreach (Models.ProjectPerson landowner in project.ProjectPeople.Where(x => x.ProjectPersonRelationshipType.ToEnum == ProjectPersonRelationshipTypeEnum.Landowner))
                {
                    var urlString = SitkaRoute<PdfFormController>.BuildLinkFromExpression(c => c.CostShareAgreementPdf(landowner.PrimaryKey), $"Cost Share Agreement for {landowner.Person.FullNameFirstLast}").ToHTMLFormattedString();
                    CostShareAgreementLinks.Add(urlString);
                }
            }
        }

    }
}
