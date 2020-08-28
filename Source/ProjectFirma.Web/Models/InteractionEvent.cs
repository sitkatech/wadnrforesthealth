using System.Linq;
using ProjectFirma.Web.Views.Shared.FileResourceControls;

namespace ProjectFirma.Web.Models
{
    public partial class InteractionEvent : IAuditableEntity, ICanUploadNewFiles
    {
        public string DateDisplay => InteractionEventDate.ToShortDateString();

        public string AuditDescriptionString => InteractionEventTitle;

        public bool HasLocationSet => InteractionEventLocationSimple != null;

        public void AddNewFileResource(FileResource fileResource, string displayName, string description)
        {
            var interactionEventFileResource = new InteractionEventFileResource(this, fileResource, displayName) {Description = description};
            InteractionEventFileResources.Add(interactionEventFileResource);
        }

        public void DeleteFullAndChildless(DatabaseEntities dbContext)
        {
            foreach (var x in InteractionEventFileResources.ToList())
            {
                x.DeleteFullAndChildless(dbContext);
            }

            DeleteChildren(dbContext);
            Delete(dbContext);
        }
    }
}
