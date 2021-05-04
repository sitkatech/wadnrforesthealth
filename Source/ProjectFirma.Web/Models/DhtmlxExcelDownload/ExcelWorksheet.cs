using System;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelWorksheet
    {
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public ExcelDocument Document { get; protected set; }
        public ExcelRows Rows { get; protected set; }
        public ExcelColumns Columns { get; protected set; }
        public ExcelCells Cells { get; protected set; }

        internal bool Modified { get; set; }

        private WorksheetCache _sheetCache;

        internal ExcelWorksheet(string name, ExcelDocument parentDoc)
        {
            _name = name;
            this.Document = parentDoc;
            this.Rows = new ExcelRows(this);
            this.Columns = new ExcelColumns(this);
            this.Cells = new ExcelCells(this);

            _sheetCache = new WorksheetCache(this);
            _sheetCache.Load();
        }





        public void InsertRows(uint rowStart, int qty)
        {
            if (qty < 1)
                throw new ArgumentException("Quantity cannot be less than 0");
            if (qty == 0)
                return;

            _sheetCache.InsertOrDeleteRows(rowStart, qty, true);

            SheetChange sheetChange = new SheetChange() { SheetName = this.Name, RowStart = rowStart, RowDelta = qty };
            this.Document.RecalcCellReferences(sheetChange);
        }

        public void InsertColumns(uint colStart, int qty)
        {
            if (qty < 1)
                throw new ArgumentException("Quantity cannot be less than 0");
            if (qty == 0)
                return;

            _sheetCache.InsertOrDeleteColumns(colStart, qty, true);

            SheetChange sheetChange = new SheetChange() { SheetName = this.Name, ColumnStart = colStart, ColumnDelta = qty };
            this.Document.RecalcCellReferences(sheetChange);
        }

        public void PushRows(uint rowStart, int qty)
        {
            if (qty < 1)
                throw new ArgumentException("Quantity cannot be less than 0");
            if (qty == 0)
                return;

            _sheetCache.InsertOrDeleteRows(rowStart, qty, false);

            SheetChange sheetChange = new SheetChange() { SheetName = this.Name, RowStart = rowStart, RowDelta = qty };
            this.Document.RecalcCellReferences(sheetChange);
        }

        public void PushColumns(uint colStart, int qty)
        {
            if (qty < 1)
                throw new ArgumentException("Quantity cannot be less than 0");
            if (qty == 0)
                return;

            _sheetCache.InsertOrDeleteColumns(colStart, qty, false);

            SheetChange sheetChange = new SheetChange() { SheetName = this.Name, ColumnStart = colStart, ColumnDelta = qty };
            this.Document.RecalcCellReferences(sheetChange);
        }

        public void DeleteRows(uint rowStart, int qty)
        {
            if (qty < 1)
                throw new ArgumentException("Quantity cannot be less than 0");
            if (qty == 0)
                return;

            _sheetCache.InsertOrDeleteRows(rowStart, -qty, false);

            SheetChange sheetChange = new SheetChange() { SheetName = this.Name, RowStart = rowStart, RowDelta = -qty };
            this.Document.RecalcCellReferences(sheetChange);
        }

        public void DeleteColumns(uint col, int qty)
        {
            if (qty < 1)
                throw new ArgumentException("Quantity cannot be less than 0");
            if (qty == 0)
                return;

            _sheetCache.InsertOrDeleteColumns(col, -qty, false);

            SheetChange sheetChange = new SheetChange() { SheetName = this.Name, ColumnStart = col, ColumnDelta = -qty };
            this.Document.RecalcCellReferences(sheetChange);
        }


        public void MergeTwoCells(string cell1Name, string cell2Name)
        {
            WorkbookPart wkbkPart = this.Document.GetOSpreadsheet().WorkbookPart;
            Worksheet worksheet = wkbkPart.Workbook.WorkbookPart.WorksheetParts.First().Worksheet;

            MergeCells mergeCells;
            if (worksheet.Elements<MergeCells>().Count() > 0)
            {
                mergeCells = worksheet.Elements<MergeCells>().First();
            }
            else
            {
                mergeCells = new MergeCells();

                // Insert a MergeCells object into the specified position.
                if (worksheet.Elements<CustomSheetView>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<CustomSheetView>().First());
                }
                else if (worksheet.Elements<DataConsolidate>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<DataConsolidate>().First());
                }
                else if (worksheet.Elements<SortState>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SortState>().First());
                }
                else if (worksheet.Elements<AutoFilter>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<AutoFilter>().First());
                }
                else if (worksheet.Elements<Scenarios>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<Scenarios>().First());
                }
                else if (worksheet.Elements<ProtectedRanges>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<ProtectedRanges>().First());
                }
                else if (worksheet.Elements<SheetProtection>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetProtection>().First());
                }
                else if (worksheet.Elements<SheetCalculationProperties>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetCalculationProperties>().First());
                }
                else
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetData>().First());
                }
            }

            // Create the merged cell and append it to the MergeCells collection.
            MergeCell mergeCell = new MergeCell() { Reference = new StringValue(cell1Name + ":" + cell2Name) };
            mergeCells.Append(mergeCell);

        }

        public bool Save()
        {
            if (!this.Modified && !_sheetCache.Modified)
                return false;

            WorkbookPart wkbkPart = this.Document.GetOSpreadsheet().WorkbookPart;
            Sheet sheet = wkbkPart.Workbook.Sheets.Elements<Sheet>().Where(s => s.Name == this.Name).First();



            if (sheet != null)
            {
                string relationshipId = sheet.Id.Value;
                WorksheetPart existingWpart = (WorksheetPart)wkbkPart.GetPartById(relationshipId);
                wkbkPart.DeletePart(existingWpart);
            }
            WorksheetPart wpart = wkbkPart.AddNewPart<WorksheetPart>();

            // RESEARCH: reuse part instead of delete?
            //WorksheetPart wpart;
            //if (sheet != null)
            //{
            //    string relationshipId = sheet.Id.Value;
            //    wpart = (WorksheetPart)wkbkPart.GetPartById(relationshipId);
            //}
            //else
            //    wpart = wkbkPart.AddNewPart<WorksheetPart>();

            if (sheet == null)
            {
                uint sheetId = (uint)wkbkPart.Workbook.Sheets.Count() + 1;
                wkbkPart.Workbook.GetFirstChild<Sheets>().AppendChild(sheet = new Sheet()
                {
                    Id = wkbkPart.GetIdOfPart(wpart),
                    SheetId = sheetId,
                    Name = this.Name
                });
            }
            else
            {
                sheet.Id = wkbkPart.GetIdOfPart(wpart);
            }

            using (OpenXmlWriter writer = OpenXmlWriter.Create(wpart))
            {
                _sheetCache.WriteWorksheetPart(writer);
            }
            return true;
        }

        #region Insert/delete overloads
        public void InsertRow(uint row)
        {
            InsertRows(row, 1);
        }

        public void PushRow(uint row)
        {
            PushRows(row, 1);
        }

        public void InsertColumn(string columnName)
        {
            InsertColumns(ExcelAddress.ColumnNameToIndex(columnName), 1);
        }

        public void InsertColumns(string columnName, int qty)
        {
            InsertColumns(ExcelAddress.ColumnNameToIndex(columnName), qty);
        }

        public void PushColumn(string columnName)
        {
            PushColumns(ExcelAddress.ColumnNameToIndex(columnName), 1);
        }

        public void PushColumns(string columnName, int qty)
        {
            PushColumns(ExcelAddress.ColumnNameToIndex(columnName), qty);
        }

        public void InsertColumn(uint col)
        {
            InsertColumns(col, 1);
        }

        public void DeleteRow(uint row)
        {
            DeleteRows(row, 1);
        }

        public void DeleteColumn(string columnName)
        {
            DeleteColumns(ExcelAddress.ColumnNameToIndex(columnName), 1);
        }

        public void DeleteColumns(string columnName, int qty)
        {
            DeleteColumns(ExcelAddress.ColumnNameToIndex(columnName), qty);
        }

        public void DeleteColumn(uint col)
        {
            DeleteColumns(col, 1);
        }
        #endregion

        internal CellProxy EnsureCell(uint row, uint col)
        {
            return _sheetCache.EnsureCell(row, col);
        }

        internal CellProxy GetCell(uint row, uint col)
        {
            return _sheetCache.GetCell(row, col);
        }

        internal Row EnsureRow(uint row)
        {
            return _sheetCache.EnsureRow(row);
        }

        internal Row GetRow(uint row)
        {
            return _sheetCache.GetRow(row);
        }

        internal Column EnsureColumnDefinition(uint col)
        {
            return _sheetCache.EnsureSingleSpanColumn(col);
        }

        internal Column GetColumnDefinition(uint col)
        {
            return _sheetCache.GetContainingColumn(col);
        }

        internal void DeleteColumnDefinition(uint col)
        {
            _sheetCache.DeleteSingleSpanColumn(col);
        }

        internal WorksheetPart GetOWorksheetPart()
        {
            WorkbookPart wbpart = this.Document.GetOSpreadsheet().WorkbookPart;
            Sheet sheet = wbpart.Workbook.Sheets.Elements<Sheet>().Where(s => s.Name == this.Name).FirstOrDefault();
            if (sheet != null)
            {
                string relationshipId = sheet.Id.Value;
                return (WorksheetPart)wbpart.GetPartById(relationshipId);
            }
            return null;
        }

        internal void RecalcCellReferences(SheetChange sheetChange)
        {
            _sheetCache.RecalcCellReferences(sheetChange);
        }
    }
}