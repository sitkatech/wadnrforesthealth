//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectGrantAllocationExpenditureUpdate
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectGrantAllocationExpenditureUpdatePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectGrantAllocationExpenditureUpdate>
    {
        public ProjectGrantAllocationExpenditureUpdatePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectGrantAllocationExpenditureUpdatePrimaryKey(ProjectGrantAllocationExpenditureUpdate projectGrantAllocationExpenditureUpdate) : base(projectGrantAllocationExpenditureUpdate){}

        public static implicit operator ProjectGrantAllocationExpenditureUpdatePrimaryKey(int primaryKeyValue)
        {
            return new ProjectGrantAllocationExpenditureUpdatePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectGrantAllocationExpenditureUpdatePrimaryKey(ProjectGrantAllocationExpenditureUpdate projectGrantAllocationExpenditureUpdate)
        {
            return new ProjectGrantAllocationExpenditureUpdatePrimaryKey(projectGrantAllocationExpenditureUpdate);
        }
    }
}