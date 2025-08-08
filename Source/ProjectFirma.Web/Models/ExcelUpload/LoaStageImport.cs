/*-----------------------------------------------------------------------
<copyright file="BudgetStageImport.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjectFirma.Web.Models.ExcelUpload
{
    public class LoaStageImport
    {
        public readonly string ProjectID;
        public readonly string Status;
        public readonly DateTime? LetterDate;
        public readonly DateTime? ProjectExpirationDate;
        public readonly DateTime? ApplicationDate;
        public readonly DateTime? DecisionDate;
        public readonly string FundSourceNumber;
        public readonly string FocusArea;
        public readonly string ProjectCode;
        public readonly string ProgramIndex;
        public readonly string Forester;
        public readonly string ForesterPhone;
        public readonly string ForesterEmail;
        public readonly double? MatchAmount;
        public readonly double? PayAmount;

        public LoaStageImport(KeyValuePair<int, DataRow> keyValuePair, Dictionary<string,int> columnMappingDictionary, List<string> problemList)
        {
            var rowIndex = keyValuePair.Key;
            var dr = keyValuePair.Value;

            // Column - Project ID Key
            ProjectID = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, LoaStageImports.ProjectIDKey, columnMappingDictionary);

            if (!string.IsNullOrEmpty(ProjectID))
            {
                // Column - Status Key
                Status = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, LoaStageImports.StatusKey, columnMappingDictionary);

                try
                {
                    // Column - Letter Date Key
                    LetterDate = ExcelColumnHelper.GetDateTimeDataValueForColumnName(dr, rowIndex,
                        columnMappingDictionary, LoaStageImports.LetterDateKey, true);
                }
                catch (ExcelImportBadCellException e)
                {
                    problemList.Add(e.Message);
                }

                try
                {
                    // Column - Project Expiration Date Key
                    ProjectExpirationDate = ExcelColumnHelper.GetDateTimeDataValueForColumnName(dr, rowIndex, columnMappingDictionary, LoaStageImports.ProjectExpirationDateKey, true);
                }

                catch (ExcelImportBadCellException e)
                {
                    problemList.Add(e.Message);
                }

                try
                {
                    // Column - Application Date Key
                    ApplicationDate = ExcelColumnHelper.GetDateTimeDataValueForColumnName(dr, rowIndex, columnMappingDictionary, LoaStageImports.ApplicationDateKey, true);
                }

                catch (ExcelImportBadCellException e)
                {
                    problemList.Add(e.Message);
                }

                try
                {
                    // Column - Decision Date Key
                    DecisionDate = ExcelColumnHelper.GetDateTimeDataValueForColumnName(dr, rowIndex, columnMappingDictionary, LoaStageImports.DecisionDateKey, true);
                }

                catch (ExcelImportBadCellException e)
                {
                    problemList.Add(e.Message);
                }


                // Column - FundSource Number Key
                FundSourceNumber = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, LoaStageImports.FundSourceNumberKey, columnMappingDictionary);

                // Column - FundSource Key
                FocusArea = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, LoaStageImports.FundSourceKey, columnMappingDictionary);

                // Column - Code Key
                ProjectCode = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, LoaStageImports.CodeKey, columnMappingDictionary);

                // Column - Index Key
                ProgramIndex = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, LoaStageImports.IndexKey, columnMappingDictionary);

                // Column - Match Key
                MatchAmount = ExcelColumnHelper.GetDoubleDataValueForColumnName(dr, rowIndex, LoaStageImports.MatchKey, columnMappingDictionary);

                // Column - Pay Key
                PayAmount = ExcelColumnHelper.GetDoubleDataValueForColumnName(dr, rowIndex, LoaStageImports.PayKey, columnMappingDictionary);

                // Column - Forester Key
                Forester = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, LoaStageImports.ForesterKey, columnMappingDictionary);
                ForesterPhone = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, LoaStageImports.ForesterPhoneKey, columnMappingDictionary);
                ForesterEmail = ExcelColumnHelper.GetStringDataValueForColumnName(dr, rowIndex, LoaStageImports.ForesterEmailKey, columnMappingDictionary);
            }

          
        }


        /// <summary>
        /// Are all relevant columns in this row blank?
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static bool RowIsBlank(DataRow dr)
        {
            var columnsToCheck = LoaStageImports.GetColumnLetterToColumnNameDictionary().Keys.ToList();
            var allColumnsBlank = columnsToCheck.All(col => String.IsNullOrWhiteSpace(dr[col].ToString()));
            return allColumnsBlank;
        }

    }
}
