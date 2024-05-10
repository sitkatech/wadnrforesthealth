using ProjectFirma.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
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
            var userHasDeletePermissions = new ProjectDeleteFeature().HasPermissionByPerson(currentPerson);

            if (userHasDeletePermissions)
            {
                AddMasterCheckBoxColumn();
                Add("ProjectID", x => x.ProjectID, 0);
                BulkDeleteModalDialogForm = new BulkDeleteModalDialogForm(SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.BulkDeleteProjects(null)), $"Delete Checked {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", $"Delete {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}");
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, AgGridColumnFilterType.None);
            }

            if (hasProgramManagePermissions)
            {
                Add("Remove", x => x.ProjectImportBlockLists
                        .Any(b => b.ProgramID == currentProgram.ProgramID) ? RemoveFromBlockListModalLink(x) : AddToBlockListModalLink(currentProgram, x),
                    125, AgGridColumnFilterType.None, true);
            }


            Add(Models.FieldDefinition.FhtProjectNumber.ToGridHeaderString(), x => $"{{ \"link\":\"{x.GetDetailUrl()}\",\"displayText\":\"{x.FhtProjectNumber}\" }}", 105, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(),
                x => $"{{ \"link\":\"{x.GetDetailUrl()}\",\"displayText\":\"{x.ProjectName}\" }}",
                300, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.ProjectType.ToGridHeaderString(), x => x.ProjectType.DisplayName,
                160, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(),
                x => new HtmlString(x.ProjectStage.ProjectStageDisplayName),
                100, AgGridColumnFilterType.SelectFilterStrict, AgGridColumnAlignType.Center, false);
            Add(Models.FieldDefinition.Program.ToGridHeaderStringPlural("Programs"), x => Program(x, programsByProject), 180, AgGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.UpdatesFromImport.ToGridHeaderString(),
                x => x.ProjectImportBlockLists.Any(b => b.ProgramID == currentProgram.ProgramID)
                    ? new HtmlString("<span class='red'>Blocked</span>")
                    : new HtmlString("Updates"),
                80, AgGridColumnFilterType.SelectFilterHtmlStrict, AgGridColumnAlignType.Center, true);
        }

        private static HtmlString Program(Models.Project project, Dictionary<int, List<Models.Program>> programsByProject)
        {
            return project.ProgramListDisplayHelper(programsByProject, false);
        }

        private static HtmlString RemoveFromBlockListModalLink(Models.Project project)
        {
            return ModalDialogFormHelper.ModalDialogFormLink(null, "Remove from Block List",
                SitkaRoute<ProjectImportBlockListController>.BuildUrlFromExpression(c =>
                    c.RemoveBlockListProject(project)),
                $"Remove '{project.DisplayName}' from Import Block List", 950,
                "btnRemoveImportBlockList", "Yes", "Cancel", null, null, null, null,
                "Allow project to be updated by the imports of its programs.", false);
        }

        private static HtmlString AddToBlockListModalLink(Models.Program program, Models.Project project)
        {
            var contentUrl = SitkaRoute<ProgramController>.BuildUrlFromExpression(x => x.NewBlockListEntryFromProject(program.PrimaryKey, project.PrimaryKey));
            return ModalDialogFormHelper.ModalDialogFormLink(null, "Add to Block List",
                contentUrl, 
                $"Add '{project.DisplayName}' to Import Block List", 950,
                "btnAddImportBlockList", "Save", "Cancel", null, null, null, null,
                "Block project from being updated by the imports of its programs.", false);
        }
    }
}