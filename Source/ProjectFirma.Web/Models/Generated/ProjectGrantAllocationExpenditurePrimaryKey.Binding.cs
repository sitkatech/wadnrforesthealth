//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectGrantAllocationExpenditure
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectGrantAllocationExpenditurePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectGrantAllocationExpenditure>
    {
        public ProjectGrantAllocationExpenditurePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectGrantAllocationExpenditurePrimaryKey(ProjectGrantAllocationExpenditure projectGrantAllocationExpenditure) : base(projectGrantAllocationExpenditure){}

        public static implicit operator ProjectGrantAllocationExpenditurePrimaryKey(int primaryKeyValue)
        {
            return new ProjectGrantAllocationExpenditurePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectGrantAllocationExpenditurePrimaryKey(ProjectGrantAllocationExpenditure projectGrantAllocationExpenditure)
        {
            return new ProjectGrantAllocationExpenditurePrimaryKey(projectGrantAllocationExpenditure);
        }
    }
}