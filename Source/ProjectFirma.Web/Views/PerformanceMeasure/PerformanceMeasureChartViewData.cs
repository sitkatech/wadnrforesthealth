﻿/*-----------------------------------------------------------------------
<copyright file="PerformanceMeasureChartViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.PerformanceMeasure
{
    public class PerformanceMeasureChartViewData : FirmaUserControlViewData
    {
        private const int DefaultHeight = 350;
        public readonly Models.PerformanceMeasure PerformanceMeasure;
        public readonly bool HyperlinkPerformanceMeasureName;
        public readonly List<GoogleChartJson> GoogleChartJsons;
        public readonly bool CanManagePerformanceMeasures;
        public readonly bool ShowLastUpdatedDate;
        public readonly string ChartTitle;
        public double? ChartTotal { get; }
        public string ChartTotalFormatted { get; }
        public string ChartTotalUnit { get;  }

        public readonly ViewGoogleChartViewData ViewGoogleChartViewData;

        public PerformanceMeasureChartViewData(Models.PerformanceMeasure performanceMeasure,
            int height,
            Person currentPerson,
            bool showLastUpdatedDate,
            bool fromPerformanceMeasureDetailPage,
            List<Models.Project> projects)
        {
            PerformanceMeasure = performanceMeasure;
            HyperlinkPerformanceMeasureName = !fromPerformanceMeasureDetailPage;

            GoogleChartJsons = performanceMeasure.GetGoogleChartJsonDictionary(projects);

            var performanceMeasureActuals = PerformanceMeasure.PerformanceMeasureActuals.Where(x => projects.Contains(x.Project)).ToList();
            ChartTotal = performanceMeasureActuals.Any() ? performanceMeasureActuals.Sum(x => x.ActualValue) : (double?) null;
            ChartTotalFormatted = PerformanceMeasure.MeasurementUnitType.DisplayValue(ChartTotal);
            ChartTotalUnit = PerformanceMeasure.MeasurementUnitType.LegendDisplayName;
            
            var currentPersonHasManagePermission = new PerformanceMeasureManageFeature().HasPermissionByPerson(currentPerson);
            CanManagePerformanceMeasures = currentPersonHasManagePermission && fromPerformanceMeasureDetailPage;

            ShowLastUpdatedDate = showLastUpdatedDate;
            ChartTitle = performanceMeasure.DisplayName;
            ViewGoogleChartViewData = new ViewGoogleChartViewData(GoogleChartJsons,
                ChartTitle,
                height,
                null,
                performanceMeasure.GetJavascriptSafeChartUniqueName(),
                CanManagePerformanceMeasures,
                SitkaRoute<GoogleChartController>.BuildUrlFromExpression(c => c.DownloadPerformanceMeasureChartData()),
                true,
                true,
                performanceMeasure,
                HyperlinkPerformanceMeasureName);
        }

        public PerformanceMeasureChartViewData(Models.PerformanceMeasure performanceMeasure, Person currentPerson, bool showLastUpdatedDate, List<Models.Project> projects) : this(
            performanceMeasure,
            DefaultHeight,
            currentPerson,
            showLastUpdatedDate,
            false, 
            projects)
        {
        }

        public PerformanceMeasureChartViewData(Models.PerformanceMeasure performanceMeasure, Person currentPerson, bool showLastUpdatedDate, bool showConfigureOption, List<Models.Project> projects) : this(
            performanceMeasure,
            DefaultHeight,
            currentPerson,
            showLastUpdatedDate,
            showConfigureOption, 
            projects)
        {
        }
    }
}
