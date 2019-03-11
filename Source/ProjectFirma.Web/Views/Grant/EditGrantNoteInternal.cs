using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.Grant
{
    public abstract class EditGrantNoteInternal : TypedWebPartialViewPage<EditGrantNoteInternalViewData, EditGrantNoteInternalViewModel>
    {
    }

    public abstract class EditGrantNoteInternalType
    {
        public readonly string IntroductoryText;

        internal EditGrantNoteInternalType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditGrantNoteInternalTypeNewNote NewNote = EditGrantNoteInternalTypeNewNote.Instance;
        public static readonly EditGrantNoteInternalTypeExistingNote ExistingNote = EditGrantNoteInternalTypeExistingNote.Instance;
    }

    public class EditGrantNoteInternalTypeNewNote : EditGrantNoteInternalType
    {
        private EditGrantNoteInternalTypeNewNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditGrantNoteInternalTypeNewNote Instance = new EditGrantNoteInternalTypeNewNote(
            $"<p>Enter a new {Models.FieldDefinition.GrantNoteInternal.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditGrantNoteInternalTypeExistingNote : EditGrantNoteInternalType
    {
        private EditGrantNoteInternalTypeExistingNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditGrantNoteInternalTypeExistingNote Instance =
            new EditGrantNoteInternalTypeExistingNote(
                $"<p>Update this {Models.FieldDefinition.GrantNoteInternal.GetFieldDefinitionLabel()}.</p>");
    }
}