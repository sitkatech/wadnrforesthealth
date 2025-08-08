using System;
using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectFundSourceAllocationRequestSimple
    {

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public ProjectFundSourceAllocationRequestSimple()
        {
        }
        
        public ProjectFundSourceAllocationRequestSimple(ProjectFundSourceAllocationRequest projectFundSourceAllocationRequest)
            : this()
        {
            ProjectID = projectFundSourceAllocationRequest.ProjectID;
            FundSourceAllocationID = projectFundSourceAllocationRequest.FundSourceAllocationID;
            TotalAmount = projectFundSourceAllocationRequest.TotalAmount;
            MatchAmount = projectFundSourceAllocationRequest.MatchAmount;
            PayAmount = projectFundSourceAllocationRequest.PayAmount;
        }

        public ProjectFundSourceAllocationRequestSimple(ProjectFundSourceAllocationRequestUpdate projectFundSourceAllocationRequestUpdate)
        {
            ProjectID = projectFundSourceAllocationRequestUpdate.ProjectUpdateBatchID;
            FundSourceAllocationID = projectFundSourceAllocationRequestUpdate.FundSourceAllocationID;
            TotalAmount = projectFundSourceAllocationRequestUpdate.TotalAmount;
            MatchAmount = projectFundSourceAllocationRequestUpdate.MatchAmount;
            PayAmount = projectFundSourceAllocationRequestUpdate.PayAmount;
        }

        public ProjectFundSourceAllocationRequest ToProjectFundSourceAllocationRequest(DateTime createDate, DateTime? updateDate, bool importedFromTabularData)
        {
            return new ProjectFundSourceAllocationRequest(ProjectID, FundSourceAllocationID, createDate, importedFromTabularData)
            {
                TotalAmount = TotalAmount
                , MatchAmount = MatchAmount
                , PayAmount = PayAmount
                , UpdateDate = updateDate
            };
        }

        public int ProjectID { get; set; }
        public int FundSourceAllocationID { get; set; }
        
        [ValidateMoneyInRangeForSqlServer]
        public decimal? TotalAmount { get; set; }

        [ValidateMoneyInRangeForSqlServer]
        public decimal? MatchAmount { get; set; }

        [ValidateMoneyInRangeForSqlServer]
        public decimal? PayAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool ImportedFromTabularData { get; set; }

      

        public ProjectFundSourceAllocationRequestUpdate ToProjectFundSourceAllocationRequestUpdate(DateTime createDate, DateTime? updateDate, bool importedFromTabularData)
        {
            return new ProjectFundSourceAllocationRequestUpdate(ProjectID, FundSourceAllocationID, createDate, importedFromTabularData)
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