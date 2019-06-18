//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandownerCostShareLineItemStatus]
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
    // Table [dbo].[LandownerCostShareLineItemStatus] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[LandownerCostShareLineItemStatus]")]
    public partial class LandownerCostShareLineItemStatus : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected LandownerCostShareLineItemStatus()
        {
            this.GrantAllocationAwardLandownerCostShareLineItems = new HashSet<GrantAllocationAwardLandownerCostShareLineItem>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public LandownerCostShareLineItemStatus(int landownerCostShareLineItemStatusID, string landownerCostShareLineItemStatusName, string landownerCostShareLineItemStatusDisplayName) : this()
        {
            this.LandownerCostShareLineItemStatusID = landownerCostShareLineItemStatusID;
            this.LandownerCostShareLineItemStatusName = landownerCostShareLineItemStatusName;
            this.LandownerCostShareLineItemStatusDisplayName = landownerCostShareLineItemStatusDisplayName;
        }



        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static LandownerCostShareLineItemStatus CreateNewBlank()
        {
            return new LandownerCostShareLineItemStatus();
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantAllocationAwardLandownerCostShareLineItems.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(LandownerCostShareLineItemStatus).Name, typeof(GrantAllocationAwardLandownerCostShareLineItem).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.LandownerCostShareLineItemStatuses.Remove(this);
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

            foreach(var x in GrantAllocationAwardLandownerCostShareLineItems.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int LandownerCostShareLineItemStatusID { get; set; }
        public string LandownerCostShareLineItemStatusName { get; set; }
        public string LandownerCostShareLineItemStatusDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return LandownerCostShareLineItemStatusID; } set { LandownerCostShareLineItemStatusID = value; } }

        public virtual ICollection<GrantAllocationAwardLandownerCostShareLineItem> GrantAllocationAwardLandownerCostShareLineItems { get; set; }

        public static class FieldLengths
        {
            public const int LandownerCostShareLineItemStatusName = 255;
            public const int LandownerCostShareLineItemStatusDisplayName = 255;
        }
    }
}