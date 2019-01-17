//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OrganizationBoundaryStaging]
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
        public static OrganizationBoundaryStaging GetOrganizationBoundaryStaging(this IQueryable<OrganizationBoundaryStaging> organizationBoundaryStagings, int organizationBoundaryStagingID)
        {
            var organizationBoundaryStaging = organizationBoundaryStagings.SingleOrDefault(x => x.OrganizationBoundaryStagingID == organizationBoundaryStagingID);
            Check.RequireNotNullThrowNotFound(organizationBoundaryStaging, "OrganizationBoundaryStaging", organizationBoundaryStagingID);
            return organizationBoundaryStaging;
        }

        public static void DeleteOrganizationBoundaryStaging(this IQueryable<OrganizationBoundaryStaging> organizationBoundaryStagings, List<int> organizationBoundaryStagingIDList)
        {
            if(organizationBoundaryStagingIDList.Any())
            {
                organizationBoundaryStagings.Where(x => organizationBoundaryStagingIDList.Contains(x.OrganizationBoundaryStagingID)).Delete();
            }
        }

        public static void DeleteOrganizationBoundaryStaging(this IQueryable<OrganizationBoundaryStaging> organizationBoundaryStagings, ICollection<OrganizationBoundaryStaging> organizationBoundaryStagingsToDelete)
        {
            if(organizationBoundaryStagingsToDelete.Any())
            {
                var organizationBoundaryStagingIDList = organizationBoundaryStagingsToDelete.Select(x => x.OrganizationBoundaryStagingID).ToList();
                organizationBoundaryStagings.Where(x => organizationBoundaryStagingIDList.Contains(x.OrganizationBoundaryStagingID)).Delete();
            }
        }

        public static void DeleteOrganizationBoundaryStaging(this IQueryable<OrganizationBoundaryStaging> organizationBoundaryStagings, int organizationBoundaryStagingID)
        {
            DeleteOrganizationBoundaryStaging(organizationBoundaryStagings, new List<int> { organizationBoundaryStagingID });
        }

        public static void DeleteOrganizationBoundaryStaging(this IQueryable<OrganizationBoundaryStaging> organizationBoundaryStagings, OrganizationBoundaryStaging organizationBoundaryStagingToDelete)
        {
            DeleteOrganizationBoundaryStaging(organizationBoundaryStagings, new List<OrganizationBoundaryStaging> { organizationBoundaryStagingToDelete });
        }
    }
}