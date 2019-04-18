//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantModificationNoteInternal
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantModificationNoteInternalPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantModificationNoteInternal>
    {
        public GrantModificationNoteInternalPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantModificationNoteInternalPrimaryKey(GrantModificationNoteInternal grantModificationNoteInternal) : base(grantModificationNoteInternal){}

        public static implicit operator GrantModificationNoteInternalPrimaryKey(int primaryKeyValue)
        {
            return new GrantModificationNoteInternalPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantModificationNoteInternalPrimaryKey(GrantModificationNoteInternal grantModificationNoteInternal)
        {
            return new GrantModificationNoteInternalPrimaryKey(grantModificationNoteInternal);
        }
    }
}