//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantNoteInternal
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantNoteInternalPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<FundSourceNoteInternal>
    {
        public GrantNoteInternalPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantNoteInternalPrimaryKey(FundSourceNoteInternal fundSourceNoteInternal) : base(fundSourceNoteInternal){}

        public static implicit operator GrantNoteInternalPrimaryKey(int primaryKeyValue)
        {
            return new GrantNoteInternalPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantNoteInternalPrimaryKey(FundSourceNoteInternal fundSourceNoteInternal)
        {
            return new GrantNoteInternalPrimaryKey(fundSourceNoteInternal);
        }
    }
}