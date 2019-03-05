//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Invoice]
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
    // Table [dbo].[Invoice] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Invoice]")]
    public partial class Invoice : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Invoice()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Invoice(int invoiceID, string requestorName, DateTime invoiceDate, string purchaseAuthority, string status, decimal? totalPaymentAmount, int preparedByPersonID) : this()
        {
            this.InvoiceID = invoiceID;
            this.RequestorName = requestorName;
            this.InvoiceDate = invoiceDate;
            this.PurchaseAuthority = purchaseAuthority;
            this.Status = status;
            this.TotalPaymentAmount = totalPaymentAmount;
            this.PreparedByPersonID = preparedByPersonID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Invoice(string requestorName, DateTime invoiceDate, int preparedByPersonID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InvoiceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.RequestorName = requestorName;
            this.InvoiceDate = invoiceDate;
            this.PreparedByPersonID = preparedByPersonID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Invoice(string requestorName, DateTime invoiceDate, Person preparedByPerson) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InvoiceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.RequestorName = requestorName;
            this.InvoiceDate = invoiceDate;
            this.PreparedByPersonID = preparedByPerson.PersonID;
            this.PreparedByPerson = preparedByPerson;
            preparedByPerson.InvoicesWhereYouAreThePreparedByPerson.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Invoice CreateNewBlank(Person preparedByPerson)
        {
            return new Invoice(default(string), default(DateTime), preparedByPerson);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Invoice).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.Invoices.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int InvoiceID { get; set; }
        public string RequestorName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string PurchaseAuthority { get; set; }
        public string Status { get; set; }
        public decimal? TotalPaymentAmount { get; set; }
        public int PreparedByPersonID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return InvoiceID; } set { InvoiceID = value; } }

        public virtual Person PreparedByPerson { get; set; }

        public static class FieldLengths
        {
            public const int RequestorName = 255;
            public const int PurchaseAuthority = 255;
            public const int Status = 30;
        }
    }
}