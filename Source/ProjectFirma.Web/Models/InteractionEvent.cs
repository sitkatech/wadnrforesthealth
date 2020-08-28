using ProjectFirma.Web.Views.Shared.FileResourceControls;

namespace ProjectFirma.Web.Models
{
    public partial class InteractionEvent : IAuditableEntity, ICanUploadNewFiles
    {
        public string DateDisplay => InteractionEventDate.ToShortDateString();

        public string AuditDescriptionString => InteractionEventTitle;

        public bool HasLocationSet => InteractionEventLocationSimple != null;

        public void AddNewFileResource(FileResource fileResource)
        {
            var interactionEventFileResource = new InteractionEventFileResource(this, fileResource, fileResource.OriginalCompleteFileName);
            InteractionEventFileResources.Add(interactionEventFileResource);
        }
    }
}
