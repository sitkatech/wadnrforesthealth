using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectUpdateProgram : IAuditableEntity
    {
        public string AuditDescriptionString => $"ProjectUpdateProgram: {ProjectUpdateProgramID}";



        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectProgram> allProjectProgramsInDB)
        {
            var project = projectUpdateBatch.Project;
            var projectProgramsFromProjectUpdate = projectUpdateBatch.ProjectUpdatePrograms.Select(x => new ProjectProgram(project.ProjectID, x.ProgramID){ Program = x.Program, Project = project}).ToList();
            project.ProjectPrograms.Merge(projectProgramsFromProjectUpdate,
                allProjectProgramsInDB,
                (x, y) => x.ProjectID == y.ProjectID && x.ProgramID == y.ProgramID);

        }
    }
}