//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectLocationStagingUpdate
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectLocationStagingUpdatePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectLocationStagingUpdate>
    {
        public ProjectLocationStagingUpdatePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectLocationStagingUpdatePrimaryKey(ProjectLocationStagingUpdate projectLocationStagingUpdate) : base(projectLocationStagingUpdate){}

        public static implicit operator ProjectLocationStagingUpdatePrimaryKey(int primaryKeyValue)
        {
            return new ProjectLocationStagingUpdatePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectLocationStagingUpdatePrimaryKey(ProjectLocationStagingUpdate projectLocationStagingUpdate)
        {
            return new ProjectLocationStagingUpdatePrimaryKey(projectLocationStagingUpdate);
        }
    }
}