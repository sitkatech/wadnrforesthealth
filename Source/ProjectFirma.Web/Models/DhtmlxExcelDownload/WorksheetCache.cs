using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    internal class WorksheetCache
    {
        #region Static members
        public static RowColumn CacheIndexToRowCol(ulong cacheIdx)
        {
            RowColumn rc = new RowColumn();
            // Note: 0-indexed --> 1-indexed
            rc.Row = (uint)(cacheIdx / (ulong)ExcelConstraints.MaxColumns) + 1;
            rc.Column = (uint)(cacheIdx % (ulong)ExcelConstraints.MaxColumns) + 1;
            return rc;
        }

        public static void CacheIndexToRowCol(ulong cacheIdx, out uint rowIdx, out uint colIdx)
        {
            // Note: 0-indexed --> 1-indexed
            rowIdx = (uint)(cacheIdx / (ulong)ExcelConstraints.MaxColumns) + 1;
            colIdx = (uint)(cacheIdx % (ulong)ExcelConstraints.MaxColumns) + 1;
        }

        public static ulong RowColToCacheIndex(uint rowIdx, uint colIdx)
        {
            return (rowIdx - 1) * (uint)ExcelConstraints.MaxColumns + (colIdx - 1);
        }
        #endregion

        public bool Modified { get; set; }

        private List<OpenXmlElement> _cachedElements = new List<OpenXmlElement>();
        private SortedList<uint, SortedList<uint, CellProxy>> _cachedCells = new SortedList<uint, SortedList<uint, CellProxy>>();
        private SortedList<uint, Row> _cachedRows = new SortedList<uint, Row>();

        private ExcelWorksheet _wsheet;

        public WorksheetCache(ExcelWorksheet wsheet)
        {
            _wsheet = wsheet;
        }





        public void Load()
        {
            WorksheetPart wp = _wsheet.GetOWorksheetPart();
            if (wp == null)
                return;

            Action<Cell> readCell = (cell) =>
            {
                RowColumn rc = ExcelAddress.ToRowColumn(cell.CellReference);
                CellProxy cellProxy = this.EnsureCell(rc.Row, rc.Column);
                if (cell.DataType != null)
                {
                    cellProxy.DataType = cell.DataType.Value;
                    if (cell.DataType.Value == CellValues.InlineString)
                        cellProxy.Value = cell.InlineString.Text.Text;
                    else
                    {
                        if (cell.CellValue != null)
                            cellProxy.Value = cell.CellValue.Text;
                        else
                            cellProxy.Value = string.Empty;
                    }
                }
                else
                {
                    if (cell.CellValue != null)
                        cellProxy.Value = cell.CellValue.Text;
                }
                if (cell.StyleIndex != null)
                    cellProxy.StyleIndex = cell.StyleIndex;
                if (cell.ShowPhonetic != null)
                    cellProxy.ShowPhonetic = cell.ShowPhonetic;
                if (cell.ValueMetaIndex != null)
                    cellProxy.ValueMetaIndex = cell.ValueMetaIndex;
                if (cell.CellFormula != null)
                {
                    cellProxy.CreateFormula();
                    cellProxy.Formula.Text = cell.CellFormula.Text;
                    cellProxy.Formula.R1 = cell.CellFormula.R1;
                    cellProxy.Formula.R2 = cell.CellFormula.R2;
                    cellProxy.Formula.Reference = cell.CellFormula.Reference;
                    if (cell.CellFormula.AlwaysCalculateArray != null)
                        cellProxy.Formula.AlwaysCalculateArray = cell.CellFormula.AlwaysCalculateArray;
                    if (cell.CellFormula.Bx != null)
                        cellProxy.Formula.Bx = cell.CellFormula.Bx;
                    if (cell.CellFormula.CalculateCell != null)
                        cellProxy.Formula.CalculateCell = cell.CellFormula.CalculateCell;
                    if (cell.CellFormula.DataTable2D != null)
                        cellProxy.Formula.DataTable2D = cell.CellFormula.DataTable2D;
                    if (cell.CellFormula.DataTableRow != null)
                        cellProxy.Formula.DataTableRow = cell.CellFormula.DataTableRow;
                    if (cell.CellFormula.FormulaType != null)
                        cellProxy.Formula.FormulaType = cell.CellFormula.FormulaType;
                    if (cell.CellFormula.Input1Deleted != null)
                        cellProxy.Formula.Input1Deleted = cell.CellFormula.Input1Deleted;
                    if (cell.CellFormula.Input2Deleted != null)
                        cellProxy.Formula.Input2Deleted = cell.CellFormula.Input2Deleted;
                    if (cell.CellFormula.SharedIndex != null)
                        cellProxy.Formula.SharedIndex = cell.CellFormula.SharedIndex;

                    cellProxy.Value = null; // Don't cache/store values for formulas
                }
            };

            OpenXmlReader reader = OpenXmlReader.Create(_wsheet.GetOWorksheetPart());
            bool inWorksheet = false;
            bool inSheetData = false;
            while (reader.Read())
            {
                if (inWorksheet && reader.IsStartElement)
                {
                    if (reader.ElementType == typeof(Row) && reader.IsStartElement)
                    {
                        // ----------------------------------------
                        // Scan row if anything other than RowIndex and Spans has been set,
                        // if so then cache
                        Row r = (Row)reader.LoadCurrentElement();
                        var needToCacheRow = (from a in r.GetAttributes()
                                              let ln = a.LocalName
                                              where ln != "r" && ln != "spans"
                                              select a).Any();

                        if (needToCacheRow)
                        {
                            _cachedRows.Add(r.RowIndex, (Row)r.CloneNode(false));
                        }
                        foreach (Cell cell in r.Elements<Cell>())
                        {
                            readCell(cell);
                        }
                        // ----------------------------------------
                    }
                    else if (reader.ElementType == typeof(SheetData))
                    {
                        inSheetData = reader.IsStartElement;
                    }
                    else if (reader.IsStartElement)
                    {
                        var e = reader.LoadCurrentElement();
                        _cachedElements.Add(e);
                    }
                }
                else if (reader.ElementType == typeof(Worksheet))
                {
                    inWorksheet = reader.IsStartElement;
                }
            }

            // Reset modified to false (loading sets it to true due to CellProxy loading)
            this.Modified = false;
        }

        public void RecalcCellReferences(SheetChange sheetChange)
        {
            foreach (var i in this.Cells())
            {
                var c = i.Value;
                if (c.Formula != null)
                {
                    c.Formula.Text = ExcelFormula.TranslateForSheetChange(c.Formula.Text, sheetChange, _wsheet.Name);
                    c.Formula.R1 = ExcelRange.TranslateForSheetChange(c.Formula.R1, sheetChange, _wsheet.Name);
                    c.Formula.R2 = ExcelRange.TranslateForSheetChange(c.Formula.R2, sheetChange, _wsheet.Name);
                    c.Formula.Reference = ExcelRange.TranslateForSheetChange(c.Formula.Reference, sheetChange, _wsheet.Name);
                }
            }

            // Adjust conditional formatting
            List<ConditionalFormatting> cfToRemoveList = new List<ConditionalFormatting>();
            foreach (var cf in this.GetElements<ConditionalFormatting>())
            {
                bool removeCf = false;
                List<StringValue> lst = new List<StringValue>();
                foreach (var sqrefItem in cf.SequenceOfReferences.Items)
                {
                    string newRef = ExcelRange.TranslateForSheetChange(sqrefItem.Value, sheetChange, _wsheet.Name);
                    if (!newRef.StartsWith("#")) // no error
                        lst.Add(new StringValue(newRef));
                    else
                    {
                        cfToRemoveList.Add(cf);
                        removeCf = true;
                        break;
                    }
                }
                if (removeCf)
                    break;
                cf.SequenceOfReferences = new ListValue<StringValue>(lst);
                foreach (var f in cf.Descendants<Formula>())
                {
                    f.Text = ExcelFormula.TranslateForSheetChange(f.Text, sheetChange, _wsheet.Name);
                }
            }
            foreach (ConditionalFormatting cf in cfToRemoveList)
            {
                this.RemoveElement(cf);
            }
        }

        public void WriteWorksheetPart(OpenXmlWriter writer)
        {
            // TODO: final cleanup
            // - merge redundant columns
            // - remove unused shared strings

            // Remove rows without cells
            foreach (var rowItem in _cachedCells.ToList())
            {
                if (rowItem.Value.Count == 0)
                    _cachedCells.Remove(rowItem.Key);
            }

            // Simulate rows for cached rows
            foreach (var rowIdx in _cachedRows.Keys)
            {
                if (!_cachedCells.ContainsKey(rowIdx))
                    _cachedCells[rowIdx] = new SortedList<uint, CellProxy>();
            }

            // Get first and last addresses
            uint minRow = uint.MaxValue;
            uint minCol = uint.MaxValue;
            uint maxRow = 0;
            uint maxCol = 0;

            foreach (var rowItem in _cachedCells)
            {
                uint rowIdx = rowItem.Key;
                var cells = rowItem.Value;
                if (minRow == uint.MaxValue)
                    minRow = rowIdx;
                maxRow = rowIdx;
                if (cells.Count > 0)
                {
                    minCol = Math.Min(minCol, cells.Keys.First());
                    maxCol = Math.Max(maxCol, cells.Keys.Last());
                }
            }

            string firstAddress = null, lastAddress = null;
            if (minRow < uint.MaxValue && minCol < uint.MaxValue)
            {
                firstAddress = RowColumn.ToAddress(minRow, minCol);
                if (minRow != maxRow || minCol != maxCol)
                    lastAddress = RowColumn.ToAddress(maxRow, maxCol);
            }
            else
                firstAddress = "A1";

            writer.WriteStartDocument();
            writer.WriteStartElement(new Worksheet());
            foreach (string childTagName in SchemaInfo.WorksheetChildSequence)
            {
                if (childTagName == "sheetData")
                    WriteSheetData(writer);
                else if (childTagName == "dimension")
                {
                    string dimensionRef = firstAddress + (lastAddress != null ? ":" + lastAddress : "");
                    writer.WriteElement(new SheetDimension() { Reference = dimensionRef });
                }
                else if (childTagName == "sheetViews")
                {
                    SheetViews svs = GetFirstElement<SheetViews>();
                    if (svs != null)
                    {
                        foreach (SheetView sv in svs.Elements<SheetView>())
                        {
                            foreach (Selection sel in sv.Elements<Selection>())
                            {
                                if (minRow < uint.MaxValue)
                                {
                                    sel.ActiveCell = firstAddress;
                                    sel.SequenceOfReferences = new ListValue<StringValue>(new StringValue[] { new StringValue(firstAddress) });
                                }
                                else
                                    sel.Remove();
                            }
                        }
                        writer.WriteElement(svs);
                    }
                }
                else
                {
                    foreach (var e in GetElementsByTagName(childTagName))
                    {
                        writer.WriteElement(e);
                    }
                }
            }
            writer.WriteEndElement(); // worksheet
        }




        private void WriteSheetData(OpenXmlWriter writer)
        {
            writer.WriteStartElement(new SheetData());
            foreach (var rowItem in _cachedCells)
            {
                uint rowIdx = rowItem.Key;
                var cells = rowItem.Value;
                var rowAttrs = EnumRowAttributes(rowIdx, cells).ToList();
                if (rowAttrs.Count > 0 || cells.Count > 0)
                {
                    writer.WriteStartElement(new Row(), rowAttrs);

                    foreach (var cellItem in cells)
                    {
                        uint colIdx = cellItem.Key;
                        CellProxy cellProxy = cellItem.Value;
                        writer.WriteStartElement(new Cell(), EnumCellProxyAttributes(rowIdx, colIdx, cellProxy));
                        if (cellProxy.Formula != null)
                        {
                            CellFormula cf = new CellFormula(cellProxy.Formula.Text);
                            if (cellProxy.Formula.R1 != null)
                                cf.R1 = cellProxy.Formula.R1;
                            if (cellProxy.Formula.R2 != null)
                                cf.R2 = cellProxy.Formula.R2;
                            if (cellProxy.Formula.Reference != null)
                                cf.Reference = cellProxy.Formula.Reference;

                            if (cellProxy.Formula.AlwaysCalculateArray != null)
                                cf.AlwaysCalculateArray = cellProxy.Formula.AlwaysCalculateArray;
                            if (cellProxy.Formula.Bx != null)
                                cf.Bx = cellProxy.Formula.Bx;
                            if (cellProxy.Formula.CalculateCell != null)
                                cf.CalculateCell = cellProxy.Formula.CalculateCell;
                            if (cellProxy.Formula.DataTable2D != null)
                                cf.DataTable2D = cellProxy.Formula.DataTable2D;
                            if (cellProxy.Formula.DataTableRow != null)
                                cf.DataTableRow = cellProxy.Formula.DataTableRow;
                            if (cellProxy.Formula.FormulaType != null)
                                cf.FormulaType = cellProxy.Formula.FormulaType;
                            if (cellProxy.Formula.Input1Deleted != null)
                                cf.Input1Deleted = cellProxy.Formula.Input1Deleted;
                            if (cellProxy.Formula.Input2Deleted != null)
                                cf.Input2Deleted = cellProxy.Formula.Input2Deleted;
                            if (cellProxy.Formula.SharedIndex != null)
                                cf.SharedIndex = cellProxy.Formula.SharedIndex;

                            writer.WriteElement(cf);
                        }
                        if (cellProxy.Value != null)
                            writer.WriteElement(new CellValue(cellProxy.SerializedValue));
                        writer.WriteEndElement(); // c
                    }

                    writer.WriteEndElement(); // row
                }
            }
            writer.WriteEndElement(); // sheetData
        }

        public CellProxy GetCell(uint row, uint col)
        {
            CellProxy c;
            SortedList<uint, CellProxy> rowCache;
            if (_cachedCells.TryGetValue(row, out rowCache))
                if (rowCache.TryGetValue(col, out c))
                    return c;
            return null;
        }

        public CellProxy EnsureCell(uint row, uint col)
        {
            CellProxy c = null;
            SortedList<uint, CellProxy> rowCache;
            if (!_cachedCells.TryGetValue(row, out rowCache))
            {
                c = new CellProxy(this);
                AddCellToCache(c, row, col);
            }
            else
            {
                if (!rowCache.TryGetValue(col, out c))
                {
                    c = new CellProxy(this);
                    if (rowCache != null)
                        AddCellToCache(c, rowCache, col);
                }
            }
            return c;
        }

        public Row EnsureRow(uint row)
        {
            Row r = null;
            if (!_cachedRows.TryGetValue(row, out r))
                _cachedRows[row] = (r = new Row() { RowIndex = row });
            return r;
        }

        public Row GetRow(uint row)
        {
            Row r;
            if (_cachedRows.TryGetValue(row, out r))
                return r;
            return null;
        }

        public Column EnsureSingleSpanColumn(uint col)
        {
            Columns cols = this.GetFirstElement<Columns>();
            if (cols == null)
            {
                cols = new Columns();
                _cachedElements.Add(cols);
            }
            Column colExisting = (from c in cols.Elements<Column>()
                                  where c.Min <= col && c.Max >= col
                                  select c).FirstOrDefault();

            if (colExisting != null)
            {
                if (colExisting.Min < col)
                {
                    Column colBefore = (Column)colExisting.CloneNode(false);
                    colBefore.Min = colExisting.Min;
                    colBefore.Max = col - 1;
                    colExisting.Min = col;
                    cols.InsertBefore(colBefore, colExisting);
                }
                if (colExisting.Max > col)
                {
                    Column colAfter = (Column)colExisting.CloneNode(false);
                    colAfter.Min = col + 1;
                    colAfter.Max = colExisting.Max;
                    colExisting.Max = col;
                    cols.InsertAfter(colAfter, colExisting);
                }
                return colExisting;
            }
            else
            {
                Column colNew = new Column() { Min = col, Max = col };
                Column colNext = (from c in cols.Elements<Column>()
                                  where c.Min > col
                                  select c).FirstOrDefault();
                if (colNext != null)
                    cols.InsertBefore(colNew, colNext);
                else
                    cols.Append(colNew);
                return colNew;
            }
        }



        public void DeleteSingleSpanColumn(uint col)
        {
            Columns cols = this.GetFirstElement<Columns>();
            if (cols != null)
            {
                Column colExisting = (from c in cols.Elements<Column>()
                                      where c.Min == col && c.Max == col
                                      select c).FirstOrDefault();
                if (colExisting != null)
                    colExisting.Remove();
            }
        }

        public Column GetContainingColumn(uint col)
        {
            Columns cols = this.GetFirstElement<Columns>();
            if (cols != null)
            {
                Column colExisting = (from c in cols.Elements<Column>()
                                      where c.Min <= col && c.Max >= col
                                      select c).FirstOrDefault();
                return colExisting;
            }
            return null;
        }

        private void AddCellToCache(CellProxy c, uint rowIdx, uint colIdx)
        {
            this.Modified = true;

            SortedList<uint, CellProxy> rowCache;
            if (!_cachedCells.TryGetValue(rowIdx, out rowCache))
            {
                rowCache = new SortedList<uint, CellProxy>();
                _cachedCells[rowIdx] = rowCache;
            }
            rowCache[colIdx] = c;
        }

        /// <summary>
        /// For optimization -- enables use of rowCache if we already have it
        /// </summary>
        /// <param name="c"></param>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        private void AddCellToCache(CellProxy c, SortedList<uint, CellProxy> rowCache, uint colIdx)
        {
            this.Modified = true;

            rowCache[colIdx] = c;
        }

        private CellProxy RemoveCellFromCache(uint rowIdx, uint colIdx)
        {
            this.Modified = true;

            CellProxy c = null;
            SortedList<uint, CellProxy> rowCache;
            if (_cachedCells.TryGetValue(rowIdx, out rowCache))
                if (rowCache.TryGetValue(colIdx, out c))
                {
                    rowCache.Remove(colIdx);
                    return c;
                }
            return null;
        }

        private T GetFirstElement<T>() where T : OpenXmlElement
        {
            return (T)(from e in _cachedElements
                       where e.GetType() == typeof(T)
                       select e).FirstOrDefault();
        }

        private IEnumerable<T> GetElements<T>() where T : OpenXmlElement
        {
            return (from e in _cachedElements
                    where e.GetType() == typeof(T)
                    select (T)e);
        }

        private IEnumerable<OpenXmlElement> GetElementsByTagName(string name)
        {
            return (from e in _cachedElements
                    where e.LocalName == name
                    select e);
        }

        private void RemoveElement(OpenXmlElement e)
        {
            this.Modified = true;

            _cachedElements.Remove(e);
        }

        private IEnumerable<KeyValuePair<ulong, CellProxy>> Cells()
        {
            foreach (var rowCacheItm in _cachedCells)
            {
                foreach (var itm in rowCacheItm.Value)
                {
                    ulong cacheIdx = RowColToCacheIndex(rowCacheItm.Key, itm.Key);
                    yield return new KeyValuePair<ulong, CellProxy>(cacheIdx, itm.Value);
                }
            }
        }

        private void LoopCells(Action<uint, uint> action)
        {
            IList<uint> rowIdxList = _cachedCells.Keys.ToList();
            uint firstRow = rowIdxList.First();
            uint lastRow = rowIdxList.Last();

            for (uint row = firstRow; row <= lastRow; row++)
            {
                SortedList<uint, CellProxy> rowCache;
                if (_cachedCells.TryGetValue(row, out rowCache))
                {
                    IList<uint> colIdxList = rowCache.Keys.ToList();
                    uint firstCol = colIdxList.First();
                    uint lastCol = colIdxList.Last();

                    for (uint col = firstCol; col <= lastCol; col++)
                    {
                        if (rowCache.ContainsKey(col))
                            action(row, col);
                    }
                }
            }
        }

        private void LoopCellsReverse(Action<uint, uint> action)
        {
            IList<uint> rowIdxList = _cachedCells.Keys.ToList();
            uint firstRow = rowIdxList.First();
            uint lastRow = rowIdxList.Last();

            for (uint row = lastRow; row >= firstRow; row--)
            {
                SortedList<uint, CellProxy> rowCache;
                if (_cachedCells.TryGetValue(row, out rowCache))
                {
                    IList<uint> colIdxList = rowCache.Keys.ToList();
                    uint firstCol = colIdxList.First();
                    uint lastCol = colIdxList.Last();

                    for (uint col = lastCol; col >= firstCol; col--)
                    {
                        if (rowCache.ContainsKey(col))
                            action(row, col);
                    }
                }
            }
        }

        public void InsertOrDeleteRows(uint rowStart, int rowDelta, bool copyPreviousStyle)
        {
            if (rowDelta == 0)
                return;

            this.Modified = true;

            IList<uint> rowIndexes;
            if (rowDelta > 0)
                rowIndexes = _cachedCells.Keys.Reverse().ToList();
            else
                rowIndexes = _cachedCells.Keys.ToList();

            var newCellProxies = new SortedList<uint, SortedList<uint, CellProxy>>();
            foreach (uint rowIdx in rowIndexes)
            {
                uint newRowIdx;
                if (rowIdx >= rowStart)
                    newRowIdx = (uint)(rowIdx + rowDelta);
                else
                    newRowIdx = rowIdx;

                newCellProxies[newRowIdx] = _cachedCells[rowIdx];
            }
            _cachedCells = newCellProxies;

            // Adjust cached <row> elements
            IEnumerable<uint> affectedCachedRowIndexes;
            if (rowDelta > 0)
                affectedCachedRowIndexes = _cachedRows.Keys.Where(k => k >= rowStart).Reverse().ToList();
            else
                affectedCachedRowIndexes = _cachedRows.Keys.Where(k => k >= rowStart).ToList();
            foreach (var rowIdx in affectedCachedRowIndexes)
            {
                Row r = _cachedRows[rowIdx];
                int newRowIdx = (int)r.RowIndex.Value + rowDelta;
                _cachedRows.Remove(rowIdx);

                // If delta is negative, rows will be not put back, i.e. deleted
                if (newRowIdx >= rowStart && newRowIdx >= 1)
                {
                    r.RowIndex = (uint)(newRowIdx);
                    _cachedRows[r.RowIndex] = r;
                }
            }

            if (rowDelta > 0 && copyPreviousStyle)
            {
                CreateRowCopies(rowStart - 1, rowDelta,
                    cOld =>
                    {
                        if (cOld.StyleIndex != null)
                        {
                            var cNew = new CellProxy(this);
                            cNew.StyleIndex = cOld.StyleIndex;
                            return cNew;
                        }
                        return null;
                    }
                );
            }
        }

        public void InsertOrDeleteColumns(uint colStart, int colDelta, bool copyPreviousStyle)
        {
            if (colDelta == 0)
                return;

            this.Modified = true;

            Action<uint, uint> shiftCells = (rowIdx, colIdx) =>
            {
                if (colIdx >= colStart)
                {
                    CellProxy c = RemoveCellFromCache(rowIdx, colIdx);

                    int newColIdx = (int)colIdx + colDelta;
                    // If delta is negative, cells will be not put back, i.e. deleted
                    if (newColIdx >= colStart && newColIdx >= 1)
                    {
                        if (colIdx >= ExcelConstraints.MaxColumns)
                            throw new InvalidOperationException("Max number of columns exceeded");

                        AddCellToCache(c, rowIdx, (uint)newColIdx);
                    }
                }
            };

            if (colDelta > 0)
                LoopCellsReverse(shiftCells);
            else
                LoopCells(shiftCells);

            // Adjust cached <col> elements
            Columns cols = this.GetFirstElement<Columns>();
            if (cols != null)
            {
                List<Column> colsToRemove = new List<Column>();
                foreach (Column col in cols)
                {
                    if (col.Min >= colStart)
                        col.Min = (uint)Math.Max(0, col.Min + colDelta);
                    if (col.Max >= colStart)
                        col.Max = (uint)Math.Max(0, col.Max + colDelta);
                    if (col.Min <= 0 || col.Max < col.Min)
                        colsToRemove.Add(col);
                }
                foreach (Column col in colsToRemove)
                {
                    col.Remove();
                }

                Column colPrev = (from col in cols.Elements<Column>()
                                  where col.Max == colStart - 1
                                  select col).FirstOrDefault();
                if (colPrev != null)
                {
                    colPrev.Max = (uint)(colPrev.Max + colDelta);
                }
                if (cols.ChildElements.Count == 0)
                    _cachedElements.Remove(cols);
            }

            if (colDelta > 0)
            {
                if (copyPreviousStyle)
                {
                    CreateColumnCopies(colStart - 1, colDelta,
                        cOld =>
                        {
                            if (cOld.StyleIndex != null)
                            {
                                var cNew = new CellProxy(this);
                                cNew.StyleIndex = cOld.StyleIndex;
                                return cNew;
                            }
                            return null;
                        }
                    );
                }
                else
                {
                    // Ensure this column does not have a column defintion
                    // so we don't copy previous column style

                    // TODO: make this more efficient
                    for (uint ctr = colStart; ctr < colStart + colDelta; ctr++)
                    {
                        EnsureSingleSpanColumn(ctr);
                        DeleteSingleSpanColumn(ctr);
                    }
                }
            }
        }

        private void CreateRowCopies(uint rowFromIdx, int numOfRows, Func<CellProxy, CellProxy> fnCreate)
        {
            SortedList<uint, CellProxy> rowCache;
            if (_cachedCells.TryGetValue(rowFromIdx, out rowCache))
            {
                Row cachedRow;
                _cachedRows.TryGetValue(rowFromIdx, out cachedRow);

                foreach (var newRowIdx in Enumerable.Range((int)rowFromIdx + 1, numOfRows))
                {
                    if (cachedRow != null)
                    {
                        Row cachedRowCopy = (Row)cachedRow.CloneNode(false);
                        cachedRowCopy.RowIndex = (uint)newRowIdx;
                        _cachedRows.Add(cachedRowCopy.RowIndex, cachedRowCopy);
                    }

                    foreach (var colIdx in rowCache.Keys)
                    {
                        RowColumn rcOld = new RowColumn() { Row = rowFromIdx, Column = colIdx };
                        RowColumn rcNew = new RowColumn() { Row = (uint)newRowIdx, Column = colIdx };
                        CellProxy cOld = GetCell(rcOld.Row, rcOld.Column);
                        CellProxy cNew = fnCreate(cOld);
                        if (cNew != null)
                            AddCellToCache(cNew, rcNew.Row, rcNew.Column);
                    }
                }
            }
        }

        private void CreateColumnCopies(uint colFromIdx, int numOfColumns, Func<CellProxy, CellProxy> fnCreate)
        {
            Action<uint, uint> actionCopyColumn = (rowIdx, colIdx) =>
            {
                RowColumn rcOld = new RowColumn() { Row = rowIdx, Column = colIdx };
                if (rcOld.Column == colFromIdx)
                {
                    foreach (var newColIdx in Enumerable.Range((int)colFromIdx + 1, numOfColumns))
                    {
                        RowColumn rcNew = new RowColumn() { Row = rcOld.Row, Column = (uint)newColIdx };
                        CellProxy cOld = GetCell(rcOld.Row, rcOld.Column);
                        if (cOld != null)
                        {
                            CellProxy cNew = fnCreate(cOld);
                            if (cNew != null)
                                AddCellToCache(cNew, rcNew.Row, rcNew.Column);
                        }
                    }
                }
            };
            LoopCells(actionCopyColumn);
        }

        private IEnumerable<OpenXmlAttribute> EnumRowAttributes(uint thisRowIdx, SortedList<uint, CellProxy> cells)
        {
            Row cachedRow;
            bool returnedAttr = false;
            if (_cachedRows.TryGetValue(thisRowIdx, out cachedRow))
            {
                var cachedRowAttrsToSave = from a in cachedRow.GetAttributes()
                                           where a.LocalName != "r" && a.LocalName != "spans"
                                           select a;
                foreach (var att in cachedRowAttrsToSave)
                {
                    yield return (att);
                    returnedAttr = true;
                }
            }

            // Don't write row if there's nothing to write
            if (!returnedAttr && cells.Count == 0)
            {
            }
            else
            {
                yield return (new OpenXmlAttribute(null,"r", null, thisRowIdx.ToString()));
                if (cells.Count > 0)
                {
                    uint firstColIdx = cells.Keys.First();
                    uint lastColIdx = cells.Keys.Last();
                    yield return (new OpenXmlAttribute(null, "spans", null, firstColIdx + ":" + lastColIdx));
                }
            }
        }

        private IEnumerable<OpenXmlAttribute> EnumCellProxyAttributes(uint row, uint col, CellProxy cellProxy)
        {
            yield return (new OpenXmlAttribute(null, "r", null, RowColumn.ToAddress(row, col)));
            if (cellProxy.DataType != null)
                yield return (new OpenXmlAttribute(null, "t", null, STCellType((CellValues)cellProxy.DataType)));
            if (cellProxy.StyleIndex != null)
                yield return (new OpenXmlAttribute(null, "s", null, cellProxy.StyleIndex.Value.ToString()));
        }

        private string STCellType(CellValues v)
        {
            switch (v)
            {
                case CellValues.Boolean:
                    return "b";
                case CellValues.Date:
                    return "d";
                case CellValues.Error:
                    return "e";
                case CellValues.InlineString:
                    return "inlineStr";
                case CellValues.Number:
                    return "n";
                case CellValues.SharedString:
                    return "s";
                case CellValues.String:
                    return "str";
                default:
                    return "";
            }
        }
    }
}
