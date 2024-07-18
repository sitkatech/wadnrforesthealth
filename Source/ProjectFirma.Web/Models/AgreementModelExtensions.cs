using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using Microsoft.Ajax.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class AgreementModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<AgreementController>.BuildUrlFromExpression(t => t.DeleteAgreement(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this Agreement agreement)
        {
            return DeleteUrlTemplate.ParameterReplace(agreement.PrimaryKey);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<AgreementController>.BuildUrlFromExpression(t => t.AgreementDetail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Agreement agreement)
        {
            return DetailUrlTemplate.ParameterReplace(agreement.AgreementID);
        }

        public static readonly UrlTemplate<int> OrganizationDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetOrganizationDetailUrl(this Agreement agreement)
        {
            return OrganizationDetailUrlTemplate.ParameterReplace(agreement.OrganizationID);
        }

        public static readonly UrlTemplate<int> GrantDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantController>.BuildUrlFromExpression(t => t.GrantDetail(UrlTemplate.Parameter1Int)));

        public static HtmlString GetListOfGrantHrefs(this Models.Agreement agreement)
        {
            var distinctListOfGrants = agreement.AgreementGrantAllocations.Where(x => x != null).Select(x => x.GrantAllocation.GrantModification.Grant).DistinctBy(x => x.GrantNumber).OrderBy(x => x.GrantNumber, StringComparer.InvariantCultureIgnoreCase);

            var distinctListOfGrantHrefs = distinctListOfGrants.Select(x =>
                UrlTemplate.MakeHrefString(GrantDetailUrlTemplate.ParameterReplace(x.GrantID), x.GrantNumber));
            return new HtmlString(string.Join(", ", distinctListOfGrantHrefs.Select(x => x.ToHtmlString())));
        }
        
        public static string GetListOfGrantHrefsForAgGrid(this Models.Agreement agreement)
        {
            var distinctListOfGrants = agreement.AgreementGrantAllocations.Where(x => x != null).Select(x => x.GrantAllocation.GrantModification.Grant).DistinctBy(x => x.GrantNumber).OrderBy(x => x.GrantNumber, StringComparer.InvariantCultureIgnoreCase);

            var distinctListOfGrantHrefs = distinctListOfGrants.Select(x => new HtmlLinkObject(x.GrantNumber, GrantDetailUrlTemplate.ParameterReplace(x.GrantID)));
            return distinctListOfGrantHrefs.ToJsonArrayForAgGrid();
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<AgreementController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this Agreement agreement)
        {
            return EditUrlTemplate.ParameterReplace(agreement.AgreementID);
        }

        public static string GetFileDownloadUrl(this Agreement agreement)
        {
            if (agreement.AgreementFileResource != null)
            {
                var url = FileResource.FileResourceByGuidUrlTemplate;
                return url.ParameterReplace(agreement.AgreementFileResource.FileResourceGUIDAsString);
            }

            return string.Empty;
        }

        public static HtmlString GetAgreementTitleAsUrl(this Agreement agreement)
        {
            return agreement != null ? UrlTemplate.MakeHrefString(agreement.GetDetailUrl(), agreement.AgreementTitle) : new HtmlString(null);
        }



    }
}