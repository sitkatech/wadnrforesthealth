/*-----------------------------------------------------------------------
<copyright file="EditProjectRegionsViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectRegion
{
    public class EditProjectRegionsViewData : FirmaViewData
    {
        public readonly EditProjectRegionsViewDataForAngular ViewDataForAngular;
        public readonly string EditProjectRegionsFormID;
        public readonly string EditProjectRegionsUrl;
        public readonly bool HasProjectLocationPoint;
        public readonly bool HasProjectLocationDetail;
        public readonly string SimplePointMarkerImg;

        public EditProjectRegionsViewData(Person currentPerson, MapInitJson mapInitJson,
            List<Models.DNRUplandRegion> dnrUplandRegionsInViewModel, string editProjectRegionsUrl,
            string editProjectRegionsFormID, bool hasProjectLocationPoint, bool hasProjectLocationDetail) : base(currentPerson)
        {
            ViewDataForAngular = new EditProjectRegionsViewDataForAngular(mapInitJson, dnrUplandRegionsInViewModel);
            EditProjectRegionsFormID = editProjectRegionsFormID;
            EditProjectRegionsUrl = editProjectRegionsUrl;
            HasProjectLocationPoint = hasProjectLocationPoint;
            HasProjectLocationDetail = hasProjectLocationDetail;

            SimplePointMarkerImg = $"https://api.tiles.mapbox.com/v3/marker/pin-s-marker+838383.png?access_token={FirmaWebConfiguration.MapBoxApiKey}";
        }
    }

    public class EditProjectRegionsViewDataForAngular
    {
        public readonly MapInitJson MapInitJson;
        public readonly string FindRegionByNameUrl;
        public readonly string TypeAheadInputId;
        public Dictionary<int, string> RegionNameByID;
        public readonly string RegionMapServiceLayerName;
        public readonly string MapServiceUrl;

        public EditProjectRegionsViewDataForAngular(MapInitJson mapInitJson, List<Models.DNRUplandRegion> dnrUplandRegionsInViewModel)
        {
            MapInitJson = mapInitJson;
            FindRegionByNameUrl = SitkaRoute<ProjectRegionController>.BuildUrlFromExpression(c => c.FindRegionByName(null));
            TypeAheadInputId = "regionSearch";
            RegionNameByID = dnrUplandRegionsInViewModel.ToDictionary(x => x.DNRUplandRegionID, x => x.DNRUplandRegionName);
            RegionMapServiceLayerName = FirmaWebConfiguration.GetDNRUplandRegionWmsLayerName();
            MapServiceUrl = FirmaWebConfiguration.WebMapServiceUrl;
        }
    }
}
