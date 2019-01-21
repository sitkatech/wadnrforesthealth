//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantNote
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantNotePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantNote>
    {
        public GrantNotePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantNotePrimaryKey(GrantNote grantNote) : base(grantNote){}

        public static implicit operator GrantNotePrimaryKey(int primaryKeyValue)
        {
            return new GrantNotePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantNotePrimaryKey(GrantNote grantNote)
        {
            return new GrantNotePrimaryKey(grantNote);
        }
    }
}