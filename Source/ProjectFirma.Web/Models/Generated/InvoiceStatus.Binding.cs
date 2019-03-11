//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[InvoiceStatus]
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
    // Table [dbo].[InvoiceStatus] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[InvoiceStatus]")]
    public partial class InvoiceStatus : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected InvoiceStatus()
        {
            this.Invoices = new HashSet<Invoice>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public InvoiceStatus(int invoiceStatusID, string invoiceStatusName, string invoiceStatusDisplayName) : this()
        {
            this.InvoiceStatusID = invoiceStatusID;
            this.InvoiceStatusName = invoiceStatusName;
            this.InvoiceStatusDisplayName = invoiceStatusDisplayName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public InvoiceStatus(string invoiceStatusName, string invoiceStatusDisplayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InvoiceStatusID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.InvoiceStatusName = invoiceStatusName;
            this.InvoiceStatusDisplayName = invoiceStatusDisplayName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static InvoiceStatus CreateNewBlank()
        {
            return new InvoiceStatus(default(string), default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(InvoiceStatus).Name, typeof(Invoice).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.InvoiceStatuses.Remove(this);
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
        public int InvoiceStatusID { get; set; }
        public string InvoiceStatusName { get; set; }
        public string InvoiceStatusDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return InvoiceStatusID; } set { InvoiceStatusID = value; } }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public static class FieldLengths
        {
            public const int InvoiceStatusName = 50;
            public const int InvoiceStatusDisplayName = 50;
        }
    }
}