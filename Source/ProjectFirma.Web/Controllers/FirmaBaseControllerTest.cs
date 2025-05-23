﻿/*-----------------------------------------------------------------------
<copyright file="FirmaBaseControllerTest.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using LtInfo.Common.EntityModelBinding;
using NUnit.Framework;

namespace ProjectFirma.Web.Controllers
{
    [TestFixture]
    public class FirmaBaseControllerTest
    {
        [Test]
        [Description("Controller Actions should prefer using the primary key types as possible")]
        public void GivenControllerActionWhenHasIntegerIDParameterThenPreferPrimaryKeyObject()
        {
            var methods = FirmaBaseController.AllControllerActionMethods;
            var missing =
                methods.Where(
                    x =>
                        x.GetParameters()
                            .Any(
                                p =>
                                    new[] {typeof(byte), typeof(Int16), typeof(Int32), typeof(Int64)}.Contains(p.ParameterType) && p.Name.ToLower().Contains("id") && !p.Name.ToLower().Contains("guid")))
                    .ToList();
            var exceptions = new List<string>
            {
                "SampleControllerToExclude.SampleControllerAction",
                "FileResourceController.GetFileResourceResized",
                "FieldDefinitionController.Edit",
                "FieldDefinitionController.FieldDefinitionDetails",
                "FirmaPageController.FirmaPageDetails",
                "ResultsController.SpendingByOrganizationTypeByOrganization",
                "ResultsController.SpendingByOrganizationTypeByOrganizationExcelDownload",
                "RoleController.Detail",
                "RoleController.PersonWithRoleGridJsonData",
            };
            var missingHumanReadable = missing.Select(x => $"{x.ReflectedType.Name}.{x.Name}").Where(x => !exceptions.Contains(x)).ToList();
            Assert.That(missingHumanReadable,
                Is.Empty,
                $"Some controller actions methods may be using integral data types for ID fields, consider using one of the types derived from \"{typeof(LtInfoEntityPrimaryKey<>)}\" instead or add an exception to this test");
        }
    }
}
