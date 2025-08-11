/*-----------------------------------------------------------------------
<copyright file="EditFundSourceAllocationNote.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Views.Shared.FundSourceAllocationControls;

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public abstract class EditFundSourceAllocationNote : TypedWebPartialViewPage<EditFundSourceAllocationNoteViewData, EditFundSourceAllocationNoteViewModel>
    {
    }

    public abstract class EditFundSourceAllocationNoteType
    {
        public readonly string IntroductoryText;

        internal EditFundSourceAllocationNoteType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditFundSourceAllocationNoteTypeNewNote NewNote = EditFundSourceAllocationNoteTypeNewNote.Instance;
        public static readonly EditFundSourceAllocationNoteTypeExistingNote ExistingFundSourceAllocationNote = EditFundSourceAllocationNoteTypeExistingNote.Instance;
    }

    public class EditFundSourceAllocationNoteTypeNewNote : EditFundSourceAllocationNoteType
    {
        private EditFundSourceAllocationNoteTypeNewNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceAllocationNoteTypeNewNote Instance = new EditFundSourceAllocationNoteTypeNewNote(
            $"<p>Enter a new {Models.FieldDefinition.FundSourceAllocationNote.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditFundSourceAllocationNoteTypeExistingNote : EditFundSourceAllocationNoteType
    {
        private EditFundSourceAllocationNoteTypeExistingNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceAllocationNoteTypeExistingNote Instance =
            new EditFundSourceAllocationNoteTypeExistingNote(
                $"<p>Update this {Models.FieldDefinition.FundSourceAllocationNote.GetFieldDefinitionLabel()}.</p>");
    }

    
}
