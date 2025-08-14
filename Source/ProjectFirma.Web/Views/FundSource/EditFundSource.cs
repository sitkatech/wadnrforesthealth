/*-----------------------------------------------------------------------
<copyright file="EditFundSource.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Views.Shared.ProjectControls;

namespace ProjectFirma.Web.Views.FundSource
{
    public abstract class EditFundSource : TypedWebPartialViewPage<EditFundSourceViewData, EditFundSourceViewModel>
    {
    }

    public abstract class EditFundSourceType
    {
        public readonly string IntroductoryText;

        internal EditFundSourceType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditFundSourceTypeNewFundSource NewFundSource = EditFundSourceTypeNewFundSource.Instance;
        public static readonly EditFundSourceTypeExistingFundSource ExistingFundSource = EditFundSourceTypeExistingFundSource.Instance;
    }

    public class EditFundSourceTypeNewFundSource : EditFundSourceType
    {
        private EditFundSourceTypeNewFundSource(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceTypeNewFundSource Instance = new EditFundSourceTypeNewFundSource(
            $"<p>Enter basic information about the {Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditFundSourceTypeExistingFundSource : EditFundSourceType
    {
        private EditFundSourceTypeExistingFundSource(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceTypeExistingFundSource Instance =
            new EditFundSourceTypeExistingFundSource(
                $"<p>Update this {Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()}'s information.</p>");
    }

    
}
