//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Person]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    // Table [dbo].[Person] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Person]")]
    public partial class Person : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Person()
        {
            this.AgreementPeople = new HashSet<AgreementPerson>();
            this.AuditLogs = new HashSet<AuditLog>();
            this.FileResourcesWhereYouAreTheCreatePerson = new HashSet<FileResource>();
            this.GrantAllocationNotesWhereYouAreTheCreatedByPerson = new HashSet<GrantAllocationNote>();
            this.GrantAllocationNotesWhereYouAreTheLastUpdatedByPerson = new HashSet<GrantAllocationNote>();
            this.GrantAllocationProgramManagers = new HashSet<GrantAllocationProgramManager>();
            this.GrantNotesWhereYouAreTheCreatedByPerson = new HashSet<GrantNote>();
            this.GrantNotesWhereYouAreTheLastUpdatedByPerson = new HashSet<GrantNote>();
            this.GrantNoteInternalsWhereYouAreTheCreatedByPerson = new HashSet<GrantNoteInternal>();
            this.GrantNoteInternalsWhereYouAreTheLastUpdatedByPerson = new HashSet<GrantNoteInternal>();
            this.InvoicesWhereYouAreThePreparedByPerson = new HashSet<Invoice>();
            this.Notifications = new HashSet<Notification>();
            this.OrganizationsWhereYouAreThePrimaryContactPerson = new HashSet<Organization>();
            this.PerformanceMeasureNotesWhereYouAreTheCreatePerson = new HashSet<PerformanceMeasureNote>();
            this.PerformanceMeasureNotesWhereYouAreTheUpdatePerson = new HashSet<PerformanceMeasureNote>();
            this.PeopleWhereYouAreTheAddedByPerson = new HashSet<Person>();
            this.PersonEnvironmentCredentials = new HashSet<PersonEnvironmentCredential>();
            this.PersonStewardOrganizations = new HashSet<PersonStewardOrganization>();
            this.PersonStewardRegions = new HashSet<PersonStewardRegion>();
            this.PersonStewardTaxonomyBranches = new HashSet<PersonStewardTaxonomyBranch>();
            this.ProjectsWhereYouAreThePrimaryContactPerson = new HashSet<Project>();
            this.ProjectsWhereYouAreTheProposingPerson = new HashSet<Project>();
            this.ProjectsWhereYouAreTheReviewedByPerson = new HashSet<Project>();
            this.ProjectInternalNotesWhereYouAreTheCreatePerson = new HashSet<ProjectInternalNote>();
            this.ProjectInternalNotesWhereYouAreTheUpdatePerson = new HashSet<ProjectInternalNote>();
            this.ProjectLocationStagings = new HashSet<ProjectLocationStaging>();
            this.ProjectLocationStagingUpdates = new HashSet<ProjectLocationStagingUpdate>();
            this.ProjectNotesWhereYouAreTheCreatePerson = new HashSet<ProjectNote>();
            this.ProjectNotesWhereYouAreTheUpdatePerson = new HashSet<ProjectNote>();
            this.ProjectNoteUpdatesWhereYouAreTheCreatePerson = new HashSet<ProjectNoteUpdate>();
            this.ProjectNoteUpdatesWhereYouAreTheUpdatePerson = new HashSet<ProjectNoteUpdate>();
            this.ProjectPeople = new HashSet<ProjectPerson>();
            this.ProjectPersonUpdates = new HashSet<ProjectPersonUpdate>();
            this.ProjectUpdatesWhereYouAreThePrimaryContactPerson = new HashSet<ProjectUpdate>();
            this.ProjectUpdateBatchesWhereYouAreTheLastUpdatePerson = new HashSet<ProjectUpdateBatch>();
            this.ProjectUpdateHistoriesWhereYouAreTheUpdatePerson = new HashSet<ProjectUpdateHistory>();
            this.SupportRequestLogsWhereYouAreTheRequestPerson = new HashSet<SupportRequestLog>();
            this.SystemAttributesWhereYouAreThePrimaryContactPerson = new HashSet<SystemAttribute>();
            this.TreatmentActivitiesWhereYouAreTheTreatmentActivityContact = new HashSet<TreatmentActivity>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Person(int personID, string firstName, string lastName, string email, string phone, int roleID, DateTime createDate, DateTime? updateDate, DateTime? lastActivityDate, bool isActive, int? organizationID, bool receiveSupportEmails, Guid? webServiceAccessToken, string loginName, string middleName, string statewideVendorNumber, string notes, string personAddress, int? addedByPersonID, int allowedAuthenticatorID) : this()
        {
            this.PersonID = personID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            this.RoleID = roleID;
            this.CreateDate = createDate;
            this.UpdateDate = updateDate;
            this.LastActivityDate = lastActivityDate;
            this.IsActive = isActive;
            this.OrganizationID = organizationID;
            this.ReceiveSupportEmails = receiveSupportEmails;
            this.WebServiceAccessToken = webServiceAccessToken;
            this.LoginName = loginName;
            this.MiddleName = middleName;
            this.StatewideVendorNumber = statewideVendorNumber;
            this.Notes = notes;
            this.PersonAddress = personAddress;
            this.AddedByPersonID = addedByPersonID;
            this.AllowedAuthenticatorID = allowedAuthenticatorID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Person(string firstName, string lastName, int roleID, DateTime createDate, bool isActive, bool receiveSupportEmails, int allowedAuthenticatorID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PersonID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FirstName = firstName;
            this.LastName = lastName;
            this.RoleID = roleID;
            this.CreateDate = createDate;
            this.IsActive = isActive;
            this.ReceiveSupportEmails = receiveSupportEmails;
            this.AllowedAuthenticatorID = allowedAuthenticatorID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Person(string firstName, string lastName, Role role, DateTime createDate, bool isActive, bool receiveSupportEmails, Authenticator allowedAuthenticator) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PersonID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FirstName = firstName;
            this.LastName = lastName;
            this.RoleID = role.RoleID;
            this.CreateDate = createDate;
            this.IsActive = isActive;
            this.ReceiveSupportEmails = receiveSupportEmails;
            this.AllowedAuthenticatorID = allowedAuthenticator.AuthenticatorID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Person CreateNewBlank(Role role, Authenticator allowedAuthenticator)
        {
            return new Person(default(string), default(string), role, default(DateTime), default(bool), default(bool), allowedAuthenticator);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return AgreementPeople.Any() || AuditLogs.Any() || FileResourcesWhereYouAreTheCreatePerson.Any() || GrantAllocationNotesWhereYouAreTheCreatedByPerson.Any() || GrantAllocationNotesWhereYouAreTheLastUpdatedByPerson.Any() || GrantAllocationProgramManagers.Any() || GrantNotesWhereYouAreTheCreatedByPerson.Any() || GrantNotesWhereYouAreTheLastUpdatedByPerson.Any() || GrantNoteInternalsWhereYouAreTheCreatedByPerson.Any() || GrantNoteInternalsWhereYouAreTheLastUpdatedByPerson.Any() || InvoicesWhereYouAreThePreparedByPerson.Any() || Notifications.Any() || OrganizationsWhereYouAreThePrimaryContactPerson.Any() || PerformanceMeasureNotesWhereYouAreTheCreatePerson.Any() || PerformanceMeasureNotesWhereYouAreTheUpdatePerson.Any() || PeopleWhereYouAreTheAddedByPerson.Any() || PersonEnvironmentCredentials.Any() || PersonStewardOrganizations.Any() || PersonStewardRegions.Any() || PersonStewardTaxonomyBranches.Any() || ProjectsWhereYouAreThePrimaryContactPerson.Any() || ProjectsWhereYouAreTheProposingPerson.Any() || ProjectsWhereYouAreTheReviewedByPerson.Any() || ProjectInternalNotesWhereYouAreTheCreatePerson.Any() || ProjectInternalNotesWhereYouAreTheUpdatePerson.Any() || ProjectLocationStagings.Any() || ProjectLocationStagingUpdates.Any() || ProjectNotesWhereYouAreTheCreatePerson.Any() || ProjectNotesWhereYouAreTheUpdatePerson.Any() || ProjectNoteUpdatesWhereYouAreTheCreatePerson.Any() || ProjectNoteUpdatesWhereYouAreTheUpdatePerson.Any() || ProjectPeople.Any() || ProjectPersonUpdates.Any() || ProjectUpdatesWhereYouAreThePrimaryContactPerson.Any() || ProjectUpdateBatchesWhereYouAreTheLastUpdatePerson.Any() || ProjectUpdateHistoriesWhereYouAreTheUpdatePerson.Any() || SupportRequestLogsWhereYouAreTheRequestPerson.Any() || SystemAttributesWhereYouAreThePrimaryContactPerson.Any() || TreatmentActivitiesWhereYouAreTheTreatmentActivityContact.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Person).Name, typeof(AgreementPerson).Name, typeof(AuditLog).Name, typeof(FileResource).Name, typeof(GrantAllocationNote).Name, typeof(GrantAllocationProgramManager).Name, typeof(GrantNote).Name, typeof(GrantNoteInternal).Name, typeof(Invoice).Name, typeof(Notification).Name, typeof(Organization).Name, typeof(PerformanceMeasureNote).Name, typeof(PersonEnvironmentCredential).Name, typeof(PersonStewardOrganization).Name, typeof(PersonStewardRegion).Name, typeof(PersonStewardTaxonomyBranch).Name, typeof(Project).Name, typeof(ProjectInternalNote).Name, typeof(ProjectLocationStaging).Name, typeof(ProjectLocationStagingUpdate).Name, typeof(ProjectNote).Name, typeof(ProjectNoteUpdate).Name, typeof(ProjectPerson).Name, typeof(ProjectPersonUpdate).Name, typeof(ProjectUpdate).Name, typeof(ProjectUpdateBatch).Name, typeof(ProjectUpdateHistory).Name, typeof(SupportRequestLog).Name, typeof(SystemAttribute).Name, typeof(TreatmentActivity).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.People.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in AgreementPeople.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in AuditLogs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in FileResourcesWhereYouAreTheCreatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationNotesWhereYouAreTheCreatedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationNotesWhereYouAreTheLastUpdatedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationProgramManagers.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantNotesWhereYouAreTheCreatedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantNotesWhereYouAreTheLastUpdatedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantNoteInternalsWhereYouAreTheCreatedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantNoteInternalsWhereYouAreTheLastUpdatedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in InvoicesWhereYouAreThePreparedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in Notifications.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OrganizationsWhereYouAreThePrimaryContactPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in PerformanceMeasureNotesWhereYouAreTheCreatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in PerformanceMeasureNotesWhereYouAreTheUpdatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in PeopleWhereYouAreTheAddedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in PersonEnvironmentCredentials.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in PersonStewardOrganizations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in PersonStewardRegions.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in PersonStewardTaxonomyBranches.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectsWhereYouAreThePrimaryContactPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectsWhereYouAreTheProposingPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectsWhereYouAreTheReviewedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectInternalNotesWhereYouAreTheCreatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectInternalNotesWhereYouAreTheUpdatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectLocationStagings.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectLocationStagingUpdates.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectNotesWhereYouAreTheCreatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectNotesWhereYouAreTheUpdatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectNoteUpdatesWhereYouAreTheCreatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectNoteUpdatesWhereYouAreTheUpdatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectPeople.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectPersonUpdates.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectUpdatesWhereYouAreThePrimaryContactPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectUpdateBatchesWhereYouAreTheLastUpdatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectUpdateHistoriesWhereYouAreTheUpdatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in SupportRequestLogsWhereYouAreTheRequestPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in SystemAttributesWhereYouAreThePrimaryContactPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentActivitiesWhereYouAreTheTreatmentActivityContact.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public bool IsActive { get; set; }
        public int? OrganizationID { get; set; }
        public bool ReceiveSupportEmails { get; set; }
        public Guid? WebServiceAccessToken { get; set; }
        public string LoginName { get; set; }
        public string MiddleName { get; set; }
        public string StatewideVendorNumber { get; set; }
        public string Notes { get; set; }
        public string PersonAddress { get; set; }
        public int? AddedByPersonID { get; set; }
        public int AllowedAuthenticatorID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PersonID; } set { PersonID = value; } }

        public virtual ICollection<AgreementPerson> AgreementPeople { get; set; }
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<FileResource> FileResourcesWhereYouAreTheCreatePerson { get; set; }
        public virtual ICollection<GrantAllocationNote> GrantAllocationNotesWhereYouAreTheCreatedByPerson { get; set; }
        public virtual ICollection<GrantAllocationNote> GrantAllocationNotesWhereYouAreTheLastUpdatedByPerson { get; set; }
        public virtual ICollection<GrantAllocationProgramManager> GrantAllocationProgramManagers { get; set; }
        public virtual ICollection<GrantNote> GrantNotesWhereYouAreTheCreatedByPerson { get; set; }
        public virtual ICollection<GrantNote> GrantNotesWhereYouAreTheLastUpdatedByPerson { get; set; }
        public virtual ICollection<GrantNoteInternal> GrantNoteInternalsWhereYouAreTheCreatedByPerson { get; set; }
        public virtual ICollection<GrantNoteInternal> GrantNoteInternalsWhereYouAreTheLastUpdatedByPerson { get; set; }
        public virtual ICollection<Invoice> InvoicesWhereYouAreThePreparedByPerson { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Organization> OrganizationsWhereYouAreThePrimaryContactPerson { get; set; }
        public virtual ICollection<PerformanceMeasureNote> PerformanceMeasureNotesWhereYouAreTheCreatePerson { get; set; }
        public virtual ICollection<PerformanceMeasureNote> PerformanceMeasureNotesWhereYouAreTheUpdatePerson { get; set; }
        public virtual ICollection<Person> PeopleWhereYouAreTheAddedByPerson { get; set; }
        public virtual Person AddedByPerson { get; set; }
        public virtual ICollection<PersonEnvironmentCredential> PersonEnvironmentCredentials { get; set; }
        public virtual ICollection<PersonStewardOrganization> PersonStewardOrganizations { get; set; }
        public virtual ICollection<PersonStewardRegion> PersonStewardRegions { get; set; }
        public virtual ICollection<PersonStewardTaxonomyBranch> PersonStewardTaxonomyBranches { get; set; }
        public virtual ICollection<Project> ProjectsWhereYouAreThePrimaryContactPerson { get; set; }
        public virtual ICollection<Project> ProjectsWhereYouAreTheProposingPerson { get; set; }
        public virtual ICollection<Project> ProjectsWhereYouAreTheReviewedByPerson { get; set; }
        public virtual ICollection<ProjectInternalNote> ProjectInternalNotesWhereYouAreTheCreatePerson { get; set; }
        public virtual ICollection<ProjectInternalNote> ProjectInternalNotesWhereYouAreTheUpdatePerson { get; set; }
        public virtual ICollection<ProjectLocationStaging> ProjectLocationStagings { get; set; }
        public virtual ICollection<ProjectLocationStagingUpdate> ProjectLocationStagingUpdates { get; set; }
        public virtual ICollection<ProjectNote> ProjectNotesWhereYouAreTheCreatePerson { get; set; }
        public virtual ICollection<ProjectNote> ProjectNotesWhereYouAreTheUpdatePerson { get; set; }
        public virtual ICollection<ProjectNoteUpdate> ProjectNoteUpdatesWhereYouAreTheCreatePerson { get; set; }
        public virtual ICollection<ProjectNoteUpdate> ProjectNoteUpdatesWhereYouAreTheUpdatePerson { get; set; }
        public virtual ICollection<ProjectPerson> ProjectPeople { get; set; }
        public virtual ICollection<ProjectPersonUpdate> ProjectPersonUpdates { get; set; }
        public virtual ICollection<ProjectUpdate> ProjectUpdatesWhereYouAreThePrimaryContactPerson { get; set; }
        public virtual ICollection<ProjectUpdateBatch> ProjectUpdateBatchesWhereYouAreTheLastUpdatePerson { get; set; }
        public virtual ICollection<ProjectUpdateHistory> ProjectUpdateHistoriesWhereYouAreTheUpdatePerson { get; set; }
        public virtual ICollection<SupportRequestLog> SupportRequestLogsWhereYouAreTheRequestPerson { get; set; }
        public virtual ICollection<SystemAttribute> SystemAttributesWhereYouAreThePrimaryContactPerson { get; set; }
        public virtual ICollection<TreatmentActivity> TreatmentActivitiesWhereYouAreTheTreatmentActivityContact { get; set; }
        public Role Role { get { return Role.AllLookupDictionary[RoleID]; } }
        public virtual Organization Organization { get; set; }
        public Authenticator AllowedAuthenticator { get { return Authenticator.AllLookupDictionary[AllowedAuthenticatorID]; } }

        public static class FieldLengths
        {
            public const int FirstName = 100;
            public const int LastName = 100;
            public const int Email = 255;
            public const int Phone = 30;
            public const int LoginName = 128;
            public const int MiddleName = 100;
            public const int StatewideVendorNumber = 100;
            public const int Notes = 500;
            public const int PersonAddress = 255;
        }
    }
}