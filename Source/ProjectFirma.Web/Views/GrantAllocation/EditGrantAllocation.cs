/*-----------------------------------------------------------------------
<copyright file="EditProject.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
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

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public abstract class EditGrantAllocation : TypedWebPartialViewPage<EditGrantAllocationViewData, EditGrantAllocationViewModel>
    {
    }

    public abstract class EditGrantAllocationType
    {
        public readonly string IntroductoryText;

        internal EditGrantAllocationType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditGrantAllocationTypeNewGrant NewGrantAllocation = EditGrantAllocationTypeNewGrant.Instance;
        public static readonly EditGrantAllocationTypeExistingGrantAllocation ExistingGrantAllocation = EditGrantAllocationTypeExistingGrantAllocation.Instance;
    }

    public class EditGrantAllocationTypeNewGrant : EditGrantAllocationType
    {
        private EditGrantAllocationTypeNewGrant(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditGrantAllocationTypeNewGrant Instance = new EditGrantAllocationTypeNewGrant(
            $"<p>Enter basic information about the {Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditGrantAllocationTypeExistingGrantAllocation : EditGrantAllocationType
    {
        private EditGrantAllocationTypeExistingGrantAllocation(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditGrantAllocationTypeExistingGrantAllocation Instance =
            new EditGrantAllocationTypeExistingGrantAllocation(
                $"<p>Update this {Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()}'s information.</p>");
    }

    
}
