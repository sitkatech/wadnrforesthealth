//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PclBoundaryLine
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PclBoundaryLinePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PclBoundaryLine>
    {
        public PclBoundaryLinePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PclBoundaryLinePrimaryKey(PclBoundaryLine pclBoundaryLine) : base(pclBoundaryLine){}

        public static implicit operator PclBoundaryLinePrimaryKey(int primaryKeyValue)
        {
            return new PclBoundaryLinePrimaryKey(primaryKeyValue);
        }

        public static implicit operator PclBoundaryLinePrimaryKey(PclBoundaryLine pclBoundaryLine)
        {
            return new PclBoundaryLinePrimaryKey(pclBoundaryLine);
        }
    }
}