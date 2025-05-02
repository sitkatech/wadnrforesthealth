/*-----------------------------------------------------------------------
<copyright file="UpdateStatus.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class UpdateStatus
    {
        public bool IsBasicsUpdated { get; }
        public bool IsPerformanceMeasuresUpdated { get; }
        public bool IsExpectedFundingUpdated { get; }
        public bool IsExpendituresUpdated { get; }
        public bool IsBudgetsUpdated { get; }
        public bool IsPhotosUpdated { get; }
        public bool IsLocationSimpleUpdated { get; }
        public bool IsLocationDetailUpdated { get; }
        public bool IsRegionsUpdated { get; }
        public bool IsCountiesUpdated { get; }
        public bool IsPriorityLandscapesUpdated { get; }
        public bool IsExternalLinksUpdated { get; }
        public bool IsNotesUpdated { get; }
        public bool IsOrganizationsUpdated { get; }
        public bool IsContactsUpdated { get; }
        public bool IsTreatmentsUpdated { get; }

        public UpdateStatus(bool isBasicsUpdated,
            bool isPerformanceMeasuresUpdated,
            bool isExpendituresUpdated,
            bool isBudgetsUpdated,
            bool isPhotosUpdated,
            bool isLocationSimpleUpdated,
            bool isLocationDetailUpdated,
            bool isPriorityLandscapesUpdated,
            bool isRegionsUpdated,
            bool isCountiesUpdated,
            bool isExternalLinksUpdated,
            bool isNotesUpdated,
            bool isExpectedFundingUpdated,
            bool isOrganizationsUpdated,
            bool isContactsUpdated,
            bool isTreatmentsUpdated
            )
        {
            IsBasicsUpdated = isBasicsUpdated;
            IsPerformanceMeasuresUpdated = isPerformanceMeasuresUpdated;
            IsExpendituresUpdated = isExpendituresUpdated;
            IsBudgetsUpdated = isBudgetsUpdated;
            IsPhotosUpdated = isPhotosUpdated;
            IsLocationSimpleUpdated = isLocationSimpleUpdated;
            IsLocationDetailUpdated = isLocationDetailUpdated;
            IsPriorityLandscapesUpdated = isPriorityLandscapesUpdated;
            IsRegionsUpdated = isRegionsUpdated;
            IsCountiesUpdated = isCountiesUpdated;
            IsExternalLinksUpdated = isExternalLinksUpdated;
            IsNotesUpdated = isNotesUpdated;
            IsExpectedFundingUpdated = isExpectedFundingUpdated;
            IsOrganizationsUpdated = isOrganizationsUpdated;
            IsContactsUpdated = isContactsUpdated;
            IsTreatmentsUpdated = isTreatmentsUpdated;
        }
    }
}
