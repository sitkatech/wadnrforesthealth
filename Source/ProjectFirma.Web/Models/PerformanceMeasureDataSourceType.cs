using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public abstract partial class PerformanceMeasureDataSourceType
    {
        public virtual List<PerformanceMeasureReportedValue> GetReportedPerformanceMeasureValues(PerformanceMeasure performanceMeasure, List<Project> projects)
        {
            List<PerformanceMeasureActual> performanceMeasureActuals;
            if (projects == null || !projects.Any())
            {
                performanceMeasureActuals = HttpRequestStorage.DatabaseEntities.PerformanceMeasureActuals.Where(pmav => pmav.PerformanceMeasureID == performanceMeasure.PerformanceMeasureID).ToList();
            }
            else
            {
                var projectIDs = projects.Select(x => x.ProjectID).ToList();
                performanceMeasureActuals =
                    HttpRequestStorage.DatabaseEntities.PerformanceMeasureActuals.Where(
                        pmav => pmav.PerformanceMeasureID == performanceMeasure.PerformanceMeasureID && projectIDs.Contains(pmav.Project.ProjectID)).ToList();
            }
            var performanceMeasureReportedValues = PerformanceMeasureReportedValue.MakeFromList(performanceMeasureActuals);
            return performanceMeasureReportedValues.OrderByDescending(pma => pma.CalendarYear).ThenBy(pma => pma.ProjectName).ToList();
        }
    }
}
