﻿/*-----------------------------------------------------------------------
<copyright file="ProgramInfoController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.ProgramInfo;
using ProjectFirma.Web.Views.Shared.SortOrder;

namespace ProjectFirma.Web.Controllers
{
    public class ProgramInfoController : FirmaBaseController
    {
        [AnonymousUnclassifiedFeature]
        public ViewResult Taxonomy()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.Taxonomy);
            var topLevelTaxonomyTierAsFancyTreeNodes = MultiTenantHelpers.GetTaxonomyLevel()
                .GetTaxonomyTiers().SortByOrderThenName().Select(x => x.ToFancyTreeNode(CurrentPerson))
                .ToList();
            var viewData = new TaxonomyViewData(CurrentPerson, firmaPage, topLevelTaxonomyTierAsFancyTreeNodes);
            return RazorView<Taxonomy, TaxonomyViewData>(viewData);
        }

        [AnonymousUnclassifiedFeature]
        public ViewResult ClassificationSystem(ClassificationSystemPrimaryKey classificationSystemPrimaryKey)
        {
            var classificationSystem = classificationSystemPrimaryKey.EntityObject;
            var viewData = new ClassificationSystemViewData(CurrentPerson, classificationSystem);
            return RazorView<Views.ProgramInfo.ClassificationSystem, ClassificationSystemViewData>(viewData);
        }
    }
}
