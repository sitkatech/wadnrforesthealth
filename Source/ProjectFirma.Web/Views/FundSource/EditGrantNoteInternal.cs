using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.FundSource
{
    public abstract class EditFundSourceNoteInternal : TypedWebPartialViewPage<EditFundSourceNoteInternalViewData, EditFundSourceNoteInternalViewModel>
    {
    }

    public abstract class EditFundSourceNoteInternalType
    {
        public readonly string IntroductoryText;

        internal EditFundSourceNoteInternalType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditFundSourceNoteInternalTypeNewNote NewNote = EditFundSourceNoteInternalTypeNewNote.Instance;
        public static readonly EditFundSourceNoteInternalTypeExistingNote ExistingNote = EditFundSourceNoteInternalTypeExistingNote.Instance;
    }

    public class EditFundSourceNoteInternalTypeNewNote : EditFundSourceNoteInternalType
    {
        private EditFundSourceNoteInternalTypeNewNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceNoteInternalTypeNewNote Instance = new EditFundSourceNoteInternalTypeNewNote(
            $"<p>Enter a new {Models.FieldDefinition.FundSourceNoteInternal.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditFundSourceNoteInternalTypeExistingNote : EditFundSourceNoteInternalType
    {
        private EditFundSourceNoteInternalTypeExistingNote(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditFundSourceNoteInternalTypeExistingNote Instance =
            new EditFundSourceNoteInternalTypeExistingNote(
                $"<p>Update this {Models.FieldDefinition.FundSourceNoteInternal.GetFieldDefinitionLabel()}.</p>");
    }
}