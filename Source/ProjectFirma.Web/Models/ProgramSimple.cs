namespace ProjectFirma.Web.Models
{
    public class ProgramSimple
    {
        public int ProgramID { get; set; }

        public string DisplayString { get; set; }

        public ProgramSimple(Program program)
        {
            ProgramID = program.ProgramID;
            DisplayString = program.DisplayName;
        }
    }
}