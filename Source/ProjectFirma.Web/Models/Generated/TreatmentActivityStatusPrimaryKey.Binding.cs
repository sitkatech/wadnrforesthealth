//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentActivityStatus
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class TreatmentActivityStatusPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentActivityStatus>
    {
        public TreatmentActivityStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentActivityStatusPrimaryKey(TreatmentActivityStatus treatmentActivityStatus) : base(treatmentActivityStatus){}

        public static implicit operator TreatmentActivityStatusPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentActivityStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentActivityStatusPrimaryKey(TreatmentActivityStatus treatmentActivityStatus)
        {
            return new TreatmentActivityStatusPrimaryKey(treatmentActivityStatus);
        }
    }
}