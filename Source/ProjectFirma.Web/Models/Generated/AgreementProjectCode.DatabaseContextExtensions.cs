//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[AgreementProjectCode]
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
        public static AgreementProjectCode GetAgreementProjectCode(this IQueryable<AgreementProjectCode> agreementProjectCodes, int agreementProjectCodeID)
        {
            var agreementProjectCode = agreementProjectCodes.SingleOrDefault(x => x.AgreementProjectCodeID == agreementProjectCodeID);
            Check.RequireNotNullThrowNotFound(agreementProjectCode, "AgreementProjectCode", agreementProjectCodeID);
            return agreementProjectCode;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteAgreementProjectCode(this IQueryable<AgreementProjectCode> agreementProjectCodes, List<int> agreementProjectCodeIDList)
        {
            if(agreementProjectCodeIDList.Any())
            {
                var agreementProjectCodesInSourceCollectionToDelete = agreementProjectCodes.Where(x => agreementProjectCodeIDList.Contains(x.AgreementProjectCodeID));
                foreach (var agreementProjectCodeToDelete in agreementProjectCodesInSourceCollectionToDelete.ToList())
                {
                    agreementProjectCodeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteAgreementProjectCode(this IQueryable<AgreementProjectCode> agreementProjectCodes, ICollection<AgreementProjectCode> agreementProjectCodesToDelete)
        {
            if(agreementProjectCodesToDelete.Any())
            {
                var agreementProjectCodeIDList = agreementProjectCodesToDelete.Select(x => x.AgreementProjectCodeID).ToList();
                var agreementProjectCodesToDeleteFromSourceList = agreementProjectCodes.Where(x => agreementProjectCodeIDList.Contains(x.AgreementProjectCodeID)).ToList();

                foreach (var agreementProjectCodeToDelete in agreementProjectCodesToDeleteFromSourceList)
                {
                    agreementProjectCodeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteAgreementProjectCode(this IQueryable<AgreementProjectCode> agreementProjectCodes, int agreementProjectCodeID)
        {
            DeleteAgreementProjectCode(agreementProjectCodes, new List<int> { agreementProjectCodeID });
        }

        public static void DeleteAgreementProjectCode(this IQueryable<AgreementProjectCode> agreementProjectCodes, AgreementProjectCode agreementProjectCodeToDelete)
        {
            DeleteAgreementProjectCode(agreementProjectCodes, new List<AgreementProjectCode> { agreementProjectCodeToDelete });
        }
    }
}