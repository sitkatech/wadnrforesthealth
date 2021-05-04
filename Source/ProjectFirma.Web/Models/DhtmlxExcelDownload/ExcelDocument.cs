using System;
using System.IO;
using System.IO.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelDocument : IDisposable
    {
        public static ExcelDocument CreateWorkbook(Package package)
        {
            SpreadsheetDocumentType type = SpreadsheetDocumentType.Workbook;
            SpreadsheetDocument doc = SpreadsheetDocument.Create(package, type);
            return CreateBlankWorkbook(doc);
        }

        public static ExcelDocument CreateWorkbook(Stream stream)
        {
            SpreadsheetDocumentType type = SpreadsheetDocumentType.Workbook;
            SpreadsheetDocument doc = SpreadsheetDocument.Create(stream, type);
            return CreateBlankWorkbook(doc);
        }

        public static ExcelDocument CreateWorkbook(string path)
        {
            SpreadsheetDocumentType type = SpreadsheetDocumentType.Workbook;
            SpreadsheetDocument doc = SpreadsheetDocument.Create(path, type);
            return CreateBlankWorkbook(doc);
        }

        public static ExcelDocument CreateWorkbook(Package package, bool autoSave)
        {
            SpreadsheetDocumentType type = SpreadsheetDocumentType.Workbook;
            SpreadsheetDocument doc = SpreadsheetDocument.Create(package, type, autoSave);
            return CreateBlankWorkbook(doc);
        }

        public static ExcelDocument CreateWorkbook(Stream stream, bool autoSave)
        {
            SpreadsheetDocumentType type = SpreadsheetDocumentType.Workbook;
            SpreadsheetDocument doc = SpreadsheetDocument.Create(stream, type, autoSave);
            return CreateBlankWorkbook(doc);
        }

        public static ExcelDocument CreateWorkbook(string path, bool autoSave)
        {
            SpreadsheetDocumentType type = SpreadsheetDocumentType.Workbook;
            SpreadsheetDocument doc = SpreadsheetDocument.Create(path, type, autoSave);
            return CreateBlankWorkbook(doc);
        }

        public static ExcelDocument Open(Package package)
        {
            SpreadsheetDocument doc = SpreadsheetDocument.Open(package);
            return new ExcelDocument(doc);
        }

        public static ExcelDocument Open(Package package, OpenSettings openSettings)
        {
            SpreadsheetDocument doc = SpreadsheetDocument.Open(package, openSettings);
            return new ExcelDocument(doc);
        }

        public static ExcelDocument Open(Stream stream, bool isEditable)
        {
            SpreadsheetDocument doc = SpreadsheetDocument.Open(stream, isEditable);
            return new ExcelDocument(doc);
        }

        public static ExcelDocument Open(string path, bool isEditable)
        {
            SpreadsheetDocument doc = SpreadsheetDocument.Open(path, isEditable);
            return new ExcelDocument(doc);
        }

        public static ExcelDocument Open(Stream stream, bool isEditable, OpenSettings openSettings)
        {
            SpreadsheetDocument doc = SpreadsheetDocument.Open(stream, isEditable, openSettings);
            return new ExcelDocument(doc);
        }

        public static ExcelDocument Open(string path, bool isEditable, OpenSettings openSettings)
        {
            SpreadsheetDocument doc = SpreadsheetDocument.Open(path, isEditable, openSettings);
            return new ExcelDocument(doc);
        }

        private static ExcelDocument CreateBlankWorkbook(SpreadsheetDocument doc)
        {
            doc.AddWorkbookPart();
            doc.WorkbookPart.Workbook = new Workbook(); // create the worksheet
            doc.WorkbookPart.Workbook.AppendChild(new Sheets());
            ExcelDocument xldoc = new ExcelDocument(doc);
            return xldoc;
        }

        private SpreadsheetDocument _doc;
        private DocumentStyles _styles;
        private DocumentSharedStrings _sharedStrings;

        internal SpreadsheetDocument GetOSpreadsheet()
        {
            return _doc;
        }



        public DocumentStyles Styles
        {
            get
            {

                return _styles;
            }
        }

        internal DocumentSharedStrings SharedStrings
        {
            get
            {
                return _sharedStrings;
            }
        }

        public ExcelWorkbook Workbook { get; protected set; }

        public void EnsureStylesDefined()
        {
            _styles.EnsureStylesheet();
        }

        public ExcelFont CreateFont(string name, double size)
        {
            return new ExcelFont(null, _styles, null) { Name = name, Size = size };
        }

        private ExcelDocument(SpreadsheetDocument doc)
        {
            _doc = doc;
            WorkbookPart wpart = this.GetOSpreadsheet().WorkbookPart;
            _styles = new DocumentStyles(wpart);
            _sharedStrings = new DocumentSharedStrings(wpart);
            this.Workbook = new ExcelWorkbook(this);
        }




        private void Cleanup()
        {
            bool docModified = false;
            foreach (ExcelWorksheet w in this.Workbook.Worksheets)
            {
                if (w.Save())
                    docModified = true;
            }

            WorkbookPart wbpart = this.GetOSpreadsheet().WorkbookPart;

            // Remove calculation chain
            if (docModified)
            {
                if (wbpart.CalculationChainPart != null)
                {
                    wbpart.DeletePart(wbpart.CalculationChainPart);
                }
            }
        }

        internal void RecalcCellReferences(SheetChange sheetChange)
        {
            foreach (var w in this.Workbook.Worksheets)
                w.RecalcCellReferences(sheetChange);
        }

        #region Dispose/Finalize
        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    this.Cleanup();
                    this.SharedStrings.Save();
                }
                // Clean unmanaged resources
                _doc.Dispose();

                _disposed = true;
            }
        }

        ~ExcelDocument()
        {
            Dispose(false);
        }
        #endregion

    }
}