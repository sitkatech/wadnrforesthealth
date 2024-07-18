using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
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
    public class ProjectBlockListGridSpec : GridSpec<Models.ProjectImportBlockList>
    {
        public ProjectBlockListGridSpec(Person currentPerson, Models.Program currentProgram)
        {
            var hasProgramManagePermissions = new ProgramManageFeature().HasPermissionByPerson(currentPerson);

            if (hasProgramManagePermissions)
            {
                var contentUrl = SitkaRoute<ProgramController>.BuildUrlFromExpression(x => x.NewBlockListEntry(currentProgram.PrimaryKey));
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, $"Create a new Project Import Block List entry");
                
                Add("Remove", x => ModalDialogFormHelper.ModalDialogFormLink(null, "Remove from Block List",
                    SitkaRoute<ProjectImportBlockListController>.BuildUrlFromExpression(c =>
                        c.RemoveBlockListEntry(x.PrimaryKey)),
                    $"Remove '{x.ProjectName}' ({x.ProjectGisIdentifier}) from Import Block List", 950,
                    "btnRemoveImportBlockList", "Yes", "Cancel", null, null, null, null,
                    "Allow project to be updated by the imports of its programs.", false),
                    125, AgGridColumnFilterType.None, true);
            }

            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(),
                x => ((x.Project != null) ? new HtmlLinkObject(x.Project.ProjectName,x.Project.GetDetailUrl()) : new HtmlLinkObject(x.ProjectName, string.Empty)).ToJsonObjectForAgGrid(),
                300, AgGridColumnFilterType.HtmlLinkJson);

            Add(Models.FieldDefinition.ProjectIdentifier.ToGridHeaderString(),
                x => (x.Project != null) ? x.Project.ProjectGisIdentifier : x.ProjectGisIdentifier,
                200, AgGridColumnFilterType.Text);

            Add("Notes",
                x => (!string.IsNullOrEmpty(x.Notes)) ? x.Notes : string.Empty,
                500, AgGridColumnFilterType.Text);
        }
    }
}