/*-----------------------------------------------------------------------
<copyright file="EditViewModelValidator.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System;
using System.Collections.Generic;
using System.Data.Entity;
using ProjectFirma.Web.Common;
using FluentValidation;

namespace ProjectFirma.Web.Views.Program
{
    public class EditViewModelValidator : AbstractValidator<EditViewModel>
    {
        // Validators are singletons, so this list must be initialized every time.
        public Func<IList<Models.Program>> Programs = () =>
        {
            HttpRequestStorage.DatabaseEntities.Programs.Load();
            return HttpRequestStorage.DatabaseEntities.Programs.Local;
        };

        public EditViewModelValidator(IList<Models.Program> programs) : this()
        {
            Programs = (() => programs);
        }

        public EditViewModelValidator()
        {
            RuleFor(x => x.ProgramName)
                .NotEmpty()
                .WithMessage($"{Models.FieldDefinition.Program.GetFieldDefinitionLabel()} name is required")
                .Length(1, Models.Program.FieldLengths.ProgramName)
                .Must((viewModel, programName) => Models.Program.IsProgramNameUnique(Programs(), programName, viewModel.ProgramID, viewModel.OrganizationID.GetValueOrDefault()))
                .WithMessage(FirmaValidationMessages.ProgramNameUnique);
            RuleFor(x => x.ProgramShortName)
                .Must((viewModel, programShortName) => Models.Program.IsProgramShortNameUniqueIfProvided(Programs(), programShortName, viewModel.ProgramID,viewModel.OrganizationID.GetValueOrDefault()))
                .WithMessage(FirmaValidationMessages.ProgramShortNameUnique);
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("Is Active is required");
        }
    }
}
