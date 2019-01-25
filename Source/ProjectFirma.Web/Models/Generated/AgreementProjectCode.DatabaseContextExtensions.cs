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

        public static void DeleteAgreementProjectCode(this IQueryable<AgreementProjectCode> agreementProjectCodes, List<int> agreementProjectCodeIDList)
        {
            if(agreementProjectCodeIDList.Any())
            {
                agreementProjectCodes.Where(x => agreementProjectCodeIDList.Contains(x.AgreementProjectCodeID)).Delete();
            }
        }

        public static void DeleteAgreementProjectCode(this IQueryable<AgreementProjectCode> agreementProjectCodes, ICollection<AgreementProjectCode> agreementProjectCodesToDelete)
        {
            if(agreementProjectCodesToDelete.Any())
            {
                var agreementProjectCodeIDList = agreementProjectCodesToDelete.Select(x => x.AgreementProjectCodeID).ToList();
                agreementProjectCodes.Where(x => agreementProjectCodeIDList.Contains(x.AgreementProjectCodeID)).Delete();
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