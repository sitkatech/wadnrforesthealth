//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectTypePerformanceMeasure
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectTypePerformanceMeasurePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectTypePerformanceMeasure>
    {
        public ProjectTypePerformanceMeasurePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectTypePerformanceMeasurePrimaryKey(ProjectTypePerformanceMeasure projectTypePerformanceMeasure) : base(projectTypePerformanceMeasure){}

        public static implicit operator ProjectTypePerformanceMeasurePrimaryKey(int primaryKeyValue)
        {
            return new ProjectTypePerformanceMeasurePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectTypePerformanceMeasurePrimaryKey(ProjectTypePerformanceMeasure projectTypePerformanceMeasure)
        {
            return new ProjectTypePerformanceMeasurePrimaryKey(projectTypePerformanceMeasure);
        }
    }
}