using System;
using System.Text;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public static class ExcelFormula
    {
        private static Parser _parser = new Parser(new Scanner());

        /// <summary>
        /// Parses formula into a tree.
        /// DEV NOTE: Subject to change.
        /// </summary>
        /// <param name="formula">Formula, e.g. SUM(A1,Sheet2!B2)</param>
        /// <returns></returns>
        public static ParseTree Parse(string formula)
        {
            return _parser.Parse(formula);
        }

        /// <summary>
        /// Translate a formula.
        /// </summary>
        /// <param name="formula">Formula, e.g. SUM(A1,Sheet2!B2)</param>
        /// <param name="rowDelta">Number of rows to move up(+) or down(-)</param>
        /// <param name="colDelta">Number of columns to move right(+) or left(-)</param>
        /// <returns></returns>
        public static string Translate(string formula, int rowDelta, int colDelta)
        {
            if (formula == null)
                return null;

            ParseTree tree = ExcelFormula.Parse(formula);
            StringBuilder rebuilt = new StringBuilder();
            if (tree.Errors.Count > 0)
                throw new ArgumentException("Error in parsing formula");
            BuildTranslated(rebuilt, tree,
                            n => TranslateRangeParseNodeWithOffset(n, rowDelta, colDelta));
            return rebuilt.ToString();
        }

        /// <summary>
        /// Translate a fornula due to a sheet change, e.g. insertion of rows.
        /// </summary>
        /// <param name="formula">Formula, e.g. SUM(A1,Sheet2!B2)</param>
        /// <param name="sheetChange">Details of change</param>
        /// <param name="currentSheetName">The sheet where the range is, to determine if this range is affected. If sheetChange.SheetName is null and currentSheetName is null, translation is always applied.</param>
        /// <param name="currentSheetName">The sheet where the range is, to determine if this range is affected. If sheetChange.SheetName is null and currentSheetName is null, translation is always applied.</param>
        /// <returns></returns>
        public static string TranslateForSheetChange(string formula, SheetChange sheetChange, string currentSheetName)
        {
            if (formula == null)
                return null;

            ParseTree tree = ExcelFormula.Parse(formula);
            StringBuilder rebuilt = new StringBuilder();
            if (tree.Errors.Count > 0)
                throw new ArgumentException("Error in parsing formula");
            BuildTranslated(rebuilt, tree,
                            n => TranslateRangeParseNodeForSheetChange(n, sheetChange, currentSheetName));
            return rebuilt.ToString();
        }

        private static void BuildTranslated(StringBuilder buf, ParseNode n, Func<ParseNode, string> translateFn)
        {
            foreach (ParseNode sub in n.Nodes)
            {
                if (sub.Token.Type == TokenType.Range)
                    buf.Append(translateFn(sub));
                else
                {
                    if (sub.Token.Text != null)
                        buf.Append(sub.Token.Text);
                    BuildTranslated(buf, sub, translateFn);
                }
            }
        }

        private static string TranslateRangeParseNodeWithOffset(ParseNode rangeNode, int rowDelta, int colDelta)
        {
            string range = "";
            foreach (ParseNode sub in rangeNode.Nodes)
                range += sub.Token.Text;
            return ExcelRange.Translate(range, rowDelta, colDelta);
        }

        private static string TranslateRangeParseNodeForSheetChange(ParseNode rangeNode, SheetChange sheetChange, string currentSheetName)
        {
            string range = "";
            foreach (ParseNode sub in rangeNode.Nodes)
                range += sub.Token.Text;
            return ExcelRange.TranslateForSheetChange(range, sheetChange, currentSheetName);
        }
    }
}