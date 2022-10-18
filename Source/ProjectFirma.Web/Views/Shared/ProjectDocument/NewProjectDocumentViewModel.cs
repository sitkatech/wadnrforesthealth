using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Models.Attributes;
using LtInfo.Common.Mvc;
using Microsoft.Ajax.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectDocument
{
    public class NewProjectDocumentViewModel: IValidatableObject
    {
        [RequiredMultiFile]
        [DisplayName("File(s)")]
        [SitkaFileExtensions("pdf|zip|doc|docx|xls|xlsx")]
        public List<HttpPostedFileBase> Files { get; set; }

        [Required]
        [DisplayName("Display Name")]
        public List<string> DisplayNames { get; set; }

        [DisplayName("Description")]
        public List<string> Descriptions { get; set; }

        // can be the ID of a Project or a ProjectUpdateBatch depending on whether this ViewModel or its child type is invoked.
        public int? ParentID { get; set; }

        [Required]
        [DisplayName("Document Type")]
        public int? ProjectDocumentTypeID { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public NewProjectDocumentViewModel() { }
        public NewProjectDocumentViewModel(Models.Project project)
        {
            ParentID = project.ProjectID;
        }

        public void UpdateModel(Models.Project project, Person currentPerson)
        {
            for (int key = 0; key < Files.Count; key++)
            {
                var fileResource = FileResource.CreateNewFromHttpPostedFile(Files[key], currentPerson);
                HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
                var projectDocument = new Models.ProjectDocument(project.ProjectID, fileResource.FileResourceID, DisplayNames[key])
                {
                    Description = !Descriptions[key].IsNullOrWhiteSpace() ? Descriptions[key] : null,
                    ProjectDocumentTypeID = ProjectDocumentTypeID
                };
                project.ProjectDocuments.Add(projectDocument);
            }
        }

        public void UpdateModel(ProjectUpdateBatch projectUpdateBatch, Person currentPerson)
        {
            for (int key = 0; key < Files.Count; key++)
            {
                var fileResource = FileResource.CreateNewFromHttpPostedFile(Files[key], currentPerson);
                HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
                var projectDocument = new ProjectDocumentUpdate(projectUpdateBatch.ProjectID, fileResource.FileResourceID, DisplayNames[key])
                {
                    Description = !Descriptions[key].IsNullOrWhiteSpace() ? Descriptions[key] : null
                };
                projectUpdateBatch.ProjectDocumentUpdates.Add(projectDocument);
            }
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (Files[0] == null)
            {
                validationResults.Add(new SitkaValidationResult<NewProjectDocumentViewModel, List<HttpPostedFileBase>>($"You must select at least one file to upload.", m => m.Files));
            }

            if (DisplayNames.Distinct().Count() < DisplayNames.Count())
            {
                validationResults.Add(new SitkaValidationResult<NewProjectDocumentViewModel, List<string>>($"Cannot submit multiple files with the same Display Name for a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Document", m => m.DisplayNames));
            }

            for (int key = 0; key < Files.Count; key++)
            {
                if (DisplayNames[key].IsNullOrWhiteSpace())
                {
                    validationResults.Add(new SitkaValidationResult<NewProjectDocumentViewModel, List<string>>($"Display Name is a required field.", m => m.DisplayNames));
                }


                if (Descriptions[key].Length > Models.ProjectDocument.FieldLengths.Description)
                {
                    validationResults.Add(new SitkaValidationResult<NewProjectDocumentViewModel, List<string>>($"Display Name \"{Descriptions[key]}\" is longer than the allowed {Models.ProjectDocument.FieldLengths.Description} character maximum.", m => m.Descriptions));
                }

                if (DisplayNames[key].Length > Models.ProjectDocument.FieldLengths.DisplayName)
                {
                    validationResults.Add(new SitkaValidationResult<NewProjectDocumentViewModel, List<string>>($"Display Name \"{DisplayNames[key]}\" is longer than the allowed {Models.ProjectDocument.FieldLengths.DisplayName} character maximum.", m => m.DisplayNames));
                }

                FileResource.ValidateFileSize(Files[key], validationResults, "Files");

                var displayNameToLower = DisplayNames[key].ToLower();

                if (HttpRequestStorage.DatabaseEntities.ProjectDocuments.Where(x => x.ProjectID == ParentID)
                    .Any(x => x.DisplayName.ToLower() == displayNameToLower))
                {
                    validationResults.Add(new SitkaValidationResult<NewProjectDocumentViewModel, List<string>>($"There is already a document with the Display Name \"{DisplayNames[key]}\" attached to this {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}. Display Name must be unique for each Document attached to a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}", m => m.DisplayNames));
                }
            }
            
            return validationResults;
        }
    }
}
