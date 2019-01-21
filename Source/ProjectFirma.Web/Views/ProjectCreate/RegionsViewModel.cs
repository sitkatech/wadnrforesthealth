/*-----------------------------------------------------------------------
<copyright file="LocationSimpleViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Collections.ObjectModel;
using ProjectFirma.Web.Views.ProjectRegion;


namespace ProjectFirma.Web.Views.ProjectCreate
{    
    public class RegionsViewModel : EditProjectRegionsViewModel
    {
        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public RegionsViewModel()
        {
        }

        public RegionsViewModel(List<int> regionIDs, string noRegionsExplanation) : base(regionIDs, noRegionsExplanation)
        {
        }
        
        public void UpdateModel(Models.Project project, List<Models.ProjectRegion> currentProjectRegions, ObservableCollection<Models.ProjectRegion> allProjectRegions)
        {
            base.UpdateModel(project, currentProjectRegions, allProjectRegions);
        }
    }    
}
