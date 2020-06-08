//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectTypeProjectCustomAttributeType
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectTypeProjectCustomAttributeTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectTypeProjectCustomAttributeType>
    {
        public ProjectTypeProjectCustomAttributeTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectTypeProjectCustomAttributeTypePrimaryKey(ProjectTypeProjectCustomAttributeType projectTypeProjectCustomAttributeType) : base(projectTypeProjectCustomAttributeType){}

        public static implicit operator ProjectTypeProjectCustomAttributeTypePrimaryKey(int primaryKeyValue)
        {
            return new ProjectTypeProjectCustomAttributeTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectTypeProjectCustomAttributeTypePrimaryKey(ProjectTypeProjectCustomAttributeType projectTypeProjectCustomAttributeType)
        {
            return new ProjectTypeProjectCustomAttributeTypePrimaryKey(projectTypeProjectCustomAttributeType);
        }
    }
}