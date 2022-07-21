//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ForesterUnit]
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
        public static ForesterUnit GetForesterUnit(this IQueryable<ForesterUnit> foresterUnits, int foresterUnitID)
        {
            var foresterUnit = foresterUnits.SingleOrDefault(x => x.ForesterUnitID == foresterUnitID);
            Check.RequireNotNullThrowNotFound(foresterUnit, "ForesterUnit", foresterUnitID);
            return foresterUnit;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteForesterUnit(this IQueryable<ForesterUnit> foresterUnits, List<int> foresterUnitIDList)
        {
            if(foresterUnitIDList.Any())
            {
                var foresterUnitsInSourceCollectionToDelete = foresterUnits.Where(x => foresterUnitIDList.Contains(x.ForesterUnitID));
                foreach (var foresterUnitToDelete in foresterUnitsInSourceCollectionToDelete.ToList())
                {
                    foresterUnitToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteForesterUnit(this IQueryable<ForesterUnit> foresterUnits, ICollection<ForesterUnit> foresterUnitsToDelete)
        {
            if(foresterUnitsToDelete.Any())
            {
                var foresterUnitIDList = foresterUnitsToDelete.Select(x => x.ForesterUnitID).ToList();
                var foresterUnitsToDeleteFromSourceList = foresterUnits.Where(x => foresterUnitIDList.Contains(x.ForesterUnitID)).ToList();

                foreach (var foresterUnitToDelete in foresterUnitsToDeleteFromSourceList)
                {
                    foresterUnitToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteForesterUnit(this IQueryable<ForesterUnit> foresterUnits, int foresterUnitID)
        {
            DeleteForesterUnit(foresterUnits, new List<int> { foresterUnitID });
        }

        public static void DeleteForesterUnit(this IQueryable<ForesterUnit> foresterUnits, ForesterUnit foresterUnitToDelete)
        {
            DeleteForesterUnit(foresterUnits, new List<ForesterUnit> { foresterUnitToDelete });
        }
    }
}