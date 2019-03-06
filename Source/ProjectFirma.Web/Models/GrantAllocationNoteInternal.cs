using System;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationNoteInternal : IAuditableEntity, IEntityNote
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
            get { return GrantAllocationNoteInternalText; }
            set { GrantAllocationNoteInternalText = value; }
        }

        public string DeleteUrl
        {
            get { return SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(c => c.DeleteGrantAllocationNoteInternal(GrantAllocationNoteInternalID)); }
        }

        public string EditUrl
        {
            get { return SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(c => c.EditGrantAllocationNoteInternal(GrantAllocationNoteInternalID)); }
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