using System;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectInternalNote : IEntityNote, IAuditableEntity
    {
        public DateTime Created
        {
            get { return CreateDate; }
        }

        public string CreatedBy
        {
            get
            {
                if (CreatePersonID.HasValue)
                {
                    return CreatePerson.FullNameFirstLast;
                }
                return "System";
            }
        }
        public DateTime LastUpdated
        {
            get { return UpdateDate ?? CreateDate; }
        }

        public string LastUpdatedBy
        {
            get
            {
                if (UpdatePersonID.HasValue)
                {
                    return UpdatePerson.FullNameFirstLast;
                }
                return string.Empty;
            }
        }

        public string DeleteUrl
        {
            get { return SitkaRoute<ProjectInternalNoteController>.BuildUrlFromExpression(c => c.DeleteProjectInternalNote(this)); }
        }

        public string EditUrl
        {
            get { return SitkaRoute<ProjectInternalNoteController>.BuildUrlFromExpression(c => c.Edit(this)); }
        }

        public string AuditDescriptionString
        {
            get
            {
                var project = HttpRequestStorage.DatabaseEntities.Projects.Find(ProjectID);
                var projectName = project != null ? project.AuditDescriptionString : ViewUtilities.NotFoundString;
                return $"Project: {projectName}";
            }
        }
    }
}