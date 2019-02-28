//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Authenticator]
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
        public static Authenticator GetAuthenticator(this IQueryable<Authenticator> authenticators, int authenticatorID)
        {
            var authenticator = authenticators.SingleOrDefault(x => x.AuthenticatorID == authenticatorID);
            Check.RequireNotNullThrowNotFound(authenticator, "Authenticator", authenticatorID);
            return authenticator;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteAuthenticator(this IQueryable<Authenticator> authenticators, List<int> authenticatorIDList)
        {
            if(authenticatorIDList.Any())
            {
                var authenticatorsInSourceCollectionToDelete = authenticators.Where(x => authenticatorIDList.Contains(x.AuthenticatorID));
                foreach (var authenticatorToDelete in authenticatorsInSourceCollectionToDelete.ToList())
                {
                    authenticatorToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteAuthenticator(this IQueryable<Authenticator> authenticators, ICollection<Authenticator> authenticatorsToDelete)
        {
            if(authenticatorsToDelete.Any())
            {
                var authenticatorIDList = authenticatorsToDelete.Select(x => x.AuthenticatorID).ToList();
                var authenticatorsToDeleteFromSourceList = authenticators.Where(x => authenticatorIDList.Contains(x.AuthenticatorID)).ToList();

                foreach (var authenticatorToDelete in authenticatorsToDeleteFromSourceList)
                {
                    authenticatorToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteAuthenticator(this IQueryable<Authenticator> authenticators, int authenticatorID)
        {
            DeleteAuthenticator(authenticators, new List<int> { authenticatorID });
        }

        public static void DeleteAuthenticator(this IQueryable<Authenticator> authenticators, Authenticator authenticatorToDelete)
        {
            DeleteAuthenticator(authenticators, new List<Authenticator> { authenticatorToDelete });
        }
    }
}