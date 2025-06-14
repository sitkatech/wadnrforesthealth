namespace ProjectFirma.Web.Models
{
    public class ProjectSectionSimple
    {
        public ProjectSectionSimple(string sectionDisplayName, int sortOrder, bool hasCompletionStatus, ProjectWorkflowSectionGrouping projectWorkflowSectionGrouping, string sectionUrl, bool isComplete, bool sectionIsUpdated, bool isSectionRequired)
        {
            SectionDisplayName = sectionDisplayName;
            SortOrder = sortOrder;
            HasCompletionStatus = hasCompletionStatus;
            ProjectWorkflowSectionGrouping = projectWorkflowSectionGrouping;
            SectionUrl = sectionUrl;
            IsComplete = isComplete;
            SectionIsUpdated = sectionIsUpdated;
            IsSectionRequired = isSectionRequired;
        }

        public ProjectSectionSimple(ProjectUpdateSection projectUpdateSection, string sectionUrl, bool isComplete, bool sectionIsUpdated)
        {
            SectionUrl = sectionUrl;
            IsComplete = isComplete;
            SectionIsUpdated = sectionIsUpdated;
            SectionDisplayName = projectUpdateSection.ProjectUpdateSectionDisplayName;
            SortOrder = projectUpdateSection.SortOrder;
            HasCompletionStatus = projectUpdateSection.HasCompletionStatus;
            ProjectWorkflowSectionGrouping = projectUpdateSection.ProjectWorkflowSectionGrouping;
            IsSectionRequired = false;//cause we don't have this field on the update sections
        }

        public ProjectSectionSimple(ProjectCreateSection projectCreateSection, string sectionUrl, bool isComplete, bool sectionIsUpdated, bool hasCompletionStatus)
        {
            SectionUrl = sectionUrl;
            IsComplete = isComplete;
            SectionIsUpdated = sectionIsUpdated;
            SectionDisplayName = projectCreateSection.ProjectCreateSectionDisplayName;
            SortOrder = projectCreateSection.SortOrder;
            HasCompletionStatus = hasCompletionStatus;
            ProjectWorkflowSectionGrouping = projectCreateSection.ProjectWorkflowSectionGrouping;
            IsSectionRequired = projectCreateSection.IsSectionRequired;
        }

        public string SectionDisplayName { get; }
        public int SortOrder { get; }
        public bool HasCompletionStatus { get; }
        public ProjectWorkflowSectionGrouping ProjectWorkflowSectionGrouping { get; }
        public string SectionUrl { get; }
        public bool IsComplete { get; private set; }
        public bool SectionIsUpdated { get; }
        public bool IsSectionRequired { get; }
    }
}