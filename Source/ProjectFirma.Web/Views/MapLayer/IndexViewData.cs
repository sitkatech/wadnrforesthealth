﻿/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.MapLayer
{
    public class IndexViewData : FirmaViewData
    {
        public ExternalMapLayerGridSpec ExternalMapLayerGridSpec { get; }
        public string ExternalMapLayerGridName { get; }
        public string ExternalMapLayerGridDataUrl { get; }
        public string NewUrl { get; }
        public bool UserCanManage { get; }


        public IndexViewData(Person currentPerson,
            Models.FirmaPage externalMapLayersFirmaPage, string externalMapLayerGridDataUrl,
            bool userCanManage)
            : base(currentPerson, externalMapLayersFirmaPage)
        {
            PageTitle = Models.FieldDefinition.ExternalMapLayer.GetFieldDefinitionLabelPluralized();
            ExternalMapLayerGridSpec = new ExternalMapLayerGridSpec(userCanManage)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.ExternalMapLayer.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.ExternalMapLayer.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };
            ExternalMapLayerGridName = "externalMapLayersGrid";
            ExternalMapLayerGridDataUrl = externalMapLayerGridDataUrl;
            NewUrl = SitkaRoute<MapLayerController>.BuildUrlFromExpression(x => x.New());

            UserCanManage = userCanManage;
        }
    }
}
