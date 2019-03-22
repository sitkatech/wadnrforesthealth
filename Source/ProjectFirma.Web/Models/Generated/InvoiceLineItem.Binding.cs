//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[InvoiceLineItem]
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
    // Table [dbo].[InvoiceLineItem] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[InvoiceLineItem]")]
    public partial class InvoiceLineItem : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected InvoiceLineItem()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public InvoiceLineItem(int invoiceLineItemID, int invoiceID, int grantAllocationID, int costTypeID, decimal invoiceLineItemAmount, string invoiceLineItemNote) : this()
        {
            this.InvoiceLineItemID = invoiceLineItemID;
            this.InvoiceID = invoiceID;
            this.GrantAllocationID = grantAllocationID;
            this.CostTypeID = costTypeID;
            this.InvoiceLineItemAmount = invoiceLineItemAmount;
            this.InvoiceLineItemNote = invoiceLineItemNote;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public InvoiceLineItem(int invoiceID, int grantAllocationID, int costTypeID, decimal invoiceLineItemAmount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InvoiceLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.InvoiceID = invoiceID;
            this.GrantAllocationID = grantAllocationID;
            this.CostTypeID = costTypeID;
            this.InvoiceLineItemAmount = invoiceLineItemAmount;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public InvoiceLineItem(Invoice invoice, GrantAllocation grantAllocation, CostType costType, decimal invoiceLineItemAmount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InvoiceLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.InvoiceID = invoice.InvoiceID;
            this.Invoice = invoice;
            invoice.InvoiceLineItems.Add(this);
            this.GrantAllocationID = grantAllocation.GrantAllocationID;
            this.GrantAllocation = grantAllocation;
            grantAllocation.InvoiceLineItems.Add(this);
            this.CostTypeID = costType.CostTypeID;
            this.InvoiceLineItemAmount = invoiceLineItemAmount;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static InvoiceLineItem CreateNewBlank(Invoice invoice, GrantAllocation grantAllocation, CostType costType)
        {
            return new InvoiceLineItem(invoice, grantAllocation, costType, default(decimal));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(InvoiceLineItem).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.InvoiceLineItems.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int InvoiceLineItemID { get; set; }
        public int InvoiceID { get; set; }
        public int GrantAllocationID { get; set; }
        public int CostTypeID { get; set; }
        public decimal InvoiceLineItemAmount { get; set; }
        public string InvoiceLineItemNote { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return InvoiceLineItemID; } set { InvoiceLineItemID = value; } }

        public virtual Invoice Invoice { get; set; }
        public virtual GrantAllocation GrantAllocation { get; set; }
        public CostType CostType { get { return CostType.AllLookupDictionary[CostTypeID]; } }

        public static class FieldLengths
        {
            public const int InvoiceLineItemNote = 8000;
        }
    }
}