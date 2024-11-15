﻿/*-----------------------------------------------------------------------
<copyright file="EntityNote.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Models
{
    public class EntityNote : IEntityNote
    {
        public DateTime Created { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public string LastUpdatedBy { get; private set; }
        public string DeleteUrl { get; private set; }
        public string EditUrl { get; private set; }
        public string Note { get; set; }
        public string DisplayCssClass { get; set; }

        public EntityNote(DateTime created, string createdBy, DateTime lastUpdated, string lastUpdatedBy, string deleteUrl, string editUrl, string note, string displayCssClass)
        {
            Created = created;
            CreatedBy = createdBy;
            LastUpdated = lastUpdated;
            LastUpdatedBy = lastUpdatedBy;
            DeleteUrl = deleteUrl;
            EditUrl = editUrl;
            Note = note;
            DisplayCssClass = displayCssClass;
        }

        public static List<EntityNote> CreateFromEntityNote(List<IEntityNote> entityNotes)
        {
            return entityNotes.Select(x => new EntityNote(x.Created, x.CreatedBy, x.LastUpdated, x.LastUpdatedBy, x.DeleteUrl, x.EditUrl, x.Note, null)).ToList();
        }
    }
}
