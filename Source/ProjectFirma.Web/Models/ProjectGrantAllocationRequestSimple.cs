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
            MatchAmount = projectGrantAllocationRequest.MatchAmount;
            PayAmount = projectGrantAllocationRequest.PayAmount;
        }

        public ProjectGrantAllocationRequestSimple(ProjectGrantAllocationRequestUpdate projectGrantAllocationRequestUpdate)
        {
            ProjectID = projectGrantAllocationRequestUpdate.ProjectUpdateBatchID;
            GrantAllocationID = projectGrantAllocationRequestUpdate.GrantAllocationID;
            TotalAmount = projectGrantAllocationRequestUpdate.TotalAmount;
            MatchAmount = projectGrantAllocationRequestUpdate.MatchAmount;
            PayAmount = projectGrantAllocationRequestUpdate.PayAmount;
        }

        public ProjectGrantAllocationRequest ToProjectGrantAllocationRequest(DateTime createDate, DateTime? updateDate, bool importedFromTabularData)
        {
            return new ProjectGrantAllocationRequest(ProjectID, GrantAllocationID, createDate, importedFromTabularData)
            {
                TotalAmount = TotalAmount
                , MatchAmount = MatchAmount
                , PayAmount = PayAmount
                , UpdateDate = updateDate
            };
        }

        public int ProjectID { get; set; }
        public int GrantAllocationID { get; set; }
        
        [ValidateMoneyInRangeForSqlServer]
        public decimal? TotalAmount { get; set; }

        [ValidateMoneyInRangeForSqlServer]
        public decimal? MatchAmount { get; set; }

        [ValidateMoneyInRangeForSqlServer]
        public decimal? PayAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool ImportedFromTabularData { get; set; }

      

        public ProjectGrantAllocationRequestUpdate ToProjectGrantAllocationRequestUpdate(DateTime createDate, DateTime? updateDate, bool importedFromTabularData)
        {
            return new ProjectGrantAllocationRequestUpdate(ProjectID, GrantAllocationID, createDate, importedFromTabularData)
            {
                TotalAmount = TotalAmount
                , MatchAmount = MatchAmount
                , PayAmount = PayAmount
                , UpdateDate = updateDate
            };
        }

        //public bool AreBothValuesZero()
        //{
        //    return
        //        SecuredAmount == 0 && UnsecuredAmount == 0;
        //}
    }
}