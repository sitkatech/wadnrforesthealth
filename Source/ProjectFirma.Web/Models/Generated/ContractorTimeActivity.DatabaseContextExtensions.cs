//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ContractorTimeActivity]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ContractorTimeActivity GetContractorTimeActivity(this IQueryable<ContractorTimeActivity> contractorTimeActivities, int contractorTimeActivityID)
        {
            var contractorTimeActivity = contractorTimeActivities.SingleOrDefault(x => x.ContractorTimeActivityID == contractorTimeActivityID);
            Check.RequireNotNullThrowNotFound(contractorTimeActivity, "ContractorTimeActivity", contractorTimeActivityID);
            return contractorTimeActivity;
        }

        public static void DeleteContractorTimeActivity(this List<int> contractorTimeActivityIDList)
        {
            if(contractorTimeActivityIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllContractorTimeActivities.RemoveRange(HttpRequestStorage.DatabaseEntities.ContractorTimeActivities.Where(x => contractorTimeActivityIDList.Contains(x.ContractorTimeActivityID)));
            }
        }

        public static void DeleteContractorTimeActivity(this ICollection<ContractorTimeActivity> contractorTimeActivitiesToDelete)
        {
            if(contractorTimeActivitiesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllContractorTimeActivities.RemoveRange(contractorTimeActivitiesToDelete);
            }
        }

        public static void DeleteContractorTimeActivity(this int contractorTimeActivityID)
        {
            DeleteContractorTimeActivity(new List<int> { contractorTimeActivityID });
        }

        public static void DeleteContractorTimeActivity(this ContractorTimeActivity contractorTimeActivityToDelete)
        {
            DeleteContractorTimeActivity(new List<ContractorTimeActivity> { contractorTimeActivityToDelete });
        }
    }
}