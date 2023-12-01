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
        public static GrantAllocationLikelyPerson GetGrantAllocationLikelyPerson(this IQueryable<GrantAllocationLikelyPerson> grantAllocationLikelyPeople, int grantAllocationLikelyPersonID)
        {
            var grantAllocationLikelyPerson = grantAllocationLikelyPeople.SingleOrDefault(x => x.GrantAllocationLikelyPersonID == grantAllocationLikelyPersonID);
            Check.RequireNotNullThrowNotFound(grantAllocationLikelyPerson, "GrantAllocationLikelyPerson", grantAllocationLikelyPersonID);
            return grantAllocationLikelyPerson;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationLikelyPerson(this IQueryable<GrantAllocationLikelyPerson> grantAllocationLikelyPeople, List<int> grantAllocationLikelyPersonIDList)
        {
            if(grantAllocationLikelyPersonIDList.Any())
            {
                var grantAllocationLikelyPeopleInSourceCollectionToDelete = grantAllocationLikelyPeople.Where(x => grantAllocationLikelyPersonIDList.Contains(x.GrantAllocationLikelyPersonID));
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
                var grantAllocationLikelyPersonIDList = grantAllocationLikelyPeopleToDelete.Select(x => x.GrantAllocationLikelyPersonID).ToList();
                var grantAllocationLikelyPeopleToDeleteFromSourceList = grantAllocationLikelyPeople.Where(x => grantAllocationLikelyPersonIDList.Contains(x.GrantAllocationLikelyPersonID)).ToList();

                foreach (var grantAllocationLikelyPersonToDelete in grantAllocationLikelyPeopleToDeleteFromSourceList)
                {
                    grantAllocationLikelyPersonToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationLikelyPerson(this IQueryable<GrantAllocationLikelyPerson> grantAllocationLikelyPeople, int grantAllocationLikelyPersonID)
        {
            DeleteGrantAllocationLikelyPerson(grantAllocationLikelyPeople, new List<int> { grantAllocationLikelyPersonID });
        }

        public static void DeleteGrantAllocationLikelyPerson(this IQueryable<GrantAllocationLikelyPerson> grantAllocationLikelyPeople, GrantAllocationLikelyPerson grantAllocationLikelyPersonToDelete)
        {
            DeleteGrantAllocationLikelyPerson(grantAllocationLikelyPeople, new List<GrantAllocationLikelyPerson> { grantAllocationLikelyPersonToDelete });
        }
    }
}