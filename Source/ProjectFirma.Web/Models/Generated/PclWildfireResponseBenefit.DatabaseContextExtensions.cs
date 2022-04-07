//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PclWildfireResponseBenefit]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static PclWildfireResponseBenefit GetPclWildfireResponseBenefit(this IQueryable<PclWildfireResponseBenefit> pclWildfireResponseBenefits, int pclWildfireResponseBenefitID)
        {
            var pclWildfireResponseBenefit = pclWildfireResponseBenefits.SingleOrDefault(x => x.PclWildfireResponseBenefitID == pclWildfireResponseBenefitID);
            Check.RequireNotNullThrowNotFound(pclWildfireResponseBenefit, "PclWildfireResponseBenefit", pclWildfireResponseBenefitID);
            return pclWildfireResponseBenefit;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePclWildfireResponseBenefit(this IQueryable<PclWildfireResponseBenefit> pclWildfireResponseBenefits, List<int> pclWildfireResponseBenefitIDList)
        {
            if(pclWildfireResponseBenefitIDList.Any())
            {
                var pclWildfireResponseBenefitsInSourceCollectionToDelete = pclWildfireResponseBenefits.Where(x => pclWildfireResponseBenefitIDList.Contains(x.PclWildfireResponseBenefitID));
                foreach (var pclWildfireResponseBenefitToDelete in pclWildfireResponseBenefitsInSourceCollectionToDelete.ToList())
                {
                    pclWildfireResponseBenefitToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePclWildfireResponseBenefit(this IQueryable<PclWildfireResponseBenefit> pclWildfireResponseBenefits, ICollection<PclWildfireResponseBenefit> pclWildfireResponseBenefitsToDelete)
        {
            if(pclWildfireResponseBenefitsToDelete.Any())
            {
                var pclWildfireResponseBenefitIDList = pclWildfireResponseBenefitsToDelete.Select(x => x.PclWildfireResponseBenefitID).ToList();
                var pclWildfireResponseBenefitsToDeleteFromSourceList = pclWildfireResponseBenefits.Where(x => pclWildfireResponseBenefitIDList.Contains(x.PclWildfireResponseBenefitID)).ToList();

                foreach (var pclWildfireResponseBenefitToDelete in pclWildfireResponseBenefitsToDeleteFromSourceList)
                {
                    pclWildfireResponseBenefitToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePclWildfireResponseBenefit(this IQueryable<PclWildfireResponseBenefit> pclWildfireResponseBenefits, int pclWildfireResponseBenefitID)
        {
            DeletePclWildfireResponseBenefit(pclWildfireResponseBenefits, new List<int> { pclWildfireResponseBenefitID });
        }

        public static void DeletePclWildfireResponseBenefit(this IQueryable<PclWildfireResponseBenefit> pclWildfireResponseBenefits, PclWildfireResponseBenefit pclWildfireResponseBenefitToDelete)
        {
            DeletePclWildfireResponseBenefit(pclWildfireResponseBenefits, new List<PclWildfireResponseBenefit> { pclWildfireResponseBenefitToDelete });
        }
    }
}