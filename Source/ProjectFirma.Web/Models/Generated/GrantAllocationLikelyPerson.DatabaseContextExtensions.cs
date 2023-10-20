//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationLikelyPerson]
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
        public static GrantAllocationLikelyPerson GetGrantAllocationLikelyPerson(this IQueryable<GrantAllocationLikelyPerson> grantAllocationLikelyPeople, int grantAllocationLikelyUserID)
        {
            var grantAllocationLikelyPerson = grantAllocationLikelyPeople.SingleOrDefault(x => x.GrantAllocationLikelyUserID == grantAllocationLikelyUserID);
            Check.RequireNotNullThrowNotFound(grantAllocationLikelyPerson, "GrantAllocationLikelyPerson", grantAllocationLikelyUserID);
            return grantAllocationLikelyPerson;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationLikelyPerson(this IQueryable<GrantAllocationLikelyPerson> grantAllocationLikelyPeople, List<int> grantAllocationLikelyUserIDList)
        {
            if(grantAllocationLikelyUserIDList.Any())
            {
                var grantAllocationLikelyPeopleInSourceCollectionToDelete = grantAllocationLikelyPeople.Where(x => grantAllocationLikelyUserIDList.Contains(x.GrantAllocationLikelyUserID));
                foreach (var grantAllocationLikelyPersonToDelete in grantAllocationLikelyPeopleInSourceCollectionToDelete.ToList())
                {
                    grantAllocationLikelyPersonToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationLikelyPerson(this IQueryable<GrantAllocationLikelyPerson> grantAllocationLikelyPeople, ICollection<GrantAllocationLikelyPerson> grantAllocationLikelyPeopleToDelete)
        {
            if(grantAllocationLikelyPeopleToDelete.Any())
            {
                var grantAllocationLikelyUserIDList = grantAllocationLikelyPeopleToDelete.Select(x => x.GrantAllocationLikelyUserID).ToList();
                var grantAllocationLikelyPeopleToDeleteFromSourceList = grantAllocationLikelyPeople.Where(x => grantAllocationLikelyUserIDList.Contains(x.GrantAllocationLikelyUserID)).ToList();

                foreach (var grantAllocationLikelyPersonToDelete in grantAllocationLikelyPeopleToDeleteFromSourceList)
                {
                    grantAllocationLikelyPersonToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationLikelyPerson(this IQueryable<GrantAllocationLikelyPerson> grantAllocationLikelyPeople, int grantAllocationLikelyUserID)
        {
            DeleteGrantAllocationLikelyPerson(grantAllocationLikelyPeople, new List<int> { grantAllocationLikelyUserID });
        }

        public static void DeleteGrantAllocationLikelyPerson(this IQueryable<GrantAllocationLikelyPerson> grantAllocationLikelyPeople, GrantAllocationLikelyPerson grantAllocationLikelyPersonToDelete)
        {
            DeleteGrantAllocationLikelyPerson(grantAllocationLikelyPeople, new List<GrantAllocationLikelyPerson> { grantAllocationLikelyPersonToDelete });
        }
    }
}