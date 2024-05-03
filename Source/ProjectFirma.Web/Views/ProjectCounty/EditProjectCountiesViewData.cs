/*-----------------------------------------------------------------------
<copyright file="EditProjectCountiesViewData.cs" company="Tahoe Countyal Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Countyal Planning Agency and Environmental Science Associates. All rights reserved.
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
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectCounty
{
    public class EditProjectCountiesViewData : FirmaViewData
    {
        public readonly EditProjectCountiesViewDataForAngular ViewDataForAngular;
        public readonly string EditProjectCountiesFormID;
        public readonly string EditProjectCountiesUrl;
        public readonly bool HasProjectLocationPoint;
        public readonly bool HasProjectLocationDetail;
        public readonly string SimplePointMarkerImg;

        public EditProjectCountiesViewData(Person currentPerson, MapInitJson mapInitJson,
            List<Models.County> countiesInViewModel, string editProjectCountiesUrl,
            string editProjectCountiesFormID, bool hasProjectLocationPoint, bool hasProjectLocationDetail) : base(currentPerson)
        {
            ViewDataForAngular = new EditProjectCountiesViewDataForAngular(mapInitJson, countiesInViewModel);
            EditProjectCountiesFormID = editProjectCountiesFormID;
            EditProjectCountiesUrl = editProjectCountiesUrl;
            HasProjectLocationPoint = hasProjectLocationPoint;
            HasProjectLocationDetail = hasProjectLocationDetail;

            SimplePointMarkerImg = $"https://api.tiles.mapbox.com/v3/marker/pin-s-marker+838383.png?access_token={FirmaWebConfiguration.MapBoxApiKey}";
        }
    }

    public class EditProjectCountiesViewDataForAngular
    {
        public readonly MapInitJson MapInitJson;
        public readonly string FindCountyByNameUrl;
        public readonly string TypeAheadInputId;
        public Dictionary<int, string> CountyNameByID;
        public readonly string CountyMapServiceLayerName;
        public readonly string MapServiceUrl;

        public EditProjectCountiesViewDataForAngular(MapInitJson mapInitJson, List<Models.County> countiesInViewModel)
        {
            MapInitJson = mapInitJson;
            FindCountyByNameUrl = SitkaRoute<ProjectCountyController>.BuildUrlFromExpression(c => c.FindCountyByName(null));
            TypeAheadInputId = "countySearch";
            CountyNameByID = countiesInViewModel.ToDictionary(x => x.CountyID, x => x.CountyName);
            CountyMapServiceLayerName = FirmaWebConfiguration.GetCountyWmsLayerName();
            MapServiceUrl = FirmaWebConfiguration.WebMapServiceUrl;
        }
    }
}
