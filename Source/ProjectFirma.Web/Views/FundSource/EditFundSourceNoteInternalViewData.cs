namespace ProjectFirma.Web.Views.FundSource
{
    public class EditFundSourceNoteInternalViewData : FirmaUserControlViewData
    {

        public EditFundSourceNoteInternalType EditFundSourceNoteInternalType { get; set; }

        public EditFundSourceNoteInternalViewData(EditFundSourceNoteInternalType editFundSourceNoteInternalType)
        {
            EditFundSourceNoteInternalType = editFundSourceNoteInternalType;
        }
    }
}