//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PodVectorRanked
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PodVectorRankedPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PodVectorRanked>
    {
        public PodVectorRankedPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PodVectorRankedPrimaryKey(PodVectorRanked podVectorRanked) : base(podVectorRanked){}

        public static implicit operator PodVectorRankedPrimaryKey(int primaryKeyValue)
        {
            return new PodVectorRankedPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PodVectorRankedPrimaryKey(PodVectorRanked podVectorRanked)
        {
            return new PodVectorRankedPrimaryKey(podVectorRanked);
        }
    }
}