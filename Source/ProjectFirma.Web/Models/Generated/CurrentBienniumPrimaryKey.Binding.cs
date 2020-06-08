//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.CurrentBiennium
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class CurrentBienniumPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<CurrentBiennium>
    {
        public CurrentBienniumPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public CurrentBienniumPrimaryKey(CurrentBiennium currentBiennium) : base(currentBiennium){}

        public static implicit operator CurrentBienniumPrimaryKey(int primaryKeyValue)
        {
            return new CurrentBienniumPrimaryKey(primaryKeyValue);
        }

        public static implicit operator CurrentBienniumPrimaryKey(CurrentBiennium currentBiennium)
        {
            return new CurrentBienniumPrimaryKey(currentBiennium);
        }
    }
}