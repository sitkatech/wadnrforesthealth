using System;
using System.Text.RegularExpressions;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class RangeComponents
    {
        private static Regex _rgxRef = new Regex(@"^((?<SheetName>.+)!)?" +
                                 @"(?<C1>" +
                                    @"(" +
                                    @"(?<C1ColDollar>\$)?(?<C1Col>[A-Z]+)(?<C1RowDollar>\$)?(?<C1Row>[0-9]+)" +
                                    @"|(?<C1Err>#[^:]+))" +
                                 @")" +
                                 @"(:" +
                                    @"(?<C2>" +
                                        @"(" +
                                        @"(?<C2ColDollar>\$)?(?<C2Col>[A-Z]+)(?<C2RowDollar>\$)?(?<C2Row>[0-9]+)" +
                                        @"|(?<C2Err>#[^:]+))" +
                                    @")" +
                                 @")?$", RegexOptions.Compiled);

        private string _text;
        private Match _m;

        internal RangeComponents(string range)
        {
            _text = range;
            _m = _rgxRef.Match(range);
            if (!_m.Success)
                throw new ArgumentException("Invalid range: " + range);
        }

        public string Text
        {
            get
            {
                return _text;
            }
        }

        public string SheetName
        {
            get
            {
                string val = _m.Groups["SheetName"].Value;
                if (val != "")
                {
                    // Remove quotes
                    if (val.StartsWith("'") && val.EndsWith("'"))
                    {
                        val = val.Replace("''", "'");
                        val = val.Substring(1, val.Length - 2);
                    }
                    return val;
                }
                return "";
            }
        }

        public string EscapedSheetName
        {
            get
            {
                string val = _m.Groups["SheetName"].Value;
                if (val != "")
                {
                    return val;
                }
                return "";
            }
        }

        public RowColumn Cell1RowColumn
        {
            get
            {
                return ExcelAddress.ToRowColumn(this.Cell1Col + this.Cell1Row);
            }
        }

        public RowColumn Cell2RowColumn
        {
            get
            {
                if (Cell2Col != "")
                {
                    return ExcelAddress.ToRowColumn(this.Cell2Col + this.Cell2Row);
                }
                else
                {
                    return new RowColumn();
                }
            }
        }

        public string Cell1 { get { return _m.Groups["C1"].Value; } }
        public string Cell1Error { get { return _m.Groups["C1Err"].Value; } }
        public string Cell1ColDollar { get { return _m.Groups["C1ColDollar"].Value; } }
        public string Cell1RowDollar { get { return _m.Groups["C1RowDollar"].Value; } }
        public string Cell2 { get { return _m.Groups["C2"].Value; } }
        public string Cell2Error { get { return _m.Groups["C2Err"].Value; } }

        public string Cell2ColDollar { get { return _m.Groups["C2ColDollar"].Value; } }
        public string Cell2RowDollar { get { return _m.Groups["C2RowDollar"].Value; } }

        private string Cell1Col { get { return _m.Groups["C1Col"].Value; } }
        private string Cell1Row { get { return _m.Groups["C1Row"].Value; } }
        private string Cell2Col { get { return _m.Groups["C2Col"].Value; } }
        private string Cell2Row { get { return _m.Groups["C2Row"].Value; } }
    }
}