//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CurrentBiennium]
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
        public static CurrentBiennium GetCurrentBiennium(this IQueryable<CurrentBiennium> currentBiennia, int currentBienniumID)
        {
            var currentBiennium = currentBiennia.SingleOrDefault(x => x.CurrentBienniumID == currentBienniumID);
            Check.RequireNotNullThrowNotFound(currentBiennium, "CurrentBiennium", currentBienniumID);
            return currentBiennium;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteCurrentBiennium(this IQueryable<CurrentBiennium> currentBiennia, List<int> currentBienniumIDList)
        {
            if(currentBienniumIDList.Any())
            {
                var currentBienniaInSourceCollectionToDelete = currentBiennia.Where(x => currentBienniumIDList.Contains(x.CurrentBienniumID));
                foreach (var currentBienniumToDelete in currentBienniaInSourceCollectionToDelete.ToList())
                {
                    currentBienniumToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteCurrentBiennium(this IQueryable<CurrentBiennium> currentBiennia, ICollection<CurrentBiennium> currentBienniaToDelete)
        {
            if(currentBienniaToDelete.Any())
            {
                var currentBienniumIDList = currentBienniaToDelete.Select(x => x.CurrentBienniumID).ToList();
                var currentBienniaToDeleteFromSourceList = currentBiennia.Where(x => currentBienniumIDList.Contains(x.CurrentBienniumID)).ToList();

                foreach (var currentBienniumToDelete in currentBienniaToDeleteFromSourceList)
                {
                    currentBienniumToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteCurrentBiennium(this IQueryable<CurrentBiennium> currentBiennia, int currentBienniumID)
        {
            DeleteCurrentBiennium(currentBiennia, new List<int> { currentBienniumID });
        }

        public static void DeleteCurrentBiennium(this IQueryable<CurrentBiennium> currentBiennia, CurrentBiennium currentBienniumToDelete)
        {
            DeleteCurrentBiennium(currentBiennia, new List<CurrentBiennium> { currentBienniumToDelete });
        }
    }
}