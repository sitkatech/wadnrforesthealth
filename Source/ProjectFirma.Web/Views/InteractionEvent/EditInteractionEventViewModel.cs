/*-----------------------------------------------------------------------
<copyright file="EditProjectViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ApprovalUtilities.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class EditInteractionEventViewModel : FormViewModel, IValidatableObject
    {
        public int InteractionEventID { get; set; }

        //[FieldDefinitionDisplay(FieldDefinitionEnum.InteractionEvent)]
        [Required]
        public int InteractionEventTypeID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int? DNRStaffPersonID { get; set; }

        public List<InteractionEventProjectSimpleJson> InteractionEventProjects { get; set; }

        public List<InteractionEventContactSimpleJson> InteractionEventContacts { get; set; }

        [DisplayName("Interaction/Event File Upload")]
        [WADNRFileExtensions(FileResourceMimeTypeEnum.PDF, FileResourceMimeTypeEnum.ExcelXLSX, FileResourceMimeTypeEnum.xExcelXLSX, FileResourceMimeTypeEnum.ExcelXLS, FileResourceMimeTypeEnum.PowerpointPPT, FileResourceMimeTypeEnum.PowerpointPPTX, FileResourceMimeTypeEnum.WordDOC, FileResourceMimeTypeEnum.WordDOCX, FileResourceMimeTypeEnum.TXT, FileResourceMimeTypeEnum.JPEG, FileResourceMimeTypeEnum.PNG)]
        public List<HttpPostedFileBase> InteractionEventFileResourceData { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditInteractionEventViewModel()
        {
            Date = DateTime.Now;
            InteractionEventProjects = new List<InteractionEventProjectSimpleJson>();
            InteractionEventContacts = new List<InteractionEventContactSimpleJson>();
        }

        public EditInteractionEventViewModel(Models.Project project)
        {
            Date = DateTime.Now;
            var interactionEventProjectSimples = new List<InteractionEventProjectSimpleJson>();
            interactionEventProjectSimples.Add(new InteractionEventProjectSimpleJson(InteractionEventID, project.ProjectID));
            InteractionEventProjects = interactionEventProjectSimples;

            InteractionEventContacts = new List<InteractionEventContactSimpleJson>();

        }

        public EditInteractionEventViewModel(Person userToAssociate)
        {
            Date = DateTime.Now;
            InteractionEventProjects = new List<InteractionEventProjectSimpleJson>();

            var interactionEventContactSimples = new List<InteractionEventContactSimpleJson>();
            interactionEventContactSimples.Add(new InteractionEventContactSimpleJson(InteractionEventID, userToAssociate.PersonID));
            InteractionEventContacts = interactionEventContactSimples;
        }

        public EditInteractionEventViewModel(Models.InteractionEvent interactionEvent)
        {
            InteractionEventID = interactionEvent.InteractionEventID;
            InteractionEventTypeID = interactionEvent.InteractionEventTypeID;
            Date = interactionEvent.InteractionEventDate;
            Title = interactionEvent.InteractionEventTitle;
            Description = interactionEvent.InteractionEventDescription;
            DNRStaffPersonID = interactionEvent.StaffPersonID;
            InteractionEventProjects = interactionEvent.InteractionEventProjects.Select(x => new InteractionEventProjectSimpleJson(x.InteractionEvent, x.Project)).ToList();
            InteractionEventContacts = interactionEvent.InteractionEventContacts.Select(x => new InteractionEventContactSimpleJson(x.InteractionEvent, x.Person)).ToList();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == "")
            {
                yield return new SitkaValidationResult<EditInteractionEventViewModel, string>(
                    FirmaValidationMessages.InteractionEventMustHaveTitle, m => m.Title);
            }
        }

        public void UpdateModel(Models.InteractionEvent interactionEvent, Person currentPerson, ICollection<InteractionEventProject> allInteractionEventProjects, ICollection<InteractionEventContact> allInteractionEventContacts)
        {
            interactionEvent.InteractionEventTypeID = InteractionEventTypeID;
            interactionEvent.InteractionEventDate = Date;
            interactionEvent.InteractionEventTitle = Title;
            interactionEvent.InteractionEventDescription = Description;
            interactionEvent.StaffPersonID = DNRStaffPersonID;
            if (InteractionEventProjects != null)
            {
                var interactionEventProjectsUpdated = InteractionEventProjects
                    .Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.ProjectID)).Select(x =>
                        new Models.InteractionEventProject(interactionEvent.InteractionEventID, x.ProjectID)).ToList();

                interactionEvent.InteractionEventProjects.Merge(interactionEventProjectsUpdated,
                    allInteractionEventProjects,
                    (x, y) => x.InteractionEventID == y.InteractionEventID && x.ProjectID == y.ProjectID);
            }

            if (InteractionEventContacts != null)
            {
                var interactionEventContactsUpdated = InteractionEventContacts.Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.PersonID)).Select(x =>
                    new Models.InteractionEventContact(interactionEvent.InteractionEventID, x.PersonID)).ToList();

                interactionEvent.InteractionEventContacts.Merge(interactionEventContactsUpdated,
                    allInteractionEventContacts,
                    (x, y) => x.InteractionEventID == y.InteractionEventID && x.PersonID == y.PersonID);
            }

            if (InteractionEventFileResourceData?[0] != null)
            {
                var fileResources = InteractionEventFileResourceData.Select(fileData =>
                    FileResource.CreateNewFromHttpPostedFile(fileData, currentPerson));

                foreach (var fileResource in fileResources)
                {
                    HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
                    var interactionEventFileResource = new InteractionEventFileResource(interactionEvent, fileResource, fileResource.OriginalCompleteFileName);
                    interactionEvent.InteractionEventFileResources.Add(interactionEventFileResource);
                }
            }
        }
    }

    public class InteractionEventProjectSimpleJson
    {
        public InteractionEventProjectSimpleJson()
        {
        }

        public InteractionEventProjectSimpleJson(Models.InteractionEvent interactionEvent, Models.Project project)
        {
            InteractionEventID = interactionEvent.InteractionEventID;
            ProjectID = project.ProjectID;
        }

        public InteractionEventProjectSimpleJson(int interactionEventID, int projectID)
        {
            InteractionEventID = interactionEventID;
            ProjectID = projectID;
        }

        public int InteractionEventID { get; set; }
        public int ProjectID { get; set; }
    }

    public class InteractionEventContactSimpleJson
    {
        public InteractionEventContactSimpleJson()
        {
        }

        public InteractionEventContactSimpleJson(Models.InteractionEvent interactionEvent, Person person)
        {
            InteractionEventID = interactionEvent.InteractionEventID;
            PersonID = person.PersonID;
        }

        public InteractionEventContactSimpleJson(int interactionEventID, int personID)
        {
            InteractionEventID = interactionEventID;
            PersonID = personID;
        }

        public int InteractionEventID { get; set; }
        public int PersonID { get; set; }
    }
}