﻿/*-----------------------------------------------------------------------
<copyright file="PriorityLandscapesViewModel.cs" company="Tahoe PriorityLandscapeal Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe PriorityLandscapeal Planning Agency and Sitka Technology Group. All rights reserved.
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

using ProjectFirma.Web.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ProjectFirma.Web.Views.ProjectPriorityLandscape;


namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class PriorityLandscapesViewModel : EditProjectPriorityLandscapesViewModel
    {
        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public PriorityLandscapesViewModel()
        {
        }

        public PriorityLandscapesViewModel(List<int> priorityLandscapeIDs, string noPriorityLandscapesExplanation) : base(priorityLandscapeIDs, noPriorityLandscapesExplanation)
        {
        }

        public void UpdateModelBatch(ProjectUpdateBatch projectUpdateBatch, List<ProjectPriorityLandscapeUpdate> currentProjectUpdatePriorityLandscapes, ObservableCollection<ProjectPriorityLandscapeUpdate> allProjectUpdatePriorityLandscapes)
        {
            UpdateModel(projectUpdateBatch, currentProjectUpdatePriorityLandscapes, allProjectUpdatePriorityLandscapes);
        }
    }
}