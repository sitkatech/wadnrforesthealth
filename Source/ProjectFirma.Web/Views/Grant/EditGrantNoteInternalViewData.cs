namespace ProjectFirma.Web.Views.Grant
{
    public class EditGrantNoteInternalViewData : FirmaUserControlViewData
    {

        public EditGrantNoteType EditGrantNoteType { get; set; }

        public EditGrantNoteInternalViewData(EditGrantNoteType editGrantNoteType)
        {
            EditGrantNoteType = editGrantNoteType;
        }
    }
}