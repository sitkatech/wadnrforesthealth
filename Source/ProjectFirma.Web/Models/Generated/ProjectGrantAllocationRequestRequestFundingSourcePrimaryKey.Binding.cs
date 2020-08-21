//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectGrantAllocationRequestRequestFundingSource
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectGrantAllocationRequestRequestFundingSourcePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectGrantAllocationRequestRequestFundingSource>
    {
        public ProjectGrantAllocationRequestRequestFundingSourcePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectGrantAllocationRequestRequestFundingSourcePrimaryKey(ProjectGrantAllocationRequestRequestFundingSource projectGrantAllocationRequestRequestFundingSource) : base(projectGrantAllocationRequestRequestFundingSource){}

        public static implicit operator ProjectGrantAllocationRequestRequestFundingSourcePrimaryKey(int primaryKeyValue)
        {
            return new ProjectGrantAllocationRequestRequestFundingSourcePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectGrantAllocationRequestRequestFundingSourcePrimaryKey(ProjectGrantAllocationRequestRequestFundingSource projectGrantAllocationRequestRequestFundingSource)
        {
            return new ProjectGrantAllocationRequestRequestFundingSourcePrimaryKey(projectGrantAllocationRequestRequestFundingSource);
        }
    }
}