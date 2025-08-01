using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;

namespace ProjectFirma.Web.Models
{
    public partial class AgreementGrantAllocation : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get { return this.FundSourceAllocation != null ? this.FundSourceAllocation.GrantAllocationName : "NullGrantAllocation"; }

        }

        public static List<AgreementGrantAllocation> OrderAgreementGrantAllocationsByYearPrefixedGrantNumbersThenEverythingElse(List<AgreementGrantAllocation> agreementGrantAllocations)
        {
            var interiorGrantAllocations = agreementGrantAllocations.Select(aga => aga.FundSourceAllocation).ToList();
            var interiorGrantAllocationInProperOrder = FundSourceAllocation.OrderGrantAllocationsByYearPrefixedGrantNumbersThenEverythingElse(interiorGrantAllocations);

            List<AgreementGrantAllocation> outgoingAgreementGrantAllocations = new List<AgreementGrantAllocation>();
            foreach (var grantAllocation in interiorGrantAllocationInProperOrder)
            {
                var currentSubset = agreementGrantAllocations.Where(aga => aga.GrantAllocationID == grantAllocation.GrantAllocationID).ToList();
                outgoingAgreementGrantAllocations.AddRange(currentSubset);
            }

            return outgoingAgreementGrantAllocations;
        }
    }
}