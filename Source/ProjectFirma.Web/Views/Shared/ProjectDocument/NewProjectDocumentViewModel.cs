using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Mvc;
using Microsoft.Ajax.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectDocument
{
    public class NewProjectDocumentViewModel: IValidatableObject
    {
        [Required]
        [DisplayName("File")]
        [SitkaFileExtensions("pdf|zip|doc|docx|xls|xlsx")]
        public List<HttpPostedFileBase> Files { get; set; }

        [Required]
        [DisplayName("Display Name")]
        public List<string> DisplayNames { get; set; }

        [DisplayName("Description")]
        public List<string> Descriptions { get; set; }

        // can be the ID of a Project or a ProjectUpdateBatch depending on whether this ViewModel or its child type is invoked.
        public int? ParentID { get; set; }

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
                    Description = !Descriptions[key].IsNullOrWhiteSpace() ? Descriptions[key] : null
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


            // todo: need to do the string length validations here instead of as attribute on 
            // [StringLength(Models.ProjectDocument.FieldLengths.Description, ErrorMessage = "1000 character maximum.")]
            // [StringLength(Models.ProjectDocument.FieldLengths.DisplayName, ErrorMessage = "200 character maximum")]

            //FileResource.ValidateFileSize(File, validationResults, "File");

            //if (HttpRequestStorage.DatabaseEntities.ProjectDocuments.Where(x => x.ProjectID == ParentID)
            //    .Any(x => x.DisplayName.ToLower() == DisplayName.ToLower()))
            //{
            //    validationResults.Add(new SitkaValidationResult<NewProjectDocumentViewModel, string>($"The Display Name must be unique for each Document attached to a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}", m=>m.DisplayName));
            //}

            return validationResults;
        }
    }
}
