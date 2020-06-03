/*-----------------------------------------------------------------------
<copyright file="EditInteractionEventLocationSimpleViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using GeoJSON.Net.Feature;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class EditInteractionEventLocationSimpleViewData : FirmaViewData
    {
        public InteractionEventLocationSimpleViewDataForAngular ViewDataForAngular { get; }
        public string MapFormID { get; }
        public string MapPostUrl { get; }

        public EditInteractionEventLocationSimpleViewData(Person currentPerson, MapInitJson mapInitJson, List<string> wmsLayerNames, Feature currentFeature, string mapPostUrl, string mapFormID, string mapServiceUrl)
            : base(currentPerson)
        {
            ViewDataForAngular = new InteractionEventLocationSimpleViewDataForAngular(mapInitJson, wmsLayerNames, currentFeature, mapServiceUrl);
            MapPostUrl = mapPostUrl;
            MapFormID = mapFormID;
        }
    }

    public class InteractionEventLocationSimpleViewDataForAngular
    {
        public MapInitJson MapInitJson { get; }
        public string TypeAheadInputId { get; }
        public string InteractionEventLocationFieldDefinitionLabel { get; }
        public List<string> GeospatialAreaMapServiceLayerNames { get; }
        public string MapServiceUrl { get; }
        public Feature CurrentFeature { get; }

        public InteractionEventLocationSimpleViewDataForAngular(MapInitJson mapInitJson, List<string> wmsLayerNames, Feature currentFeature, string mapServiceUrl)
        {
            MapInitJson = mapInitJson;
            TypeAheadInputId = "interactionEventLocationSearch";
            InteractionEventLocationFieldDefinitionLabel = ProjectFirma.Web.Models.FieldDefinition.InteractionEventLocation.GetFieldDefinitionLabel();
            GeospatialAreaMapServiceLayerNames = wmsLayerNames;
            CurrentFeature = currentFeature;
            MapServiceUrl = mapServiceUrl;
        }
    }
}