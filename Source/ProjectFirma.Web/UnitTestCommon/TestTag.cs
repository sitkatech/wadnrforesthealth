﻿/*-----------------------------------------------------------------------
<copyright file="TestTag.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public static class TestTag
        {
            public static Tag Create()
            {
                var tag = Tag.CreateNewBlank();
                tag.TagName = MakeTestTagName();
                return tag;
            }

            public static Tag Create(DatabaseEntities dbContext)
            {
                var tag = Create();
                dbContext.Tags.Add(tag);
                return tag;
            }

            private static string MakeTestTagName()
            {
                return TestFramework.MakeTestName("Test Tag Name", Tag.FieldLengths.TagName);
            }
        }
    }
}
