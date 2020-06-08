//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[tmpAgreementContactsImportTemplate]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    // Table [dbo].[tmpAgreementContactsImportTemplate] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[tmpAgreementContactsImportTemplate]")]
    public partial class tmpAgreementContactsImportTemplate : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected tmpAgreementContactsImportTemplate()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public tmpAgreementContactsImportTemplate(int tmpAgreementContactsImportTemplateID, string agreementNumber, string title, string firstName, string lastName, string emailAddress, string organization, string agreementRole) : this()
        {
            this.tmpAgreementContactsImportTemplateID = tmpAgreementContactsImportTemplateID;
            this.AgreementNumber = agreementNumber;
            this.Title = title;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
            this.Organization = organization;
            this.AgreementRole = agreementRole;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public tmpAgreementContactsImportTemplate(string agreementNumber, string title, string firstName, string lastName, string agreementRole) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.tmpAgreementContactsImportTemplateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.AgreementNumber = agreementNumber;
            this.Title = title;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.AgreementRole = agreementRole;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static tmpAgreementContactsImportTemplate CreateNewBlank()
        {
            return new tmpAgreementContactsImportTemplate(default(string), default(string), default(string), default(string), default(string));
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
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(tmpAgreementContactsImportTemplate).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.tmpAgreementContactsImportTemplates.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int tmpAgreementContactsImportTemplateID { get; set; }
        public string AgreementNumber { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Organization { get; set; }
        public string AgreementRole { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return tmpAgreementContactsImportTemplateID; } set { tmpAgreementContactsImportTemplateID = value; } }



        public static class FieldLengths
        {
            public const int AgreementNumber = 50;
            public const int Title = 100;
            public const int FirstName = 50;
            public const int LastName = 50;
            public const int EmailAddress = 1;
            public const int Organization = 100;
            public const int AgreementRole = 50;
        }
    }
}