using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectDocumentUpdate : IEntityDocument
    {
        public string DeleteUrl
        {
            get
            {
                return SitkaRoute<ProjectDocumentUpdateController>.BuildUrlFromExpression(x =>
                    x.Delete(ProjectDocumentUpdateID));
            }
        }
        public string EditUrl {
            get
            {
                return SitkaRoute<ProjectDocumentUpdateController>.BuildUrlFromExpression(x =>
                    x.Edit(ProjectDocumentUpdateID));
            }
        }
        public string DisplayCssClass { get; set; }

        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            projectUpdateBatch.ProjectDocumentUpdates =
                project.ProjectDocuments.Select(
                    po => new ProjectDocumentUpdate(projectUpdateBatch, po.FileResource, po.DisplayName) { Description = po.Description, ProjectDocumentTypeID = po.ProjectDocumentTypeID}
                ).ToList();
        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectDocument> allProjectDocuments)
        {
            var project = projectUpdateBatch.Project;
            var projectDocumentsFromProjectUpdate =
                projectUpdateBatch.ProjectDocumentUpdates.Select(
                    x => new ProjectDocument(project.ProjectID, x.FileResourceID, x.DisplayName){Description = x.Description, ProjectDocumentTypeID = x.ProjectDocumentTypeID}).ToList();
            project.ProjectDocuments.Merge(projectDocumentsFromProjectUpdate, allProjectDocuments,
                (x, y) => x.ProjectID == y.ProjectID && x.FileResourceID == y.FileResourceID);
        }
    }
}