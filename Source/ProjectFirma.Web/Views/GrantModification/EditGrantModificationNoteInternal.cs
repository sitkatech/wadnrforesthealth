/*-----------------------------------------------------------------------
<copyright file="EditGrantModificationNoteInternal.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace ProjectFirma.Web.Views.GrantModification
{
    public abstract class EditGrantModificationNoteInternal : TypedWebPartialViewPage<EditGrantModificationNoteInternalViewData, EditGrantModificationNoteInternalViewModel>
    {
    }

    public abstract class EditGrantModificationNoteInternalType
    {
        public readonly string IntroductoryText;

        internal EditGrantModificationNoteInternalType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditGrantModificationNoteInternalTypeNewNote NewNote = EditGrantModificationNoteInternalTypeNewNote.Instance;
        public static readonly EditGrantModificationNoteInternalTypeExistingNote ExistingGrantModificationNoteInternal = EditGrantModificationNoteInternalTypeExistingNote.Instance;
    }

    public class EditGrantModificationNoteInternalTypeNewNote : EditGrantModificationNoteInternalType
    {
        private EditGrantModificationNoteInternalTypeNewNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditGrantModificationNoteInternalTypeNewNote Instance = new EditGrantModificationNoteInternalTypeNewNote(
            $"<p>Enter a new {Models.FieldDefinition.GrantModificationNoteInternal.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditGrantModificationNoteInternalTypeExistingNote : EditGrantModificationNoteInternalType
    {
        private EditGrantModificationNoteInternalTypeExistingNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditGrantModificationNoteInternalTypeExistingNote Instance =
            new EditGrantModificationNoteInternalTypeExistingNote(
                $"<p>Update this {Models.FieldDefinition.GrantModificationNoteInternal.GetFieldDefinitionLabel()}.</p>");
    }


}
