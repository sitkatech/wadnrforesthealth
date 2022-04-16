//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PclLandscapeTreatmentPriority
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PclLandscapeTreatmentPriorityPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PclLandscapeTreatmentPriority>
    {
        public PclLandscapeTreatmentPriorityPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PclLandscapeTreatmentPriorityPrimaryKey(PclLandscapeTreatmentPriority pclLandscapeTreatmentPriority) : base(pclLandscapeTreatmentPriority){}

        public static implicit operator PclLandscapeTreatmentPriorityPrimaryKey(int primaryKeyValue)
        {
            return new PclLandscapeTreatmentPriorityPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PclLandscapeTreatmentPriorityPrimaryKey(PclLandscapeTreatmentPriority pclLandscapeTreatmentPriority)
        {
            return new PclLandscapeTreatmentPriorityPrimaryKey(pclLandscapeTreatmentPriority);
        }
    }
}