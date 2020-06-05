namespace ProjectFirma.Web.Models
{
    public class GisUploadAttemptWorkflowSectionSimple
    {
        public string SectionDisplayName { get; }
        public int SortOrder { get; }
        public bool HasCompletionStatus { get; }
        public GisUploadAttemptWorkflowSectionGrouping GisUploadAttemptWorkflowSectionGrouping { get; }
        public string SectionUrl { get; }
        public bool IsComplete { get; private set; }
        public bool SectionIsUpdated { get; }


        public GisUploadAttemptWorkflowSectionSimple(GisUploadAttemptWorkflowSection gisUploadAttemptWorkflowSection, string sectionUrl, bool isComplete, bool sectionIsUpdated, bool hasCompletionStatus)
        {
            SectionUrl = sectionUrl;
            IsComplete = isComplete;
            SectionIsUpdated = sectionIsUpdated;
            SectionDisplayName = gisUploadAttemptWorkflowSection.GisUploadAttemptWorkflowSectionDisplayName;
            SortOrder = gisUploadAttemptWorkflowSection.SortOrder;
            HasCompletionStatus = hasCompletionStatus;
            GisUploadAttemptWorkflowSectionGrouping = gisUploadAttemptWorkflowSection.GisUploadAttemptWorkflowSectionGrouping;
        }
    }
}