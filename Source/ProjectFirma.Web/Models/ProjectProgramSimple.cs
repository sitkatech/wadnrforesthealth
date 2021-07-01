namespace ProjectFirma.Web.Models
{
    public class ProjectProgramSimple
    {
        public int ProjectID { get; set; }

        public int ProgramID { get; set; }

        public int ProjectProgramID { get; set; }

        public string DisplayString { get; set; }


        public ProjectProgramSimple(ProjectProgram projectProgram)
        {
            ProjectID = projectProgram.ProjectID;
            ProgramID = projectProgram.ProgramID;
            DisplayString = projectProgram.Program.DisplayName;
            ProjectProgramID = projectProgram.ProjectProgramID;
        }

        public ProjectProgramSimple()
        {

        }
    }
}