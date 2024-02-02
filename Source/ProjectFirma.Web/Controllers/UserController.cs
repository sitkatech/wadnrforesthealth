/*-----------------------------------------------------------------------
<copyright file="UserController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
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
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.User;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.Agreement;
using ProjectFirma.Web.Views.InteractionEvent;
using ProjectFirma.Web.Views.Shared.UserStewardshipAreas;
using Detail = ProjectFirma.Web.Views.User.Detail;
using DetailViewData = ProjectFirma.Web.Views.User.DetailViewData;
using Person = ProjectFirma.Web.Models.Person;

namespace ProjectFirma.Web.Controllers
{
    public class UserController : FirmaBaseController
    {
        // 7/25/23 TK - originally tried to pass the enum as a variable but that complained because the enum cannot be null and a plain /User/Index URL would throw an error. doing it this way allows the plain /User/Index URL to work
        [HttpGet]
        [UserAndContactIndexViewFeature]
        public ViewResult Index(int? selectedUsersStatusFilterTypeEnum = (int)IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsersAndContacts)
        {

            return ViewIndex(selectedUsersStatusFilterTypeEnum ?? (int)IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsersAndContacts);
        }

        private ViewResult ViewIndex(int selectedUsersStatusFilterTypeEnum)
        {
            List<SelectListItem> activeOnlyOrAllUserSelectListItems = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "All Active Users and Contacts",
                    Value = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index((int)IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsersAndContacts)),
                    Selected = selectedUsersStatusFilterTypeEnum == (int)IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsersAndContacts
                },
                new SelectListItem()
                {
                    Text = "All Active Users",
                    Value = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index((int)IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsers)),
                    Selected = selectedUsersStatusFilterTypeEnum == (int)IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsers
                },
                new SelectListItem()
                {
                    Text = "All Contacts",
                    Value = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index((int)IndexGridSpec.UsersStatusFilterTypeEnum.AllContacts)),
                    Selected = selectedUsersStatusFilterTypeEnum == (int)IndexGridSpec.UsersStatusFilterTypeEnum.AllContacts
                },
                new SelectListItem()
                {
                    Text = "All Users and Contacts",
                    Value = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index((int)IndexGridSpec.UsersStatusFilterTypeEnum.AllUsersAndContacts)),
                    Selected = selectedUsersStatusFilterTypeEnum == (int)IndexGridSpec.UsersStatusFilterTypeEnum.AllUsersAndContacts
                }
            };

            var viewData = new IndexViewData(CurrentPerson, activeOnlyOrAllUserSelectListItems, (IndexGridSpec.UsersStatusFilterTypeEnum)selectedUsersStatusFilterTypeEnum);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [UserAndContactIndexViewFeature]
        public GridJsonNetJObjectResult<Person> IndexGridJsonData(IndexGridSpec.UsersStatusFilterTypeEnum usersStatusFilterType)
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var persons = HttpRequestStorage.DatabaseEntities.People.ToList().Where(x => new UserViewFeature().HasPermission(CurrentPerson, x).HasPermission);

            switch (usersStatusFilterType)
            {
                case IndexGridSpec.UsersStatusFilterTypeEnum.AllContacts:
                    persons = persons.Where(x => !x.IsFullUser());
                    break;
                case IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsers:
                    persons = persons.Where(x => x.IsFullUser() && x.IsActive);
                    break;
                case IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsersAndContacts:
                    persons = persons.Where(x => x.IsActive);
                    break;
                case IndexGridSpec.UsersStatusFilterTypeEnum.AllUsersAndContacts:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("usersStatusFilterType", usersStatusFilterType,
                        null);
            }

            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Person>(persons.OrderBy(x => x.FullNameLastFirst).ToList(), gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [UserEditAsAdminFeature]
        public PartialViewResult EditRoles(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var viewModel = new EditRolesViewModel(person);
            return ViewEdit(viewModel, person);
        }

        [HttpPost]
        [UserEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditRoles(PersonPrimaryKey personPrimaryKey, EditRolesViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, person);
            }
            viewModel.UpdateModel(person, CurrentPerson);
            SetMessageForDisplay($"Successfully updated the roles for {person.GetFullNameFirstLastAsUrl()}");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditRolesViewModel viewModel, Person personBeingUpdated)
        {
            var canEditPersonBaseRole = CurrentPerson.IsAdministrator() || (CurrentPerson.HasRole(Role.CanAddEditUsersContactsOrganizations) && !personBeingUpdated.IsAdministrator());
            var baseRoles = new List<Role>();
            if (CurrentPerson.IsSitkaAdministrator())
            {
                baseRoles = Role.AllBaseRoles();
            }
            else if (CurrentPerson.IsAdministrator())
            {
                baseRoles = Role.AllBaseRoles().Except(new[] { Role.EsaAdmin }).ToList();
            }
            else if (canEditPersonBaseRole)
            {
                baseRoles = Role.AllBaseRoles().Except(new[] { Role.Admin }).Except(new[] { Role.EsaAdmin }).ToList();
            } 
            var baseRolesAsSimples = baseRoles.Select(x => new RoleSimple(x)).ToList();
            var supplementalRoles = Role.AllSupplementalRoles();
            var supplementalRolesAsSimples = supplementalRoles.Select(x => new RoleSimple(x)).ToList();
            var viewData = new EditRolesViewData(supplementalRolesAsSimples, baseRolesAsSimples, canEditPersonBaseRole);
            return RazorPartialView<EditRoles, EditRolesViewData, EditRolesViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [PersonDeleteFeature]
        public PartialViewResult Delete(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(person.PersonID);
            return ViewDelete(person, viewModel);
        }

        private PartialViewResult ViewDelete(Person person, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = person != CurrentPerson;
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete {person.FullNameFirstLast}?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Person", SitkaRoute<UserController>.BuildLinkFromExpression(x => x.Detail(person), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [PersonDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(PersonPrimaryKey personPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(person, viewModel);
            }
            person.DeleteFull(HttpRequestStorage.DatabaseEntities);
            var personText = person.IsFullUser() ? "User" : "Contact";
            SetMessageForDisplay($"{personText} '{person.FullNameFirstLast}' has been deleted.");
            return new ModalDialogFormJsonResult(SitkaRoute<UserController>.BuildUrlFromExpression(uc => uc.Index((int)IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsersAndContacts)));
        }

        [UserViewFeature]
        public ViewResult Detail(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var userNotificationGridSpec = new UserNotificationGridSpec();
            var userNotificationGridDataUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.UserNotificationsGridJsonData(personPrimaryKey));
            var basicProjectInfoGridSpec = new Views.Project.ProjectInfoForUserDetailGridSpec(CurrentPerson, person)
            {
                ObjectNameSingular = $"{FieldDefinition.Project.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };
            const string basicProjectInfoGridName = "userProjectListGrid";
            var projectInfoForUserDetailGridDataUrl = SitkaRoute<UserController>.BuildUrlFromExpression(tc => tc.ProjectInfoForUserDetailGridJsonData(person));
            var activateInactivateUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.ActivateInactivatePerson(person));
            var agreements = GetAgreementsByPerson(person);
            var viewData = new DetailViewData(CurrentPerson,
                person,
                basicProjectInfoGridSpec,
                basicProjectInfoGridName,
                projectInfoForUserDetailGridDataUrl,
                userNotificationGridSpec,
                "userNotifications",
                userNotificationGridDataUrl,
                activateInactivateUrl,
                agreements.Any(x => x.AgreementFileResourceID.HasValue));
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [UserViewFeature]
        public GridJsonNetJObjectResult<ProjectPersonRelationship> ProjectInfoForUserDetailGridJsonData(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var gridSpec = new Views.Project.ProjectInfoForUserDetailGridSpec(CurrentPerson, person);
            var projectPersons = person.IsFullUser()
                ? person.GetPrimaryContactProjects(CurrentPerson).OrderBy(x => x.DisplayName).Select(x=> new ProjectPersonRelationship(x, person, null)).ToList()
                : person.ProjectPeople.Where(x => person.PersonID == CurrentPerson.PersonID || x.ProjectPersonRelationshipType.IsViewableByUser(CurrentPerson)).Select(x=>new ProjectPersonRelationship(x)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ProjectPersonRelationship>(projectPersons, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [UserViewFeature]
        public GridJsonNetJObjectResult<Notification> UserNotificationsGridJsonData(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var gridSpec = new UserNotificationGridSpec();
            var notifications = person.Notifications.OrderByDescending(x => x.NotificationDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Notification>(notifications, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [UserEditAsAdminFeature]
        public PartialViewResult ActivateInactivatePerson(PersonPrimaryKey personPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(personPrimaryKey.PrimaryKeyValue);
            return ViewActivateInactivatePerson(personPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewActivateInactivatePerson(Person person, ConfirmDialogFormViewModel viewModel)
        {
            string confirmMessage;
            if (person.IsActive)
            {
                var isPrimaryContactForAnyOrganization = person.OrganizationsWhereYouAreThePrimaryContactPerson.Any();
                if (isPrimaryContactForAnyOrganization)
                {
                    confirmMessage =
                        $@"You cannot inactive user '{person.FullNameFirstLast}' because {person.FirstName} is the {FieldDefinition.OrganizationPrimaryContact.GetFieldDefinitionLabel()} for the following organizations: <ul> {string.Join("\r\n", person.PrimaryContactOrganizations.Select(x =>$"<li>{x.OrganizationName}</li>"))}</ul>";
                }
                else
                {
                    confirmMessage = $"Are you sure you want to inactivate user '{person.FullNameFirstLast}'?";
                }
                var viewData = new ConfirmDialogFormViewData(confirmMessage, !isPrimaryContactForAnyOrganization);
                return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
            }
            else
            {
                confirmMessage = $"Are you sure you want to activate user '{person.FullNameFirstLast}'?";
                var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
                return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
            }
        }

        [HttpPost]
        [UserEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ActivateInactivatePerson(PersonPrimaryKey personPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (person.IsActive)
            {
                Check.Require(!person.OrganizationsWhereYouAreThePrimaryContactPerson.Any(),
                    $@"You cannot inactive user '{person.FullNameFirstLast}' because {
                            person.FirstName
                        } is the {FieldDefinition.OrganizationPrimaryContact.GetFieldDefinitionLabel()} for one or more {FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}!");
            }
            if (!ModelState.IsValid)
            {
                return ViewActivateInactivatePerson(person, viewModel);
            }
            if (person.IsActive)
            {
                // if the person is currently active, we need to remove them from the support email list no matter what since we are about to inactivate the person
                person.ReceiveSupportEmails = false;
            }
            person.IsActive = !person.IsActive;
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [UserEditAsAdminFeature]
        public PartialViewResult EditStewardshipAreas(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var viewModel = new EditUserStewardshipAreasViewModel(person, MultiTenantHelpers.GetProjectStewardshipAreaType());
            return ViewEditStewardshipAreas(viewModel);
        }

        [HttpPost]
        [UserEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditStewardshipAreas(PersonPrimaryKey personPrimaryKey, EditUserStewardshipAreasViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditStewardshipAreas(viewModel);
            }
            var projectStewardshipAreaType = MultiTenantHelpers.GetProjectStewardshipAreaType().ToEnum;

            switch (projectStewardshipAreaType)
            {
                case ProjectStewardshipAreaTypeEnum.ProjectStewardingOrganizations:
                    HttpRequestStorage.DatabaseEntities.Organizations.Load();
                    viewModel.UpdateModel(person, HttpRequestStorage.DatabaseEntities.PersonStewardOrganizations.Local);
                    break;
                case ProjectStewardshipAreaTypeEnum.TaxonomyBranches:
                    HttpRequestStorage.DatabaseEntities.TaxonomyBranches.Load();
                    viewModel.UpdateModel(person, HttpRequestStorage.DatabaseEntities.PersonStewardTaxonomyBranches.Local);
                    break;
                case ProjectStewardshipAreaTypeEnum.Regions:
                    HttpRequestStorage.DatabaseEntities.DNRUplandRegions.Load();
                    viewModel.UpdateModel(person, HttpRequestStorage.DatabaseEntities.PersonStewardRegions.Local);
                    break;
                default:
                    throw new InvalidOperationException(
                        "The Stewardship Area editor should only be allowed for tenants with a Project Stewardship Area Type");
            }


            SetMessageForDisplay($"Assigned {FieldDefinition.ProjectStewardshipArea.GetFieldDefinitionLabelPluralized()} successfully changed for {person.FullNameFirstLast}.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditStewardshipAreas(EditUserStewardshipAreasViewModel viewModel)
        {
            EditUserStewardshipAreasViewData viewData;
            var projectStewardshipAreaType = MultiTenantHelpers.GetProjectStewardshipAreaType().ToEnum;

            switch (projectStewardshipAreaType)
            {
                case ProjectStewardshipAreaTypeEnum.ProjectStewardingOrganizations:
                    var allOrganizations = HttpRequestStorage.DatabaseEntities.Organizations.ToList().Where(x=>x.CanStewardProjects()).ToList();
                    viewData = new EditUserStewardshipAreasViewData(CurrentPerson, allOrganizations, false);
                    break;
                case ProjectStewardshipAreaTypeEnum.TaxonomyBranches:
                    var allTaxonomyBranches = HttpRequestStorage.DatabaseEntities.TaxonomyBranches.ToList();
                    viewData = new EditUserStewardshipAreasViewData(CurrentPerson, allTaxonomyBranches, false);
                    break;
                case ProjectStewardshipAreaTypeEnum.Regions:
                    var allRegions = HttpRequestStorage.DatabaseEntities.DNRUplandRegions.ToList();
                    viewData = new EditUserStewardshipAreasViewData(CurrentPerson, allRegions, false);
                    break;
                default:
                    throw new InvalidOperationException(
                        "The Stewardship Area editor should only be allowed for tenants with a Project Stewardship Area Type");
            }

            return RazorPartialView<EditUserStewardshipAreas, EditUserStewardshipAreasViewData, EditUserStewardshipAreasViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [ContactCreateFeature]
        public PartialViewResult AddContact()
        {
            var viewModel = new EditContactViewModel();
            return ViewAddContact(viewModel, null);
        }

        [HttpPost]
        [ContactCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult AddContact(EditContactViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewAddContact(viewModel, null);
            }

            var person = new Person(viewModel.FirstName, 
                                         DateTime.Now, 
                                         true, 
                                         false)
                { LastName = viewModel.LastName, PersonAddress = viewModel.Address, Email = viewModel.Email, Phone = viewModel.Phone, OrganizationID = viewModel.OrganizationID, AddedByPersonID = CurrentPerson.PersonID};
            person.PersonRoles.Add(new PersonRole(person, Role.Unassigned));
            HttpRequestStorage.DatabaseEntities.People.Add(person);

            EditContactViewModel.SetAuthenticatorsForGivenEmailAddress(viewModel, person);

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay($"Successfully added {person.GetFullNameFirstLastAsUrl()}");
            return new ModalDialogFormJsonResult();
        }


        private PartialViewResult ViewAddContact(EditContactViewModel viewModel, Person person)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.OrderBy(x=>x.OrganizationName)
                .ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture),
                    x => x.DisplayName.ToString(CultureInfo.InvariantCulture), "No Organization");
            bool fullUpUser = person != null && person.IsFullUser();

            var viewData = new EditContactViewData(organizations, fullUpUser);
            return RazorPartialView<EditContact, EditContactViewData, EditContactViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [UserEditBasicsFeature]
        public ActionResult EditContact(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var viewModel = new EditContactViewModel(person);
            return ViewAddContact(viewModel, person);
        }

        [HttpPost]
        [UserEditBasicsFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditContact(PersonPrimaryKey personPrimaryKey, EditContactViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewAddContact(viewModel, person);
            }

            viewModel.UpdateModel(person);

            SetMessageForDisplay($"Successfully updated {person.GetFullNameFirstLastAsUrl()}");

            return new ModalDialogFormJsonResult();
        }

        [UserViewFeature]
        public ExcelResult UserAgreementsExcelDownload(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var agreements = GetAgreementsByPerson(person);
            var workbookTitle = $"{FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()} for {person.FullNameFirstLast}";
            return AgreementController.AgreementsExcelDownloadImpl(agreements, workbookTitle);
        }

        private static List<Agreement> GetAgreementsByPerson(Person person)
        {
            var agreementContacts = person.AgreementPeople.Select(a => a.AgreementID).Distinct().ToList();
            var agreements = HttpRequestStorage.DatabaseEntities.Agreements
                .Where(a => agreementContacts.Contains(a.AgreementID)).OrderBy(a => a.AgreementNumber).ToList();
            return agreements;
        }

        [UserViewFeature]
        public GridJsonNetJObjectResult<Agreement> UserAgreementsGridJsonData(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var agreements = GetAgreementsByPerson(person);
            var gridSpec = new AgreementGridSpec(CurrentPerson, agreements.Any(x => x.AgreementFileResourceID.HasValue), false, false)
            {
                CustomExcelDownloadUrl = SitkaRoute<UserController>.BuildUrlFromExpression(tc => tc.UserAgreementsExcelDownload(personPrimaryKey))
            };
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Agreement>(agreements, gridSpec);
            return gridJsonNetJObjectResult;
        }


        [UserViewFeature]
        public GridJsonNetJObjectResult<InteractionEvent> UserInteractionEventsGridJsonData(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var interactionEvents =
                person.InteractionEventContacts.Where(ie => ie.PersonID == person.PersonID).Select(ie => ie.InteractionEvent).ToList();
            var gridSpec = new InteractionEventGridSpec(CurrentPerson, person);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<InteractionEvent>(interactionEvents, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}
