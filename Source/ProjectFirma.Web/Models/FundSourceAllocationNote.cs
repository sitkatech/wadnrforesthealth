using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using System;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocationNote : IAuditableEntity, IEntityNote
    {
        public DateTime Created
        {
            get { return CreatedDate; }
        }

        public string CreatedBy
        {
            get
            {
                return CreatedByPerson.FullNameFirstLast;
            }
        }

        public DateTime LastUpdated
        {
            get { return LastUpdatedDate ?? CreatedDate; }
        }

        public string LastUpdatedBy
        {
            get
            {
                if (LastUpdatedByPersonID.HasValue)
                {
                    return LastUpdatedByPerson.FullNameFirstLast;
                }
                return string.Empty;
            }
        }

        public string Note
        {
            get { return FundSourceAllocationNoteText; }
            set { FundSourceAllocationNoteText = value; }
        }

        public string DeleteUrl
        {
            get { return SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(c => c.DeleteFundSourceAllocationNote(FundSourceAllocationNoteID)); }
        }

        public string EditUrl
        {
            get { return SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(c => c.EditFundSourceAllocationNote(FundSourceAllocationNoteID)); }
        }

        public string AuditDescriptionString
        {
            get
            {
                var fundSourceAllocation = HttpRequestStorage.DatabaseEntities.FundSourceAllocations.Find(FundSourceAllocationID);
                var fundSourceAllocationName = fundSourceAllocation != null ? fundSourceAllocation.AuditDescriptionString : ViewUtilities.NotFoundString;
                return $"FundSourceAllocation: {fundSourceAllocationName}";
            }
        }
    }
}