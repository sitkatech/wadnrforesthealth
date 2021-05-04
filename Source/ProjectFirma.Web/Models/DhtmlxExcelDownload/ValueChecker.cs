using System;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public static class ValueChecker
    {
        /// <summary>
        /// Checks if object type is numeric.
        /// </summary>
        /// <param name="valueType">Type of object to check</param>
        /// <returns></returns>
        public static bool IsNumeric(Type valueType)
        {
            TypeCode typeCode = Type.GetTypeCode(valueType);

            if (typeCode == TypeCode.Int16 || typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64 ||
                typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64 ||
                typeCode == TypeCode.Double || typeCode == TypeCode.Decimal)
                return true;
            else
                return false;
        }
    }
}