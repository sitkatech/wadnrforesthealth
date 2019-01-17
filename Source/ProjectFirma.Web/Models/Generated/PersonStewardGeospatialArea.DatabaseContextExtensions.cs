//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PersonStewardGeospatialArea]
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
        public static PersonStewardGeospatialArea GetPersonStewardGeospatialArea(this IQueryable<PersonStewardGeospatialArea> personStewardGeospatialAreas, int personStewardGeospatialAreaID)
        {
            var personStewardGeospatialArea = personStewardGeospatialAreas.SingleOrDefault(x => x.PersonStewardGeospatialAreaID == personStewardGeospatialAreaID);
            Check.RequireNotNullThrowNotFound(personStewardGeospatialArea, "PersonStewardGeospatialArea", personStewardGeospatialAreaID);
            return personStewardGeospatialArea;
        }

        public static void DeletePersonStewardGeospatialArea(this IQueryable<PersonStewardGeospatialArea> personStewardGeospatialAreas, List<int> personStewardGeospatialAreaIDList)
        {
            if(personStewardGeospatialAreaIDList.Any())
            {
                personStewardGeospatialAreas.Where(x => personStewardGeospatialAreaIDList.Contains(x.PersonStewardGeospatialAreaID)).Delete();
            }
        }

        public static void DeletePersonStewardGeospatialArea(this IQueryable<PersonStewardGeospatialArea> personStewardGeospatialAreas, ICollection<PersonStewardGeospatialArea> personStewardGeospatialAreasToDelete)
        {
            if(personStewardGeospatialAreasToDelete.Any())
            {
                var personStewardGeospatialAreaIDList = personStewardGeospatialAreasToDelete.Select(x => x.PersonStewardGeospatialAreaID).ToList();
                personStewardGeospatialAreas.Where(x => personStewardGeospatialAreaIDList.Contains(x.PersonStewardGeospatialAreaID)).Delete();
            }
        }

        public static void DeletePersonStewardGeospatialArea(this IQueryable<PersonStewardGeospatialArea> personStewardGeospatialAreas, int personStewardGeospatialAreaID)
        {
            DeletePersonStewardGeospatialArea(personStewardGeospatialAreas, new List<int> { personStewardGeospatialAreaID });
        }

        public static void DeletePersonStewardGeospatialArea(this IQueryable<PersonStewardGeospatialArea> personStewardGeospatialAreas, PersonStewardGeospatialArea personStewardGeospatialAreaToDelete)
        {
            DeletePersonStewardGeospatialArea(personStewardGeospatialAreas, new List<PersonStewardGeospatialArea> { personStewardGeospatialAreaToDelete });
        }
    }
}