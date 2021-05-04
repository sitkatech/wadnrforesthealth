using System;


namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public static class ExcelRange
    {
        /// <summary>
        /// <summary>
        /// Parses formula into a tree.
        /// DEV NOTE: Subject to change.
        /// </summary>
        /// </summary>
        /// <param name="range">Range, e.g. A1, Sheet1!A1</param>
        /// <returns></returns>
        public static RangeComponents Parse(string range)
        {
            return new RangeComponents(range);
        }

        /// <summary>
        /// Translate a range.
        /// </summary>
        /// <param name="range">Range, e.g. A1, Sheet1!A1</param>
        /// <param name="rowDelta">Number of rows to move up(+) or down(-)</param>
        /// <param name="colDelta">Number of columns to move right(+) or left(-)</param>
        /// <returns></returns>
        public static string Translate(string range, int rowDelta, int colDelta)
        {
            if (range == null)
                return null;

            RangeComponents er = ExcelRange.Parse(range);
            // Row and column start of 0,0 means all rows and columns are affected by delta
            return TranslateInternal(er, 0, 0, rowDelta, colDelta, true);
        }

        /// <summary>
        /// Translate a range due to a sheet change, e.g. insertion of rows.
        /// </summary>
        /// <param name="range">Range, e.g. A1, Sheet1!A1</param>
        /// <param name="sheetChange">Details of change</param>
        /// <param name="currentSheetName">The sheet where the range is, to determine if this range is affected. If sheetChange.SheetName is null and currentSheetName is null, translation is always applied.</param>
        /// <returns></returns>
        public static string TranslateForSheetChange(string range, SheetChange sheetChange, string currentSheetName)
        {
            if (range == null)
                return null;

            RangeComponents er = ExcelRange.Parse(range);

            if ((er.SheetName == "" && currentSheetName == sheetChange.SheetName) ||
                er.SheetName == sheetChange.SheetName)
            {
                return TranslateInternal(er, sheetChange.RowStart, sheetChange.ColumnStart,
                                             sheetChange.RowDelta, sheetChange.ColumnDelta,
                                             false // Don't allow absolute refs i.e. $ to affect translate
                                             );
            }
            else
                return range;
        }

        private static string TranslateInternal(RangeComponents er, uint rowStart, uint colStart, int rowDelta, int colDelta, bool followAbsoluteRefs)
        {
            string newCellRef1 = null;
            string newCellRef2 = null;
            bool errRef1 = false, errRef2 = false;

            if (true)
            {
                if (er.Cell1Error != "")
                {
                    newCellRef1 = er.Cell1Error;
                }
                else
                {
                    RowColumn rc1 = er.Cell1RowColumn;
                    newCellRef1 = TranslateInternal(
                                        er.Cell1RowDollar, rc1.Row,
                                        er.Cell1ColDollar, rc1.Column,
                                        rowStart, colStart,
                                        rowDelta, colDelta,
                                        followAbsoluteRefs,
                                        out errRef1);
                }
            }

            if (er.Cell2 != "")
            {
                if (er.Cell2Error != "")
                {
                    newCellRef2 = er.Cell2Error;
                }
                else
                {
                    RowColumn rc2 = er.Cell2RowColumn;
                    newCellRef2 = TranslateInternal(
                                        er.Cell2RowDollar, rc2.Row,
                                        er.Cell2ColDollar, rc2.Column,
                                        rowStart, colStart,
                                        rowDelta, colDelta,
                                        followAbsoluteRefs,
                                        out errRef2);
                }
            }

            string newRange = "";
            if (er.SheetName != "")
                newRange += er.EscapedSheetName + "!";
            if (errRef1 && (errRef2 || newCellRef2 == null))
            {
                newRange += "#REF!";
            }
            else
            {
                newRange += newCellRef1;
                if (newCellRef2 != null)
                    newRange += ":" + newCellRef2;
            }

            return newRange;
        }

        private static string TranslateInternal(string rowDollar, uint rowIdx, string colDollar, uint colIdx, uint rowStart, uint colStart, int rowDelta, int colDelta, bool followAbsoluteRefs, out bool exceededBounds)
        {
            int row = (int)rowIdx, col = (int)colIdx;
            if (row >= rowStart)
            {
                if (rowDollar != "$" || !followAbsoluteRefs)
                    row = (row + rowDelta);
            }
            if (col >= colStart)
            {
                if (colDollar != "$" || !followAbsoluteRefs)
                    col = (col + colDelta);
            }

            exceededBounds = false;
            if (row < 1 || row > ExcelConstraints.MaxRows ||
                col < 1 || col > ExcelConstraints.MaxColumns)
                exceededBounds = true;

            row = Math.Max(1, Math.Min(row, ExcelConstraints.MaxRows));
            col = Math.Max(1, Math.Min(col, ExcelConstraints.MaxColumns));

            return rowDollar + ExcelAddress.ColumnIndexToName((uint)col) +
                    colDollar + row;
        }
    }
}