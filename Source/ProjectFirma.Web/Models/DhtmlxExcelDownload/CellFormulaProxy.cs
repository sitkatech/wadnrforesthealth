
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    internal class CellFormulaProxy
    {
        private WorksheetCache _wscache;
        private bool? _AlwaysCalculateArray;
        private bool? _Bx;
        private bool? _CalculateCell;
        private bool? _DataTable2D;
        private bool? _DataTableRow;
        private CellFormulaValues? _FormulaType;
        private bool? _Input1Deleted;
        private bool? _Input2Deleted;
        private string _R1;
        private string _R2;
        private string _Reference;
        private uint? _SharedIndex;
        private string _Text;

        public CellFormulaProxy(WorksheetCache wscache)
        {
            _wscache = wscache;
        }

        public bool? AlwaysCalculateArray
        {
            get { return _AlwaysCalculateArray; }
            set { _AlwaysCalculateArray = value; _wscache.Modified = true; }
        }

        public bool? Bx
        {
            get { return _Bx; }
            set { _Bx = value; _wscache.Modified = true; }
        }

        public bool? CalculateCell
        {
            get { return _CalculateCell; }
            set { _CalculateCell = value; _wscache.Modified = true; }
        }

        public bool? DataTable2D
        {
            get { return _DataTable2D; }
            set { _DataTable2D = value; _wscache.Modified = true; }
        }

        public bool? DataTableRow
        {
            get { return _DataTableRow; }
            set { _DataTableRow = value; _wscache.Modified = true; }
        }

        public CellFormulaValues? FormulaType
        {
            get { return _FormulaType; }
            set { _FormulaType = value; _wscache.Modified = true; }
        }

        public bool? Input1Deleted
        {
            get { return _Input1Deleted; }
            set { _Input1Deleted = value; _wscache.Modified = true; }
        }

        public bool? Input2Deleted
        {
            get { return _Input2Deleted; }
            set { _Input2Deleted = value; _wscache.Modified = true; }
        }

        public string R1
        {
            get { return _R1; }
            set { _R1 = value; _wscache.Modified = true; }
        }

        public string R2
        {
            get { return _R2; }
            set { _R2 = value; _wscache.Modified = true; }
        }

        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; _wscache.Modified = true; }
        }

        public uint? SharedIndex
        {
            get { return _SharedIndex; }
            set { _SharedIndex = value; _wscache.Modified = true; }
        }

        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
    }
}