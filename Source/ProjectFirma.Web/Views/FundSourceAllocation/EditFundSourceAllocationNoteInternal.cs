/*-----------------------------------------------------------------------
<copyright file="EditFundSourceAllocationNoteInternal.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    public abstract class EditFundSourceAllocationNoteInternal : TypedWebPartialViewPage<EditFundSourceAllocationNoteInternalViewData, EditFundSourceAllocationNoteInternalViewModel>
    {
    }

    public abstract class EditFundSourceAllocationNoteInternalType
    {
        public readonly string IntroductoryText;

        internal EditFundSourceAllocationNoteInternalType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditFundSourceAllocationNoteInternalTypeNewNote NewNote = EditFundSourceAllocationNoteInternalTypeNewNote.Instance;
        public static readonly EditFundSourceAllocationNoteInternalTypeExistingNote ExistingFundSourceAllocationNoteInternal = EditFundSourceAllocationNoteInternalTypeExistingNote.Instance;
    }

    public class EditFundSourceAllocationNoteInternalTypeNewNote : EditFundSourceAllocationNoteInternalType
    {
        private EditFundSourceAllocationNoteInternalTypeNewNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceAllocationNoteInternalTypeNewNote Instance = new EditFundSourceAllocationNoteInternalTypeNewNote(
            $"<p>Enter a new {Models.FieldDefinition.FundSourceAllocationNoteInternal.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditFundSourceAllocationNoteInternalTypeExistingNote : EditFundSourceAllocationNoteInternalType
    {
        private EditFundSourceAllocationNoteInternalTypeExistingNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceAllocationNoteInternalTypeExistingNote Instance =
            new EditFundSourceAllocationNoteInternalTypeExistingNote(
                $"<p>Update this {Models.FieldDefinition.FundSourceAllocationNoteInternal.GetFieldDefinitionLabel()}.</p>");
    }


}
