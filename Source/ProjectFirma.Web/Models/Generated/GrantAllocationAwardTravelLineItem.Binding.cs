//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardTravelLineItem]
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
    // Table [dbo].[GrantAllocationAwardTravelLineItem] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationAwardTravelLineItem]")]
    public partial class GrantAllocationAwardTravelLineItem : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationAwardTravelLineItem()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardTravelLineItem(int grantAllocationAwardTravelLineItemID, int grantAllocationAwardID, string grantAllocationAwardTravelLineItemTarOrMonth, DateTime grantAllocationAwardTravelLineItemDate, int grantAllocationAwardTravelLineItemTypeID, int? grantAllocationAwardTravelLineItemMiles, decimal? grantAllocationAwardTravelLineItemMileageRate, decimal? grantAllocationAwardTravelLineItemAmount, string grantAllocationAwardTravelLineItemNotes, int? personID) : this()
        {
            this.GrantAllocationAwardTravelLineItemID = grantAllocationAwardTravelLineItemID;
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.GrantAllocationAwardTravelLineItemTarOrMonth = grantAllocationAwardTravelLineItemTarOrMonth;
            this.GrantAllocationAwardTravelLineItemDate = grantAllocationAwardTravelLineItemDate;
            this.GrantAllocationAwardTravelLineItemTypeID = grantAllocationAwardTravelLineItemTypeID;
            this.GrantAllocationAwardTravelLineItemMiles = grantAllocationAwardTravelLineItemMiles;
            this.GrantAllocationAwardTravelLineItemMileageRate = grantAllocationAwardTravelLineItemMileageRate;
            this.GrantAllocationAwardTravelLineItemAmount = grantAllocationAwardTravelLineItemAmount;
            this.GrantAllocationAwardTravelLineItemNotes = grantAllocationAwardTravelLineItemNotes;
            this.PersonID = personID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardTravelLineItem(int grantAllocationAwardID, string grantAllocationAwardTravelLineItemTarOrMonth, DateTime grantAllocationAwardTravelLineItemDate, int grantAllocationAwardTravelLineItemTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardTravelLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.GrantAllocationAwardTravelLineItemTarOrMonth = grantAllocationAwardTravelLineItemTarOrMonth;
            this.GrantAllocationAwardTravelLineItemDate = grantAllocationAwardTravelLineItemDate;
            this.GrantAllocationAwardTravelLineItemTypeID = grantAllocationAwardTravelLineItemTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationAwardTravelLineItem(GrantAllocationAward grantAllocationAward, string grantAllocationAwardTravelLineItemTarOrMonth, DateTime grantAllocationAwardTravelLineItemDate, GrantAllocationAwardTravelLineItemType grantAllocationAwardTravelLineItemType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardTravelLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID;
            this.GrantAllocationAward = grantAllocationAward;
            grantAllocationAward.GrantAllocationAwardTravelLineItems.Add(this);
            this.GrantAllocationAwardTravelLineItemTarOrMonth = grantAllocationAwardTravelLineItemTarOrMonth;
            this.GrantAllocationAwardTravelLineItemDate = grantAllocationAwardTravelLineItemDate;
            this.GrantAllocationAwardTravelLineItemTypeID = grantAllocationAwardTravelLineItemType.GrantAllocationAwardTravelLineItemTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationAwardTravelLineItem CreateNewBlank(GrantAllocationAward grantAllocationAward, GrantAllocationAwardTravelLineItemType grantAllocationAwardTravelLineItemType)
        {
            return new GrantAllocationAwardTravelLineItem(grantAllocationAward, default(string), default(DateTime), grantAllocationAwardTravelLineItemType);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationAwardTravelLineItem).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationAwardTravelLineItems.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationAwardTravelLineItemID { get; set; }
        public int GrantAllocationAwardID { get; set; }
        public string GrantAllocationAwardTravelLineItemTarOrMonth { get; set; }
        public DateTime GrantAllocationAwardTravelLineItemDate { get; set; }
        public int GrantAllocationAwardTravelLineItemTypeID { get; set; }
        public int? GrantAllocationAwardTravelLineItemMiles { get; set; }
        public decimal? GrantAllocationAwardTravelLineItemMileageRate { get; set; }
        public decimal? GrantAllocationAwardTravelLineItemAmount { get; set; }
        public string GrantAllocationAwardTravelLineItemNotes { get; set; }
        public int? PersonID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationAwardTravelLineItemID; } set { GrantAllocationAwardTravelLineItemID = value; } }

        public virtual GrantAllocationAward GrantAllocationAward { get; set; }
        public GrantAllocationAwardTravelLineItemType GrantAllocationAwardTravelLineItemType { get { return GrantAllocationAwardTravelLineItemType.AllLookupDictionary[GrantAllocationAwardTravelLineItemTypeID]; } }
        public virtual Person Person { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationAwardTravelLineItemTarOrMonth = 255;
            public const int GrantAllocationAwardTravelLineItemNotes = 2000;
        }
    }
}