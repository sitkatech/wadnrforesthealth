using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public class ProjectDocumentResource : IEntityDocument
    {
        public string DeleteUrl { get; }
        public string EditUrl { get; }
        public FileResource FileResource { get; set; }
        public string DisplayCssClass { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public ProjectDocumentType ProjectDocumentType { get; set; }

        public ProjectDocumentResource(string deleteUrl, string editUrl, FileResource fileResource, string displayCssClass,
            string displayName, string description, ProjectDocumentType projectDocumentType)
        {
            DeleteUrl = deleteUrl;
            EditUrl = editUrl;
            FileResource = fileResource;
            DisplayCssClass = displayCssClass;
            DisplayName = displayName;
            Description = description;
            ProjectDocumentType = projectDocumentType;
        }

        public ProjectDocumentResource(ProjectDocument projectDocument)
        {
            DeleteUrl = SitkaRoute<ProjectDocumentController>.BuildUrlFromExpression(y => y.Delete(projectDocument));
            EditUrl = SitkaRoute<ProjectDocumentController>.BuildUrlFromExpression(y => y.Edit(projectDocument));
            FileResource = projectDocument.FileResource;
            DisplayCssClass = projectDocument.DisplayCssClass;
            DisplayName = projectDocument.DisplayName;
            Description = projectDocument.Description;
            ProjectDocumentType = projectDocument.ProjectDocumentType;
        }

        public ProjectDocumentResource(ProjectDocumentUpdate projectDocumentUpdate)
        {
            DeleteUrl = SitkaRoute<ProjectDocumentUpdateController>.BuildUrlFromExpression(y => y.Delete(projectDocumentUpdate));
            EditUrl = SitkaRoute<ProjectDocumentUpdateController>.BuildUrlFromExpression(y => y.Edit(projectDocumentUpdate));
            FileResource = projectDocumentUpdate.FileResource;
            DisplayCssClass = projectDocumentUpdate.DisplayCssClass;
            DisplayName = projectDocumentUpdate.DisplayName;
            Description = projectDocumentUpdate.Description;
            ProjectDocumentType = projectDocumentUpdate.ProjectDocumentType;
        }



        public static List<ProjectDocumentResource> CreateFromProjectDocuments(IEnumerable<ProjectDocument> projectDocuments)
        {
            return projectDocuments.Select(x => new ProjectDocumentResource(x)).ToList();
        }

        public static List<ProjectDocumentResource> CreateFromProjectDocuments(IEnumerable<ProjectDocumentUpdate> projectDocumentUpdates)
        {
            return projectDocumentUpdates.Select(x => new ProjectDocumentResource(x)).ToList();
        }
    }
}