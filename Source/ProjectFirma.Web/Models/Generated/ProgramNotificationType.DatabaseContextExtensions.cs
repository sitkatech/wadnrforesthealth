//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProgramNotificationType]
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
        public static ProgramNotificationType GetProgramNotificationType(this IQueryable<ProgramNotificationType> programNotificationTypes, int programNotificationTypeID)
        {
            var programNotificationType = programNotificationTypes.SingleOrDefault(x => x.ProgramNotificationTypeID == programNotificationTypeID);
            Check.RequireNotNullThrowNotFound(programNotificationType, "ProgramNotificationType", programNotificationTypeID);
            return programNotificationType;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProgramNotificationType(this IQueryable<ProgramNotificationType> programNotificationTypes, List<int> programNotificationTypeIDList)
        {
            if(programNotificationTypeIDList.Any())
            {
                var programNotificationTypesInSourceCollectionToDelete = programNotificationTypes.Where(x => programNotificationTypeIDList.Contains(x.ProgramNotificationTypeID));
                foreach (var programNotificationTypeToDelete in programNotificationTypesInSourceCollectionToDelete.ToList())
                {
                    programNotificationTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProgramNotificationType(this IQueryable<ProgramNotificationType> programNotificationTypes, ICollection<ProgramNotificationType> programNotificationTypesToDelete)
        {
            if(programNotificationTypesToDelete.Any())
            {
                var programNotificationTypeIDList = programNotificationTypesToDelete.Select(x => x.ProgramNotificationTypeID).ToList();
                var programNotificationTypesToDeleteFromSourceList = programNotificationTypes.Where(x => programNotificationTypeIDList.Contains(x.ProgramNotificationTypeID)).ToList();

                foreach (var programNotificationTypeToDelete in programNotificationTypesToDeleteFromSourceList)
                {
                    programNotificationTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProgramNotificationType(this IQueryable<ProgramNotificationType> programNotificationTypes, int programNotificationTypeID)
        {
            DeleteProgramNotificationType(programNotificationTypes, new List<int> { programNotificationTypeID });
        }

        public static void DeleteProgramNotificationType(this IQueryable<ProgramNotificationType> programNotificationTypes, ProgramNotificationType programNotificationTypeToDelete)
        {
            DeleteProgramNotificationType(programNotificationTypes, new List<ProgramNotificationType> { programNotificationTypeToDelete });
        }
    }
}