/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Web;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.Agreement;
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
        public bool IsViewingSelf { get; }
        public ProjectInfoForUserDetailGridSpec BasicProjectInfoGridSpec { get; }
        public string BasicProjectInfoGridName { get; }
        public string BasicProjectInfoGridDataUrl { get; }
        public UserNotificationGridSpec UserNotificationGridSpec { get; }
        public string UserNotificationGridName { get; }
        public string UserNotificationGridDataUrl { get; }
        public string ActivateInactivateUrl { get; }
        public bool TenantHasStewardshipAreas { get; }
        public bool UserHasAdminPermission { get; }
        public bool PersonIsMereContact { get; }
        public string EditContactUrl { get; }
        public string ProjectsForWhichUserIsAContactGridTitle { get; }
        public string AgreementsForWhichUserIsAContactGridTitle { get; }

        public readonly AgreementGridSpec UserAgreementsGridSpec;
        public readonly string UserAgreementsGridName;
        public readonly string UserAgreementsGridDataUrl;

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
            PersonIsMereContact = string.IsNullOrWhiteSpace(personToView.PersonUniqueIdentifier);
            PageTitle = personToView.FullNameFirstLast + (!personToView.IsActive ? " (inactive)" : string.Empty);
            EntityName = "User";
            //TODO: This gets pulled up to root
            EditPersonOrganizationPrimaryContactUrl = SitkaRoute<PersonOrganizationController>.BuildUrlFromExpression(c => c.EditPersonOrganizationPrimaryContacts(personToView));
            Index = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index());

            UserHasPersonViewPermissions = new UserViewFeature().HasPermission(currentPerson, personToView).HasPermission;
            UserHasEditBasicsPermission = new UserEditBasicsFeature().HasPermission(currentPerson,personToView).HasPermission;
            UserHasViewEverythingPermissions = new FirmaAdminFeature().HasPermissionByPerson(currentPerson);
            IsViewingSelf = currentPerson != null && currentPerson.PersonID == personToView.PersonID;
            UserHasAdminPermission = new UserEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            EditRolesLink = UserHasAdminPermission
                ? ModalDialogFormHelper.MakeEditIconLink(SitkaRoute<UserController>.BuildUrlFromExpression(c => c.EditRoles(personToView)),
                    $"Edit Roles for User - {personToView.FullNameFirstLast}",
                    true)
                : new HtmlString(string.Empty);

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
                ? $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} for which {Person.FullNameFirstLast} is a {Models.FieldDefinition.OrganizationPrimaryContact.GetFieldDefinitionLabel()}"
                : $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} for which {Person.FullNameFirstLast} is a {Models.FieldDefinition.Contact.GetFieldDefinitionLabel()}";

            AgreementsForWhichUserIsAContactGridTitle = personToView.IsFullUser()
                ? $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()} for which {Person.FullNameFirstLast} is a {Models.FieldDefinition.OrganizationPrimaryContact.GetFieldDefinitionLabel()}"
                : $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()} for which {Person.FullNameFirstLast} is a {Models.FieldDefinition.Contact.GetFieldDefinitionLabel()}";

            UserAgreementsGridSpec = new AgreementGridSpec(CurrentPerson, atLeastOneAgreementHasFile, false, false)
            {
                CustomExcelDownloadUrl = SitkaRoute<UserController>.BuildUrlFromExpression(tc => tc.UserAgreementsExcelDownload(personToView.PrimaryKey))
            };
            UserAgreementsGridName = "userAgreementsFromUserGrid";
            UserAgreementsGridDataUrl =
                SitkaRoute<UserController>.BuildUrlFromExpression(
                    tc => tc.UserAgreementsGridJsonData(personToView));
        }

        public readonly HtmlString EditRolesLink;
    }
}
