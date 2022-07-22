//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ForesterRole]
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
        public static ForesterRole GetForesterRole(this IQueryable<ForesterRole> foresterRoles, int foresterRoleID)
        {
            var foresterRole = foresterRoles.SingleOrDefault(x => x.ForesterRoleID == foresterRoleID);
            Check.RequireNotNullThrowNotFound(foresterRole, "ForesterRole", foresterRoleID);
            return foresterRole;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteForesterRole(this IQueryable<ForesterRole> foresterRoles, List<int> foresterRoleIDList)
        {
            if(foresterRoleIDList.Any())
            {
                var foresterRolesInSourceCollectionToDelete = foresterRoles.Where(x => foresterRoleIDList.Contains(x.ForesterRoleID));
                foreach (var foresterRoleToDelete in foresterRolesInSourceCollectionToDelete.ToList())
                {
                    foresterRoleToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteForesterRole(this IQueryable<ForesterRole> foresterRoles, ICollection<ForesterRole> foresterRolesToDelete)
        {
            if(foresterRolesToDelete.Any())
            {
                var foresterRoleIDList = foresterRolesToDelete.Select(x => x.ForesterRoleID).ToList();
                var foresterRolesToDeleteFromSourceList = foresterRoles.Where(x => foresterRoleIDList.Contains(x.ForesterRoleID)).ToList();

                foreach (var foresterRoleToDelete in foresterRolesToDeleteFromSourceList)
                {
                    foresterRoleToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteForesterRole(this IQueryable<ForesterRole> foresterRoles, int foresterRoleID)
        {
            DeleteForesterRole(foresterRoles, new List<int> { foresterRoleID });
        }

        public static void DeleteForesterRole(this IQueryable<ForesterRole> foresterRoles, ForesterRole foresterRoleToDelete)
        {
            DeleteForesterRole(foresterRoles, new List<ForesterRole> { foresterRoleToDelete });
        }
    }
}