//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PclWildfireResponseBenefit
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PclWildfireResponseBenefitPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PclWildfireResponseBenefit>
    {
        public PclWildfireResponseBenefitPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PclWildfireResponseBenefitPrimaryKey(PclWildfireResponseBenefit pclWildfireResponseBenefit) : base(pclWildfireResponseBenefit){}

        public static implicit operator PclWildfireResponseBenefitPrimaryKey(int primaryKeyValue)
        {
            return new PclWildfireResponseBenefitPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PclWildfireResponseBenefitPrimaryKey(PclWildfireResponseBenefit pclWildfireResponseBenefit)
        {
            return new PclWildfireResponseBenefitPrimaryKey(pclWildfireResponseBenefit);
        }
    }
}