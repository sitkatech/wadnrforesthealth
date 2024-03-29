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
using CodeFirstStoreFunctions;
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
            this.GrantAllocationAwardContractorInvoices = new HashSet<GrantAllocationAwardContractorInvoice>();
            this.GrantAllocationAwardLandownerCostShareLineItems = new HashSet<GrantAllocationAwardLandownerCostShareLineItem>();
            this.GrantAllocationAwardPersonnelAndBenefitsLineItems = new HashSet<GrantAllocationAwardPersonnelAndBenefitsLineItem>();
            this.GrantAllocationAwardSuppliesLineItems = new HashSet<GrantAllocationAwardSuppliesLineItem>();
            this.GrantAllocationAwardTravelLineItems = new HashSet<GrantAllocationAwardTravelLineItem>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAward(int grantAllocationAwardID, int grantAllocationID, int focusAreaID, string grantAllocationAwardName, DateTime grantAllocationAwardExpirationDate, string contractorInvoiceContractor, int grantAllocationAwardCalendarStartYear) : this()
        {
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.GrantAllocationID = grantAllocationID;
            this.FocusAreaID = focusAreaID;
            this.GrantAllocationAwardName = grantAllocationAwardName;
            this.GrantAllocationAwardExpirationDate = grantAllocationAwardExpirationDate;
            this.ContractorInvoiceContractor = contractorInvoiceContractor;
            this.GrantAllocationAwardCalendarStartYear = grantAllocationAwardCalendarStartYear;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAward(int grantAllocationID, int focusAreaID, string grantAllocationAwardName, DateTime grantAllocationAwardExpirationDate, int grantAllocationAwardCalendarStartYear) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationID = grantAllocationID;
            this.FocusAreaID = focusAreaID;
            this.GrantAllocationAwardName = grantAllocationAwardName;
            this.GrantAllocationAwardExpirationDate = grantAllocationAwardExpirationDate;
            this.GrantAllocationAwardCalendarStartYear = grantAllocationAwardCalendarStartYear;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationAward(GrantAllocation grantAllocation, FocusArea focusArea, string grantAllocationAwardName, DateTime grantAllocationAwardExpirationDate, int grantAllocationAwardCalendarStartYear) : this()
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
            this.GrantAllocationAwardExpirationDate = grantAllocationAwardExpirationDate;
            this.GrantAllocationAwardCalendarStartYear = grantAllocationAwardCalendarStartYear;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationAward CreateNewBlank(GrantAllocation grantAllocation, FocusArea focusArea)
        {
            return new GrantAllocationAward(grantAllocation, focusArea, default(string), default(DateTime), default(int));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantAllocationAwardContractorInvoices.Any() || GrantAllocationAwardLandownerCostShareLineItems.Any() || GrantAllocationAwardPersonnelAndBenefitsLineItems.Any() || GrantAllocationAwardSuppliesLineItems.Any() || GrantAllocationAwardTravelLineItems.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(GrantAllocationAwardContractorInvoices.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationAwardContractorInvoice).Name);
            }

            if(GrantAllocationAwardLandownerCostShareLineItems.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationAwardLandownerCostShareLineItem).Name);
            }

            if(GrantAllocationAwardPersonnelAndBenefitsLineItems.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationAwardPersonnelAndBenefitsLineItem).Name);
            }

            if(GrantAllocationAwardSuppliesLineItems.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationAwardSuppliesLineItem).Name);
            }

            if(GrantAllocationAwardTravelLineItems.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationAwardTravelLineItem).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationAward).Name, typeof(GrantAllocationAwardContractorInvoice).Name, typeof(GrantAllocationAwardLandownerCostShareLineItem).Name, typeof(GrantAllocationAwardPersonnelAndBenefitsLineItem).Name, typeof(GrantAllocationAwardSuppliesLineItem).Name, typeof(GrantAllocationAwardTravelLineItem).Name};


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
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in GrantAllocationAwardContractorInvoices.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationAwardLandownerCostShareLineItems.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationAwardPersonnelAndBenefitsLineItems.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationAwardSuppliesLineItems.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationAwardTravelLineItems.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantAllocationAwardID { get; set; }
        public int GrantAllocationID { get; set; }
        public int FocusAreaID { get; set; }
        public string GrantAllocationAwardName { get; set; }
        public DateTime GrantAllocationAwardExpirationDate { get; set; }
        public string ContractorInvoiceContractor { get; set; }
        public int GrantAllocationAwardCalendarStartYear { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationAwardID; } set { GrantAllocationAwardID = value; } }

        public virtual ICollection<GrantAllocationAwardContractorInvoice> GrantAllocationAwardContractorInvoices { get; set; }
        public virtual ICollection<GrantAllocationAwardLandownerCostShareLineItem> GrantAllocationAwardLandownerCostShareLineItems { get; set; }
        public virtual ICollection<GrantAllocationAwardPersonnelAndBenefitsLineItem> GrantAllocationAwardPersonnelAndBenefitsLineItems { get; set; }
        public virtual ICollection<GrantAllocationAwardSuppliesLineItem> GrantAllocationAwardSuppliesLineItems { get; set; }
        public virtual ICollection<GrantAllocationAwardTravelLineItem> GrantAllocationAwardTravelLineItems { get; set; }
        public virtual GrantAllocation GrantAllocation { get; set; }
        public virtual FocusArea FocusArea { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationAwardName = 250;
            public const int ContractorInvoiceContractor = 255;
        }
    }
}