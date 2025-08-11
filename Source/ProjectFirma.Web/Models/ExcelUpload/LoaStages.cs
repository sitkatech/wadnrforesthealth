/*-----------------------------------------------------------------------
<copyright file="BudgetStageImports.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Data;
using System.Linq;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Models.ExcelUpload
{
    public class LoaStageImports : List<LoaStageImport>
    {
        public const string FbmsUnexpendedPayrecV3SheetName = "Master Data";
        /// <summary>
        /// If there is only a single worksheet in a file, try to use it, no matter what it is named
        /// </summary>
        public const bool UseExistingSheetNameIfSingleSheetFound = true;

        public static LoaStageImports LoadFromXlsFile(DataTable dataTable, List<string> errorList)
        {
            
            var columnMappingDictionary = EnsureWorksheetHasCorrectShape(dataTable);

            // Create index-to-row dictionary. This is generally discouraged, but we don't have any other way to get the exact cell address, which 
            // can save the user a great deal of time.
            int rowNumber = 0;
            var indexToRowDict = new Dictionary<int, DataRow>();
            foreach (DataRow curDataRow in dataTable.Rows)
            {
                indexToRowDict.Add(rowNumber++, curDataRow);
            }

            // Skip the first row (remove it)
            var indexesToRemove = new List<int> { 0 };

            // Remove any blank rows
            foreach (var kvp in indexToRowDict)
            {
                if (LoaStageImport.RowIsBlank(kvp.Value))
                {
                    indexesToRemove.Add(kvp.Key);
                }
            }

            // Remove entries that we need to discard
            foreach (var i in indexesToRemove)
            {
                indexToRowDict.Remove(i);
            }

            // Turn all valid rows into PayRecV3s (Unexpended Balance version)
            List<LoaStageImport> loaStageImports = indexToRowDict.Select(kvp => new LoaStageImport(kvp, columnMappingDictionary, errorList)).ToList();
            return new LoaStageImports(loaStageImports);
        }

        public LoaStageImports(IEnumerable<LoaStageImport> collection) : base(collection)
        {
        }

        private static Dictionary<string,int> EnsureWorksheetHasCorrectShape(DataTable dataTable)
        {
            var columnNames = GetColumnLetterToColumnNameDictionary();

            var dataRow = dataTable.Rows[0];
            var expectedColumns = columnNames.Values.ToList();
            var actualColumns = dataTable.Columns.Cast<DataColumn>().Select(c => (string)dataRow[c]).ToList();
            var dictionary = new Dictionary<string, int>();
            for (int index = 0; index < actualColumns.Count; index++)
            {
                var actualColumn = actualColumns[index];
                dictionary.Add(actualColumn,index);
            }
            
            var missingColumns = expectedColumns.Except(actualColumns).ToList();
            Check.RequireThrowUserDisplayable(!missingColumns.Any(),
                                              string.Format("Expected columns [{0}]\n\nBut got columns [{1}].\n\nThese columns were missing: [{2}]", string.Join(", ", expectedColumns),
                                                            string.Join(", ", actualColumns), string.Join(", ", missingColumns)));
            return dictionary;
        }

        /*
         *
         * 07-16-2020 changes
Dropped: Original "Funded Program"
Renamed: "WBS Description" => "Funded Program"
Renamed: "Vendor Name" => "Name"
         *
         */


        public const string ProjectIDKey = "Project ID";
        public const string LetterDateKey = "Letter Date";
        public const string ProjectExpirationDateKey = "Project Expiration Date";
        public const string ApplicationDateKey = "Application Date";
        public const string DecisionDateKey = "Decision Date";
        public const string FundSourceNumberKey = "FundSource #";
        public const string FundSourceKey = "FundSource";
        public const string CodeKey = "Code";
        public const string IndexKey = "Index";
        public const string MatchKey = "Match";
        public const string PayKey = "Pay";
        public const string ForesterKey = "Forester";
        public const string ForesterPhoneKey = "Forester Phone";
        public const string ForesterEmailKey = "Forester email";
        public const string StatusKey = "Status";
       

        public static Dictionary<string, string> GetColumnLetterToColumnNameDictionary()
        {
            return new Dictionary<string, string>
            {
                {"A", ProjectIDKey},
                {"K", StatusKey},
                {"M", LetterDateKey},
                {"N", ProjectExpirationDateKey},
                {"AZ", FundSourceNumberKey},
                {"BE", FundSourceKey},
                {"BF", CodeKey},
                {"BG", IndexKey},
                {"BJ", MatchKey},
                {"BK", PayKey},
                {"G", ForesterKey},
                {"H", ForesterPhoneKey},
                {"I", ForesterEmailKey},
                {"E", DecisionDateKey},
                {"D", ApplicationDateKey}
            };
        }

    }
}
