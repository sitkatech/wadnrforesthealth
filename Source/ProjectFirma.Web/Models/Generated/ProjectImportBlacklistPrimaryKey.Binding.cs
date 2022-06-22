//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectImportBlacklist
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectImportBlacklistPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectImportBlacklist>
    {
        public ProjectImportBlacklistPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectImportBlacklistPrimaryKey(ProjectImportBlacklist projectImportBlacklist) : base(projectImportBlacklist){}

        public static implicit operator ProjectImportBlacklistPrimaryKey(int primaryKeyValue)
        {
            return new ProjectImportBlacklistPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectImportBlacklistPrimaryKey(ProjectImportBlacklist projectImportBlacklist)
        {
            return new ProjectImportBlacklistPrimaryKey(projectImportBlacklist);
        }
    }
}