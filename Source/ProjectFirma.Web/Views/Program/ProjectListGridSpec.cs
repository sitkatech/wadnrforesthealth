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

namespace ProjectFirma.Web.Views.Program
{
    public class ProjectListGridSpec : GridSpec<ProjectListViewModel>
    {
        public ProjectListGridSpec(Person currentPerson)
        {
            Add(string.Empty, x => x.ExistsOnImportBlockList
                    ? ModalDialogFormHelper.ModalDialogFormLink("Remove from Block List",
                        SitkaRoute<ProjectImportBlockListController>.BuildUrlFromExpression(c =>
                            c.RemoveBlockListProject(x.Project)),
                        $"Remove {x.Project.DisplayName} from Import Block List", 950, "Yes", "Cancel", null, null,
                        null)
                    : ModalDialogFormHelper.ModalDialogFormLink("Add to Block List",
                        SitkaRoute<ProjectImportBlockListController>.BuildUrlFromExpression(c =>
                            c.BlockListProject(x.Project)), $"Add {x.Project.DisplayName} to Import Block List", 950,
                        "Yes", "Cancel", null, null, null),
                125, DhtmlxGridColumnFilterType.None, true);

            Add(Models.FieldDefinition.FhtProjectNumber.ToGridHeaderString(),
                x => UrlTemplate.MakeHrefString(x.Project.GetDetailUrl(), x.Project.FhtProjectNumber), 105,
                DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(),
                x => UrlTemplate.MakeHrefString(x.Project.GetDetailUrl(), x.Project.ProjectName), 350,
                DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.ProjectType.ToGridHeaderString(), x => x.Project.ProjectType.DisplayName, 240,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(),
                x => new HtmlString(x.Project.ProjectStage.ProjectStageDisplayName), 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict, DhtmlxGridColumnAlignType.Center, false);
            Add(Models.FieldDefinition.UpdatesFromImport.ToGridHeaderString(),
                x => x.ExistsOnImportBlockList
                    ? new HtmlString("<span class='red'>Blocked</span>")
                    : new HtmlString("Updates"),
                90, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center, true);
        }
    }
}