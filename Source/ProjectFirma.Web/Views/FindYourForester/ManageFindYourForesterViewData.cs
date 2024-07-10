/*-----------------------------------------------------------------------
<copyright file="ManageFindYourForesterViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Collections.Generic;
using LtInfo.Common;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FindYourForester
{
    public class ManageFindYourForesterViewData : FirmaViewData
    {
        public readonly MapInitJson MapInitJson;
        public readonly ManageFindYourForesterGridSpec GridSpec;
        public string GridName { get; }
        public readonly string GridDataUrl;
        public string GridDataUrlTemplate { get; }
        public string PageUrlTemplate { get; }

        public int DefaultForesterRoleID { get; }

        public ManageFindYourForesterViewData(Person currentPerson, MapInitJson mapInitJson, Models.FirmaPage firmaPage, string bulkAssignForestersUrl, int initialForesterRoleIdToLoad) : base(currentPerson, firmaPage)
        {
            PageTitle = "Manage Find Your Forester";
            MapInitJson = mapInitJson;
            GridSpec = new ManageFindYourForesterGridSpec(currentPerson);
            GridName = "manageFindYourForesterGrid";
            GridDataUrl = SitkaRoute<FindYourForesterController>.BuildUrlFromExpression(tc => tc.ManageFindYourForesterGridJsonData(initialForesterRoleIdToLoad));
            GridDataUrlTemplate = SitkaRoute<FindYourForesterController>.BuildUrlFromExpression(tc => tc.ManageFindYourForesterGridJsonData(UrlTemplate.Parameter1Int));
            PageUrlTemplate = SitkaRoute<FindYourForesterController>.BuildUrlFromExpression(tc => tc.Manage(UrlTemplate.Parameter1Int));
            DefaultForesterRoleID = initialForesterRoleIdToLoad;

            var getForesterWorkUnitID =
                $"function() {{ return {GridName}GetValuesFromCheckedGridRows(\'ForesterWorkUnitID\', \'ForesterWorkUnitIDList\'); }}";
 
            var modalDialogFormLink = ModalDialogFormHelper.ModalDialogFormLink(
                "<span class=\"glyphicon glyphicon-edit\" style=\"margin-right:5px\"></span>Manage Forester Assignments",
                bulkAssignForestersUrl,
                "Manage Forester Assignments",
                700,
                "Save",
                "Cancel",
                new List<string>(),
                null,
                getForesterWorkUnitID);

            GridSpec.ArbitraryHtml = new List<string> { modalDialogFormLink.ToString() };

        }

        
    }
}
