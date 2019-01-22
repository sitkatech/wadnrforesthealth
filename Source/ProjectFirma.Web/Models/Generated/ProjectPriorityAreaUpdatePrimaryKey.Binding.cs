//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectPriorityAreaUpdate
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectPriorityAreaUpdatePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectPriorityAreaUpdate>
    {
        public ProjectPriorityAreaUpdatePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectPriorityAreaUpdatePrimaryKey(ProjectPriorityAreaUpdate projectPriorityAreaUpdate) : base(projectPriorityAreaUpdate){}

        public static implicit operator ProjectPriorityAreaUpdatePrimaryKey(int primaryKeyValue)
        {
            return new ProjectPriorityAreaUpdatePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectPriorityAreaUpdatePrimaryKey(ProjectPriorityAreaUpdate projectPriorityAreaUpdate)
        {
            return new ProjectPriorityAreaUpdatePrimaryKey(projectPriorityAreaUpdate);
        }
    }
}