﻿/*-----------------------------------------------------------------------
<copyright file="PerformanceMeasureExpectedSubcategoryOptionSimple.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
namespace ProjectFirma.Web.Models
{
    public class PerformanceMeasureExpectedSubcategoryOptionSimple
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public PerformanceMeasureExpectedSubcategoryOptionSimple()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public PerformanceMeasureExpectedSubcategoryOptionSimple(int performanceMeasureExpectedSubcategoryOptionID, int performanceMeasureExpectedID, int performanceMeasureSubcategoryOptionID, int performanceMeasureID, int performanceMeasureSubcategoryID)
            : this()
        {
            PerformanceMeasureExpectedSubcategoryOptionID = performanceMeasureExpectedSubcategoryOptionID;
            PerformanceMeasureExpectedID = performanceMeasureExpectedID;
            PerformanceMeasureSubcategoryOptionID = performanceMeasureSubcategoryOptionID;
            PerformanceMeasureID = performanceMeasureID;
            PerformanceMeasureSubcategoryID = performanceMeasureSubcategoryID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public PerformanceMeasureExpectedSubcategoryOptionSimple(PerformanceMeasureExpectedSubcategoryOption performanceMeasureExpectedSubcategoryOption)
            : this()
        {
            PerformanceMeasureExpectedSubcategoryOptionID = performanceMeasureExpectedSubcategoryOption.PerformanceMeasureExpectedSubcategoryOptionID;
            PerformanceMeasureExpectedID = performanceMeasureExpectedSubcategoryOption.PerformanceMeasureExpectedID;
            PerformanceMeasureSubcategoryOptionID = performanceMeasureExpectedSubcategoryOption.PerformanceMeasureSubcategoryOptionID;
            PerformanceMeasureID = performanceMeasureExpectedSubcategoryOption.PerformanceMeasureID;
            PerformanceMeasureSubcategoryID = performanceMeasureExpectedSubcategoryOption.PerformanceMeasureSubcategoryID;
        }

        public PerformanceMeasureExpectedSubcategoryOptionSimple(PerformanceMeasureValueSubcategoryOption performanceMeasureExpectedSubcategoryOption,
            PerformanceMeasureExpected performanceMeasureExpected)
            : this(
                performanceMeasureExpectedSubcategoryOption.PrimaryKey,
                performanceMeasureExpected.PerformanceMeasureExpectedID,
                performanceMeasureExpectedSubcategoryOption.PerformanceMeasureSubcategoryOptionID,
                performanceMeasureExpectedSubcategoryOption.PerformanceMeasureID,
                performanceMeasureExpectedSubcategoryOption.PerformanceMeasureSubcategoryID)
        {
        }

        public int PerformanceMeasureExpectedSubcategoryOptionID { get; set; }
        public int PerformanceMeasureExpectedID { get; set; }
        public int PerformanceMeasureSubcategoryOptionID { get; set; }
        public int PerformanceMeasureID { get; set; }
        public int PerformanceMeasureSubcategoryID { get; set; }
    }
}
