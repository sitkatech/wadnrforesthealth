using DocumentFormat.OpenXml.Spreadsheet;
using System;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    internal class CellProxy
    {
        private WorksheetCache _wscache;
        private CellValues? _DataType;
        private uint? _StyleIndex;
        private object _Value;
        private CellFormulaProxy _Formula;
        private bool? _ShowPhonetic;
        private uint? _ValueMetaIndex;

        public CellProxy(WorksheetCache wscache)
        {
            _wscache = wscache;
        }

        public CellValues? DataType
        {
            get { return _DataType; }
            set { _DataType = value; _wscache.Modified = true; }
        }

        public uint? StyleIndex
        {
            get { return _StyleIndex; }
            set { _StyleIndex = value; _wscache.Modified = true; }
        }

        public CellFormulaProxy Formula
        {
            get { return _Formula; }
        }

        public bool? ShowPhonetic
        {
            get { return _ShowPhonetic; }
            set { _ShowPhonetic = value; _wscache.Modified = true; }
        }

        public uint? ValueMetaIndex
        {
            get { return _ValueMetaIndex; }
            set { _ValueMetaIndex = value; _wscache.Modified = true; }
        }

        public object Value
        {
            get { return _Value; }
            set { _Value = value; _wscache.Modified = true; }
        }

        public string SerializedValue
        {
            get
            {
                if (_Value == null)
                    return "";

                DateTime? valueAsDateTime = _Value as DateTime?;
                if (valueAsDateTime != null)
                {
                    return valueAsDateTime.Value.ToOADate().ToString();
                }
                return _Value.ToString();
            }
        }

        public void CreateFormula()
        {
            _Formula = new CellFormulaProxy(_wscache);
        }

        public void RemoveFormula()
        {
            _Formula = null;
        }
    }
}