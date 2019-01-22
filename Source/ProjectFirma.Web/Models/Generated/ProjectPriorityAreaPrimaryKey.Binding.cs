//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectPriorityArea
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectPriorityAreaPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectPriorityArea>
    {
        public ProjectPriorityAreaPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectPriorityAreaPrimaryKey(ProjectPriorityArea projectPriorityArea) : base(projectPriorityArea){}

        public static implicit operator ProjectPriorityAreaPrimaryKey(int primaryKeyValue)
        {
            return new ProjectPriorityAreaPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectPriorityAreaPrimaryKey(ProjectPriorityArea projectPriorityArea)
        {
            return new ProjectPriorityAreaPrimaryKey(projectPriorityArea);
        }
    }
}