/*-----------------------------------------------------------------------
<copyright file="CountiesViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.ProjectCounty;
using ProjectFirma.Web.Views.ProjectRegion;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class CountiesViewModel : EditProjectCountiesViewModel
    {
        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public CountiesViewModel()
        {
        }

        public CountiesViewModel(List<int> countyIDs, string noCountiesExplanation) : base(countyIDs, noCountiesExplanation)
        {
        }

        public void UpdateModelBatch(ProjectUpdateBatch projectUpdateBatch, List<ProjectCountyUpdate> currentProjectUpdateCounties, ObservableCollection<ProjectCountyUpdate> allProjectUpdateCounties)
        {
            UpdateModel(projectUpdateBatch, currentProjectUpdateCounties, allProjectUpdateCounties);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
