using System;

namespace LtInfo.Common
{
    public static class DecimalFormats
    {
        /// <summary>
        /// Formats a decimal value to display up to 2 decimal places and remove any trailing 0's
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimalFormatted(this decimal value)
        {
            return decimal.Parse(value.ToString("#.##"));
        }
    }
}