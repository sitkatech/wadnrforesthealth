//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[tmpAgreementContact]
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
        public static tmpAgreementContact GettmpAgreementContact(this IQueryable<tmpAgreementContact> tmpAgreementContacts, int agreementContactID)
        {
            var tmpAgreementContact = tmpAgreementContacts.SingleOrDefault(x => x.AgreementContactID == agreementContactID);
            Check.RequireNotNullThrowNotFound(tmpAgreementContact, "tmpAgreementContact", agreementContactID);
            return tmpAgreementContact;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletetmpAgreementContact(this IQueryable<tmpAgreementContact> tmpAgreementContacts, List<int> agreementContactIDList)
        {
            if(agreementContactIDList.Any())
            {
                var tmpAgreementContactsInSourceCollectionToDelete = tmpAgreementContacts.Where(x => agreementContactIDList.Contains(x.AgreementContactID));
                foreach (var tmpAgreementContactToDelete in tmpAgreementContactsInSourceCollectionToDelete.ToList())
                {
                    tmpAgreementContactToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletetmpAgreementContact(this IQueryable<tmpAgreementContact> tmpAgreementContacts, ICollection<tmpAgreementContact> tmpAgreementContactsToDelete)
        {
            if(tmpAgreementContactsToDelete.Any())
            {
                var agreementContactIDList = tmpAgreementContactsToDelete.Select(x => x.AgreementContactID).ToList();
                var tmpAgreementContactsToDeleteFromSourceList = tmpAgreementContacts.Where(x => agreementContactIDList.Contains(x.AgreementContactID)).ToList();

                foreach (var tmpAgreementContactToDelete in tmpAgreementContactsToDeleteFromSourceList)
                {
                    tmpAgreementContactToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletetmpAgreementContact(this IQueryable<tmpAgreementContact> tmpAgreementContacts, int agreementContactID)
        {
            DeletetmpAgreementContact(tmpAgreementContacts, new List<int> { agreementContactID });
        }

        public static void DeletetmpAgreementContact(this IQueryable<tmpAgreementContact> tmpAgreementContacts, tmpAgreementContact tmpAgreementContactToDelete)
        {
            DeletetmpAgreementContact(tmpAgreementContacts, new List<tmpAgreementContact> { tmpAgreementContactToDelete });
        }
    }
}