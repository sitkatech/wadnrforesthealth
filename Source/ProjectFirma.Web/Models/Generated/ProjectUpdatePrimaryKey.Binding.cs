//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectUpdate
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectUpdatePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectUpdate>
    {
        public ProjectUpdatePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectUpdatePrimaryKey(ProjectUpdate projectUpdate) : base(projectUpdate){}

        public static implicit operator ProjectUpdatePrimaryKey(int primaryKeyValue)
        {
            return new ProjectUpdatePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectUpdatePrimaryKey(ProjectUpdate projectUpdate)
        {
            return new ProjectUpdatePrimaryKey(projectUpdate);
        }
    }
}