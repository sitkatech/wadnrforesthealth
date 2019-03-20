/*-----------------------------------------------------------------------
<copyright file="JsonExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class JsonExtensions
{
    public static string ToString(this JToken token, Formatting formatting = Formatting.Indented, JsonSerializerSettings settings = null)
    {
        using (var sw = new StringWriter(CultureInfo.InvariantCulture))
        {
            using (var jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = formatting;
                jsonWriter.Culture = CultureInfo.InvariantCulture;
                if (settings != null)
                {
                    jsonWriter.DateFormatHandling = settings.DateFormatHandling;
                    jsonWriter.DateFormatString = settings.DateFormatString;
                    jsonWriter.DateTimeZoneHandling = settings.DateTimeZoneHandling;
                    jsonWriter.FloatFormatHandling = settings.FloatFormatHandling;
                    jsonWriter.StringEscapeHandling = settings.StringEscapeHandling;
                }
                token.WriteTo(jsonWriter);
            }
            return sw.ToString();
        }
    }
}