//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[tmpAgreement2]
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
        public static tmpAgreement2 GettmpAgreement2(this IQueryable<tmpAgreement2> tmpAgreement2s, int tmpAgreement2ID)
        {
            var tmpAgreement2 = tmpAgreement2s.SingleOrDefault(x => x.TmpAgreement2ID == tmpAgreement2ID);
            Check.RequireNotNullThrowNotFound(tmpAgreement2, "tmpAgreement2", tmpAgreement2ID);
            return tmpAgreement2;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletetmpAgreement2(this IQueryable<tmpAgreement2> tmpAgreement2s, List<int> tmpAgreement2IDList)
        {
            if(tmpAgreement2IDList.Any())
            {
                var tmpAgreement2sInSourceCollectionToDelete = tmpAgreement2s.Where(x => tmpAgreement2IDList.Contains(x.TmpAgreement2ID));
                foreach (var tmpAgreement2ToDelete in tmpAgreement2sInSourceCollectionToDelete.ToList())
                {
                    tmpAgreement2ToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletetmpAgreement2(this IQueryable<tmpAgreement2> tmpAgreement2s, ICollection<tmpAgreement2> tmpAgreement2sToDelete)
        {
            if(tmpAgreement2sToDelete.Any())
            {
                var tmpAgreement2IDList = tmpAgreement2sToDelete.Select(x => x.TmpAgreement2ID).ToList();
                var tmpAgreement2sToDeleteFromSourceList = tmpAgreement2s.Where(x => tmpAgreement2IDList.Contains(x.TmpAgreement2ID)).ToList();

                foreach (var tmpAgreement2ToDelete in tmpAgreement2sToDeleteFromSourceList)
                {
                    tmpAgreement2ToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletetmpAgreement2(this IQueryable<tmpAgreement2> tmpAgreement2s, int tmpAgreement2ID)
        {
            DeletetmpAgreement2(tmpAgreement2s, new List<int> { tmpAgreement2ID });
        }

        public static void DeletetmpAgreement2(this IQueryable<tmpAgreement2> tmpAgreement2s, tmpAgreement2 tmpAgreement2ToDelete)
        {
            DeletetmpAgreement2(tmpAgreement2s, new List<tmpAgreement2> { tmpAgreement2ToDelete });
        }
    }
}