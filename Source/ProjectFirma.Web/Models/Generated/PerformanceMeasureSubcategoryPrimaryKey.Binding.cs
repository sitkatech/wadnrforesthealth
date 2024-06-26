//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PerformanceMeasureSubcategory
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PerformanceMeasureSubcategoryPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PerformanceMeasureSubcategory>
    {
        public PerformanceMeasureSubcategoryPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PerformanceMeasureSubcategoryPrimaryKey(PerformanceMeasureSubcategory performanceMeasureSubcategory) : base(performanceMeasureSubcategory){}

        public static implicit operator PerformanceMeasureSubcategoryPrimaryKey(int primaryKeyValue)
        {
            return new PerformanceMeasureSubcategoryPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PerformanceMeasureSubcategoryPrimaryKey(PerformanceMeasureSubcategory performanceMeasureSubcategory)
        {
            return new PerformanceMeasureSubcategoryPrimaryKey(performanceMeasureSubcategory);
        }
    }
}