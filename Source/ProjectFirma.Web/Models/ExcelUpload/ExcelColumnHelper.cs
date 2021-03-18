using System;
using System.Collections.Generic;
using System.Data;
using LtInfo.Common;

namespace ProjectFirma.Web.Models.ExcelUpload
{
    public static class ExcelColumnHelper
    {
        public static string GetStringDataValueForColumnName(DataRow dr, int rowIndex, Dictionary<string, string> columnNameToLetterDict, string humanReadableNameOfColumn)
        {
            string columnKeyLetterName = columnNameToLetterDict[humanReadableNameOfColumn];

            string dataValue;
            try
            {
                dataValue = string.IsNullOrWhiteSpace(dr[columnKeyLetterName].ToString())
                    ? null
                    : dr[columnKeyLetterName].ToString();
            }
            catch (Exception e)
            {
                throw new ExcelImportBadCellException(columnKeyLetterName, rowIndex,
                    dr[columnKeyLetterName].ToString(),
                    $"Problem parsing Source {humanReadableNameOfColumn}", e);
            }

            return dataValue;
        }

        public static double? GetDoubleDataValueForColumnName(DataRow dr, int rowIndex, Dictionary<string, string> columnNameToLetterDict, string humanReadableNameOfColumn)
        {
            string columnKeyLetterName = columnNameToLetterDict[humanReadableNameOfColumn];
            double? returnValue = null;
            try
            {
                if (double.TryParse(dr[columnKeyLetterName].ToString(), out double dataValueAsDouble))
                {
                    returnValue = dataValueAsDouble;
                }

            }
            catch (Exception e)
            {
                throw new ExcelImportBadCellException(columnKeyLetterName, rowIndex, dr[columnKeyLetterName].ToString(), $"Problem parsing {humanReadableNameOfColumn}", e);
            }

            return returnValue;
        }

        /// <summary>
        /// The underlying type in the Excel file of a given date/time.
        /// </summary>
        public enum ExcelDateTimeCellType
        {
            // This is an floating point number with 1 equal to 1 day. So, 39938 is 05/05/2009.
            SerialDateTimeValue,
            // A string with a date (and possibly time) value. For example "06/01/2020".
            StringWithDateTime,
        }

        public static DateTime? GetDateTimeDataValueForColumnName(DataRow dr,
                                                                  int rowIndex,
                                                                  Dictionary<string, string> columnNameToLetterDict,
                                                                  string humanReadableNameOfColumn,
                                                                  ExcelDateTimeCellType excelDateTimeCellType)
        {
            string columnKeyLetterName = columnNameToLetterDict[humanReadableNameOfColumn];
            DateTime? returnValue = null;
            string cellValue = "(cell value not set yet)";
            try
            {
                cellValue = dr[columnKeyLetterName].ToString();
                // Turn "#" into a null
                if (cellValue == "#")
                {
                    returnValue = null;
                }
                // string: "05/05/2009"
                else switch (excelDateTimeCellType)
                    {
                        // double: 39938
                        case ExcelDateTimeCellType.StringWithDateTime:
                            {
                                if (DateTime.TryParse(cellValue, out DateTime dataValueAsDateTime))
                                {
                                    returnValue = dataValueAsDateTime;
                                }

                                break;
                            }
                        case ExcelDateTimeCellType.SerialDateTimeValue:
                            {
                                double cellValueAsDouble = double.Parse(cellValue);
                                returnValue = DateTime.FromOADate(cellValueAsDouble);
                                break;
                            }
                        default:
                            throw new SitkaDisplayErrorException($"Unknown ExcelDateTimeCellType cell type: {excelDateTimeCellType}");
                    }
            }
            catch (Exception e)
            {
                throw new ExcelImportBadCellException(columnKeyLetterName, rowIndex, cellValue, $"Problem parsing {humanReadableNameOfColumn}", e);
            }

            return returnValue;
        }
    }
}