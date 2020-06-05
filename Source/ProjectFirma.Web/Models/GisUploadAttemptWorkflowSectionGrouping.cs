using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class GisUploadAttemptWorkflowSectionGrouping
    {
        public abstract List<GisUploadAttemptWorkflowSectionSimple> GetGisUploadAttemptWorkflowSections(GisUploadAttempt gisUploadAttempt, bool ignoreStatus);

        protected List<GisUploadAttemptWorkflowSectionSimple> GetGisUploadAttemptWorkflowSectionsImpl(GisUploadAttempt gisUploadAttempt,
            List<GisUploadAttemptWorkflowSection> gisUploadAttemptWorkflowSections, bool ignoreStatus)
        {
            return gisUploadAttemptWorkflowSections.Select(x => new GisUploadAttemptWorkflowSectionSimple(x, x.GetSectionUrl(gisUploadAttempt), !ignoreStatus && x.IsComplete(gisUploadAttempt), false, gisUploadAttempt != null && x.HasCompletionStatus)).OrderBy(x => x.SortOrder).ToList();
        }

    }


    public partial class GisUploadAttemptWorkflowSectionGroupingGeospatialValidation
    {
        public override List<GisUploadAttemptWorkflowSectionSimple> GetGisUploadAttemptWorkflowSections(GisUploadAttempt gisUploadAttempt, bool ignoreStatus)
        {
            return GetGisUploadAttemptWorkflowSectionsImpl(gisUploadAttempt, GisUploadAttemptWorkflowSections, ignoreStatus);
        }
       
    }

    public partial class GisUploadAttemptWorkflowSectionGroupingMetadataMapping
    {
        public override List<GisUploadAttemptWorkflowSectionSimple> GetGisUploadAttemptWorkflowSections(GisUploadAttempt gisUploadAttempt, bool ignoreStatus)
        {
            return GetGisUploadAttemptWorkflowSectionsImpl(gisUploadAttempt, GisUploadAttemptWorkflowSections, ignoreStatus);
        }

    }
}