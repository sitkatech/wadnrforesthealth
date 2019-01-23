/*-----------------------------------------------------------------------
<copyright file="PriorityAreasViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Views.ProjectPriorityArea;



namespace ProjectFirma.Web.Views.ProjectCreate
{    
    public class PriorityAreasViewModel : EditProjectPriorityAreasViewModel
    {
        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public PriorityAreasViewModel()
        {
        }

        public PriorityAreasViewModel(List<int> priorityAreasIDs, string noPriorityAreasExplanation) : base(priorityAreasIDs, noPriorityAreasExplanation)
        {
        }
        
        public void UpdateModel(Models.Project project, List<Models.ProjectPriorityArea> currentProjectPriorityAreas, ObservableCollection<Models.ProjectPriorityArea> allProjectPriorityAreas)
        {
            base.UpdateModel(project, currentProjectPriorityAreas, allProjectPriorityAreas);
        }
    }    
}
