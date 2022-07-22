//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ForesterWorkUnitPerson
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ForesterWorkUnitPersonPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ForesterWorkUnitPerson>
    {
        public ForesterWorkUnitPersonPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ForesterWorkUnitPersonPrimaryKey(ForesterWorkUnitPerson foresterWorkUnitPerson) : base(foresterWorkUnitPerson){}

        public static implicit operator ForesterWorkUnitPersonPrimaryKey(int primaryKeyValue)
        {
            return new ForesterWorkUnitPersonPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ForesterWorkUnitPersonPrimaryKey(ForesterWorkUnitPerson foresterWorkUnitPerson)
        {
            return new ForesterWorkUnitPersonPrimaryKey(foresterWorkUnitPerson);
        }
    }
}