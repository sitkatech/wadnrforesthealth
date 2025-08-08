/*-----------------------------------------------------------------------
<copyright file="EditFundSourceNote.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.FundSource
{
    public abstract class EditFundSourceNote : TypedWebPartialViewPage<EditFundSourceNoteViewData, EditFundSourceNoteViewModel>
    {
    }

    public abstract class EditFundSourceNoteType
    {
        public readonly string IntroductoryText;

        internal EditFundSourceNoteType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditFundSourceNoteTypeNewNote NewNote = EditFundSourceNoteTypeNewNote.Instance;
        public static readonly EditFundSourceNoteTypeExistingNote ExistingNote = EditFundSourceNoteTypeExistingNote.Instance;
    }

    public class EditFundSourceNoteTypeNewNote : EditFundSourceNoteType
    {
        private EditFundSourceNoteTypeNewNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceNoteTypeNewNote Instance = new EditFundSourceNoteTypeNewNote(
            $"<p>Enter a new {Models.FieldDefinition.FundSourceNote.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditFundSourceNoteTypeExistingNote : EditFundSourceNoteType
    {
        private EditFundSourceNoteTypeExistingNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceNoteTypeExistingNote Instance =
            new EditFundSourceNoteTypeExistingNote(
                $"<p>Update this {Models.FieldDefinition.FundSourceNote.GetFieldDefinitionLabel()}.</p>");
    }

    
}
