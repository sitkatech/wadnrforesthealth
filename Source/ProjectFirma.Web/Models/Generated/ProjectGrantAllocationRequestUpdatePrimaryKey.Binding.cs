//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectGrantAllocationRequestUpdate
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectGrantAllocationRequestUpdatePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectGrantAllocationRequestUpdate>
    {
        public ProjectGrantAllocationRequestUpdatePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectGrantAllocationRequestUpdatePrimaryKey(ProjectGrantAllocationRequestUpdate projectGrantAllocationRequestUpdate) : base(projectGrantAllocationRequestUpdate){}

        public static implicit operator ProjectGrantAllocationRequestUpdatePrimaryKey(int primaryKeyValue)
        {
            return new ProjectGrantAllocationRequestUpdatePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectGrantAllocationRequestUpdatePrimaryKey(ProjectGrantAllocationRequestUpdate projectGrantAllocationRequestUpdate)
        {
            return new ProjectGrantAllocationRequestUpdatePrimaryKey(projectGrantAllocationRequestUpdate);
        }
    }
}