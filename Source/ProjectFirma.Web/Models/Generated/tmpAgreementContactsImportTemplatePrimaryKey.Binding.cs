//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.tmpAgreementContactsImportTemplate
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class tmpAgreementContactsImportTemplatePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<tmpAgreementContactsImportTemplate>
    {
        public tmpAgreementContactsImportTemplatePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public tmpAgreementContactsImportTemplatePrimaryKey(tmpAgreementContactsImportTemplate tmpAgreementContactsImportTemplate) : base(tmpAgreementContactsImportTemplate){}

        public static implicit operator tmpAgreementContactsImportTemplatePrimaryKey(int primaryKeyValue)
        {
            return new tmpAgreementContactsImportTemplatePrimaryKey(primaryKeyValue);
        }

        public static implicit operator tmpAgreementContactsImportTemplatePrimaryKey(tmpAgreementContactsImportTemplate tmpAgreementContactsImportTemplate)
        {
            return new tmpAgreementContactsImportTemplatePrimaryKey(tmpAgreementContactsImportTemplate);
        }
    }
}