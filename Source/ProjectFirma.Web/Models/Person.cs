/*-----------------------------------------------------------------------
<copyright file="Person.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Bibliography;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Models
{
    public partial class Person : IAuditableEntity
    {
        public const int AnonymousPersonID = -999;

        /// <summary>
        /// Needed for Keystone; basically <see cref="HttpRequestStorage.Person" /> is set to this fake
        /// "Anonymous" person when we are not authenticated to not have to handle the null Person case.
        /// Seems like MR and all the other RPs do this so following the pattern
        /// </summary>
        /// <returns></returns>
        public static Person GetAnonymousSitkaUser()
        {
            var anonymousSitkaUser = new Person
            {
                PersonID = AnonymousPersonID,
                //RoleID = Role.Unassigned.RoleID
            };

            anonymousSitkaUser.PersonRoles.Add(new PersonRole(anonymousSitkaUser, Role.Unassigned));
            // as we add new areas, we need to make sure we assign the anonymous user with the unassigned roles for each area
            return anonymousSitkaUser;
        }

        public bool IsAnonymousUser => PersonID == AnonymousPersonID;

        public string FullNameFirstLast 
        {
            get
            {
                if (!string.IsNullOrEmpty(LastName))
                {
                    return $"{FirstName} {LastName}";
                }

                return FirstName;

            }

        } 

        public string FullNameFirstLastAndOrg
        {
            get
            {
                if (Organization != null && !string.IsNullOrEmpty(LastName))
                {
                    return $"{FirstName} {LastName} - {Organization.DisplayNameWithoutAbbreviation}";
                }
                else if (Organization != null)
                {
                    return $"{FirstName} - {Organization.DisplayNameWithoutAbbreviation}";
                }
                else return FullNameFirstLast;
            }
        }

        public string FullNameFirstLastAndOrgShortName
        {
            get
            {
                if (Organization != null)
                {
                    return $"{FullNameFirstLast} ({Organization.OrganizationShortNameIfAvailable})";
                }
                return FullNameFirstLast;
            }
        }

        public string OrganizationOrBlankString
        {
            get
            {
                if (Organization != null)
                {
                    return $"{Organization.OrganizationShortNameIfAvailable}";
                }

                return string.Empty;
            }
        }

        public string FullNameLastFirst
        {
            get
            {
                if (!string.IsNullOrEmpty(LastName))
                {
                    return $"{LastName}, {FirstName}";
                }

                return FirstName;
            }
        }

        /// <summary>
        /// List of Projects for which this Person is the primary contact
        /// </summary>
        public List<Project> GetPrimaryContactProjects(Person currentPerson)
        {
            var projectsWhereYouAreThePrimaryContactPerson = currentPerson.ProjectPeople.Where(pp => 
                                                                                        pp.ProjectPersonRelationshipTypeID == ProjectPersonRelationshipType.PrimaryContact.ProjectPersonRelationshipTypeID)
                                                                                 .Select(pprt => pprt.Project).ToList();

            var isPersonViewingThePrimaryContact = currentPerson.PersonID == PersonID;
            if (isPersonViewingThePrimaryContact)
            {
                
                return projectsWhereYouAreThePrimaryContactPerson.ToList().Where(x => x.ProjectStage != ProjectStage.Cancelled).ToList();
            }
            return projectsWhereYouAreThePrimaryContactPerson.ToList().GetActiveProjectsVisibleToUser(currentPerson).ToList();
        }


        public List<Project> GetPrimaryContactUpdatableProjects(Person person)
        {
            return GetPrimaryContactProjects(person).Where(x => x.IsUpdatableViaProjectUpdateProcess).ToList();
        }

        /// <summary>
        /// List of Organizations for which this Person is the primary contact
        /// </summary>
        public List<Organization> PrimaryContactOrganizations
        {
            get { return OrganizationsWhereYouAreThePrimaryContactPerson.OrderBy(x => x.OrganizationName).ToList(); }
        }

        public string AuditDescriptionString => FullNameFirstLast;

        public Notification GetMostRecentReminder()
        {
            var notifications = Notifications.Where(x => x.NotificationType == NotificationType.ProjectUpdateReminder).ToList();

            if (notifications.Count == 0)
            {
                return null;
            }
            return notifications.OrderByDescending(y => y.NotificationDate).First();
        }

        /// <summary>
        /// All role names of BOTH types used by Keystone not for user display 
        /// </summary>
        public IEnumerable<string> RoleNames
        {
            get
            {
                if (IsAnonymousUser)
                {
                    // the presence of roles switches you from being IsAuthenticated or not
                    return new List<string>();
                }

                var roleNames = this.PersonRoles.Select(x => x.Role.RoleName);
                return roleNames;
            }
        }

        /// <summary>
        /// All role names of BOTH types used by Keystone for user display 
        /// </summary>
        public IEnumerable<string> RoleNamesForDisplay
        {
            get
            {
                if (IsAnonymousUser)
                {
                    // the presence of roles switches you from being IsAuthenticated or not
                    return new List<string>();
                }

                var roleNames = this.PersonRoles.Select(x => x.Role.RoleDisplayName);
                return roleNames;
            }
        }

        public string BaseRoleNameForDisplay
        {
            get
            {
                if (IsAnonymousUser)
                {
                    // the presence of roles switches you from being IsAuthenticated or not
                    return string.Empty;
                }

                var roleName = this.GetUsersBaseRole().RoleDisplayName;
                return roleName;
            }
        }

        public IEnumerable<string> SupplementalRoleNamesForDisplay
        {
            get
            {
                if (IsAnonymousUser)
                {
                    // the presence of roles switches you from being IsAuthenticated or not
                    return new List<string>();
                }

                var roleNames = this.GetUsersSupplementalRoles().Select(x => x.RoleDisplayName);
                return roleNames;
            }
        }


        public bool CanStewardProject(Project project)
        {
            return MultiTenantHelpers.GetProjectStewardshipAreaType()?.CanStewardProject(this, project) ?? true;
        }

        public bool CanProgramEditorManageProject(Project project)
        {
            if (!this.HasRole(Role.CanEditProgram))
            {
                return false;
            }

            if (!this.ProgramPeople.Any())
            {
                //they are not assigned any programs, no permission!
                return false;
            }

            var personsProgramIDs = this.ProgramPeople.Select(x => x.ProgramID);
            var projectsProgramIDs = project.ProjectPrograms.Select(x => x.ProgramID);

            return projectsProgramIDs.Intersect(personsProgramIDs).Any();
        }

        public bool PersonIsProjectOwnerWhoCanStewardProjects
        {
            get
            {
                var canStewardProjectsOrganizationRelationship = MultiTenantHelpers.GetCanStewardProjectsOrganizationRelationship();
                if (MultiTenantHelpers.GetProjectStewardshipAreaType() == ProjectStewardshipAreaType.ProjectStewardingOrganizations)
                {
                    return this.HasRole(Role.ProjectSteward) &&
                           canStewardProjectsOrganizationRelationship != null &&
                           canStewardProjectsOrganizationRelationship.OrganizationTypeRelationshipTypes.Any(
                               x => x.OrganizationTypeID == Organization.OrganizationTypeID);
                }

                return this.HasRole(Role.ProjectSteward);
            }
        }

        public List<HtmlString> GetProjectStewardshipAreaHtmlStringList()
        {
            return MultiTenantHelpers.GetProjectStewardshipAreaType()?.GetProjectStewardshipAreaHtmlStringList(this);
        }

        public bool IsAnonymousOrUnassigned => IsAnonymousUser || this.PersonRoles.All(x => x.Role == Role.Unassigned);

        public bool CanViewProposals => MultiTenantHelpers.ShowApplicationsToThePublic() || !IsAnonymousOrUnassigned;       
        public bool CanViewPendingProjects => new PendingProjectsViewListFeature().HasPermissionByPerson(this);
        public string FullNameFirstLastAndMiddle => $"{FirstName} {MiddleName} {LastName}";

        public bool IsFullUser()
        {
            return PersonAllowedAuthenticators.Any();
        }

        public static Person CreateNewBlank(Role role)
        {
            var person = CreateNewBlank();
            person.PersonRoles.Add( new PersonRole(person, role));
            return person;
        }



    }
}
