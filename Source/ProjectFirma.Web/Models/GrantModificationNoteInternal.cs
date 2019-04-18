using System;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class GrantModificationNoteInternal : IAuditableEntity, IEntityNote
    {
        public DateTime Created => CreatedDate;

        public string CreatedBy => CreatedByPerson.FullNameFirstLast;

        public DateTime LastUpdated => LastUpdatedDate ?? CreatedDate;

        public string LastUpdatedBy => LastUpdatedByPersonID.HasValue ? LastUpdatedByPerson.FullNameFirstLast : string.Empty;

        public string Note
        {
            get => GrantModificationNoteInternalText;
            set => GrantModificationNoteInternalText = value;
        }

        public string DeleteUrl
        {
            get { return SitkaRoute<GrantModificationController>.BuildUrlFromExpression(c => c.DeleteGrantModificationNoteInternal(GrantModificationNoteInternalID)); }
        }

        public string EditUrl
        {
            get { return SitkaRoute<GrantModificationController>.BuildUrlFromExpression(c => c.EditGrantModificationNoteInternal(GrantModificationNoteInternalID)); }
        }

        public string AuditDescriptionString
        {
            get
            {
                var grantModification = HttpRequestStorage.DatabaseEntities.GrantModifications.Find(GrantModificationID);
                var grantModificationName = grantModification != null ? grantModification.AuditDescriptionString : ViewUtilities.NotFoundString;
                return $"GrantModification: {grantModificationName}";
            }
        }
    }
}