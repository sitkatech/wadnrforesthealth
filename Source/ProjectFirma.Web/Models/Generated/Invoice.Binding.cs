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
            this.InvoiceLineItems = new HashSet<InvoiceLineItem>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Invoice(int invoiceID, string invoiceIdentifyingName, string requestorName, DateTime invoiceDate, string purchaseAuthority, decimal? totalPaymentAmount, int preparedByPersonID, int invoiceApprovalStatusID, string invoiceApprovalStatusComment, bool purchaseAuthorityIsLandownerCostShareAgreement, int invoiceMatchAmountTypeID, decimal? matchAmount, int invoiceStatusID, int? invoiceFileResourceID) : this()
        {
            this.InvoiceID = invoiceID;
            this.InvoiceIdentifyingName = invoiceIdentifyingName;
            this.RequestorName = requestorName;
            this.InvoiceDate = invoiceDate;
            this.PurchaseAuthority = purchaseAuthority;
            this.TotalPaymentAmount = totalPaymentAmount;
            this.PreparedByPersonID = preparedByPersonID;
            this.InvoiceApprovalStatusID = invoiceApprovalStatusID;
            this.InvoiceApprovalStatusComment = invoiceApprovalStatusComment;
            this.PurchaseAuthorityIsLandownerCostShareAgreement = purchaseAuthorityIsLandownerCostShareAgreement;
            this.InvoiceMatchAmountTypeID = invoiceMatchAmountTypeID;
            this.MatchAmount = matchAmount;
            this.InvoiceStatusID = invoiceStatusID;
            this.InvoiceFileResourceID = invoiceFileResourceID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Invoice(string requestorName, DateTime invoiceDate, int preparedByPersonID, int invoiceApprovalStatusID, bool purchaseAuthorityIsLandownerCostShareAgreement, int invoiceMatchAmountTypeID, int invoiceStatusID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InvoiceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.RequestorName = requestorName;
            this.InvoiceDate = invoiceDate;
            this.PreparedByPersonID = preparedByPersonID;
            this.InvoiceApprovalStatusID = invoiceApprovalStatusID;
            this.PurchaseAuthorityIsLandownerCostShareAgreement = purchaseAuthorityIsLandownerCostShareAgreement;
            this.InvoiceMatchAmountTypeID = invoiceMatchAmountTypeID;
            this.InvoiceStatusID = invoiceStatusID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Invoice(string requestorName, DateTime invoiceDate, Person preparedByPerson, InvoiceApprovalStatus invoiceApprovalStatus, bool purchaseAuthorityIsLandownerCostShareAgreement, InvoiceMatchAmountType invoiceMatchAmountType, InvoiceStatus invoiceStatus) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InvoiceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.RequestorName = requestorName;
            this.InvoiceDate = invoiceDate;
            this.PreparedByPersonID = preparedByPerson.PersonID;
            this.PreparedByPerson = preparedByPerson;
            preparedByPerson.InvoicesWhereYouAreThePreparedByPerson.Add(this);
            this.InvoiceApprovalStatusID = invoiceApprovalStatus.InvoiceApprovalStatusID;
            this.PurchaseAuthorityIsLandownerCostShareAgreement = purchaseAuthorityIsLandownerCostShareAgreement;
            this.InvoiceMatchAmountTypeID = invoiceMatchAmountType.InvoiceMatchAmountTypeID;
            this.InvoiceStatusID = invoiceStatus.InvoiceStatusID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Invoice CreateNewBlank(Person preparedByPerson, InvoiceApprovalStatus invoiceApprovalStatus, InvoiceMatchAmountType invoiceMatchAmountType, InvoiceStatus invoiceStatus)
        {
            return new Invoice(default(string), default(DateTime), preparedByPerson, invoiceApprovalStatus, default(bool), invoiceMatchAmountType, invoiceStatus);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return InvoiceLineItems.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(InvoiceLineItems.Any())
            {
                dependentObjects.Add(typeof(InvoiceLineItem).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Invoice).Name, typeof(InvoiceLineItem).Name};


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
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in InvoiceLineItems.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int InvoiceID { get; set; }
        public string InvoiceIdentifyingName { get; set; }
        public string RequestorName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string PurchaseAuthority { get; set; }
        public decimal? TotalPaymentAmount { get; set; }
        public int PreparedByPersonID { get; set; }
        public int InvoiceApprovalStatusID { get; set; }
        public string InvoiceApprovalStatusComment { get; set; }
        public bool PurchaseAuthorityIsLandownerCostShareAgreement { get; set; }
        public int InvoiceMatchAmountTypeID { get; set; }
        public decimal? MatchAmount { get; set; }
        public int InvoiceStatusID { get; set; }
        public int? InvoiceFileResourceID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return InvoiceID; } set { InvoiceID = value; } }

        public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; }
        public virtual Person PreparedByPerson { get; set; }
        public InvoiceApprovalStatus InvoiceApprovalStatus { get { return InvoiceApprovalStatus.AllLookupDictionary[InvoiceApprovalStatusID]; } }
        public InvoiceMatchAmountType InvoiceMatchAmountType { get { return InvoiceMatchAmountType.AllLookupDictionary[InvoiceMatchAmountTypeID]; } }
        public InvoiceStatus InvoiceStatus { get { return InvoiceStatus.AllLookupDictionary[InvoiceStatusID]; } }
        public virtual FileResource InvoiceFileResource { get; set; }

        public static class FieldLengths
        {
            public const int InvoiceIdentifyingName = 255;
            public const int RequestorName = 255;
            public const int PurchaseAuthority = 255;
        }
    }
}