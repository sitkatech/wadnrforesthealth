//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationNote
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationNotePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationNote>
    {
        public GrantAllocationNotePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationNotePrimaryKey(GrantAllocationNote grantAllocationNote) : base(grantAllocationNote){}

        public static implicit operator GrantAllocationNotePrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationNotePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationNotePrimaryKey(GrantAllocationNote grantAllocationNote)
        {
            return new GrantAllocationNotePrimaryKey(grantAllocationNote);
        }
    }
}