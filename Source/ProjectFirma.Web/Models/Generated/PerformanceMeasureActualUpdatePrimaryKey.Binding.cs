//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PerformanceMeasureActualUpdate
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PerformanceMeasureActualUpdatePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PerformanceMeasureActualUpdate>
    {
        public PerformanceMeasureActualUpdatePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PerformanceMeasureActualUpdatePrimaryKey(PerformanceMeasureActualUpdate performanceMeasureActualUpdate) : base(performanceMeasureActualUpdate){}

        public static implicit operator PerformanceMeasureActualUpdatePrimaryKey(int primaryKeyValue)
        {
            return new PerformanceMeasureActualUpdatePrimaryKey(primaryKeyValue);
        }

        public static implicit operator PerformanceMeasureActualUpdatePrimaryKey(PerformanceMeasureActualUpdate performanceMeasureActualUpdate)
        {
            return new PerformanceMeasureActualUpdatePrimaryKey(performanceMeasureActualUpdate);
        }
    }
}