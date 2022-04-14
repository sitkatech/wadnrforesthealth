//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PclVectorRanked
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PclVectorRankedPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PclVectorRanked>
    {
        public PclVectorRankedPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PclVectorRankedPrimaryKey(PclVectorRanked pclVectorRanked) : base(pclVectorRanked){}

        public static implicit operator PclVectorRankedPrimaryKey(int primaryKeyValue)
        {
            return new PclVectorRankedPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PclVectorRankedPrimaryKey(PclVectorRanked pclVectorRanked)
        {
            return new PclVectorRankedPrimaryKey(pclVectorRanked);
        }
    }
}