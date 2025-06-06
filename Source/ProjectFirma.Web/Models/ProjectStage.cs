﻿/*-----------------------------------------------------------------------
<copyright file="ProjectStage.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Models
{
    public partial class ProjectStage
    {
        public abstract bool IsOnCompletedList();
        public abstract bool IsDeletable();

        public abstract bool RequiresReportedExpenditures();
        public abstract bool IsStagedIncludedInTransporationCostCalculations();
        public abstract bool ShouldShowOnMap();

        public abstract IEnumerable<ProjectStage> GetProjectStagesThatProjectCanUpdateTo();

        // ReSharper disable once InconsistentNaming
        public static IEnumerable<ProjectStage> _forwardLookingFactSheetProjectStages;

        public static IEnumerable<ProjectStage> ForwardLookingFactSheetProjectStages => 
            _forwardLookingFactSheetProjectStages ?? (_forwardLookingFactSheetProjectStages =
            new List<ProjectStage>
            {
                Planned
            });
    }

    public partial class ProjectStagePlanned
    {
        public override bool IsOnCompletedList()
        {
            return false;
        }

        public override bool IsDeletable()
        {
            return false;
        }

        public override bool RequiresReportedExpenditures()
        {
            return true;
        }

        public override bool IsStagedIncludedInTransporationCostCalculations()
        {
            return true;
        }

        public override bool ShouldShowOnMap()
        {
            return true;
        }

        public override IEnumerable<ProjectStage> GetProjectStagesThatProjectCanUpdateTo()
        {
            return All;
        }
    }

    public partial class ProjectStageImplementation
    {
        public override bool IsOnCompletedList()
        {
            return false;
        }

        public override bool IsDeletable()
        {
            return false;
        }

        public override bool RequiresReportedExpenditures()
        {
            return true;
        }

        public override bool IsStagedIncludedInTransporationCostCalculations()
        {
            return true;
        }

        public override bool ShouldShowOnMap()
        {
            return true;
        }

        public override IEnumerable<ProjectStage> GetProjectStagesThatProjectCanUpdateTo()
        {
            return All.Except(new ProjectStage[] {Planned});
        }
    }

    public partial class ProjectStageCompleted
    {
        public override bool IsOnCompletedList()
        {
            return true;
        }

        public override bool IsDeletable()
        {
            return false;
        }

        public override bool RequiresReportedExpenditures()
        {
            return false;
        }

        public override bool IsStagedIncludedInTransporationCostCalculations()
        {
            return false;
        }

        public override bool ShouldShowOnMap()
        {
            return true;
        }

        public override IEnumerable<ProjectStage> GetProjectStagesThatProjectCanUpdateTo()
        {
            return new[] { Completed };
        }
    }

    public partial class ProjectStageCancelled
    {
        public override bool IsOnCompletedList()
        {
            return false;
        }


        public override bool IsDeletable()
        {
            return false;
        }

        public override bool RequiresReportedExpenditures()
        {
            return false;
        }

        public override bool IsStagedIncludedInTransporationCostCalculations()
        {
            return false;
        }

        public override bool ShouldShowOnMap()
        {
            return true;
        }

        public override IEnumerable<ProjectStage> GetProjectStagesThatProjectCanUpdateTo()
        {
            return new[] { Cancelled };
        }
    }

}
