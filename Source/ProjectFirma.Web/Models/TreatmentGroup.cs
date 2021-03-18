using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class TreatmentGroup
    {
        public List<Treatment> Treatments { get; set; }

        public TreatmentArea TreatmentArea
        {
            get;
            set;
        }

        public GrantAllocationAwardLandownerCostShareLineItem GrantAllocationAwardLandownerCostShareLineItem
        {
            get;
            set;
        }

        public TreatmentGroup(TreatmentArea treatmentArea)
        {
            Treatments = treatmentArea.Treatments.ToList();
            TreatmentArea = treatmentArea;
        }

        public TreatmentGroup(GrantAllocationAwardLandownerCostShareLineItem grantAllocationAwardLandownerCostShareLineItem)
        {
            Treatments = grantAllocationAwardLandownerCostShareLineItem.Treatments.ToList();
            GrantAllocationAwardLandownerCostShareLineItem = grantAllocationAwardLandownerCostShareLineItem;
        }
    }
}