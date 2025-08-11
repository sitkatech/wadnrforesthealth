using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using System;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceNote : IAuditableEntity, IEntityNote
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
            get { return FundSourceNoteText; }
            set { FundSourceNoteText = value; }
        }

        public string DeleteUrl
        {
            get { return SitkaRoute<FundSourceController>.BuildUrlFromExpression(c => c.DeleteFundSourceNote(FundSourceNoteID)); }
        }

        public string EditUrl
        {
            get { return SitkaRoute<FundSourceController>.BuildUrlFromExpression(c => c.EditFundSourceNote(FundSourceNoteID)); }
        }
        public string AuditDescriptionString
        {
            get
            {
                var fundSource = HttpRequestStorage.DatabaseEntities.FundSources.Find(FundSourceID);
                var fundSourceName = fundSource != null ? fundSource.AuditDescriptionString : ViewUtilities.NotFoundString;
                return $"FundSource: {fundSourceName}";
            }
        }
    }
}