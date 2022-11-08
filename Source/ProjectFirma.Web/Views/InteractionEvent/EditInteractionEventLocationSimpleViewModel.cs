/*-----------------------------------------------------------------------
<copyright file="EditInteractionEventLocationSimpleViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class EditInteractionEventLocationSimpleViewModel : FormViewModel, IValidatableObject
    {
        public double? InteractionEventLocationPointX { get; set; }
        public double? InteractionEventLocationPointY { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditInteractionEventLocationSimpleViewModel()
        {
        }

        public EditInteractionEventLocationSimpleViewModel(DbGeometry interactionEventLocationPoint)
        {
            if (interactionEventLocationPoint != null)
            {
                InteractionEventLocationPointX = interactionEventLocationPoint.XCoordinate;
                InteractionEventLocationPointY = interactionEventLocationPoint.YCoordinate;
            }
            else
            {
                InteractionEventLocationPointX = null;
                InteractionEventLocationPointY = null;
            }
        }

        public void UpdateModel(Models.InteractionEvent interactionEvent)
        {
            if (InteractionEventLocationPointX != null && InteractionEventLocationPointY != null)
            {
                interactionEvent.InteractionEventLocationSimple = DbSpatialHelper.MakeDbGeometryFromCoordinates(InteractionEventLocationPointX.Value, InteractionEventLocationPointY.Value, MapInitJson.CoordinateSystemId);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

    }
}
