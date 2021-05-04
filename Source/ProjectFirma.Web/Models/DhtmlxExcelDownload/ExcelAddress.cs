using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public static class ExcelAddress
    {
        // ex. $A$1
        // 1 = $ 
        // 2 = A
        // 3 = $
        // 4 = 1
        private static Regex _rgxAddress = new Regex(@"(\$)?([A-Z]+)(\$)?([0-9]+)" +
                                 @"(:((\$)?([A-Z]+)(\$)?([0-9]+)))?$", RegexOptions.Compiled);

        /// <summary>
        /// Convert to RowColumn object with row and column index values
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static RowColumn ToRowColumn(string address)
        {
            Match m = _rgxAddress.Match(address);
            if (!m.Success)
                throw new ArgumentException("Invalid Excel address");
            uint row = uint.Parse(m.Groups[4].Value);
            if (row == 0)
                throw new ArgumentException("Row cannot be zero");
            uint col = ColumnNameToIndex(m.Groups[2].Value);
            return new RowColumn() { Row = row, Column = col };
        }

        /// <summary>
        /// Get column index of address, e.g. A1=1, B1=2, Z1=26.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static uint GetColumn(string address)
        {
            Match m = _rgxAddress.Match(address);
            if (!m.Success)
                throw new ArgumentException("Invalid Excel address");
            return ColumnNameToIndex(m.Groups[2].Value);
        }

        /// <summary>
        /// Convert column number to letters/name, e.g. 1=A, 2=B, ... 27=AA ...
        /// </summary>
        /// <param name="col">Column index</param>
        /// <returns></returns>
        public static string ColumnIndexToName(uint col)
        {
            col--; // 1-indexed --> 0-indexed

            // Determine row ref length L, then how many row refs
            // for lengths 1 to length L-1 -- this is the number
            // of combinations that must be skipped over before doing
            // Base-26 conversion

            // A-Z     (1..26)               = 26       
            // --> 0..(26-1)
            // AA-ZZ   (1..26)(1..26)        = 26*26
            // --> 0..(26*26-1) start from 26
            // --> 26..702
            // AAA-ZZZ (1..26)(1..26)(1..26) = 26*26*26
            // --> 0..(26*26*26-1) start from 702
            // --> 702..18277

            int len = 1;
            uint colCountForThisLen = 26;
            uint lastColForLen = 25;
            uint skip = 0; // Cumulative no. of columns for previous lengths

            while (col > lastColForLen)
            {
                colCountForThisLen *= 26;
                skip = lastColForLen + 1;
                lastColForLen += colCountForThisLen;
                len++;
            }

            col -= skip;

            char[] colRefChars = new char[len];
            for (var idx = 0; idx < len; idx++)
            {
                colRefChars[len - idx - 1] = (char)('A' + (int)(col % 26));
                col /= 26;
            }
            return new string(colRefChars);
        }

        /// <summary>
        /// Convert column name to number, e.g. A=1, B=2, ... AA=27 ...
        /// </summary>
        /// <param name="colName">Name of column for cell address, e.g. "A" for cell "A1"</param>
        /// <returns></returns>
        public static uint ColumnNameToIndex(string colName)
        {
            if (string.IsNullOrEmpty(colName))
                throw new ArgumentException("Invalid columnName [" + colName + "]");

            // Convert column name
            int len = colName.Length;
            uint colCountForThisLen = 26;
            uint lastColForLen = 25;
            uint skip = 0; // Cumulative no. of columns for previous lengths

            for (int ctr = 0; ctr < len; ctr++)
            {
                colCountForThisLen *= 26;
                if (ctr < len - 1)
                    skip = lastColForLen + 1;
                lastColForLen += colCountForThisLen;
            }
            uint col = 0;
            for (int ctr = 0; ctr < len; ctr++)
            {
                col *= 26;
                col += (uint)(colName[ctr] - (int)'A');
            }
            col += skip;
            return col + 1; // 0-indexed --> 1-indexed
        }
    }
}