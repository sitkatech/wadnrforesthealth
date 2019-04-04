namespace ProjectFirma.Web.Models
{
    public class ProjectGrantAllocationRequestSimple
    {

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public ProjectGrantAllocationRequestSimple()
        {
        }
        
        public ProjectGrantAllocationRequestSimple(ProjectGrantAllocationRequest projectGrantAllocationRequest)
            : this()
        {
            ProjectID = projectGrantAllocationRequest.ProjectID;
            GrantAllocationID = projectGrantAllocationRequest.GrantAllocationID;
            UnsecuredAmount = projectGrantAllocationRequest.UnsecuredAmount;
            SecuredAmount = projectGrantAllocationRequest.SecuredAmount;
        }

        public ProjectGrantAllocationRequestSimple(ProjectGrantAllocationRequestUpdate projectGrantAllocationRequestUpdate)
        {
            ProjectID = projectGrantAllocationRequestUpdate.ProjectUpdateBatchID;
            GrantAllocationID = projectGrantAllocationRequestUpdate.GrantAllocationID;
            UnsecuredAmount = projectGrantAllocationRequestUpdate.UnsecuredAmount;
            SecuredAmount = projectGrantAllocationRequestUpdate.SecuredAmount;
        }

        public ProjectGrantAllocationRequest ToProjectFundingSourceRequest()
        {
            return new ProjectGrantAllocationRequest(ProjectID, GrantAllocationID) {SecuredAmount = SecuredAmount, UnsecuredAmount = UnsecuredAmount};
        }

        public int ProjectID { get; set; }
        public int GrantAllocationID { get; set; }
        public decimal? SecuredAmount { get; set; }
        public decimal? UnsecuredAmount { get; set; }

        public ProjectGrantAllocationRequestUpdate ToProjectFundingSourceRequestUpdate()
        {
            return new ProjectGrantAllocationRequestUpdate(ProjectID, GrantAllocationID) {SecuredAmount = SecuredAmount, UnsecuredAmount = UnsecuredAmount};
        }

        public bool AreBothValuesZero()
        {
            return
                SecuredAmount == 0 && UnsecuredAmount == 0;
        }
    }
}