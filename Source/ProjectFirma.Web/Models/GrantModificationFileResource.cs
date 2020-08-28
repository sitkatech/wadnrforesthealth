/*-----------------------------------------------------------------------
<copyright file="GrantModificationFileResource.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
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
    public partial class GrantModificationFileResource : IAuditableEntity, IEntityDocument
    {
        public string AuditDescriptionString => $"{FieldDefinition.Grant.GetFieldDefinitionLabel()}  \"{GrantModification?.GrantModificationName ?? "<Not Found>"}\" document \"{FileResource?.OriginalCompleteFileName ?? "<Not Found>"}\"";

        public string DeleteUrl => SitkaRoute<GrantModificationController>.BuildUrlFromExpression(x =>
                    x.DeleteGrantModificationFile(GrantModificationFileResourceID));

        public string EditUrl => string.Empty;
        
        public string DisplayCssClass { get; set; }
    }
}
