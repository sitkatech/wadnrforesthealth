//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantNoteInternal
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantNoteInternalPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantNoteInternal>
    {
        public GrantNoteInternalPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantNoteInternalPrimaryKey(GrantNoteInternal grantNoteInternal) : base(grantNoteInternal){}

        public static implicit operator GrantNoteInternalPrimaryKey(int primaryKeyValue)
        {
            return new GrantNoteInternalPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantNoteInternalPrimaryKey(GrantNoteInternal grantNoteInternal)
        {
            return new GrantNoteInternalPrimaryKey(grantNoteInternal);
        }
    }
}