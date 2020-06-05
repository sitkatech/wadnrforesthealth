using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LtInfo.Common;
using Microsoft.Ajax.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectDocument
{
    // exists because validating a document requires making sure its name is unique, and to do that requires knowing if it's a Document or a Document Update
    public class NewProjectDocumentUpdateViewModel: NewProjectDocumentViewModel
    {
        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public NewProjectDocumentUpdateViewModel() { }

        public NewProjectDocumentUpdateViewModel(Models.ProjectUpdateBatch projectUpdateBatch)
        {
            ParentID = projectUpdateBatch.ProjectUpdateBatchID;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (Files[0] == null)
            {
                validationResults.Add(new SitkaValidationResult<NewProjectDocumentUpdateViewModel, List<HttpPostedFileBase>>($"You must select at least one file to upload.", m => m.Files));
            }

            if (DisplayNames.Distinct().Count() < DisplayNames.Count())
            {
                validationResults.Add(new SitkaValidationResult<NewProjectDocumentUpdateViewModel, List<string>>($"Cannot submit multiple files with the same Display Name for a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Document", m => m.DisplayNames));
            }

            for (int key = 0; key < Files.Count; key++)
            {
                if (DisplayNames[key].IsNullOrWhiteSpace())
                {
                    validationResults.Add(new SitkaValidationResult<NewProjectDocumentUpdateViewModel, List<string>>($"Display Name is a required field.", m => m.DisplayNames));
                }

                if (Descriptions[key].Length > Models.ProjectDocument.FieldLengths.Description)
                {
                    validationResults.Add(new SitkaValidationResult<NewProjectDocumentUpdateViewModel, List<string>>($"Display Name \"{Descriptions[key]}\" is longer than the allowed {Models.ProjectDocument.FieldLengths.Description} character maximum.", m => m.Descriptions));
                }

                if (DisplayNames[key].Length > Models.ProjectDocument.FieldLengths.DisplayName)
                {
                    validationResults.Add(new SitkaValidationResult<NewProjectDocumentUpdateViewModel, List<string>>($"Display Name \"{DisplayNames[key]}\" is longer than the allowed {Models.ProjectDocument.FieldLengths.DisplayName} character maximum.", m => m.DisplayNames));
                }

                FileResource.ValidateFileSize(Files[key], validationResults, "Files");

                var displayNameToLower = DisplayNames[key].ToLower();

                if (HttpRequestStorage.DatabaseEntities.ProjectDocuments.Where(x => x.ProjectID == ParentID)
                    .Any(x => x.DisplayName.ToLower() == displayNameToLower))
                {
                    validationResults.Add(new SitkaValidationResult<NewProjectDocumentUpdateViewModel, List<string>>($"There is already a document with the Display Name \"{DisplayNames[key]}\" attached to this {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} update. Display Name must be unique for each Document attached to a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} update.", m => m.DisplayNames));
                }
            }

            // because we cannot return any files back with the model, we have to clear the model
            if (validationResults.Any())
            {
                DisplayNames = null;
                Descriptions = null;
                Files = null;
            }

            return validationResults;
        }
    }
}
