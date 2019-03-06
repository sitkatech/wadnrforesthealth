//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationNoteInternal
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationNoteInternalPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationNoteInternal>
    {
        public GrantAllocationNoteInternalPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationNoteInternalPrimaryKey(GrantAllocationNoteInternal grantAllocationNoteInternal) : base(grantAllocationNoteInternal){}

        public static implicit operator GrantAllocationNoteInternalPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationNoteInternalPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationNoteInternalPrimaryKey(GrantAllocationNoteInternal grantAllocationNoteInternal)
        {
            return new GrantAllocationNoteInternalPrimaryKey(grantAllocationNoteInternal);
        }
    }
}