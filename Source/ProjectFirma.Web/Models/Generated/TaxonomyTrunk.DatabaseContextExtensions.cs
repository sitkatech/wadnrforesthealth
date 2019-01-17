//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TaxonomyTrunk]
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
        public static TaxonomyTrunk GetTaxonomyTrunk(this IQueryable<TaxonomyTrunk> taxonomyTrunks, int taxonomyTrunkID)
        {
            var taxonomyTrunk = taxonomyTrunks.SingleOrDefault(x => x.TaxonomyTrunkID == taxonomyTrunkID);
            Check.RequireNotNullThrowNotFound(taxonomyTrunk, "TaxonomyTrunk", taxonomyTrunkID);
            return taxonomyTrunk;
        }

        public static void DeleteTaxonomyTrunk(this IQueryable<TaxonomyTrunk> taxonomyTrunks, List<int> taxonomyTrunkIDList)
        {
            if(taxonomyTrunkIDList.Any())
            {
                taxonomyTrunks.Where(x => taxonomyTrunkIDList.Contains(x.TaxonomyTrunkID)).Delete();
            }
        }

        public static void DeleteTaxonomyTrunk(this IQueryable<TaxonomyTrunk> taxonomyTrunks, ICollection<TaxonomyTrunk> taxonomyTrunksToDelete)
        {
            if(taxonomyTrunksToDelete.Any())
            {
                var taxonomyTrunkIDList = taxonomyTrunksToDelete.Select(x => x.TaxonomyTrunkID).ToList();
                taxonomyTrunks.Where(x => taxonomyTrunkIDList.Contains(x.TaxonomyTrunkID)).Delete();
            }
        }

        public static void DeleteTaxonomyTrunk(this IQueryable<TaxonomyTrunk> taxonomyTrunks, int taxonomyTrunkID)
        {
            DeleteTaxonomyTrunk(taxonomyTrunks, new List<int> { taxonomyTrunkID });
        }

        public static void DeleteTaxonomyTrunk(this IQueryable<TaxonomyTrunk> taxonomyTrunks, TaxonomyTrunk taxonomyTrunkToDelete)
        {
            DeleteTaxonomyTrunk(taxonomyTrunks, new List<TaxonomyTrunk> { taxonomyTrunkToDelete });
        }
    }
}