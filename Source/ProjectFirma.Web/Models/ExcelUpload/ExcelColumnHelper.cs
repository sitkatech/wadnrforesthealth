using System;
using System.Collections.Generic;
using System.Data;
using LtInfo.Common;

namespace ProjectFirma.Web.Models.ExcelUpload
{
    public static class ExcelColumnHelper
    {
        public static string GetStringDataValueForColumnName(DataRow dr, int rowIndex, string humanReadableNameOfColumn, Dictionary<string, int> columnNameToIndexDict)
        {
            int columnIndex = columnNameToIndexDict[humanReadableNameOfColumn];

            string dataValue;
            try
            {
                dataValue = string.IsNullOrWhiteSpace(dr[columnIndex].ToString())
                    ? null
                    : dr[columnIndex].ToString();
            }
            catch (Exception e)
            {
                throw new ExcelImportBadCellException(columnIndex.ToString(), rowIndex,
                    dr[columnIndex].ToString(),
                    $"Problem parsing Source {humanReadableNameOfColumn}", e);
            }

            return dataValue;
        }

        public static double? GetDoubleDataValueForColumnName(DataRow dr, int rowIndex, string humanReadableNameOfColumn, Dictionary<string, int> columnNameToIndexDict)
        {
            int columnIndex = columnNameToIndexDict[humanReadableNameOfColumn];
            double? returnValue = null;
            try
            {
                if (double.TryParse(dr[columnIndex].ToString(), out double dataValueAsDouble))
                {
                    returnValue = dataValueAsDouble;
                }

            }
            catch (Exception e)
            {
                throw new ExcelImportBadCellException(columnIndex.ToString(), rowIndex, dr[columnIndex].ToString(), $"Problem parsing {humanReadableNameOfColumn}", e);
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
            Dictionary<string, int> columnNameToIndexDict,
            string humanReadableNameOfColumn,
            bool allowNullable)
        {
            int columnIndex = columnNameToIndexDict[humanReadableNameOfColumn];
            DateTime? returnValue = null;
            string cellValue = "(cell value not set yet)";
            try
            {
                cellValue = dr[columnIndex].ToString();
                // Turn "#" into a null
                if (cellValue == "#")
                {
                    returnValue = null;
                }

                else if (string.IsNullOrEmpty(cellValue) && allowNullable)
                {
                    returnValue = null;
                }
                else
                {
                    try
                    {
                        double cellValueAsDouble = double.Parse(cellValue);
                        returnValue = DateTime.FromOADate(cellValueAsDouble);
                    }
                    catch (Exception e)
                    {
                        var tryParse = DateTime.TryParse(cellValue, out DateTime dataValueAsDateTime);
                        if (tryParse)
                        {
                            returnValue = dataValueAsDateTime;
                        }
                        else
                        {
                            var updatedCellValue = cellValue.Replace(",", ", ");
                            var updatedTryParse = DateTime.TryParse(updatedCellValue, out DateTime updatedDataValueAsDateTime);
                            if (updatedTryParse)
                            {
                                returnValue = updatedDataValueAsDateTime;
                            }
                            else
                            {
                                throw new ExcelImportBadCellException(columnIndex.ToString(), rowIndex, cellValue,
                                    $"Problem parsing {humanReadableNameOfColumn}", e);
                            }
                        }

                    }
                }
               
            }
            catch (Exception ex)
            {
                throw new ExcelImportBadCellException(columnIndex.ToString(), rowIndex, cellValue,
                    $"Problem parsing {humanReadableNameOfColumn}", ex);
            }

            return returnValue;
        }
    }
}