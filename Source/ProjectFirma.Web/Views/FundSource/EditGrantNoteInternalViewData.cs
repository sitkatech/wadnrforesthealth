namespace ProjectFirma.Web.Views.Grant
{
    public class EditGrantNoteInternalViewData : FirmaUserControlViewData
    {

        public EditGrantNoteInternalType EditGrantNoteInternalType { get; set; }

        public EditGrantNoteInternalViewData(EditGrantNoteInternalType editGrantNoteInternalType)
        {
            EditGrantNoteInternalType = editGrantNoteInternalType;
        }
    }
}