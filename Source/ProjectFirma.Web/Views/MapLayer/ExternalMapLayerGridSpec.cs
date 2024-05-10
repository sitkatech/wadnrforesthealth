/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.MapLayer
{
    public class ExternalMapLayerGridSpec : GridSpec<ExternalMapLayer>
    {
        public ExternalMapLayerGridSpec(bool userCanManage)
        {
            if (userCanManage)
            {
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, AgGridColumnFilterType.None);
                Add("Edit", x => AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditUrl(), "Edit External Map Layer")), 30, AgGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.ExternalMapLayerDisplayName.ToGridHeaderString(), x => x?.DisplayName, 150);
            Add(Models.FieldDefinition.ExternalMapLayerUrl.ToGridHeaderString(), x => x.LayerUrl, 250);
            Add(Models.FieldDefinition.ExternalMapLayerDescription.ToGridHeaderString(), x => x.LayerDescription, 300);
            Add(Models.FieldDefinition.ExternalMapLayerFeatureNameField.ToGridHeaderString(), x => x.FeatureNameField, 150);
            Add(Models.FieldDefinition.ExternalMapLayerDisplayOnProjectMap.ToGridHeaderString(), x => x.DisplayOnProjectMap ? "Yes" : "No", 75);
            Add(Models.FieldDefinition.ExternalMapLayerDisplayOnPriorityLandscape.ToGridHeaderString(), x => x.DisplayOnPriorityLandscape ? "Yes" : "No", 75);
            Add(Models.FieldDefinition.ExternalMapLayerDisplayOnAllOthers.ToGridHeaderString(), x => x.DisplayOnAllOthers ? "Yes" : "No", 75);
            Add(Models.FieldDefinition.ExternalMapLayerIsActive.ToGridHeaderString(), x => x.IsActive ? "Yes" : "No", 75);
            Add(Models.FieldDefinition.ExternalMapLayerIsATiledMapService.ToGridHeaderString(), x => x.IsTiledMapService ? "Yes" : "No", 75);
        }
    }
}
