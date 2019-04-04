namespace ProjectFirma.Web.Models
{
    public class ProjectFundingSourceRequestSimple
    {

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public ProjectFundingSourceRequestSimple()
        {
        }
        
        public ProjectFundingSourceRequestSimple(ProjectFundingSourceRequest projectFundingSourceRequest)
            : this()
        {
            ProjectID = projectFundingSourceRequest.ProjectID;
            GrantAllocationID = projectFundingSourceRequest.GrantAllocationID;
            UnsecuredAmount = projectFundingSourceRequest.UnsecuredAmount;
            SecuredAmount = projectFundingSourceRequest.SecuredAmount;
        }

        public ProjectFundingSourceRequestSimple(ProjectFundingSourceRequestUpdate projectFundingSourceRequestUpdate)
        {
            ProjectID = projectFundingSourceRequestUpdate.ProjectUpdateBatchID;
            GrantAllocationID = projectFundingSourceRequestUpdate.GrantAllocationID;
            UnsecuredAmount = projectFundingSourceRequestUpdate.UnsecuredAmount;
            SecuredAmount = projectFundingSourceRequestUpdate.SecuredAmount;
        }

        public ProjectFundingSourceRequest ToProjectFundingSourceRequest()
        {
            return new ProjectFundingSourceRequest(ProjectID, GrantAllocationID){SecuredAmount = SecuredAmount, UnsecuredAmount = UnsecuredAmount};
        }

        public int ProjectID { get; set; }
        public int GrantAllocationID { get; set; }
        public decimal? SecuredAmount { get; set; }
        public decimal? UnsecuredAmount { get; set; }

        public ProjectFundingSourceRequestUpdate ToProjectFundingSourceRequestUpdate()
        {
            return new ProjectFundingSourceRequestUpdate(ProjectID, GrantAllocationID){SecuredAmount = SecuredAmount, UnsecuredAmount = UnsecuredAmount};
        }

        public bool AreBothValuesZero()
        {
            return
                SecuredAmount == 0 && UnsecuredAmount == 0;
        }
    }
}