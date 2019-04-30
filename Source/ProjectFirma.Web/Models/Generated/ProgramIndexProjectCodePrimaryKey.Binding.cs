//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProgramIndexProjectCode
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProgramIndexProjectCodePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProgramIndexProjectCode>
    {
        public ProgramIndexProjectCodePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProgramIndexProjectCodePrimaryKey(ProgramIndexProjectCode programIndexProjectCode) : base(programIndexProjectCode){}

        public static implicit operator ProgramIndexProjectCodePrimaryKey(int primaryKeyValue)
        {
            return new ProgramIndexProjectCodePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProgramIndexProjectCodePrimaryKey(ProgramIndexProjectCode programIndexProjectCode)
        {
            return new ProgramIndexProjectCodePrimaryKey(programIndexProjectCode);
        }
    }
}