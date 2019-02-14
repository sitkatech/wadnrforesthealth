using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using System;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationNote : IAuditableEntity, IEntityNote
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
            get { return GrantAllocationNoteText; }
            set { GrantAllocationNoteText = value; }
        }

        public string DeleteUrl
        {
            get { return SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(c => c.DeleteGrantAllocationNote(GrantAllocationID, GrantAllocationNoteID)); }
        }

        public string EditUrl
        {
            get { return SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(c => c.EditGrantAllocationNote(GrantAllocationID, GrantAllocationNoteID)); }
        }

        public string AuditDescriptionString
        {
            get
            {
                var grantAllocation = HttpRequestStorage.DatabaseEntities.GrantAllocations.Find(GrantAllocationID);
                var grantAllocationName = grantAllocation != null ? grantAllocation.AuditDescriptionString : ViewUtilities.NotFoundString;
                return $"GrantAllocation: {grantAllocationName}";
            }
        }
    }
}