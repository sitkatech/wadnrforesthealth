﻿/*-----------------------------------------------------------------------
<copyright file="PerformanceMeasureActualUpdateSimple.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PerformanceMeasureActualUpdateSimple
    {
        public int PerformanceMeasureActualUpdateID { get; set; }
        public int ProjectUpdateBatchID { get; set; }
        public int PerformanceMeasureID { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.ReportingYear)]
        public int? CalendarYear { get; set; }

        [DisplayName("Reported Value")]
        public double? ActualValue { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public PerformanceMeasureActualUpdateSimple()
        {
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public PerformanceMeasureActualUpdateSimple(PerformanceMeasureActualUpdate performanceMeasureActualUpdate) : this()
        {
            PerformanceMeasureActualUpdateID = performanceMeasureActualUpdate.PerformanceMeasureActualUpdateID;
            ProjectUpdateBatchID = performanceMeasureActualUpdate.ProjectUpdateBatchID;
            PerformanceMeasureID = performanceMeasureActualUpdate.PerformanceMeasureID;
            CalendarYear = performanceMeasureActualUpdate.CalendarYear;
            ActualValue = performanceMeasureActualUpdate.ActualValue;
            PerformanceMeasureActualSubcategoryOptionUpdates = PerformanceMeasureValueSubcategoryOption.GetAllPossibleSubcategoryOptions(performanceMeasureActualUpdate);
        }

        public List<PerformanceMeasureActualSubcategoryOptionUpdateSimple> PerformanceMeasureActualSubcategoryOptionUpdates { get; set; }
    }
}
