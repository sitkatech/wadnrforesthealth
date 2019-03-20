using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Shared.ProjectPerson;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class ContactsViewModel : EditPeopleViewModel
    {
        [DisplayName("Comments")]
        [StringLength(ProjectUpdateBatch.FieldLengths.ExpendituresComment)]
        public string Comments { get; set; }

        /// <summary>
        /// Required by the ModelBinder
        /// </summary>
        public ContactsViewModel()
        {
        }

        public ContactsViewModel(ProjectUpdateBatch projectUpdateBatch)
        {
            ProjectPersonSimples = projectUpdateBatch.ProjectPersonUpdates.Select(x=>new ProjectPersonSimple(x)).ToList();
            Comments = projectUpdateBatch.ContactsComment;
        }

        public void UpdateModel(ProjectUpdateBatch projectUpdateBatch,
            List<ProjectPersonUpdate> currentProjectPersonUpdates,
            IList<ProjectPersonUpdate> allProjectPersonUpdates)
        {
            var projectContactUpdatesUpdated = new List<ProjectPersonUpdate>();

            if (ProjectPersonSimples != null)
            {
                // Completely rebuild the list
                projectContactUpdatesUpdated = ProjectPersonSimples.Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.PersonID)).Select(x => x.ToProjectPersonUpdate(projectUpdateBatch)).ToList();
            }

            currentProjectPersonUpdates.Merge(projectContactUpdatesUpdated,
                allProjectPersonUpdates,
                (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.PersonID == y.PersonID && x.ProjectPersonRelationshipTypeID == y.ProjectPersonRelationshipTypeID);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProjectPersonSimples == null)
            {
                ProjectPersonSimples = new List<ProjectPersonSimple>();
            }

            return new List<ValidationResult>();
        }
    }
}