using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelWorksheets : IEnumerable<ExcelWorksheet>
    {
        public ExcelDocument Document { get; protected set; }
        protected Worksheet worksheet;
        private Dictionary<string, ExcelWorksheet> _sheets = new Dictionary<string, ExcelWorksheet>();

        internal ExcelWorksheets(ExcelDocument parent)
        {
            this.Document = parent;
        }

        public ExcelWorksheet Add(string sheetName)
        {
            WorkbookPart wkbkPart = this.Document.GetOSpreadsheet().WorkbookPart;
            if (wkbkPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().Where(s => s.Name == sheetName).Count() > 0)
                throw new InvalidOperationException("Sheet \"" + sheetName + "\" already exists.");

            uint sheetId = (uint)wkbkPart.Workbook.Sheets.Count() + 1;
            WorksheetPart wpart = wkbkPart.AddNewPart<WorksheetPart>();
            wkbkPart.Workbook.GetFirstChild<Sheets>().AppendChild(new Sheet()
            {
                Id = wkbkPart.GetIdOfPart(wpart),
                SheetId = sheetId,
                Name = sheetName
            });
            wpart.Worksheet = new Worksheet();
            worksheet = wpart.Worksheet;
            wpart.Worksheet.Append(new SheetData());
            wpart.Worksheet.Save();
            return this[sheetName];
        }

        public void MoveAfter(string sheetName, string referenceSheetName)
        {
            WorkbookPart wbpart = this.Document.GetOSpreadsheet().WorkbookPart;
            Sheets parent = wbpart.Workbook.Sheets;
            Sheet sheet = parent.Elements<Sheet>().Where(s => s.Name == sheetName).First();
            if (sheet == null)
                throw new InvalidOperationException("Sheet \"" + sheetName + "\" does not exist.");
            Sheet referenceSheet = wbpart.Workbook.Sheets.Elements<Sheet>().Where(s => s.Name == referenceSheetName).First();
            if (referenceSheet == null)
                throw new InvalidOperationException("Sheet \"" + referenceSheetName + "\" does not exist.");
            sheet.Remove();
            parent.InsertAfter(sheet, referenceSheet);
        }

        public void MoveToEnd(string sheetName)
        {
            WorkbookPart wbpart = this.Document.GetOSpreadsheet().WorkbookPart;
            Sheets parent = wbpart.Workbook.Sheets;
            Sheet sheet = parent.Elements<Sheet>().Where(s => s.Name == sheetName).First();
            if (sheet == null)
                throw new InvalidOperationException("Sheet \"" + sheetName + "\" does not exist.");
            sheet.Remove();
            parent.Append(sheet);
        }

        public void Remove(string sheetName)
        {
            WorkbookPart wbpart = this.Document.GetOSpreadsheet().WorkbookPart;
            Sheet sheet = wbpart.Workbook.Sheets.Elements<Sheet>().Where(s => s.Name == sheetName).First();
            if (sheet != null)
            {
                string relationshipId = sheet.Id.Value;
                WorksheetPart existingWpart = (WorksheetPart)wbpart.GetPartById(relationshipId);
                wbpart.DeletePart(existingWpart);
                sheet.Remove();
            }
            else
                throw new InvalidOperationException("Sheet \"" + sheetName + "\" does not exist.");
        }

        public ExcelWorksheet this[string name]
        {
            get
            {
                ExcelWorksheet w;
                if (!_sheets.TryGetValue(name, out w))
                    _sheets[name] = (w = new ExcelWorksheet(name, this.Document));
                return w;
            }
        }

        public IEnumerator<ExcelWorksheet> GetEnumerator()
        {
            foreach (var w in EnumerateWorksheets())
                yield return w;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (var w in EnumerateWorksheets())
                yield return w;
        }

        private IEnumerable<ExcelWorksheet> EnumerateWorksheets()
        {
            WorkbookPart wkbkPart = this.Document.GetOSpreadsheet().WorkbookPart;
            foreach (Sheet sheet in wkbkPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>())
            {
                yield return this[sheet.Name];
            }
        }
    }
}