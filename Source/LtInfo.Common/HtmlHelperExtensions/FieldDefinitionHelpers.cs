﻿/*-----------------------------------------------------------------------
<copyright file="FieldDefinitionHelpers.cs" company="Environmental Science Associates">
Copyright (c) Environmental Science Associates. All rights reserved.
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
namespace LtInfo.Common.HtmlHelperExtensions
{
    public static class FieldDefinitionHelpers
    {
        public static string ToGridHeaderString(this IFieldDefinition fieldDefinition)
        {
            return fieldDefinition.ToGridHeaderString(LabelWithSugarForExtensions.DefaultPopupWidth, fieldDefinition.GetFieldDefinitionLabel());
        }

        public static string ToGridHeaderString(this IFieldDefinition fieldDefinition, string linkText)
        {
            return fieldDefinition.ToGridHeaderString(LabelWithSugarForExtensions.DefaultPopupWidth, linkText);
        }

        public static string ToGridHeaderString(this IFieldDefinition fieldDefinition, int popupWidth)
        {
            return fieldDefinition.ToGridHeaderString(popupWidth, fieldDefinition.GetFieldDefinitionLabel());
        }

        public static string ToGridHeaderStringPlural(this IFieldDefinition fieldDefinition, string displayNamePlural) //there should be a better way to pass this
        {
            return fieldDefinition.ToGridHeaderString(displayNamePlural);
        }

        private static string ToGridHeaderString(this IFieldDefinition fieldDefinition, int popupWidth, string fieldDefinitionDisplayName)
        {
            return
                LabelWithSugarForExtensions.LabelWithSugarFor(fieldDefinition, popupWidth,
                    LabelWithSugarForExtensions.DisplayStyle.AsGridHeader, fieldDefinitionDisplayName).ToString();
        }
    }
}
