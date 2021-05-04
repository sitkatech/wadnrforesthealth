namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelWorkbook
    {
        public ExcelDocument Document { get; protected set; }

        public ExcelWorksheets Worksheets { get; protected set; }

        internal ExcelWorkbook(ExcelDocument parent)
        {
            this.Document = parent;
            this.Worksheets = new ExcelWorksheets(parent);


        }
    }
}