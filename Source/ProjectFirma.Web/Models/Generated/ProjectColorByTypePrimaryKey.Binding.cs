//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectColorByType
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectColorByTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectColorByType>
    {
        public ProjectColorByTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectColorByTypePrimaryKey(ProjectColorByType projectColorByType) : base(projectColorByType){}

        public static implicit operator ProjectColorByTypePrimaryKey(int primaryKeyValue)
        {
            return new ProjectColorByTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectColorByTypePrimaryKey(ProjectColorByType projectColorByType)
        {
            return new ProjectColorByTypePrimaryKey(projectColorByType);
        }
    }
}