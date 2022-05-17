//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RecurrenceInterval]
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
        public static RecurrenceInterval GetRecurrenceInterval(this IQueryable<RecurrenceInterval> recurrenceIntervals, int recurrenceIntervalID)
        {
            var recurrenceInterval = recurrenceIntervals.SingleOrDefault(x => x.RecurrenceIntervalID == recurrenceIntervalID);
            Check.RequireNotNullThrowNotFound(recurrenceInterval, "RecurrenceInterval", recurrenceIntervalID);
            return recurrenceInterval;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteRecurrenceInterval(this IQueryable<RecurrenceInterval> recurrenceIntervals, List<int> recurrenceIntervalIDList)
        {
            if(recurrenceIntervalIDList.Any())
            {
                var recurrenceIntervalsInSourceCollectionToDelete = recurrenceIntervals.Where(x => recurrenceIntervalIDList.Contains(x.RecurrenceIntervalID));
                foreach (var recurrenceIntervalToDelete in recurrenceIntervalsInSourceCollectionToDelete.ToList())
                {
                    recurrenceIntervalToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteRecurrenceInterval(this IQueryable<RecurrenceInterval> recurrenceIntervals, ICollection<RecurrenceInterval> recurrenceIntervalsToDelete)
        {
            if(recurrenceIntervalsToDelete.Any())
            {
                var recurrenceIntervalIDList = recurrenceIntervalsToDelete.Select(x => x.RecurrenceIntervalID).ToList();
                var recurrenceIntervalsToDeleteFromSourceList = recurrenceIntervals.Where(x => recurrenceIntervalIDList.Contains(x.RecurrenceIntervalID)).ToList();

                foreach (var recurrenceIntervalToDelete in recurrenceIntervalsToDeleteFromSourceList)
                {
                    recurrenceIntervalToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteRecurrenceInterval(this IQueryable<RecurrenceInterval> recurrenceIntervals, int recurrenceIntervalID)
        {
            DeleteRecurrenceInterval(recurrenceIntervals, new List<int> { recurrenceIntervalID });
        }

        public static void DeleteRecurrenceInterval(this IQueryable<RecurrenceInterval> recurrenceIntervals, RecurrenceInterval recurrenceIntervalToDelete)
        {
            DeleteRecurrenceInterval(recurrenceIntervals, new List<RecurrenceInterval> { recurrenceIntervalToDelete });
        }
    }
}