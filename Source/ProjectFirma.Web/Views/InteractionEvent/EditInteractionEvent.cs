/*-----------------------------------------------------------------------
<copyright file="EditProject.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public abstract class EditInteractionEvent : TypedWebPartialViewPage<EditInteractionEventViewData, EditInteractionEventViewModel>
    {
    }

    public abstract class EditInteractionEventType
    {
        public readonly string IntroductoryText;

        internal EditInteractionEventType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditInteractionEventTypeNewInteractionEvent NewInteractionEvent = EditInteractionEventTypeNewInteractionEvent.Instance;
        public static readonly EditInteractionEventTypeExistingInteractionEvent ExistingInteractionEvent = EditInteractionEventTypeExistingInteractionEvent.Instance;
    }

    public class EditInteractionEventTypeNewInteractionEvent : EditInteractionEventType
    {
        private EditInteractionEventTypeNewInteractionEvent(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditInteractionEventTypeNewInteractionEvent Instance = new EditInteractionEventTypeNewInteractionEvent(
            $"<p>Enter basic information about the {Models.FieldDefinition.InteractionEvent.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditInteractionEventTypeExistingInteractionEvent : EditInteractionEventType
    {
        private EditInteractionEventTypeExistingInteractionEvent(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditInteractionEventTypeExistingInteractionEvent Instance =
            new EditInteractionEventTypeExistingInteractionEvent(
                $"<p>Update this {Models.FieldDefinition.InteractionEvent.GetFieldDefinitionLabel()}'s information.</p>");
    }

    
}
