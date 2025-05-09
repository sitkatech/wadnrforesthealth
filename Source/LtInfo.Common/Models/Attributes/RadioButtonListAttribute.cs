﻿/*-----------------------------------------------------------------------
<copyright file="RadioButtonListAttribute.cs" company="Environmental Science Associates">
Copyright (c) Environmental Science Associates. All rights reserved.
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

namespace LtInfo.Common.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class RadioButtonListAttribute : DropDownListAttribute
    {
        public RadioButtonListAttribute(string viewDataKey, string dataValueField) : base(viewDataKey, dataValueField) {}
        public RadioButtonListAttribute(string viewDataKey, string dataValueField, string dataTextField) : this(viewDataKey, dataValueField, dataTextField, null) {}
        public RadioButtonListAttribute(string viewDataKey, string dataValueField, string dataTextField, string htmlDivider) : base(viewDataKey, dataValueField, dataTextField, htmlDivider)
        {
            HtmlDivider = htmlDivider;
        }

        public string HtmlDivider { get; private set; }
    }
}
