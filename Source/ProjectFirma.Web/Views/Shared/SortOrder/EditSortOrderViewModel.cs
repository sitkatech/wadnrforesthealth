﻿/*-----------------------------------------------------------------------
<copyright file="EditSortOrderViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Models;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.SortOrder
{
    public class EditSortOrderViewModel : FormViewModel
    {
        const int _10 = 10;

        public List<int> ReorderedSortableIDs { get; set; }

        public EditSortOrderViewModel()
        {
        }

        public void UpdateModel(ICollection<IHaveASortOrder> classificationSystemClassifications)
        {
            for (var i = 0; i < ReorderedSortableIDs.Count; i++)
            {
                var theGuy = classificationSystemClassifications
                    .SingleOrDefault(x => x.ID == ReorderedSortableIDs[i]);

                if (theGuy != null)
                {
                    theGuy.SortOrder = i*_10; // magic number
                }
            }
        }
    }
}
