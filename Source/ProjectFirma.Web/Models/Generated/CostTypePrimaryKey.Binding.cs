//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.CostType
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class CostTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<CostType>
    {
        public CostTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public CostTypePrimaryKey(CostType costType) : base(costType){}

        public static implicit operator CostTypePrimaryKey(int primaryKeyValue)
        {
            return new CostTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator CostTypePrimaryKey(CostType costType)
        {
            return new CostTypePrimaryKey(costType);
        }
    }
}