using ProjectFirma.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Program
{
    public class ProjectListGridSpec : GridSpec<Models.Project>
    {
        public ProjectListGridSpec(Person currentPerson, Models.Program currentProgram, Dictionary<int, List<Models.Program>> programsByProject)
        {
            var hasProgramManagePermissions = new ProgramManageFeature().HasPermissionByPerson(currentPerson);

            if (hasProgramManagePermissions)
            {
                Add(string.Empty, x => x.ProjectImportBlockLists.Any(b => b.ProgramID == currentProgram.ProgramID)
                        ? ModalDialogFormHelper.ModalDialogFormLink(null, "Remove from Block List",
                            SitkaRoute<ProjectImportBlockListController>.BuildUrlFromExpression(c =>
                                c.RemoveBlockListProject(x)),
                            $"Remove '{x.DisplayName}' from Import Block List", 950,
                            "btnRemoveImportBlockList", "Yes", "Cancel", null, null, null, null,
                            "Allow project to be updated by the imports of its programs.")
                        : ModalDialogFormHelper.ModalDialogFormLink(null, "Add to Block List",
                            SitkaRoute<ProjectImportBlockListController>.BuildUrlFromExpression(c =>
                                c.BlockListProject(x)), $"Add '{x.DisplayName}' to Import Block List", 950,
                            "btnAddImportBlockList", "Yes", "Cancel", null, null, null, null,
                            "Block project from being updated by the imports of its programs."),
                    125, DhtmlxGridColumnFilterType.None, true);
            }

            Add(Models.FieldDefinition.FhtProjectNumber.ToGridHeaderString(),
                x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.FhtProjectNumber),
                105, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(),
                x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.ProjectName),
                300, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.ProjectType.ToGridHeaderString(), x => x.ProjectType.DisplayName,
                160, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(),
                x => new HtmlString(x.ProjectStage.ProjectStageDisplayName),
                100, DhtmlxGridColumnFilterType.SelectFilterStrict, DhtmlxGridColumnAlignType.Center, false);
            Add(Models.FieldDefinition.Program.ToGridHeaderStringPlural("Programs"), x => Program(x, programsByProject), 180, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.UpdatesFromImport.ToGridHeaderString(),
                x => x.ProjectImportBlockLists.Any(b => b.ProgramID == currentProgram.ProgramID)
                    ? new HtmlString("<span class='red'>Blocked</span>")
                    : new HtmlString("Updates"),
                80, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center, true);
        }

        private static HtmlString Program(Models.Project project, Dictionary<int, List<Models.Program>> programsByProject)
        {
            return project.ProgramListDisplayHelper(programsByProject, false);
        }
    }
}