using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Program
{
    public abstract class ProgramViewData : FirmaViewData
    {
        public Models.Program Program { get; }
        public string EditProgramUrl { get; set; }
        public bool UserHasEditProgramPermissions { get; set; }

        public string BackToProgramsText { get; set; }

        public string ProgramsListUrl { get; set; }

        protected ProgramViewData(Person currentPerson, Models.Program program) : base(currentPerson, null)
        {
            Program = program;
            HtmlPageTitle = program.ProgramName;
            EntityName = $"{Models.FieldDefinition.Program.GetFieldDefinitionLabel()}";
            EditProgramUrl = program.GetEditUrl();
            UserHasEditProgramPermissions = new ProgramEditFeature().HasPermissionByPerson(currentPerson);
            BackToProgramsText = $"Back to all {Models.FieldDefinition.Program.GetFieldDefinitionLabelPluralized()}";
            ProgramsListUrl = SitkaRoute<ProgramController>.BuildUrlFromExpression(c => c.Index());
        }
    }
}