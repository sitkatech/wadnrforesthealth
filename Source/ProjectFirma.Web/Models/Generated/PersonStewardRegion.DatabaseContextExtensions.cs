//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PersonStewardRegion]
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
        public static PersonStewardRegion GetPersonStewardRegion(this IQueryable<PersonStewardRegion> personStewardRegions, int personStewardRegionID)
        {
            var personStewardRegion = personStewardRegions.SingleOrDefault(x => x.PersonStewardRegionID == personStewardRegionID);
            Check.RequireNotNullThrowNotFound(personStewardRegion, "PersonStewardRegion", personStewardRegionID);
            return personStewardRegion;
        }

        public static void DeletePersonStewardRegion(this IQueryable<PersonStewardRegion> personStewardRegions, List<int> personStewardRegionIDList)
        {
            if(personStewardRegionIDList.Any())
            {
                personStewardRegions.Where(x => personStewardRegionIDList.Contains(x.PersonStewardRegionID)).Delete();
            }
        }

        public static void DeletePersonStewardRegion(this IQueryable<PersonStewardRegion> personStewardRegions, ICollection<PersonStewardRegion> personStewardRegionsToDelete)
        {
            if(personStewardRegionsToDelete.Any())
            {
                var personStewardRegionIDList = personStewardRegionsToDelete.Select(x => x.PersonStewardRegionID).ToList();
                personStewardRegions.Where(x => personStewardRegionIDList.Contains(x.PersonStewardRegionID)).Delete();
            }
        }

        public static void DeletePersonStewardRegion(this IQueryable<PersonStewardRegion> personStewardRegions, int personStewardRegionID)
        {
            DeletePersonStewardRegion(personStewardRegions, new List<int> { personStewardRegionID });
        }

        public static void DeletePersonStewardRegion(this IQueryable<PersonStewardRegion> personStewardRegions, PersonStewardRegion personStewardRegionToDelete)
        {
            DeletePersonStewardRegion(personStewardRegions, new List<PersonStewardRegion> { personStewardRegionToDelete });
        }
    }
}