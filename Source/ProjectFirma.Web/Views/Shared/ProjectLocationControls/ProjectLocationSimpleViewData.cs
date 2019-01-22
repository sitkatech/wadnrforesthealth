/*-----------------------------------------------------------------------
<copyright file="EditProjectLocationSimpleViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using GeoJSON.Net.Feature;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectLocationControls
{
    public class ProjectLocationSimpleViewData : FirmaViewData
    {
        public ProjectLocationSimpleViewDataForAngular ViewDataForAngular { get; }
        public string MapFormID { get; }
        public string MapPostUrl { get; }

        public ProjectLocationSimpleViewData(Person currentPerson, MapInitJson mapInitJson, List<string> wmsLayerNames, Feature currentFeature, string mapPostUrl, string mapFormID, string mapServiceUrl)
            : base(currentPerson)
        {
            ViewDataForAngular = new ProjectLocationSimpleViewDataForAngular(mapInitJson, wmsLayerNames, currentFeature, mapServiceUrl);
            MapPostUrl = mapPostUrl;
            MapFormID = mapFormID;
        }
    }

    public class ProjectLocationSimpleViewDataForAngular
    {
        public MapInitJson MapInitJson { get; }
        public string TypeAheadInputId { get; }
        public string ProjectLocationFieldDefinitionLabel { get; }
        public List<string> GeospatialAreaMapSericeLayerNames { get; }
        public string MapServiceUrl { get; }
        public Feature CurrentFeature { get; }

        public ProjectLocationSimpleViewDataForAngular(MapInitJson mapInitJson, List<string> wmsLayerNames, Feature currentFeature, string mapServiceUrl)
        {
            MapInitJson = mapInitJson;
            TypeAheadInputId = "projectLocationSearch";
            ProjectLocationFieldDefinitionLabel = Models.FieldDefinition.ProjectLocation.GetFieldDefinitionLabel();
            GeospatialAreaMapSericeLayerNames = wmsLayerNames;
            CurrentFeature = currentFeature;
            MapServiceUrl = mapServiceUrl;
        }
    }
}