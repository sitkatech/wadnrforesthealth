/*-----------------------------------------------------------------------
<copyright file="FirmaValidationMessages.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Common
{
    public static class FirmaValidationMessages
    {
       public static readonly string CompletionDateGreaterThanEqualToImplementationStartYear = $"{FieldDefinition.CompletionDate.GetFieldDefinitionLabel()} must be greater than or equal to the {FieldDefinition.ProjectInitiationDate.GetFieldDefinitionLabel()}.";
        public static readonly string CompletionDateGreaterThanEqualToPlannedDate = $"{FieldDefinition.CompletionDate.GetFieldDefinitionLabel()} must be greater than or equal to the {FieldDefinition.ProjectInitiationDate.GetFieldDefinitionLabel()}.";
        public static readonly string UpdateSectionIsDependentUponBasicsSection = "Your project's \"Basics\" page must be complete before you can begin updating this section.";
        public static readonly string ProjectNameUnique = $"{FieldDefinition.ProjectName.GetFieldDefinitionLabel()} already exists.";
        public static readonly string ProjectCodeInvalid = $"{FieldDefinition.ProjectCode.GetFieldDefinitionLabel()} is not valid.";
        public static readonly string ProgramIndexInvalid = $"{FieldDefinition.ProgramIndex.GetFieldDefinitionLabel()} is not valid.";
        public static readonly string OrganizationNameUnique = $"{FieldDefinition.Organization.GetFieldDefinitionLabel()} name already exists.";
        public static readonly string ProgramNameUnique = $"{FieldDefinition.Program.GetFieldDefinitionLabel()} name already exists for this organization.";
        public static readonly string GrantNoteIsEmptyText = $"{FieldDefinition.GrantNote.GetFieldDefinitionLabel()} is empty text. Please enter a note before saving.";
        public static readonly string GrantAllocationNoteIsEmptyText = $"{FieldDefinition.GrantAllocationNote.GetFieldDefinitionLabel()} is empty text. Please enter a note before saving.";

        public static readonly string OrganizationShortNameUnique = $"{FieldDefinition.Organization.GetFieldDefinitionLabel()} short name already exists.";
        public static readonly string ProgramShortNameUnique = $"{FieldDefinition.Program.GetFieldDefinitionLabel()} short name already exists for this organization.";
        public static readonly string ClassificationNameUnique = $"{FieldDefinition.Classification.GetFieldDefinitionLabel()} name already exists.";
        public static readonly string ExplanationNecessaryForProjectExemptYears = $"Please provide an explanation of why the {FieldDefinition.ReportingYear.GetFieldDefinitionLabelPluralized()} are exempt.";
        public static readonly string ExplanationNotNecessaryForProjectExemptYears = $"Explanation is not necessary since no {FieldDefinition.ReportingYear.GetFieldDefinitionLabelPluralized()} are exempt.";
        public static readonly string TagNameUnique = $"{FieldDefinition.TagName.GetFieldDefinitionLabel()} already exists.";
        public static readonly string CompletionDateMustBePastOrPresentForCompletedProjects = $"{FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} in the Completed and Post-Implementation stages cannot have a {FieldDefinition.CompletionDate.GetFieldDefinitionLabel()} in the future.";
        public const string LettersNumbersSpacesDashesAndUnderscoresOnly = "Only letters, numbers, spaces, dashes and underscores are allowed.";
        public const string LettersOnly = "Only letters are allowed.";
        public const string MoreThanOneProjectUpdateInProgress = "Cannot determine latest update state; more than one update is in progress.";
        public static readonly string ImplementationYearMustBePastOrPresentForImplementationProjects = $"{FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} in the Implementation stage cannot have an {FieldDefinition.ProjectInitiationDate.GetFieldDefinitionLabel()} in the future.";
        public static readonly string InvoiceNicknameMustBePopulated = "You must provide an Invoice nickname.";
        public static readonly string PurchaseAuthorityAgreementNumberMustBeBlankIfIsLandOwnerAgreement = "Purchase Authority Agreement Number Must Be Blank If It Is Land-Owner Agreement";
        public static readonly string InvoiceMatchAmountDollarValueMustNotBeNull = $"You must enter a dollar value for the {FieldDefinition.MatchAmount.GetFieldDefinitionLabel()} if dollar amount is selected";
        public static readonly string InvoiceApprovalStatusCommentIsRequiredIfStatusIsDenied =
            $"You must provide a comment describing why the {FieldDefinition.Invoice.GetFieldDefinitionLabel()} was denied.";

        public static readonly string InteractionEventMustHaveTitle =
            $"You must provide a title for the given {FieldDefinition.InteractionEvent.GetFieldDefinitionLabel()}.";
    }
}

