/*-----------------------------------------------------------------------
<copyright file="EditProject.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Views.Shared.GrantAllocationControls;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public abstract class EditGrantAllocationNoteInternal : TypedWebPartialViewPage<EditGrantAllocationNoteInternalViewData, EditGrantAllocationNoteInternalViewModel>
    {
    }

    public abstract class EditGrantAllocationNoteInternalType
    {
        public readonly string IntroductoryText;

        internal EditGrantAllocationNoteInternalType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditGrantAllocationNoteInternalTypeNewNote NewNote = EditGrantAllocationNoteInternalTypeNewNote.Instance;
        public static readonly EditGrantAllocationNoteInternalTypeExistingNote ExistingGrantAllocationNoteInternal = EditGrantAllocationNoteInternalTypeExistingNote.Instance;
    }

    public class EditGrantAllocationNoteInternalTypeNewNote : EditGrantAllocationNoteInternalType
    {
        private EditGrantAllocationNoteInternalTypeNewNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditGrantAllocationNoteInternalTypeNewNote Instance = new EditGrantAllocationNoteInternalTypeNewNote(
            $"<p>Enter a new {Models.FieldDefinition.GrantAllocationNoteInternal.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditGrantAllocationNoteInternalTypeExistingNote : EditGrantAllocationNoteInternalType
    {
        private EditGrantAllocationNoteInternalTypeExistingNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditGrantAllocationNoteInternalTypeExistingNote Instance =
            new EditGrantAllocationNoteInternalTypeExistingNote(
                $"<p>Update this {Models.FieldDefinition.GrantAllocationNoteInternal.GetFieldDefinitionLabel()}.</p>");
    }


}
