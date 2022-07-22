//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ForesterWorkUnitPerson]
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
        public static ForesterWorkUnitPerson GetForesterWorkUnitPerson(this IQueryable<ForesterWorkUnitPerson> foresterWorkUnitPeople, int foresterWorkUnitPersonID)
        {
            var foresterWorkUnitPerson = foresterWorkUnitPeople.SingleOrDefault(x => x.ForesterWorkUnitPersonID == foresterWorkUnitPersonID);
            Check.RequireNotNullThrowNotFound(foresterWorkUnitPerson, "ForesterWorkUnitPerson", foresterWorkUnitPersonID);
            return foresterWorkUnitPerson;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteForesterWorkUnitPerson(this IQueryable<ForesterWorkUnitPerson> foresterWorkUnitPeople, List<int> foresterWorkUnitPersonIDList)
        {
            if(foresterWorkUnitPersonIDList.Any())
            {
                var foresterWorkUnitPeopleInSourceCollectionToDelete = foresterWorkUnitPeople.Where(x => foresterWorkUnitPersonIDList.Contains(x.ForesterWorkUnitPersonID));
                foreach (var foresterWorkUnitPersonToDelete in foresterWorkUnitPeopleInSourceCollectionToDelete.ToList())
                {
                    foresterWorkUnitPersonToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteForesterWorkUnitPerson(this IQueryable<ForesterWorkUnitPerson> foresterWorkUnitPeople, ICollection<ForesterWorkUnitPerson> foresterWorkUnitPeopleToDelete)
        {
            if(foresterWorkUnitPeopleToDelete.Any())
            {
                var foresterWorkUnitPersonIDList = foresterWorkUnitPeopleToDelete.Select(x => x.ForesterWorkUnitPersonID).ToList();
                var foresterWorkUnitPeopleToDeleteFromSourceList = foresterWorkUnitPeople.Where(x => foresterWorkUnitPersonIDList.Contains(x.ForesterWorkUnitPersonID)).ToList();

                foreach (var foresterWorkUnitPersonToDelete in foresterWorkUnitPeopleToDeleteFromSourceList)
                {
                    foresterWorkUnitPersonToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteForesterWorkUnitPerson(this IQueryable<ForesterWorkUnitPerson> foresterWorkUnitPeople, int foresterWorkUnitPersonID)
        {
            DeleteForesterWorkUnitPerson(foresterWorkUnitPeople, new List<int> { foresterWorkUnitPersonID });
        }

        public static void DeleteForesterWorkUnitPerson(this IQueryable<ForesterWorkUnitPerson> foresterWorkUnitPeople, ForesterWorkUnitPerson foresterWorkUnitPersonToDelete)
        {
            DeleteForesterWorkUnitPerson(foresterWorkUnitPeople, new List<ForesterWorkUnitPerson> { foresterWorkUnitPersonToDelete });
        }
    }
}