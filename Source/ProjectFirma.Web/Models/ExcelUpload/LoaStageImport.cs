/*-----------------------------------------------------------------------
<copyright file="BudgetStageImport.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjectFirma.Web.Models.ExcelUpload
{
    public class LoaStageImport
    {
        public readonly string BusinessArea;
        public readonly string FABudgetActivity;
        public readonly string FunctionalArea;
        public readonly string ObligationNumber;
        public readonly string ObligationItem;
        public readonly string Fund;
        public readonly string WbsElement;
        public readonly string WbsElementDescription;
        public readonly string BudgetObjectClass;
        public readonly string Vendor;
        public readonly string VendorName;
        public readonly DateTime? PostingDatePerSpl;
        public readonly double? UnexpendedBalance;

        public LoaStageImport(KeyValuePair<int, DataRow> keyValuePair)
        {
            var rowIndex = keyValuePair.Key;
            var dr = keyValuePair.Value;
            var columnNameToLetterDict = LoaStageImports.GetBudgetColumnNameToColumnLetterDictionary();

            // Column - Business Area Key
            BusinessArea = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.BusinessAreaKey);

            // Column - FA Budget Activity Key
            FABudgetActivity = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.FaBudgetActivityKey);

            // Column - Functional Area Text
            FunctionalArea = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.FunctionalAreaText);

            // Column - Obligation Number Key
            ObligationNumber = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.ObligationNumberKey);

            // Column - Obligation Item Key
            ObligationItem = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.ObligationItemKey);

            // Column - Fund Key
            Fund = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.FundKey);

            // Column - WBS Element Key
            WbsElement = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.WbsElementKey);

            // Column - Funded Program (Really an alternate name for the Name of the WBS element. It's named this because of reporting engine limitations on Dorothy's side.)
            WbsElementDescription = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.FundedProgramKey);

            // Column - Budget Object Class Key
            BudgetObjectClass = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.BudgetObjectClassKey);

            // Column - Vendor Key
            Vendor = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.VendorKey);

            // Column - Vendor Key
            VendorName = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.VendorNameText);

            // Column - Posting Date (Per SPL) - Key
            PostingDatePerSpl = ExcelColumnHelper.GetDateTimeDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.PostingDatePerSplKey, ExcelColumnHelper.ExcelDateTimeCellType.SerialDateTimeValue);

            // Column - Unexpended Balance
            UnexpendedBalance = ExcelColumnHelper.GetDoubleDataValueForColumnName(dr, rowIndex, columnNameToLetterDict, LoaStageImports.UnexpendedBalanceValue);
        }


        /// <summary>
        /// Are all relevant columns in this row blank?
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static bool RowIsBlank(DataRow dr)
        {
            var columnsToCheck = LoaStageImports.GetBudgetColumnLetterToColumnNameDictionary().Keys.ToList();
            var allColumnsBlank = columnsToCheck.All(col => String.IsNullOrWhiteSpace(dr[col].ToString()));
            return allColumnsBlank;
        }

    }
}
