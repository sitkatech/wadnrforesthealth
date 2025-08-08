using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;

namespace ProjectFirma.Web.Models
{
    public partial class AgreementFundSourceAllocation : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get { return this.FundSourceAllocation != null ? this.FundSourceAllocation.FundSourceAllocationName : "NullFundSourceAllocation"; }

        }

        public static List<AgreementFundSourceAllocation> OrderAgreementFundSourceAllocationsByYearPrefixedFundSourceNumbersThenEverythingElse(List<AgreementFundSourceAllocation> agreementFundSourceAllocations)
        {
            var interiorFundSourceAllocations = agreementFundSourceAllocations.Select(aga => aga.FundSourceAllocation).ToList();
            var interiorFundSourceAllocationInProperOrder = FundSourceAllocation.OrderFundSourceAllocationsByYearPrefixedFundSourceNumbersThenEverythingElse(interiorFundSourceAllocations);

            List<AgreementFundSourceAllocation> outgoingAgreementFundSourceAllocations = new List<AgreementFundSourceAllocation>();
            foreach (var fundSourceAllocation in interiorFundSourceAllocationInProperOrder)
            {
                var currentSubset = agreementFundSourceAllocations.Where(aga => aga.FundSourceAllocationID == fundSourceAllocation.FundSourceAllocationID).ToList();
                outgoingAgreementFundSourceAllocations.AddRange(currentSubset);
            }

            return outgoingAgreementFundSourceAllocations;
        }
    }
}