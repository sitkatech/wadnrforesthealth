//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectType
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectType>
    {
        public ProjectTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectTypePrimaryKey(ProjectType projectType) : base(projectType){}

        public static implicit operator ProjectTypePrimaryKey(int primaryKeyValue)
        {
            return new ProjectTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectTypePrimaryKey(ProjectType projectType)
        {
            return new ProjectTypePrimaryKey(projectType);
        }
    }
}