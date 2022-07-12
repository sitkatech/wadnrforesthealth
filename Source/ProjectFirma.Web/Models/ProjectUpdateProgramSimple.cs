namespace ProjectFirma.Web.Models
{
    public class ProjectUpdateProgramSimple
    {
        public int ProjectUpdateBatchID { get; set; }

        public int ProgramID { get; set; }

        public int ProjectUpdateProgramID { get; set; }

        public string DisplayString { get; set; }


        public ProjectUpdateProgramSimple(ProjectUpdateProgram projectUpdateProgram)
        {
            ProjectUpdateBatchID = projectUpdateProgram.ProjectUpdateBatchID;
            ProgramID = projectUpdateProgram.ProgramID;
            DisplayString = projectUpdateProgram.Program.DisplayName;
            ProjectUpdateProgramID = projectUpdateProgram.ProjectUpdateProgramID;
        }

        public ProjectUpdateProgramSimple()
        {

        }
    }
}