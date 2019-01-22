/*-----------------------------------------------------------------------
<copyright file="EditProjectPriorityAreasViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
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

namespace ProjectFirma.Web.Views.ProjectPriorityArea
{
    public class EditProjectPriorityAreasViewData : FirmaViewData
    {
        public readonly EditProjectPriorityAreasViewDataForAngular ViewDataForAngular;
        public readonly string EditProjectPriorityAreasFormID;
        public readonly string EditProjectPriorityAreasUrl;
        public readonly bool HasProjectLocationPoint;
        public readonly bool HasProjectLocationDetail;
        public readonly string SimplePointMarkerImg;

        public EditProjectPriorityAreasViewData(Person currentPerson, MapInitJson mapInitJson,
            List<Models.PriorityArea> priorityAreasInViewModel, string editProjectPriorityAreasUrl,
            string editProjectPriorityAreasFormID, bool hasProjectLocationPoint, bool hasProjectLocationDetail) : base(currentPerson)
        {
            ViewDataForAngular =
                new EditProjectPriorityAreasViewDataForAngular(mapInitJson, priorityAreasInViewModel);
            EditProjectPriorityAreasFormID = editProjectPriorityAreasFormID;
            EditProjectPriorityAreasUrl = editProjectPriorityAreasUrl;
            HasProjectLocationPoint = hasProjectLocationPoint;
            HasProjectLocationDetail = hasProjectLocationDetail;

            SimplePointMarkerImg = "https://api.tiles.mapbox.com/v3/marker/pin-s-marker+838383.png";
        }
    }

    public class EditProjectPriorityAreasViewDataForAngular
    {
        public readonly MapInitJson MapInitJson;
        public readonly string FindPriorityAreaByNameUrl;
        public readonly string TypeAheadInputId;
        public Dictionary<int, string> PriorityAreaNameByID;
        public readonly string PriorityAreaMapServiceLayerName;
        public readonly string MapServiceUrl;

        public EditProjectPriorityAreasViewDataForAngular(MapInitJson mapInitJson, List<Models.PriorityArea> priorityAreasInViewModel)
        {
            MapInitJson = mapInitJson;
            FindPriorityAreaByNameUrl = SitkaRoute<ProjectPriorityAreaController>.BuildUrlFromExpression(c => c.FindPriorityAreaByName(null));
            TypeAheadInputId = "priorityAreaSearch";
            PriorityAreaNameByID = priorityAreasInViewModel.ToDictionary(x => x.PriorityAreaID, x => x.PriorityAreaName);
            PriorityAreaMapServiceLayerName = "WADNRForestHealth:PriorityArea";//todo: move priorityArea layer name and service url to web config
            MapServiceUrl = "https://localhost-wadnrforesthealth-mapserver.projectfirma.com/geoserver/WADNRForestHealth/wms";
        }
    }
}
