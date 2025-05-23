﻿/*-----------------------------------------------------------------------
<copyright file="ReturnDialogFormViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Models;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class ReturnDialogFormViewModel : PartialViewModel
    {
        public string SectionComments { get; set; }
        public ProjectUpdateSectionEnum? ProjectUpdateSectionEnum { get; set; }

        public void UpdateModel(ProjectUpdateBatch projectUpdateBatch)
        {
            if (ProjectUpdateSectionEnum.HasValue)
            {
                switch (ProjectUpdateSectionEnum.Value)
                {
                    case Models.ProjectUpdateSectionEnum.Basics:
                        projectUpdateBatch.BasicsComment = SectionComments;
                        break;
                    // 5/15/2019 TK - WADNR currently does not have a use. But may need expenditures in phase 2
                    //case Models.ProjectUpdateSectionEnum.Expenditures:
                    //    projectUpdateBatch.ExpendituresComment = SectionComments;
                    //    break;
                    case Models.ProjectUpdateSectionEnum.LocationSimple:
                        projectUpdateBatch.LocationSimpleComment = SectionComments;
                        break;
                    case Models.ProjectUpdateSectionEnum.LocationDetailed:
                        projectUpdateBatch.LocationDetailedComment = SectionComments;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
