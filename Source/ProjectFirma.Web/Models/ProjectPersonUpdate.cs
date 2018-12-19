using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectPersonUpdate : IAuditableEntity, IProjectPerson
    {
        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            projectUpdateBatch.ProjectUpdate.PrimaryContactPersonID = project.PrimaryContactPersonID;
            projectUpdateBatch.ProjectPersonUpdates =
                project.ProjectPeople.Select(
                    po => new ProjectPersonUpdate(projectUpdateBatch, po.Person, po.ProjectPersonRelationshipType)
                        ).ToList();
        }

        public string AuditDescriptionString
        {
            get
            {
                var projectUpdateBatch = HttpRequestStorage.DatabaseEntities.AllProjectUpdateBatches.Find(ProjectUpdateBatchID);
                var project = projectUpdateBatch?.Project;
                var organization = HttpRequestStorage.DatabaseEntities.AllPeople.Find(PersonID);
                var projectName = project != null ? project.AuditDescriptionString : ViewUtilities.NotFoundString;
                var organizationName = organization != null ? organization.AuditDescriptionString : ViewUtilities.NotFoundString;
                return $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update: {projectName}, Person: {organizationName}";
            }
        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectPerson> allProjectPeople)
        {
            var project = projectUpdateBatch.Project;
            var projectPeopleFromProjectUpdate =
                projectUpdateBatch.ProjectPersonUpdates.Select(
                    x => new ProjectPerson(project.ProjectID, x.PersonID, x.ProjectPersonRelationshipTypeID)).ToList();
            project.ProjectPeople.Merge(projectPeopleFromProjectUpdate, allProjectPeople,
                (x, y) => x.PersonID == y.PersonID && x.ProjectPersonRelationshipTypeID == y.ProjectPersonRelationshipTypeID);
        }
    }
}