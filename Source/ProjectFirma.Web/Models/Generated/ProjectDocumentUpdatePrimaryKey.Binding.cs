//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectDocumentUpdate
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectDocumentUpdatePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectDocumentUpdate>
    {
        public ProjectDocumentUpdatePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectDocumentUpdatePrimaryKey(ProjectDocumentUpdate projectDocumentUpdate) : base(projectDocumentUpdate){}

        public static implicit operator ProjectDocumentUpdatePrimaryKey(int primaryKeyValue)
        {
            return new ProjectDocumentUpdatePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectDocumentUpdatePrimaryKey(ProjectDocumentUpdate projectDocumentUpdate)
        {
            return new ProjectDocumentUpdatePrimaryKey(projectDocumentUpdate);
        }
    }
}