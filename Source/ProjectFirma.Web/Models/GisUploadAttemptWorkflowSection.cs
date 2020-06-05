using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class GisUploadAttemptWorkflowSection
    {
        public abstract bool IsComplete(GisUploadAttempt gisUploadAttempt);
        public abstract string GetSectionUrl(GisUploadAttempt gisUploadAttempt);
    }



    public partial class GisUploadAttemptWorkflowSectionUploadGisFile
    {
        public override bool IsComplete(GisUploadAttempt gisUploadAttempt)
        {
            if (gisUploadAttempt == null)
            {
                return false;
            }
            //var basicsResults = new BasicsViewModel(project).GetValidationResults();
            //return !basicsResults.Any();


            return true;
        }

        public override string GetSectionUrl(GisUploadAttempt gisUploadAttempt)
        {
            return gisUploadAttempt != null ? SitkaRoute<GisProjectBulkUpdateController>.BuildUrlFromExpression(x => x.UploadGisFile(gisUploadAttempt.GisUploadAttemptID)) : null;
        }
    }


    public partial class GisUploadAttemptWorkflowSectionValidateFeatures
    {
        public override bool IsComplete(GisUploadAttempt gisUploadAttempt)
        {
            if (gisUploadAttempt == null)
            {
                return false;
            }
            //var basicsResults = new BasicsViewModel(project).GetValidationResults();
            //return !basicsResults.Any();


            return true;
        }

        public override string GetSectionUrl(GisUploadAttempt gisUploadAttempt)
        {
            return gisUploadAttempt != null ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditBasics(gisUploadAttempt.GisUploadAttemptID)) : null;
        }
    }

    public partial class GisUploadAttemptWorkflowSectionValidateMetadata
    {
        public override bool IsComplete(GisUploadAttempt gisUploadAttempt)
        {
            if (gisUploadAttempt == null)
            {
                return false;
            }
            //var basicsResults = new BasicsViewModel(project).GetValidationResults();
            //return !basicsResults.Any();


            return true;
        }

        public override string GetSectionUrl(GisUploadAttempt gisUploadAttempt)
        {
            return gisUploadAttempt != null ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditBasics(gisUploadAttempt.GisUploadAttemptID)) : null;
        }
    }

    public partial class GisUploadAttemptWorkflowSectionReviewMapping
    {
        public override bool IsComplete(GisUploadAttempt gisUploadAttempt)
        {
            if (gisUploadAttempt == null)
            {
                return false;
            }
            //var basicsResults = new BasicsViewModel(project).GetValidationResults();
            //return !basicsResults.Any();


            return true;
        }

        public override string GetSectionUrl(GisUploadAttempt gisUploadAttempt)
        {
            return gisUploadAttempt != null ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditBasics(gisUploadAttempt.GisUploadAttemptID)) : null;
        }
    }

    public partial class GisUploadAttemptWorkflowSectionRviewStagedImport
    {
        public override bool IsComplete(GisUploadAttempt gisUploadAttempt)
        {
            if (gisUploadAttempt == null)
            {
                return false;
            }
            //var basicsResults = new BasicsViewModel(project).GetValidationResults();
            //return !basicsResults.Any();


            return true;
        }

        public override string GetSectionUrl(GisUploadAttempt gisUploadAttempt)
        {
            return gisUploadAttempt != null ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditBasics(gisUploadAttempt.GisUploadAttemptID)) : null;
        }
    }

}