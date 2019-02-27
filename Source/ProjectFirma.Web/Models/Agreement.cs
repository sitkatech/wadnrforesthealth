using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Models
{
    public partial class Agreement : IAuditableEntity
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string AuditDescriptionString => AgreementTitle;
        public string GrantNumberDisplay => GrantID.HasValue ? Grant?.GrantNumber : string.Empty;
        public string AgreementStatusDisplay => AgreementStatus != null ? AgreementStatus.AgreementStatusName : string.Empty;

        // read-only Helper accessors
        public List<ProgramIndex> ProgramIndices => this.AgreementGrantAllocations.Select(aga => aga.GrantAllocation.ProgramIndex).Where(pi => pi != null).ToList();
        public List<ProjectCode> ProjectCodes => this.AgreementGrantAllocations.SelectMany(aga => aga.GrantAllocation.ProjectCodes).Where(pc => pc != null).ToList();
    }
}