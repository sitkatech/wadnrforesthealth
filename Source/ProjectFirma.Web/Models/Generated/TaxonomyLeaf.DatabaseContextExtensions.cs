//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TaxonomyLeaf]
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
        public static TaxonomyLeaf GetTaxonomyLeaf(this IQueryable<TaxonomyLeaf> taxonomyLeafs, int taxonomyLeafID)
        {
            var taxonomyLeaf = taxonomyLeafs.SingleOrDefault(x => x.TaxonomyLeafID == taxonomyLeafID);
            Check.RequireNotNullThrowNotFound(taxonomyLeaf, "TaxonomyLeaf", taxonomyLeafID);
            return taxonomyLeaf;
        }

        public static void DeleteTaxonomyLeaf(this IQueryable<TaxonomyLeaf> taxonomyLeafs, List<int> taxonomyLeafIDList)
        {
            if(taxonomyLeafIDList.Any())
            {
                taxonomyLeafs.Where(x => taxonomyLeafIDList.Contains(x.TaxonomyLeafID)).Delete();
            }
        }

        public static void DeleteTaxonomyLeaf(this IQueryable<TaxonomyLeaf> taxonomyLeafs, ICollection<TaxonomyLeaf> taxonomyLeafsToDelete)
        {
            if(taxonomyLeafsToDelete.Any())
            {
                var taxonomyLeafIDList = taxonomyLeafsToDelete.Select(x => x.TaxonomyLeafID).ToList();
                taxonomyLeafs.Where(x => taxonomyLeafIDList.Contains(x.TaxonomyLeafID)).Delete();
            }
        }

        public static void DeleteTaxonomyLeaf(this IQueryable<TaxonomyLeaf> taxonomyLeafs, int taxonomyLeafID)
        {
            DeleteTaxonomyLeaf(taxonomyLeafs, new List<int> { taxonomyLeafID });
        }

        public static void DeleteTaxonomyLeaf(this IQueryable<TaxonomyLeaf> taxonomyLeafs, TaxonomyLeaf taxonomyLeafToDelete)
        {
            DeleteTaxonomyLeaf(taxonomyLeafs, new List<TaxonomyLeaf> { taxonomyLeafToDelete });
        }
    }
}