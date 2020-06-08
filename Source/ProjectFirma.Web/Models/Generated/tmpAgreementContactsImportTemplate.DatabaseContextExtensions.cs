//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[tmpAgreementContactsImportTemplate]
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
        public static tmpAgreementContactsImportTemplate GettmpAgreementContactsImportTemplate(this IQueryable<tmpAgreementContactsImportTemplate> tmpAgreementContactsImportTemplates, int tmpAgreementContactsImportTemplateID)
        {
            var tmpAgreementContactsImportTemplate = tmpAgreementContactsImportTemplates.SingleOrDefault(x => x.tmpAgreementContactsImportTemplateID == tmpAgreementContactsImportTemplateID);
            Check.RequireNotNullThrowNotFound(tmpAgreementContactsImportTemplate, "tmpAgreementContactsImportTemplate", tmpAgreementContactsImportTemplateID);
            return tmpAgreementContactsImportTemplate;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletetmpAgreementContactsImportTemplate(this IQueryable<tmpAgreementContactsImportTemplate> tmpAgreementContactsImportTemplates, List<int> tmpAgreementContactsImportTemplateIDList)
        {
            if(tmpAgreementContactsImportTemplateIDList.Any())
            {
                var tmpAgreementContactsImportTemplatesInSourceCollectionToDelete = tmpAgreementContactsImportTemplates.Where(x => tmpAgreementContactsImportTemplateIDList.Contains(x.tmpAgreementContactsImportTemplateID));
                foreach (var tmpAgreementContactsImportTemplateToDelete in tmpAgreementContactsImportTemplatesInSourceCollectionToDelete.ToList())
                {
                    tmpAgreementContactsImportTemplateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletetmpAgreementContactsImportTemplate(this IQueryable<tmpAgreementContactsImportTemplate> tmpAgreementContactsImportTemplates, ICollection<tmpAgreementContactsImportTemplate> tmpAgreementContactsImportTemplatesToDelete)
        {
            if(tmpAgreementContactsImportTemplatesToDelete.Any())
            {
                var tmpAgreementContactsImportTemplateIDList = tmpAgreementContactsImportTemplatesToDelete.Select(x => x.tmpAgreementContactsImportTemplateID).ToList();
                var tmpAgreementContactsImportTemplatesToDeleteFromSourceList = tmpAgreementContactsImportTemplates.Where(x => tmpAgreementContactsImportTemplateIDList.Contains(x.tmpAgreementContactsImportTemplateID)).ToList();

                foreach (var tmpAgreementContactsImportTemplateToDelete in tmpAgreementContactsImportTemplatesToDeleteFromSourceList)
                {
                    tmpAgreementContactsImportTemplateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletetmpAgreementContactsImportTemplate(this IQueryable<tmpAgreementContactsImportTemplate> tmpAgreementContactsImportTemplates, int tmpAgreementContactsImportTemplateID)
        {
            DeletetmpAgreementContactsImportTemplate(tmpAgreementContactsImportTemplates, new List<int> { tmpAgreementContactsImportTemplateID });
        }

        public static void DeletetmpAgreementContactsImportTemplate(this IQueryable<tmpAgreementContactsImportTemplate> tmpAgreementContactsImportTemplates, tmpAgreementContactsImportTemplate tmpAgreementContactsImportTemplateToDelete)
        {
            DeletetmpAgreementContactsImportTemplate(tmpAgreementContactsImportTemplates, new List<tmpAgreementContactsImportTemplate> { tmpAgreementContactsImportTemplateToDelete });
        }
    }
}