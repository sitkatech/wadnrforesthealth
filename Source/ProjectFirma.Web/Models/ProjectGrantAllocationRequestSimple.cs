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
            TotalAmount = projectGrantAllocationRequest.TotalAmount;
        }

        public ProjectGrantAllocationRequestSimple(ProjectGrantAllocationRequestUpdate projectGrantAllocationRequestUpdate)
        {
            ProjectID = projectGrantAllocationRequestUpdate.ProjectUpdateBatchID;
            GrantAllocationID = projectGrantAllocationRequestUpdate.GrantAllocationID;
            TotalAmount = projectGrantAllocationRequestUpdate.TotalAmount;
        }

        public ProjectGrantAllocationRequest ToProjectGrantAllocationRequest()
        {
            return new ProjectGrantAllocationRequest(ProjectID, GrantAllocationID) {TotalAmount = TotalAmount};
        }

        public int ProjectID { get; set; }
        public int GrantAllocationID { get; set; }
        
        [ValidateMoneyInRangeForSqlServer]
        public decimal? TotalAmount { get; set; }

        public ProjectGrantAllocationRequestUpdate ToProjectGrantAllocationRequestUpdate()
        {
            return new ProjectGrantAllocationRequestUpdate(ProjectID, GrantAllocationID) {TotalAmount = TotalAmount};
        }

        //public bool AreBothValuesZero()
        //{
        //    return
        //        SecuredAmount == 0 && UnsecuredAmount == 0;
        //}
    }
}