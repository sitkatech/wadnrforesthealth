﻿/*-----------------------------------------------------------------------
<copyright file="NotesViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Shared.TextControls;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.Shared.ProjectDocument;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class DocumentsAndNotesViewData : ProjectUpdateViewData
    {
        public EntityNotesViewData EntityNotesViewData { get; }
        public string RefreshUrl { get; }
        public string DiffUrl { get; }
        public ProjectDocumentsDetailViewData ProjectDocumentsViewData { get; }

        public DocumentsAndNotesViewData(Person currentPerson, ProjectUpdateBatch projectUpdateBatch, UpdateStatus updateStatus, string diffUrl) : base(currentPerson, projectUpdateBatch, updateStatus, new List<string>(), ProjectUpdateSection.NotesAndDocuments.ProjectUpdateSectionDisplayName)
        {
            EntityNotesViewData = new EntityNotesViewData(EntityNote.CreateFromEntityNote(new List<IEntityNote>(projectUpdateBatch.ProjectNoteUpdates)),
                SitkaRoute<ProjectNoteUpdateController>.BuildUrlFromExpression(x => x.New(projectUpdateBatch)),
                projectUpdateBatch.Project.DisplayName,
                IsEditable);
            ProjectDocumentsViewData = new ProjectDocumentsDetailViewData(
                ProjectDocumentResource.CreateFromProjectDocuments(projectUpdateBatch.ProjectDocumentUpdates),
                SitkaRoute<ProjectDocumentUpdateController>.BuildUrlFromExpression(x => x.New(projectUpdateBatch)),
                projectUpdateBatch.Project.DisplayName,
                IsEditable);
            RefreshUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.RefreshNotesAndDocuments(projectUpdateBatch.Project));
            DiffUrl = diffUrl;
        }

    }
}
