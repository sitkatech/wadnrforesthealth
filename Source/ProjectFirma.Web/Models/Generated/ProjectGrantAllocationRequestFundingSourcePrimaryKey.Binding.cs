//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectGrantAllocationRequestFundingSource
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectGrantAllocationRequestFundingSourcePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectGrantAllocationRequestFundingSource>
    {
        public ProjectGrantAllocationRequestFundingSourcePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectGrantAllocationRequestFundingSourcePrimaryKey(ProjectGrantAllocationRequestFundingSource projectGrantAllocationRequestFundingSource) : base(projectGrantAllocationRequestFundingSource){}

        public static implicit operator ProjectGrantAllocationRequestFundingSourcePrimaryKey(int primaryKeyValue)
        {
            return new ProjectGrantAllocationRequestFundingSourcePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectGrantAllocationRequestFundingSourcePrimaryKey(ProjectGrantAllocationRequestFundingSource projectGrantAllocationRequestFundingSource)
        {
            return new ProjectGrantAllocationRequestFundingSourcePrimaryKey(projectGrantAllocationRequestFundingSource);
        }
    }
}