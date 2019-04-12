using System;
using LtInfo.Common;

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
            TotalAmount = projectGrantAllocationRequest.TotalAmount;
        }

        public ProjectGrantAllocationRequestSimple(ProjectGrantAllocationRequestUpdate projectGrantAllocationRequestUpdate)
        {
            ProjectID = projectGrantAllocationRequestUpdate.ProjectUpdateBatchID;
            GrantAllocationID = projectGrantAllocationRequestUpdate.GrantAllocationID;
            UnsecuredAmount = projectGrantAllocationRequestUpdate.UnsecuredAmount;
            SecuredAmount = projectGrantAllocationRequestUpdate.SecuredAmount;
            // TODO Add TotalAmount to this once it's in the DB
        }

        public ProjectGrantAllocationRequest ToProjectGrantAllocationRequest()
        {
            return new ProjectGrantAllocationRequest(ProjectID, GrantAllocationID) {SecuredAmount = SecuredAmount, UnsecuredAmount = UnsecuredAmount, TotalAmount = TotalAmount};
        }

        public int ProjectID { get; set; }
        public int GrantAllocationID { get; set; }
        
        [ValidateMoneyInRangeForSqlServer]
        public decimal? SecuredAmount { get; set; }

        [ValidateMoneyInRangeForSqlServer]
        public decimal? UnsecuredAmount { get; set; }

        [ValidateMoneyInRangeForSqlServer]
        public decimal? TotalAmount { get; set; }

        public ProjectGrantAllocationRequestUpdate ToProjectGrantAllocationRequestUpdate()
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