﻿using System;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class GrantNoteInternal : IAuditableEntity, IEntityNote
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
            get { return GrantNoteText; }
            set { GrantNoteText = value; }
        }

        public string DeleteUrl
        {
            get { return SitkaRoute<GrantController>.BuildUrlFromExpression(c => c.DeleteGrantNoteInternal(GrantNoteInternalID)); }
        }

        public string EditUrl
        {
            get { return SitkaRoute<GrantController>.BuildUrlFromExpression(c => c.EditGrantNoteInternal(GrantNoteInternalID)); }
        }
        public string AuditDescriptionString
        {
            get
            {
                var grant = HttpRequestStorage.DatabaseEntities.Grants.Find(GrantID);
                var grantName = grant != null ? grant.AuditDescriptionString : ViewUtilities.NotFoundString;
                return $"Grant: {grantName}";
            }
        }
    }
}