//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardPersonnelAndBenefitsLineItem]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    // Table [dbo].[GrantAllocationAwardPersonnelAndBenefitsLineItem] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationAwardPersonnelAndBenefitsLineItem]")]
    public partial class GrantAllocationAwardPersonnelAndBenefitsLineItem : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationAwardPersonnelAndBenefitsLineItem()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardPersonnelAndBenefitsLineItem(int grantAllocationAwardPersonnelAndBenefitsLineItemID, int grantAllocationAwardID, string grantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth, DateTime grantAllocationAwardPersonnelAndBenefitsLineItemDate, int grantAllocationAwardPersonnelAndBenefitsLineItemTarHours, decimal grantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate, decimal grantAllocationAwardPersonnelAndBenefitsLineItemFringeRate, string grantAllocationAwardPersonnelAndBenefitsLineItemNotes, int? personID) : this()
        {
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemID = grantAllocationAwardPersonnelAndBenefitsLineItemID;
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth = grantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemDate = grantAllocationAwardPersonnelAndBenefitsLineItemDate;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemTarHours = grantAllocationAwardPersonnelAndBenefitsLineItemTarHours;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate = grantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemFringeRate = grantAllocationAwardPersonnelAndBenefitsLineItemFringeRate;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemNotes = grantAllocationAwardPersonnelAndBenefitsLineItemNotes;
            this.PersonID = personID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardPersonnelAndBenefitsLineItem(int grantAllocationAwardID, string grantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth, DateTime grantAllocationAwardPersonnelAndBenefitsLineItemDate, int grantAllocationAwardPersonnelAndBenefitsLineItemTarHours, decimal grantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate, decimal grantAllocationAwardPersonnelAndBenefitsLineItemFringeRate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth = grantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemDate = grantAllocationAwardPersonnelAndBenefitsLineItemDate;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemTarHours = grantAllocationAwardPersonnelAndBenefitsLineItemTarHours;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate = grantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemFringeRate = grantAllocationAwardPersonnelAndBenefitsLineItemFringeRate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationAwardPersonnelAndBenefitsLineItem(GrantAllocationAward grantAllocationAward, string grantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth, DateTime grantAllocationAwardPersonnelAndBenefitsLineItemDate, int grantAllocationAwardPersonnelAndBenefitsLineItemTarHours, decimal grantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate, decimal grantAllocationAwardPersonnelAndBenefitsLineItemFringeRate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID;
            this.GrantAllocationAward = grantAllocationAward;
            grantAllocationAward.GrantAllocationAwardPersonnelAndBenefitsLineItems.Add(this);
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth = grantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemDate = grantAllocationAwardPersonnelAndBenefitsLineItemDate;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemTarHours = grantAllocationAwardPersonnelAndBenefitsLineItemTarHours;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate = grantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate;
            this.GrantAllocationAwardPersonnelAndBenefitsLineItemFringeRate = grantAllocationAwardPersonnelAndBenefitsLineItemFringeRate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationAwardPersonnelAndBenefitsLineItem CreateNewBlank(GrantAllocationAward grantAllocationAward)
        {
            return new GrantAllocationAwardPersonnelAndBenefitsLineItem(grantAllocationAward, default(string), default(DateTime), default(int), default(decimal), default(decimal));
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
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationAwardPersonnelAndBenefitsLineItem).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationAwardPersonnelAndBenefitsLineItems.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationAwardPersonnelAndBenefitsLineItemID { get; set; }
        public int GrantAllocationAwardID { get; set; }
        public string GrantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth { get; set; }
        public DateTime GrantAllocationAwardPersonnelAndBenefitsLineItemDate { get; set; }
        public int GrantAllocationAwardPersonnelAndBenefitsLineItemTarHours { get; set; }
        public decimal GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate { get; set; }
        public decimal GrantAllocationAwardPersonnelAndBenefitsLineItemFringeRate { get; set; }
        public string GrantAllocationAwardPersonnelAndBenefitsLineItemNotes { get; set; }
        public int? PersonID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationAwardPersonnelAndBenefitsLineItemID; } set { GrantAllocationAwardPersonnelAndBenefitsLineItemID = value; } }

        public virtual GrantAllocationAward GrantAllocationAward { get; set; }
        public virtual Person Person { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth = 255;
            public const int GrantAllocationAwardPersonnelAndBenefitsLineItemNotes = 2000;
        }
    }
}