//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TaxonomyBranch]
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
        public static TaxonomyBranch GetTaxonomyBranch(this IQueryable<TaxonomyBranch> taxonomyBranches, int taxonomyBranchID)
        {
            var taxonomyBranch = taxonomyBranches.SingleOrDefault(x => x.TaxonomyBranchID == taxonomyBranchID);
            Check.RequireNotNullThrowNotFound(taxonomyBranch, "TaxonomyBranch", taxonomyBranchID);
            return taxonomyBranch;
        }

        public static void DeleteTaxonomyBranch(this IQueryable<TaxonomyBranch> taxonomyBranches, List<int> taxonomyBranchIDList)
        {
            if(taxonomyBranchIDList.Any())
            {
                taxonomyBranches.Where(x => taxonomyBranchIDList.Contains(x.TaxonomyBranchID)).Delete();
            }
        }

        public static void DeleteTaxonomyBranch(this IQueryable<TaxonomyBranch> taxonomyBranches, ICollection<TaxonomyBranch> taxonomyBranchesToDelete)
        {
            if(taxonomyBranchesToDelete.Any())
            {
                var taxonomyBranchIDList = taxonomyBranchesToDelete.Select(x => x.TaxonomyBranchID).ToList();
                taxonomyBranches.Where(x => taxonomyBranchIDList.Contains(x.TaxonomyBranchID)).Delete();
            }
        }

        public static void DeleteTaxonomyBranch(this IQueryable<TaxonomyBranch> taxonomyBranches, int taxonomyBranchID)
        {
            DeleteTaxonomyBranch(taxonomyBranches, new List<int> { taxonomyBranchID });
        }

        public static void DeleteTaxonomyBranch(this IQueryable<TaxonomyBranch> taxonomyBranches, TaxonomyBranch taxonomyBranchToDelete)
        {
            DeleteTaxonomyBranch(taxonomyBranches, new List<TaxonomyBranch> { taxonomyBranchToDelete });
        }
    }
}