using System;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationAwardContractorInvoice : IAuditableEntity
    {
        public string AuditDescriptionString => GrantAllocationAwardContractorInvoiceDescription;

        public Money? GrantAllocationAwardContractorInvoiceForemanAmount
        {
            get
            {
                if(GrantAllocationAwardContractorInvoiceForemanHours.HasValue && GrantAllocationAwardContractorInvoiceForemanRate.HasValue)
                { 
                    //Foreman hours * foreman rate
                    return (decimal)GrantAllocationAwardContractorInvoiceForemanHours * (decimal)GrantAllocationAwardContractorInvoiceForemanRate;
                }

                return null;
            }
        }


        public Money? GrantAllocationAwardContractorInvoiceLaborAmount
        {
            get
            {
                if (GrantAllocationAwardContractorInvoiceLaborHours.HasValue && GrantAllocationAwardContractorInvoiceLaborRate.HasValue)
                {
                    //Labor hours * Labor rate
                    return (decimal)GrantAllocationAwardContractorInvoiceLaborHours * (decimal)GrantAllocationAwardContractorInvoiceLaborRate;
                }

                return null;
            }
        }

        public Money? GrantAllocationAwardContractorInvoiceGrappleAmount
        {
            get
            {
                if (GrantAllocationAwardContractorInvoiceGrappleHours.HasValue && GrantAllocationAwardContractorInvoiceGrappleRate.HasValue)
                {
                    //Grapple hours * Grapple rate
                    return (decimal)GrantAllocationAwardContractorInvoiceGrappleHours * (decimal)GrantAllocationAwardContractorInvoiceGrappleRate;
                }

                return null;
            }
        }

        public Money? GrantAllocationAwardContractorInvoiceMasticationAmount
        {
            get
            {
                if (GrantAllocationAwardContractorInvoiceMasticationHours.HasValue && GrantAllocationAwardContractorInvoiceMasticationRate.HasValue)
                {
                    //Mastication hours * Mastication rate
                    return (decimal)GrantAllocationAwardContractorInvoiceMasticationHours * (decimal)GrantAllocationAwardContractorInvoiceMasticationRate;
                }

                return null;
            }
        }

        public Money GrantAllocationAwardContractorInvoiceTotalWithoutTax
        {
            get
            {
                if (GrantAllocationAwardContractorInvoiceForemanAmount.HasValue && GrantAllocationAwardContractorInvoiceLaborAmount.HasValue && GrantAllocationAwardContractorInvoiceGrappleAmount.HasValue && GrantAllocationAwardContractorInvoiceMasticationAmount.HasValue)
                {
                    //add all amounts together (Foreman, Labor, Grapple, Mastication)
                    return GrantAllocationAwardContractorInvoiceForemanAmount.Value + GrantAllocationAwardContractorInvoiceLaborAmount.Value + GrantAllocationAwardContractorInvoiceGrappleAmount.Value +  GrantAllocationAwardContractorInvoiceMasticationAmount.Value;
                }else if (GrantAllocationAwardContractorInvoiceTotal.HasValue)
                {
                    return GrantAllocationAwardContractorInvoiceTotal.Value;
                }

                return 0m;
            }
        }

        public Money GrantAllocationAwardContractorInvoiceTaxAmount
        {
            get
            {
                if (GrantAllocationAwardContractorInvoiceTaxRate.HasValue)
                { 
                    //TaxRate * GrantAllocationAwardContractorInvoiceTotalWithoutTax
                    return GrantAllocationAwardContractorInvoiceTaxRate.Value * (decimal)GrantAllocationAwardContractorInvoiceTotalWithoutTax;
                }

                return 0m;
            }
        }

        public Money GrantAllocationAwardContractorInvoiceTotalWithTax
        {
            get
            {
                //GrantAllocationAwardContractorInvoiceTaxAmount + GrantAllocationAwardContractorInvoiceTotalWithoutTax
                return GrantAllocationAwardContractorInvoiceTaxAmount + GrantAllocationAwardContractorInvoiceTotalWithoutTax;
            }
        }
    }
}