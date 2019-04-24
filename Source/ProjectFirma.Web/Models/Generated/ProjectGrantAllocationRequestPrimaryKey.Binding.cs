//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectGrantAllocationRequest
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectGrantAllocationRequestPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectGrantAllocationRequest>
    {
        public ProjectGrantAllocationRequestPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectGrantAllocationRequestPrimaryKey(ProjectGrantAllocationRequest projectGrantAllocationRequest) : base(projectGrantAllocationRequest){}

        public static implicit operator ProjectGrantAllocationRequestPrimaryKey(int primaryKeyValue)
        {
            return new ProjectGrantAllocationRequestPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectGrantAllocationRequestPrimaryKey(ProjectGrantAllocationRequest projectGrantAllocationRequest)
        {
            return new ProjectGrantAllocationRequestPrimaryKey(projectGrantAllocationRequest);
        }
    }
}