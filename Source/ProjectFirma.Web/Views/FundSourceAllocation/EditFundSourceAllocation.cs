/*-----------------------------------------------------------------------
<copyright file="EditFundSourceAllocation.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public abstract class EditFundSourceAllocation : TypedWebPartialViewPage<EditFundSourceAllocationViewData, EditFundSourceAllocationViewModel>
    {
    }

    public abstract class EditFundSourceAllocationType
    {
        public readonly string IntroductoryText;

        internal EditFundSourceAllocationType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditFundSourceAllocationTypeNewFundSource NewFundSourceAllocation = EditFundSourceAllocationTypeNewFundSource.Instance;
        public static readonly EditFundSourceAllocationTypeExistingFundSourceAllocation ExistingFundSourceAllocation = EditFundSourceAllocationTypeExistingFundSourceAllocation.Instance;
    }

    public class EditFundSourceAllocationTypeNewFundSource : EditFundSourceAllocationType
    {
        private EditFundSourceAllocationTypeNewFundSource(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceAllocationTypeNewFundSource Instance = new EditFundSourceAllocationTypeNewFundSource(
            $"<p>Enter basic information about the {Models.FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditFundSourceAllocationTypeExistingFundSourceAllocation : EditFundSourceAllocationType
    {
        private EditFundSourceAllocationTypeExistingFundSourceAllocation(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceAllocationTypeExistingFundSourceAllocation Instance =
            new EditFundSourceAllocationTypeExistingFundSourceAllocation(
                $"<p>Update this {Models.FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()}'s information.</p>");
    }

    
}
