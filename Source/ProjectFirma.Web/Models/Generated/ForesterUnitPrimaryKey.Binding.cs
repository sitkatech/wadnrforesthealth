//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ForesterUnit
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ForesterUnitPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ForesterUnit>
    {
        public ForesterUnitPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ForesterUnitPrimaryKey(ForesterUnit foresterUnit) : base(foresterUnit){}

        public static implicit operator ForesterUnitPrimaryKey(int primaryKeyValue)
        {
            return new ForesterUnitPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ForesterUnitPrimaryKey(ForesterUnit foresterUnit)
        {
            return new ForesterUnitPrimaryKey(foresterUnit);
        }
    }
}