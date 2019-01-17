//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TaxonomyLeafPerformanceMeasure]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TaxonomyLeafPerformanceMeasure GetTaxonomyLeafPerformanceMeasure(this IQueryable<TaxonomyLeafPerformanceMeasure> taxonomyLeafPerformanceMeasures, int taxonomyLeafPerformanceMeasureID)
        {
            var taxonomyLeafPerformanceMeasure = taxonomyLeafPerformanceMeasures.SingleOrDefault(x => x.TaxonomyLeafPerformanceMeasureID == taxonomyLeafPerformanceMeasureID);
            Check.RequireNotNullThrowNotFound(taxonomyLeafPerformanceMeasure, "TaxonomyLeafPerformanceMeasure", taxonomyLeafPerformanceMeasureID);
            return taxonomyLeafPerformanceMeasure;
        }

        public static void DeleteTaxonomyLeafPerformanceMeasure(this IQueryable<TaxonomyLeafPerformanceMeasure> taxonomyLeafPerformanceMeasures, List<int> taxonomyLeafPerformanceMeasureIDList)
        {
            if(taxonomyLeafPerformanceMeasureIDList.Any())
            {
                taxonomyLeafPerformanceMeasures.Where(x => taxonomyLeafPerformanceMeasureIDList.Contains(x.TaxonomyLeafPerformanceMeasureID)).Delete();
            }
        }

        public static void DeleteTaxonomyLeafPerformanceMeasure(this IQueryable<TaxonomyLeafPerformanceMeasure> taxonomyLeafPerformanceMeasures, ICollection<TaxonomyLeafPerformanceMeasure> taxonomyLeafPerformanceMeasuresToDelete)
        {
            if(taxonomyLeafPerformanceMeasuresToDelete.Any())
            {
                var taxonomyLeafPerformanceMeasureIDList = taxonomyLeafPerformanceMeasuresToDelete.Select(x => x.TaxonomyLeafPerformanceMeasureID).ToList();
                taxonomyLeafPerformanceMeasures.Where(x => taxonomyLeafPerformanceMeasureIDList.Contains(x.TaxonomyLeafPerformanceMeasureID)).Delete();
            }
        }

        public static void DeleteTaxonomyLeafPerformanceMeasure(this IQueryable<TaxonomyLeafPerformanceMeasure> taxonomyLeafPerformanceMeasures, int taxonomyLeafPerformanceMeasureID)
        {
            DeleteTaxonomyLeafPerformanceMeasure(taxonomyLeafPerformanceMeasures, new List<int> { taxonomyLeafPerformanceMeasureID });
        }

        public static void DeleteTaxonomyLeafPerformanceMeasure(this IQueryable<TaxonomyLeafPerformanceMeasure> taxonomyLeafPerformanceMeasures, TaxonomyLeafPerformanceMeasure taxonomyLeafPerformanceMeasureToDelete)
        {
            DeleteTaxonomyLeafPerformanceMeasure(taxonomyLeafPerformanceMeasures, new List<TaxonomyLeafPerformanceMeasure> { taxonomyLeafPerformanceMeasureToDelete });
        }
    }
}