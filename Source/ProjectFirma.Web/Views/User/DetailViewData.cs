/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Linq;
using System.Web;
using LtInfo.Common.AgGridWrappers;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.Agreement;
using ProjectFirma.Web.Views.InteractionEvent;
using ProjectFirma.Web.Views.Project;

namespace ProjectFirma.Web.Views.User
{
    public class DetailViewData : FirmaViewData
    {
        public Person Person { get; }
        public string EditPersonOrganizationPrimaryContactUrl { get; }
        public string Index { get; }

        public bool UserHasPersonViewPermissions { get; }
        public bool UserHasEditBasicsPermission { get; }
        public bool UserHasViewEverythingPermissions { get; }
        public bool UserCanViewPeople { get; }
        public bool IsViewingSelf { get; }
        public ProjectInfoForUserDetailGridSpec BasicProjectInfoGridSpec { get; }
        public string BasicProjectInfoGridName { get; }
        public string BasicProjectInfoGridDataUrl { get; }
        public UserNotificationGridSpec UserNotificationGridSpec { get; }
        public string UserNotificationGridName { get; }
        public string UserNotificationGridDataUrl { get; }
        public string ActivateInactivateUrl { get; }

        public string DeletePersonUrl { get; }
        public bool UserCanDeletePerson { get; }

        public bool TenantHasStewardshipAreas { get; }
        public bool UserHasAdminPermission { get; }
        public bool PersonIsMereContact { get; }
        public string EditContactUrl { get; }
        public string ProjectsForWhichUserIsAContactGridTitle { get; }
        public string AgreementsForWhichUserIsAContactGridTitle { get; }

        public InteractionEventGridSpec UserInteractionEventsGridSpec { get; }
        public string UserInteractionEventsGridName { get; }
        public string UserInteractionEventsGridDataUrl { get; }
        public string InteractionEventsForWhichUserIsAContactGridTitle { get; }

        public AgreementGridSpec UserAgreementsGridSpec  { get; }
        public string UserAgreementsGridName  { get; }
        public string UserAgreementsGridDataUrl  { get; }

        public string AuthenticatorsDisplayString  { get; }
        public HtmlString EditRolesLink  { get; }

        public DetailViewData(Person currentPerson,
            Person personToView,
            ProjectInfoForUserDetailGridSpec basicProjectInfoGridSpec,
            string basicProjectInfoGridName,
            string basicProjectInfoGridDataUrl,
            UserNotificationGridSpec userNotificationGridSpec,
            string userNotificationGridName,
            string userNotificationGridDataUrl,
            string activateInactivateUrl,
            bool atLeastOneAgreementHasFile)
            : base(currentPerson)
        {
            Person = personToView;
            PersonIsMereContact = !personToView.IsFullUser();
            PageTitle = personToView.FullNameFirstLast + (!personToView.IsActive ? " (inactive)" : string.Empty);
            EntityName = "User";
            //TODO: This gets pulled up to root
            EditPersonOrganizationPrimaryContactUrl = SitkaRoute<PersonOrganizationController>.BuildUrlFromExpression(c => c.EditPersonOrganizationPrimaryContacts(personToView));
            Index = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index((int)IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsersAndContacts));

            UserHasPersonViewPermissions = new UserViewFeature().HasPermission(currentPerson, personToView).HasPermission;
            UserHasEditBasicsPermission = new UserEditBasicsFeature().HasPermission(currentPerson,personToView).HasPermission;
            UserCanViewPeople = new UserAndContactIndexViewFeature().HasPermissionByPerson(currentPerson);
            UserHasViewEverythingPermissions = new FirmaAdminFeature().HasPermissionByPerson(currentPerson);
            IsViewingSelf = currentPerson != null && currentPerson.PersonID == personToView.PersonID;
            UserHasAdminPermission = new UserEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            UserCanDeletePerson = !personToView.IsFullUser() && new PersonDeleteFeature().HasPermission(currentPerson, personToView).HasPermission;
            DeletePersonUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Delete(personToView));


            EditRolesLink = UserHasAdminPermission
                ? ModalDialogFormHelper.MakeEditIconLink(SitkaRoute<UserController>.BuildUrlFromExpression(c => c.EditRoles(personToView)),
                    $"Edit Roles for User - {personToView.FullNameFirstLast}",
                    true)
                : new HtmlString(string.Empty);
            AuthenticatorsDisplayString = string.Join(", ", Person.PersonAllowedAuthenticators.OrderBy(x => x.Authenticator.AuthenticatorFullName).Select(x =>$"{x.Authenticator.AuthenticatorFullName} ({x.Authenticator.AuthenticatorName})"));

            BasicProjectInfoGridSpec = basicProjectInfoGridSpec;
            BasicProjectInfoGridName = basicProjectInfoGridName;
            BasicProjectInfoGridDataUrl = basicProjectInfoGridDataUrl;

            UserNotificationGridSpec = userNotificationGridSpec;
            UserNotificationGridName = userNotificationGridName;
            UserNotificationGridDataUrl = userNotificationGridDataUrl;
            ActivateInactivateUrl = activateInactivateUrl;

            TenantHasStewardshipAreas = MultiTenantHelpers.GetProjectStewardshipAreaType() != null;
            EditContactUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.EditContact(personToView));

            ProjectsForWhichUserIsAContactGridTitle = personToView.IsFullUser()
                ? $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} for which {Person.FullNameFirstLast} is an {Models.FieldDefinition.OrganizationPrimaryContact.GetFieldDefinitionLabel()}"
                : $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} for which {Person.FullNameFirstLast} is a {Models.FieldDefinition.Contact.GetFieldDefinitionLabel()}";

            AgreementsForWhichUserIsAContactGridTitle = personToView.IsFullUser()
                ? $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()} for which {Person.FullNameFirstLast} is an {Models.FieldDefinition.OrganizationPrimaryContact.GetFieldDefinitionLabel()}"
                : $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()} for which {Person.FullNameFirstLast} is a {Models.FieldDefinition.Contact.GetFieldDefinitionLabel()}";

            UserAgreementsGridSpec = new AgreementGridSpec(CurrentPerson, atLeastOneAgreementHasFile, false, false)
            {
                CustomExcelDownloadUrl = SitkaRoute<UserController>.BuildUrlFromExpression(tc => tc.UserAgreementsExcelDownload(personToView.PrimaryKey))
            };
            UserAgreementsGridName = "userAgreementsFromUserGrid";
            UserAgreementsGridDataUrl =
                SitkaRoute<UserController>.BuildUrlFromExpression(
                    tc => tc.UserAgreementsGridJsonData(personToView));

            InteractionEventsForWhichUserIsAContactGridTitle = $"{Models.FieldDefinition.InteractionEvent.GetFieldDefinitionLabelPluralized()} for which {Person.FullNameFirstLast} is a {Models.FieldDefinition.Contact.GetFieldDefinitionLabel()}";
            UserInteractionEventsGridSpec = new InteractionEventGridSpec(CurrentPerson, personToView);
            UserInteractionEventsGridName = "userInteractionEventsFromUserGrid";
            UserInteractionEventsGridDataUrl =
                SitkaRoute<UserController>.BuildUrlFromExpression(x => x.UserInteractionEventsGridJsonData(personToView));

        }

        
    }
}
