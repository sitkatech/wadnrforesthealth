using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.FundSource
{
    public class NewFundSourceViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> Organizations { get; }
        public IEnumerable<SelectListItem> FundSourceTypes { get; }
        public IEnumerable<SelectListItem> FundSourceStatuses { get; }

        public EditFundSourceType EditFundSourceType { get; set; }

        public NewFundSourceViewData(EditFundSourceType editFundSourceType, IEnumerable<Models.Organization> organizations, IEnumerable<Models.FundSourceStatus> fundSourceStatuses, IEnumerable<Models.FundSourceType> fundSourceTypes)
        {
            Organizations = organizations.ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture), y => y.DisplayName);
            FundSourceStatuses = fundSourceStatuses.ToSelectListWithEmptyFirstRow(x => x.FundSourceStatusID.ToString(CultureInfo.InvariantCulture), y => y.FundSourceStatusName);
            FundSourceTypes = fundSourceTypes.ToSelectListWithEmptyFirstRow(x => x.FundSourceTypeID.ToString(CultureInfo.InvariantCulture), y => y.FundSourceTypeName);
            EditFundSourceType = editFundSourceType;
        }
    }
}