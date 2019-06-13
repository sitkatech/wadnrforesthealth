//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAward]
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
    // Table [dbo].[GrantAllocationAward] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationAward]")]
    public partial class GrantAllocationAward : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationAward()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAward(int grantAllocationAwardID, int grantAllocationID, int focusAreaID, string grantAllocationAwardName, decimal grantAllocationAwardAmount, DateTime grantAllocationAwardExpirationDate) : this()
        {
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.GrantAllocationID = grantAllocationID;
            this.FocusAreaID = focusAreaID;
            this.GrantAllocationAwardName = grantAllocationAwardName;
            this.GrantAllocationAwardAmount = grantAllocationAwardAmount;
            this.GrantAllocationAwardExpirationDate = grantAllocationAwardExpirationDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAward(int grantAllocationID, int focusAreaID, string grantAllocationAwardName, decimal grantAllocationAwardAmount, DateTime grantAllocationAwardExpirationDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationID = grantAllocationID;
            this.FocusAreaID = focusAreaID;
            this.GrantAllocationAwardName = grantAllocationAwardName;
            this.GrantAllocationAwardAmount = grantAllocationAwardAmount;
            this.GrantAllocationAwardExpirationDate = grantAllocationAwardExpirationDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationAward(GrantAllocation grantAllocation, FocusArea focusArea, string grantAllocationAwardName, decimal grantAllocationAwardAmount, DateTime grantAllocationAwardExpirationDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationID = grantAllocation.GrantAllocationID;
            this.GrantAllocation = grantAllocation;
            grantAllocation.GrantAllocationAwards.Add(this);
            this.FocusAreaID = focusArea.FocusAreaID;
            this.FocusArea = focusArea;
            focusArea.GrantAllocationAwards.Add(this);
            this.GrantAllocationAwardName = grantAllocationAwardName;
            this.GrantAllocationAwardAmount = grantAllocationAwardAmount;
            this.GrantAllocationAwardExpirationDate = grantAllocationAwardExpirationDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationAward CreateNewBlank(GrantAllocation grantAllocation, FocusArea focusArea)
        {
            return new GrantAllocationAward(grantAllocation, focusArea, default(string), default(decimal), default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationAward).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationAwards.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationAwardID { get; set; }
        public int GrantAllocationID { get; set; }
        public int FocusAreaID { get; set; }
        public string GrantAllocationAwardName { get; set; }
        public decimal GrantAllocationAwardAmount { get; set; }
        public DateTime GrantAllocationAwardExpirationDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationAwardID; } set { GrantAllocationAwardID = value; } }

        public virtual GrantAllocation GrantAllocation { get; set; }
        public virtual FocusArea FocusArea { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationAwardName = 250;
        }
    }
}