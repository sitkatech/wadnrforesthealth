//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[tmpAgreementContact]
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
    // Table [dbo].[tmpAgreementContact] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[tmpAgreementContact]")]
    public partial class tmpAgreementContact : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected tmpAgreementContact()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public tmpAgreementContact(int agreementContactID, string agreementNumber, string title, string firstName, string lastName, string emailAddress, string organization, string agreementRole, string f8, string f9) : this()
        {
            this.AgreementContactID = agreementContactID;
            this.AgreementNumber = agreementNumber;
            this.Title = title;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
            this.Organization = organization;
            this.AgreementRole = agreementRole;
            this.F8 = f8;
            this.F9 = f9;
        }



        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static tmpAgreementContact CreateNewBlank()
        {
            return new tmpAgreementContact();
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return false;
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(tmpAgreementContact).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.tmpAgreementContacts.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int AgreementContactID { get; set; }
        public string AgreementNumber { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Organization { get; set; }
        public string AgreementRole { get; set; }
        public string F8 { get; set; }
        public string F9 { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return AgreementContactID; } set { AgreementContactID = value; } }



        public static class FieldLengths
        {
            public const int AgreementNumber = 255;
            public const int Title = 255;
            public const int FirstName = 255;
            public const int LastName = 255;
            public const int EmailAddress = 255;
            public const int Organization = 255;
            public const int AgreementRole = 255;
            public const int F8 = 255;
            public const int F9 = 255;
        }
    }
}