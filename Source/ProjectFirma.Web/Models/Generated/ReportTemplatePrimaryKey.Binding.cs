//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ReportTemplate
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ReportTemplatePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ReportTemplate>
    {
        public ReportTemplatePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ReportTemplatePrimaryKey(ReportTemplate reportTemplate) : base(reportTemplate){}

        public static implicit operator ReportTemplatePrimaryKey(int primaryKeyValue)
        {
            return new ReportTemplatePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ReportTemplatePrimaryKey(ReportTemplate reportTemplate)
        {
            return new ReportTemplatePrimaryKey(reportTemplate);
        }
    }
}