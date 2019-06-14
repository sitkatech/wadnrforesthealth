//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardTravelLineItemType]
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
    // Table [dbo].[GrantAllocationAwardTravelLineItemType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationAwardTravelLineItemType]")]
    public partial class GrantAllocationAwardTravelLineItemType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationAwardTravelLineItemType()
        {
            this.GrantAllocationAwardTravelLineItems = new HashSet<GrantAllocationAwardTravelLineItem>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardTravelLineItemType(int grantAllocationAwardTravelLineItemTypeID, string grantAllocationAwardTravelLineItemTypeName, string grantAllocationAwardTravelLineItemTypeDisplayName) : this()
        {
            this.GrantAllocationAwardTravelLineItemTypeID = grantAllocationAwardTravelLineItemTypeID;
            this.GrantAllocationAwardTravelLineItemTypeName = grantAllocationAwardTravelLineItemTypeName;
            this.GrantAllocationAwardTravelLineItemTypeDisplayName = grantAllocationAwardTravelLineItemTypeDisplayName;
        }



        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationAwardTravelLineItemType CreateNewBlank()
        {
            return new GrantAllocationAwardTravelLineItemType();
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantAllocationAwardTravelLineItems.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationAwardTravelLineItemType).Name, typeof(GrantAllocationAwardTravelLineItem).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationAwardTravelLineItemTypes.Remove(this);
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

            foreach(var x in GrantAllocationAwardTravelLineItems.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantAllocationAwardTravelLineItemTypeID { get; set; }
        public string GrantAllocationAwardTravelLineItemTypeName { get; set; }
        public string GrantAllocationAwardTravelLineItemTypeDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationAwardTravelLineItemTypeID; } set { GrantAllocationAwardTravelLineItemTypeID = value; } }

        public virtual ICollection<GrantAllocationAwardTravelLineItem> GrantAllocationAwardTravelLineItems { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationAwardTravelLineItemTypeName = 255;
            public const int GrantAllocationAwardTravelLineItemTypeDisplayName = 255;
        }
    }
}