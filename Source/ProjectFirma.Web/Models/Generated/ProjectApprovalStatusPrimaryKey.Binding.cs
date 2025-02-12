//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectApprovalStatus
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectApprovalStatusPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectApprovalStatus>
    {
        public ProjectApprovalStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectApprovalStatusPrimaryKey(ProjectApprovalStatus projectApprovalStatus) : base(projectApprovalStatus){}

        public static implicit operator ProjectApprovalStatusPrimaryKey(int primaryKeyValue)
        {
            return new ProjectApprovalStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectApprovalStatusPrimaryKey(ProjectApprovalStatus projectApprovalStatus)
        {
            return new ProjectApprovalStatusPrimaryKey(projectApprovalStatus);
        }
    }
}