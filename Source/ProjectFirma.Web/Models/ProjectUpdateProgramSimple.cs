namespace ProjectFirma.Web.Models
{
    public class ProjectUpdateProgramSimple
    {
        public int ProjectUpdateID { get; set; }

        public int ProgramID { get; set; }

        public int ProjectUpdateProgramID { get; set; }

        public string DisplayString { get; set; }


        public ProjectUpdateProgramSimple(ProjectUpdateProgram projectUpdateProgram)
        {
            ProjectUpdateID = projectUpdateProgram.ProjectUpdateID;
            ProgramID = projectUpdateProgram.ProgramID;
            DisplayString = projectUpdateProgram.Program.DisplayName;
            ProjectUpdateProgramID = projectUpdateProgram.ProjectUpdateProgramID;
        }

        public ProjectUpdateProgramSimple()
        {

        }
    }
}