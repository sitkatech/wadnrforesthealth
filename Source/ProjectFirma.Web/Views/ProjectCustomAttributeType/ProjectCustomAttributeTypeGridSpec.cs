using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Views.ProjectCustomAttributeType
{
    public class ProjectCustomAttributeTypeGridSpec : GridSpec<Models.ProjectCustomAttributeType>
    {
        public ProjectCustomAttributeTypeGridSpec()
        {
            Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, AgGridColumnFilterType.None);
            Add("Edit", x => AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditUrl(), ModalDialogFormHelper.DefaultDialogWidth, "Edit Attribute")), 30, AgGridColumnFilterType.None);
            Add(Models.FieldDefinition.ProjectCustomAttribute.ToGridHeaderString(), a => new HtmlLinkObject(a.ProjectCustomAttributeTypeName, a.GetDetailUrl()).ToJsonObjectForAgGrid(), 200, AgGridColumnFilterType.HtmlLinkJson);
            Add("Description", a => a.ProjectCustomAttributeTypeDescription, 300);
            Add(Models.FieldDefinition.ProjectCustomAttributeDataType.ToGridHeaderString(), a => a.ProjectCustomAttributeDataType.ProjectCustomAttributeDataTypeDisplayName, 100, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.MeasurementUnit.ToGridHeaderString(), a => a.GetMeasurementUnitDisplayName(), 100, AgGridColumnFilterType.SelectFilterStrict);
            Add("Required?", a => a.IsRequired.ToYesNo(), 100, AgGridColumnFilterType.SelectFilterStrict);
        }
    }
}
