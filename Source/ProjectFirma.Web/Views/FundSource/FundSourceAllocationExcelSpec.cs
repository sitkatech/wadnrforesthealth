using LtInfo.Common.ExcelWorkbookUtilities;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FundSource
{
    public class FundSourceAllocationExcelSpec : ExcelWorksheetSpec<Models.FundSourceAllocation>
    {
        public FundSourceAllocationExcelSpec()
        {
            AddColumn(Models.FieldDefinition.FundSourceNumber.FieldDefinitionDisplayName, x => x.FundSource.FundSourceNumber);
            AddColumn(Models.FieldDefinition.FundSourceAllocationName.FieldDefinitionDisplayName, x => x.FundSourceAllocationName);
            AddColumn(Models.FieldDefinition.ProgramManager.FieldDefinitionDisplayName, x => x.GetAllProgramManagerPersonNamesAsString());
            AddColumn(Models.FieldDefinition.FundSourceStartDate.FieldDefinitionDisplayName, x => x.StartDate);
            AddColumn(Models.FieldDefinition.FundSourceEndDate.FieldDefinitionDisplayName, x => x.EndDate);
            AddColumn($"Parent FundSource {Models.FieldDefinition.FundSourceStatus.FieldDefinitionDisplayName}", x => x.FundSource.FundSourceStatus.FundSourceStatusName);
            AddColumn(Models.FieldDefinition.DNRUplandRegion.FieldDefinitionDisplayName, x => x.DNRUplandRegion?.DNRUplandRegionName ?? string.Empty);
            AddColumn(Models.FieldDefinition.FederalFundCode.FieldDefinitionDisplayName, x => x.FederalFundCodeDisplay);
            AddColumn(Models.FieldDefinition.AllocationAmount.FieldDefinitionDisplayName, x => x.AllocationAmount);
            AddColumn(Models.FieldDefinition.ProgramIndexProjectCode.FieldDefinitionDisplayName, x => x.GetAssociatedProgramIndexProjectCodePairsCommaDelimited());
            AddColumn(Models.FieldDefinition.Organization.FieldDefinitionDisplayName, x => x.Organization?.OrganizationName ?? string.Empty);
        }
    }
}