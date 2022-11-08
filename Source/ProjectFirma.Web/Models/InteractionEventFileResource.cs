/*-----------------------------------------------------------------------
<copyright file="InteractionEventFileResource.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class InteractionEventFileResource : IAuditableEntity, IEntityDocument
    {
        public string AuditDescriptionString => $"{FieldDefinition.InteractionEvent.GetFieldDefinitionLabel()}  \"{InteractionEvent?.InteractionEventTitle ?? "<Not Found>"}\" document \"{FileResource?.OriginalCompleteFileName ?? "<Not Found>"}\"";
        public string DeleteUrl => SitkaRoute<InteractionEventController>.BuildUrlFromExpression(x => x.DeleteInteractionEventFile(InteractionEventFileResourceID));
        public string EditUrl => SitkaRoute<InteractionEventController>.BuildUrlFromExpression(x => x.EditInteractionEventFile(InteractionEventFileResourceID));
        public string DisplayCssClass { get; set; }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            FileResource.DeleteFull(dbContext);
            DeleteFull(dbContext);
        }
    }
}
