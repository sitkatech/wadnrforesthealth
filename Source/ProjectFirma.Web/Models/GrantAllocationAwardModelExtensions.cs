using System;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantAllocationAwardModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.GrantAllocationAwardDetail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this GrantAllocationAward grantAllocationAward)
        {
            return DetailUrlTemplate.ParameterReplace(grantAllocationAward.GrantAllocationAwardID);
        }


        public static decimal GetInvoicedToDateByCostType(this GrantAllocationAward grantAllocationAward, CostType costType)
        {
            decimal invoicedToDate = 0;

            switch (costType.CostTypeID)
            {
                case (int)CostTypeEnum.Contractual:
                    invoicedToDate = grantAllocationAward.ContractualLineItemSum;
                    break;
                case (int)CostTypeEnum.IndirectCosts:
                    invoicedToDate = grantAllocationAward.IndirectCostTotal;
                    break;
                case (int)CostTypeEnum.Supplies:
                    invoicedToDate = grantAllocationAward.SuppliesLineItemAmountSum;
                    break;
                case (int)CostTypeEnum.Travel:
                    invoicedToDate = grantAllocationAward.TravelLineItemSum;
                    break;
                case (int)CostTypeEnum.Personnel:
                    invoicedToDate = grantAllocationAward.GrantAllocationAwardPersonnelAndBenefitsLineItems.Sum(pb => pb.GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyTotal);
                    break;
                case (int)CostTypeEnum.Benefits:
                    invoicedToDate = grantAllocationAward.GrantAllocationAwardPersonnelAndBenefitsLineItems.Sum(pb => pb.GrantAllocationAwardPersonnelAndBenefitsLineItemFringeTotal);
                    break;
                case (int)CostTypeEnum.Equipment:
                case (int)CostTypeEnum.Other:
                    break;
                default:
                    throw new Exception($"Unhandled CostTypeID {costType.CostTypeID} when trying to get the invoiced to date dollar amount for said cost type.");
            }

            return invoicedToDate;
        }

        public static decimal GetTotalInvoicedToDate(this GrantAllocationAward grantAllocationAward)
        {
            decimal totalInvoicedToDate = 0;

            foreach (var costType in CostType.GetLineItemCostTypes())
            {
                totalInvoicedToDate += grantAllocationAward.GetInvoicedToDateByCostType(costType);
            }

            return totalInvoicedToDate;
        }


    }
}
