using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.Grant
{
    public class NewGrantViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> Organizations { get; }
        public IEnumerable<SelectListItem> GrantTypes { get; }
        public IEnumerable<SelectListItem> GrantStatuses { get; }

        public EditGrantType EditGrantType { get; set; }

        public NewGrantViewData(EditGrantType editGrantType, IEnumerable<Models.Organization> organizations, IEnumerable<Models.FundSourceStatus> grantStatuses, IEnumerable<Models.FundSourceType> grantTypes)
        {
            Organizations = organizations.ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture), y => y.DisplayName);
            GrantStatuses = grantStatuses.ToSelectListWithEmptyFirstRow(x => x.GrantStatusID.ToString(CultureInfo.InvariantCulture), y => y.GrantStatusName);
            GrantTypes = grantTypes.ToSelectListWithEmptyFirstRow(x => x.GrantTypeID.ToString(CultureInfo.InvariantCulture), y => y.GrantTypeName);
            EditGrantType = editGrantType;
        }
    }
}