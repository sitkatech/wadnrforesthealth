using System;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Web;
using LtInfo.Common.HtmlHelperExtensions;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class FieldDefinitionModelExtensions
    {
        public static readonly EnglishPluralizationService PluralizationService = new EnglishPluralizationService();

        public static HtmlString GetFieldDefinitionDescription(this FieldDefinition fieldDefinition)
        {
            var fieldDefinitionData = fieldDefinition.GetFieldDefinitionData();
            if (fieldDefinitionData != null && !String.IsNullOrWhiteSpace(fieldDefinitionData.FieldDefinitionDataValueHtmlString?.ToString()))
            {
                return fieldDefinitionData.FieldDefinitionDataValueHtmlString;
            }
            return fieldDefinition.DefaultDefinitionHtmlString;
        }

        public static string GetFieldDefinitionLabel(this FieldDefinition fieldDefinition)
        {
            var fieldDefinitionData = fieldDefinition.GetFieldDefinitionData();
            return fieldDefinition.GetFieldDefinitionLabelImpl(fieldDefinitionData);
        }

        private static string GetFieldDefinitionLabelImpl(this FieldDefinition fieldDefinition, IFieldDefinitionData fieldDefinitionData)
        {
            if (fieldDefinitionData != null && !String.IsNullOrWhiteSpace(fieldDefinitionData.FieldDefinitionLabel))
            {
                return fieldDefinitionData.FieldDefinitionLabel;
            }
            return fieldDefinition.FieldDefinitionDisplayName;
        }

        public static bool HasCustomFieldLabel(this FieldDefinition fieldDefinition)
        {
            var fieldDefinitionData = fieldDefinition.GetFieldDefinitionData();
            return fieldDefinitionData != null && !String.IsNullOrWhiteSpace(fieldDefinitionData.FieldDefinitionLabel);
        }

        public static bool HasCustomFieldDefinition(this FieldDefinition fieldDefinition)
        {
            var fieldDefinitionData = fieldDefinition.GetFieldDefinitionData();
            return fieldDefinitionData != null && fieldDefinitionData.FieldDefinitionDataValueHtmlString != null;
        }

        public static string GetFieldDefinitionLabelPluralized(this FieldDefinition fieldDefinition)
        {
            return PluralizationService.Pluralize(fieldDefinition.GetFieldDefinitionLabel());
        }

        public static IFieldDefinitionData GetFieldDefinitionData(this FieldDefinition fieldDefinition)
        {
            return fieldDefinition.GetFieldDefinitionData();
        }

        public static string GetContentUrl(this FieldDefinition fieldDefinition)
        {
            return SitkaRoute<FieldDefinitionController>.BuildUrlFromExpression(x => x.FieldDefinitionDetails(fieldDefinition.FieldDefinitionID));
        }

        public static IFieldDefinitionData GetFieldDefinitionDataForBackgroundJob(this FieldDefinition fieldDefinition)
        {
            return fieldDefinition.GetFieldDefinitionData();
        }

        public static string GetFieldDefinitionLabelForBackgroundJob(this FieldDefinition fieldDefinition)
        {
            var fieldDefinitionData = fieldDefinition.GetFieldDefinitionDataForBackgroundJob();
            return fieldDefinition.GetFieldDefinitionLabelImpl(fieldDefinitionData);
        }

        public static string GetFieldDefinitionLabelPluralizedForBackgroundJob(this FieldDefinition fieldDefinition)
        {
            return PluralizationService.Pluralize(fieldDefinition.GetFieldDefinitionLabelForBackgroundJob());
        }
    }
}