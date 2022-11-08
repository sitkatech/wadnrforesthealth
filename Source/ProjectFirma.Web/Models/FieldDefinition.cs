/*-----------------------------------------------------------------------
<copyright file="FieldDefinition.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.HtmlHelperExtensions;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class FieldDefinition : IFieldDefinition
    {
        public static readonly EnglishPluralizationService PluralizationService = new EnglishPluralizationService();

        public bool HasDefinition()
        {
            var fieldDefinitionData = GetFieldDefinitionData();
            return fieldDefinitionData != null && fieldDefinitionData.FieldDefinitionDataValueHtmlString != null;
        }

        public IFieldDefinitionData GetFieldDefinitionData()
        {
            return HttpRequestStorage.DatabaseEntities.FieldDefinitionDatas.GetFieldDefinitionDataByFieldDefinition(this);
        }

        public string GetFieldDefinitionLabel()
        {
            var fieldDefinitionData = GetFieldDefinitionData();
            if (fieldDefinitionData != null && !string.IsNullOrWhiteSpace(fieldDefinitionData.FieldDefinitionLabel))
            {
                return fieldDefinitionData.FieldDefinitionLabel;
            }
            return FieldDefinitionDisplayName;
        }

        public HtmlString GetFieldDefinitionDescription()
        {
            var fieldDefinitionData = GetFieldDefinitionData();
            if (fieldDefinitionData != null && !string.IsNullOrWhiteSpace(fieldDefinitionData.FieldDefinitionDataValueHtmlString?.ToString()))
            {
                return fieldDefinitionData.FieldDefinitionDataValueHtmlString;
            }
            return DefaultDefinitionHtmlString;
        }

        public bool HasCustomFieldLabel()
        {
            var fieldDefinitionData = GetFieldDefinitionData();
            return fieldDefinitionData != null && !string.IsNullOrWhiteSpace(fieldDefinitionData.FieldDefinitionLabel);
        }

        public bool HasCustomFieldDefinition()
        {
            var fieldDefinitionData = GetFieldDefinitionData();
            return fieldDefinitionData != null && fieldDefinitionData.FieldDefinitionDataValueHtmlString != null;
        }

        private string GetSlashedNamePluralized(string originalFieldDefinitionLabel)
        {
            Check.Ensure(originalFieldDefinitionLabel.Contains("/"));

            var allSlashedSections = originalFieldDefinitionLabel.Split('/');
            string finalString = string.Empty;
            foreach (var slashedSection in allSlashedSections)
            {
                string currentSlashedSectionPluralized = PluralizationService.Pluralize(slashedSection);
                if (finalString == String.Empty)
                {
                    finalString += currentSlashedSectionPluralized;
                }
                else
                {
                    finalString += "/" + currentSlashedSectionPluralized;
                }
            }
            return finalString;
        }

        public string GetFieldDefinitionLabelPluralized()
        {
            // Names with slashes get special treatment
            string fieldDefinitionLabel = GetFieldDefinitionLabel();
            if (fieldDefinitionLabel.Contains("/"))
            {
                return GetSlashedNamePluralized(fieldDefinitionLabel);
            }

            // Otherwise, just let .NET handle it
            return PluralizationService.Pluralize(GetFieldDefinitionLabel());
        }

        public string GetContentUrl()
        {
            return SitkaRoute<FieldDefinitionController>.BuildUrlFromExpression(x => x.FieldDefinitionDetails(FieldDefinitionID));
        }
    }
}
