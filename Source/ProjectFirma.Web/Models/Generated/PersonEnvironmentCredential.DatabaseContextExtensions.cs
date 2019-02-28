//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PersonEnvironmentCredential]
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
        public static PersonEnvironmentCredential GetPersonEnvironmentCredential(this IQueryable<PersonEnvironmentCredential> personEnvironmentCredentials, int personEnvironmentCredentialID)
        {
            var personEnvironmentCredential = personEnvironmentCredentials.SingleOrDefault(x => x.PersonEnvironmentCredentialID == personEnvironmentCredentialID);
            Check.RequireNotNullThrowNotFound(personEnvironmentCredential, "PersonEnvironmentCredential", personEnvironmentCredentialID);
            return personEnvironmentCredential;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePersonEnvironmentCredential(this IQueryable<PersonEnvironmentCredential> personEnvironmentCredentials, List<int> personEnvironmentCredentialIDList)
        {
            if(personEnvironmentCredentialIDList.Any())
            {
                var personEnvironmentCredentialsInSourceCollectionToDelete = personEnvironmentCredentials.Where(x => personEnvironmentCredentialIDList.Contains(x.PersonEnvironmentCredentialID));
                foreach (var personEnvironmentCredentialToDelete in personEnvironmentCredentialsInSourceCollectionToDelete.ToList())
                {
                    personEnvironmentCredentialToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePersonEnvironmentCredential(this IQueryable<PersonEnvironmentCredential> personEnvironmentCredentials, ICollection<PersonEnvironmentCredential> personEnvironmentCredentialsToDelete)
        {
            if(personEnvironmentCredentialsToDelete.Any())
            {
                var personEnvironmentCredentialIDList = personEnvironmentCredentialsToDelete.Select(x => x.PersonEnvironmentCredentialID).ToList();
                var personEnvironmentCredentialsToDeleteFromSourceList = personEnvironmentCredentials.Where(x => personEnvironmentCredentialIDList.Contains(x.PersonEnvironmentCredentialID)).ToList();

                foreach (var personEnvironmentCredentialToDelete in personEnvironmentCredentialsToDeleteFromSourceList)
                {
                    personEnvironmentCredentialToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePersonEnvironmentCredential(this IQueryable<PersonEnvironmentCredential> personEnvironmentCredentials, int personEnvironmentCredentialID)
        {
            DeletePersonEnvironmentCredential(personEnvironmentCredentials, new List<int> { personEnvironmentCredentialID });
        }

        public static void DeletePersonEnvironmentCredential(this IQueryable<PersonEnvironmentCredential> personEnvironmentCredentials, PersonEnvironmentCredential personEnvironmentCredentialToDelete)
        {
            DeletePersonEnvironmentCredential(personEnvironmentCredentials, new List<PersonEnvironmentCredential> { personEnvironmentCredentialToDelete });
        }
    }
}