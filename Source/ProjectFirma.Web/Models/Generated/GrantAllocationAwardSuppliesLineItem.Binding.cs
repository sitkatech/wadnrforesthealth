//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardSuppliesLineItem]
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
    // Table [dbo].[GrantAllocationAwardSuppliesLineItem] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationAwardSuppliesLineItem]")]
    public partial class GrantAllocationAwardSuppliesLineItem : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationAwardSuppliesLineItem()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardSuppliesLineItem(int grantAllocationAwardSuppliesLineItemID, int grantAllocationAwardID, string grantAllocationAwardSuppliesLineItemDescription, string grantAllocationAwardSuppliesLineItemTarOrMonth, DateTime grantAllocationAwardSuppliesLineItemDate, decimal grantAllocationAwardSuppliesLineItemAmount, string grantAllocationAwardSuppliesLineItemNotes) : this()
        {
            this.GrantAllocationAwardSuppliesLineItemID = grantAllocationAwardSuppliesLineItemID;
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.GrantAllocationAwardSuppliesLineItemDescription = grantAllocationAwardSuppliesLineItemDescription;
            this.GrantAllocationAwardSuppliesLineItemTarOrMonth = grantAllocationAwardSuppliesLineItemTarOrMonth;
            this.GrantAllocationAwardSuppliesLineItemDate = grantAllocationAwardSuppliesLineItemDate;
            this.GrantAllocationAwardSuppliesLineItemAmount = grantAllocationAwardSuppliesLineItemAmount;
            this.GrantAllocationAwardSuppliesLineItemNotes = grantAllocationAwardSuppliesLineItemNotes;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardSuppliesLineItem(int grantAllocationAwardID, string grantAllocationAwardSuppliesLineItemDescription, string grantAllocationAwardSuppliesLineItemTarOrMonth, DateTime grantAllocationAwardSuppliesLineItemDate, decimal grantAllocationAwardSuppliesLineItemAmount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardSuppliesLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.GrantAllocationAwardSuppliesLineItemDescription = grantAllocationAwardSuppliesLineItemDescription;
            this.GrantAllocationAwardSuppliesLineItemTarOrMonth = grantAllocationAwardSuppliesLineItemTarOrMonth;
            this.GrantAllocationAwardSuppliesLineItemDate = grantAllocationAwardSuppliesLineItemDate;
            this.GrantAllocationAwardSuppliesLineItemAmount = grantAllocationAwardSuppliesLineItemAmount;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationAwardSuppliesLineItem(GrantAllocationAward grantAllocationAward, string grantAllocationAwardSuppliesLineItemDescription, string grantAllocationAwardSuppliesLineItemTarOrMonth, DateTime grantAllocationAwardSuppliesLineItemDate, decimal grantAllocationAwardSuppliesLineItemAmount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardSuppliesLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID;
            this.GrantAllocationAward = grantAllocationAward;
            grantAllocationAward.GrantAllocationAwardSuppliesLineItems.Add(this);
            this.GrantAllocationAwardSuppliesLineItemDescription = grantAllocationAwardSuppliesLineItemDescription;
            this.GrantAllocationAwardSuppliesLineItemTarOrMonth = grantAllocationAwardSuppliesLineItemTarOrMonth;
            this.GrantAllocationAwardSuppliesLineItemDate = grantAllocationAwardSuppliesLineItemDate;
            this.GrantAllocationAwardSuppliesLineItemAmount = grantAllocationAwardSuppliesLineItemAmount;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationAwardSuppliesLineItem CreateNewBlank(GrantAllocationAward grantAllocationAward)
        {
            return new GrantAllocationAwardSuppliesLineItem(grantAllocationAward, default(string), default(string), default(DateTime), default(decimal));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationAwardSuppliesLineItem).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationAwardSuppliesLineItems.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationAwardSuppliesLineItemID { get; set; }
        public int GrantAllocationAwardID { get; set; }
        public string GrantAllocationAwardSuppliesLineItemDescription { get; set; }
        public string GrantAllocationAwardSuppliesLineItemTarOrMonth { get; set; }
        public DateTime GrantAllocationAwardSuppliesLineItemDate { get; set; }
        public decimal GrantAllocationAwardSuppliesLineItemAmount { get; set; }
        public string GrantAllocationAwardSuppliesLineItemNotes { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationAwardSuppliesLineItemID; } set { GrantAllocationAwardSuppliesLineItemID = value; } }

        public virtual GrantAllocationAward GrantAllocationAward { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationAwardSuppliesLineItemDescription = 255;
            public const int GrantAllocationAwardSuppliesLineItemTarOrMonth = 255;
            public const int GrantAllocationAwardSuppliesLineItemNotes = 2000;
        }
    }
}