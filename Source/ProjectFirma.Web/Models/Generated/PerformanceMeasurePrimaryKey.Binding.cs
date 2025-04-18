//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PerformanceMeasure
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PerformanceMeasurePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PerformanceMeasure>
    {
        public PerformanceMeasurePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PerformanceMeasurePrimaryKey(PerformanceMeasure performanceMeasure) : base(performanceMeasure){}

        public static implicit operator PerformanceMeasurePrimaryKey(int primaryKeyValue)
        {
            return new PerformanceMeasurePrimaryKey(primaryKeyValue);
        }

        public static implicit operator PerformanceMeasurePrimaryKey(PerformanceMeasure performanceMeasure)
        {
            return new PerformanceMeasurePrimaryKey(performanceMeasure);
        }
    }
}