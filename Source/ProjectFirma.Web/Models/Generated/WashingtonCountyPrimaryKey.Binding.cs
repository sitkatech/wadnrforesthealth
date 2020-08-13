//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WashingtonCounty
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class WashingtonCountyPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WashingtonCounty>
    {
        public WashingtonCountyPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WashingtonCountyPrimaryKey(WashingtonCounty washingtonCounty) : base(washingtonCounty){}

        public static implicit operator WashingtonCountyPrimaryKey(int primaryKeyValue)
        {
            return new WashingtonCountyPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WashingtonCountyPrimaryKey(WashingtonCounty washingtonCounty)
        {
            return new WashingtonCountyPrimaryKey(washingtonCounty);
        }
    }
}