//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OrganizationTypeRelationshipType]
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
        public static OrganizationTypeRelationshipType GetOrganizationTypeRelationshipType(this IQueryable<OrganizationTypeRelationshipType> organizationTypeRelationshipTypes, int organizationTypeRelationshipTypeID)
        {
            var organizationTypeRelationshipType = organizationTypeRelationshipTypes.SingleOrDefault(x => x.OrganizationTypeRelationshipTypeID == organizationTypeRelationshipTypeID);
            Check.RequireNotNullThrowNotFound(organizationTypeRelationshipType, "OrganizationTypeRelationshipType", organizationTypeRelationshipTypeID);
            return organizationTypeRelationshipType;
        }

        public static void DeleteOrganizationTypeRelationshipType(this IQueryable<OrganizationTypeRelationshipType> organizationTypeRelationshipTypes, List<int> organizationTypeRelationshipTypeIDList)
        {
            if(organizationTypeRelationshipTypeIDList.Any())
            {
                organizationTypeRelationshipTypes.Where(x => organizationTypeRelationshipTypeIDList.Contains(x.OrganizationTypeRelationshipTypeID)).Delete();
            }
        }

        public static void DeleteOrganizationTypeRelationshipType(this IQueryable<OrganizationTypeRelationshipType> organizationTypeRelationshipTypes, ICollection<OrganizationTypeRelationshipType> organizationTypeRelationshipTypesToDelete)
        {
            if(organizationTypeRelationshipTypesToDelete.Any())
            {
                var organizationTypeRelationshipTypeIDList = organizationTypeRelationshipTypesToDelete.Select(x => x.OrganizationTypeRelationshipTypeID).ToList();
                organizationTypeRelationshipTypes.Where(x => organizationTypeRelationshipTypeIDList.Contains(x.OrganizationTypeRelationshipTypeID)).Delete();
            }
        }

        public static void DeleteOrganizationTypeRelationshipType(this IQueryable<OrganizationTypeRelationshipType> organizationTypeRelationshipTypes, int organizationTypeRelationshipTypeID)
        {
            DeleteOrganizationTypeRelationshipType(organizationTypeRelationshipTypes, new List<int> { organizationTypeRelationshipTypeID });
        }

        public static void DeleteOrganizationTypeRelationshipType(this IQueryable<OrganizationTypeRelationshipType> organizationTypeRelationshipTypes, OrganizationTypeRelationshipType organizationTypeRelationshipTypeToDelete)
        {
            DeleteOrganizationTypeRelationshipType(organizationTypeRelationshipTypes, new List<OrganizationTypeRelationshipType> { organizationTypeRelationshipTypeToDelete });
        }
    }
}