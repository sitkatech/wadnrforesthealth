/*-----------------------------------------------------------------------
<copyright file="EditProject.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.Shared.ProjectControls
{
    public abstract class EditProjectAttributes : TypedWebPartialViewPage<EditProjectAttributesViewData, EditProjectAttributesViewModel>
    {
    }

    public abstract class EditProjectAttributesType
    {
        public readonly string IntroductoryText;

        internal EditProjectAttributesType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditProjectAttributesTypeNewProject NewProject = EditProjectAttributesTypeNewProject.Instance;
        public static readonly EditProjectAttributesTypeExistingProject ExistingProject = EditProjectAttributesTypeExistingProject.Instance;
        public static readonly EditProjectAttributesTypeProposal Proposal = EditProjectAttributesTypeProposal.Instance;
    }

    public class EditProjectAttributesTypeNewProject : EditProjectAttributesType
    {
        private EditProjectAttributesTypeNewProject(string introductoryText) : base(introductoryText) {  }
            public static readonly EditProjectAttributesTypeNewProject Instance = new EditProjectAttributesTypeNewProject($"<p>Enter attribute information about the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}.</p>");
        }

    public class EditProjectAttributesTypeExistingProject : EditProjectAttributesType
    {
            private EditProjectAttributesTypeExistingProject(string introductoryText) : base(introductoryText) { }
            public static readonly EditProjectAttributesTypeExistingProject Instance = new EditProjectAttributesTypeExistingProject($"<p>Update this {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}'s attributes.</p>");
        }

    public class EditProjectAttributesTypeProposal : EditProjectAttributesType
    {
        private EditProjectAttributesTypeProposal(string introductoryText) : base(introductoryText) { }
        public static readonly EditProjectAttributesTypeProposal Instance = new EditProjectAttributesTypeProposal($"<p>Enter additional attribute information to approve this as a full-fledged {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}.</p>");
    }
}
