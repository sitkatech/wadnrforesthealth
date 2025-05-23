﻿/*-----------------------------------------------------------------------
<copyright file="PhotoViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public class PhotoViewData : ProjectCreateViewData
    {
        public readonly ImageGalleryViewData ImageGalleryViewData;

        public PhotoViewData(Person currentPerson, string galleryName, IEnumerable<IFileResourcePhoto> galleryImages, string addNewPhotoUrl, Func<IFileResourcePhoto, object> sortFunction, Models.Project project, ProposalSectionsStatus proposalSectionsStatus)
            : base(currentPerson, project, ProjectCreateSection.Photos.ProjectCreateSectionDisplayName, proposalSectionsStatus)
        {
            var selectKeyImageUrl =
                SitkaRoute<ProjectImageController>.BuildUrlFromExpression(x =>
                    x.SetKeyPhoto(UrlTemplate.Parameter1Int));
            ImageGalleryViewData = new ImageGalleryViewData(currentPerson, galleryName, galleryImages, true, addNewPhotoUrl, selectKeyImageUrl, true, sortFunction, "Photo");                        
        }        
    }
}
