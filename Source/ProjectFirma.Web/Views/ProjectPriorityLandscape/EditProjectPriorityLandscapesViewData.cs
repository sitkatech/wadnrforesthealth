/*-----------------------------------------------------------------------
<copyright file="EditProjectPriorityLandscapesViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectPriorityLandscape
{
    public class EditProjectPriorityLandscapesViewData : FirmaViewData
    {
        public readonly EditProjectPriorityLandscapesViewDataForAngular ViewDataForAngular;
        public readonly string EditProjectPriorityLandscapesFormID;
        public readonly string EditProjectPriorityLandscapesUrl;
        public readonly bool HasProjectLocationPoint;
        public readonly bool HasProjectLocationDetail;
        public readonly string SimplePointMarkerImg;

        public EditProjectPriorityLandscapesViewData(Person currentPerson, MapInitJson mapInitJson,
            List<Models.PriorityLandscape> priorityLandscapesInViewModel, string editProjectPriorityLandscapesUrl,
            string editProjectPriorityLandscapesFormID, bool hasProjectLocationPoint, bool hasProjectLocationDetail) : base(currentPerson)
        {
            ViewDataForAngular =
                new EditProjectPriorityLandscapesViewDataForAngular(mapInitJson, priorityLandscapesInViewModel);
            EditProjectPriorityLandscapesFormID = editProjectPriorityLandscapesFormID;
            EditProjectPriorityLandscapesUrl = editProjectPriorityLandscapesUrl;
            HasProjectLocationPoint = hasProjectLocationPoint;
            HasProjectLocationDetail = hasProjectLocationDetail;

            SimplePointMarkerImg = $"https://api.tiles.mapbox.com/v3/marker/pin-s-marker+838383.png?access_token={FirmaWebConfiguration.MapBoxApiKey}";
        }
    }

    public class EditProjectPriorityLandscapesViewDataForAngular
    {
        public readonly MapInitJson MapInitJson;
        public readonly string FindPriorityLandscapeByNameUrl;
        public readonly string TypeAheadInputId;
        public Dictionary<int, string> PriorityLandscapeNameByID;
        public readonly string PriorityLandscapeMapServiceLayerName;
        public readonly string MapServiceUrl;

        public EditProjectPriorityLandscapesViewDataForAngular(MapInitJson mapInitJson, List<Models.PriorityLandscape> priorityLandscapesInViewModel)
        {
            MapInitJson = mapInitJson;
            FindPriorityLandscapeByNameUrl = SitkaRoute<ProjectPriorityLandscapeController>.BuildUrlFromExpression(c => c.FindPriorityLandscapeByName(null));
            TypeAheadInputId = "priorityLandscapeSearch";
            PriorityLandscapeNameByID = priorityLandscapesInViewModel.ToDictionary(x => x.PriorityLandscapeID, x => x.PriorityLandscapeName);
            PriorityLandscapeMapServiceLayerName =  FirmaWebConfiguration.GetPriorityLandscapeWmsLayerName();
            MapServiceUrl = FirmaWebConfiguration.WebMapServiceUrl;
        }
    }
}
