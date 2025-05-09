//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectCostType
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectCostTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectCostType>
    {
        public ProjectCostTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectCostTypePrimaryKey(ProjectCostType projectCostType) : base(projectCostType){}

        public static implicit operator ProjectCostTypePrimaryKey(int primaryKeyValue)
        {
            return new ProjectCostTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectCostTypePrimaryKey(ProjectCostType projectCostType)
        {
            return new ProjectCostTypePrimaryKey(projectCostType);
        }
    }
}