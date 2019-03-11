//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[InvoiceMatchAmountType]
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
    // Table [dbo].[InvoiceMatchAmountType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[InvoiceMatchAmountType]")]
    public partial class InvoiceMatchAmountType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected InvoiceMatchAmountType()
        {
            this.Invoices = new HashSet<Invoice>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public InvoiceMatchAmountType(int invoiceMatchAmountTypeID, string invoiceMatchAmountTypeName, string invoiceMatchAmountTypeDisplayName) : this()
        {
            this.InvoiceMatchAmountTypeID = invoiceMatchAmountTypeID;
            this.InvoiceMatchAmountTypeName = invoiceMatchAmountTypeName;
            this.InvoiceMatchAmountTypeDisplayName = invoiceMatchAmountTypeDisplayName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public InvoiceMatchAmountType(string invoiceMatchAmountTypeName, string invoiceMatchAmountTypeDisplayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InvoiceMatchAmountTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.InvoiceMatchAmountTypeName = invoiceMatchAmountTypeName;
            this.InvoiceMatchAmountTypeDisplayName = invoiceMatchAmountTypeDisplayName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static InvoiceMatchAmountType CreateNewBlank()
        {
            return new InvoiceMatchAmountType(default(string), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return Invoices.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(InvoiceMatchAmountType).Name, typeof(Invoice).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.InvoiceMatchAmountTypes.Remove(this);
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

            foreach(var x in Invoices.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int InvoiceMatchAmountTypeID { get; set; }
        public string InvoiceMatchAmountTypeName { get; set; }
        public string InvoiceMatchAmountTypeDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return InvoiceMatchAmountTypeID; } set { InvoiceMatchAmountTypeID = value; } }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public static class FieldLengths
        {
            public const int InvoiceMatchAmountTypeName = 50;
            public const int InvoiceMatchAmountTypeDisplayName = 50;
        }
    }
}