//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectUpdateState
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectUpdateStatePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectUpdateState>
    {
        public ProjectUpdateStatePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectUpdateStatePrimaryKey(ProjectUpdateState projectUpdateState) : base(projectUpdateState){}

        public static implicit operator ProjectUpdateStatePrimaryKey(int primaryKeyValue)
        {
            return new ProjectUpdateStatePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectUpdateStatePrimaryKey(ProjectUpdateState projectUpdateState)
        {
            return new ProjectUpdateStatePrimaryKey(projectUpdateState);
        }
    }
}