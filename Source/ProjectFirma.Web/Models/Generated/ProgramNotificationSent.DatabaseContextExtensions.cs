//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProgramNotificationSent]
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
        public static ProgramNotificationSent GetProgramNotificationSent(this IQueryable<ProgramNotificationSent> programNotificationSents, int programNotificationSendID)
        {
            var programNotificationSent = programNotificationSents.SingleOrDefault(x => x.ProgramNotificationSendID == programNotificationSendID);
            Check.RequireNotNullThrowNotFound(programNotificationSent, "ProgramNotificationSent", programNotificationSendID);
            return programNotificationSent;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProgramNotificationSent(this IQueryable<ProgramNotificationSent> programNotificationSents, List<int> programNotificationSendIDList)
        {
            if(programNotificationSendIDList.Any())
            {
                var programNotificationSentsInSourceCollectionToDelete = programNotificationSents.Where(x => programNotificationSendIDList.Contains(x.ProgramNotificationSendID));
                foreach (var programNotificationSentToDelete in programNotificationSentsInSourceCollectionToDelete.ToList())
                {
                    programNotificationSentToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProgramNotificationSent(this IQueryable<ProgramNotificationSent> programNotificationSents, ICollection<ProgramNotificationSent> programNotificationSentsToDelete)
        {
            if(programNotificationSentsToDelete.Any())
            {
                var programNotificationSendIDList = programNotificationSentsToDelete.Select(x => x.ProgramNotificationSendID).ToList();
                var programNotificationSentsToDeleteFromSourceList = programNotificationSents.Where(x => programNotificationSendIDList.Contains(x.ProgramNotificationSendID)).ToList();

                foreach (var programNotificationSentToDelete in programNotificationSentsToDeleteFromSourceList)
                {
                    programNotificationSentToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProgramNotificationSent(this IQueryable<ProgramNotificationSent> programNotificationSents, int programNotificationSendID)
        {
            DeleteProgramNotificationSent(programNotificationSents, new List<int> { programNotificationSendID });
        }

        public static void DeleteProgramNotificationSent(this IQueryable<ProgramNotificationSent> programNotificationSents, ProgramNotificationSent programNotificationSentToDelete)
        {
            DeleteProgramNotificationSent(programNotificationSents, new List<ProgramNotificationSent> { programNotificationSentToDelete });
        }
    }
}