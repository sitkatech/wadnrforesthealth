//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardLandownerCostShareLineItem]
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
    // Table [dbo].[GrantAllocationAwardLandownerCostShareLineItem] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationAwardLandownerCostShareLineItem]")]
    public partial class GrantAllocationAwardLandownerCostShareLineItem : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationAwardLandownerCostShareLineItem()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardLandownerCostShareLineItem(int grantAllocationAwardLandownerCostShareLineItemID, int? grantAllocationAwardID, int projectID, int landownerCostShareLineItemStatusID, DateTime? grantAllocationAwardLandownerCostShareLineItemStartDate, DateTime? grantAllocationAwardLandownerCostShareLineItemEndDate, decimal grantAllocationAwardLandownerCostShareLineItemFootprintAcres, decimal grantAllocationAwardLandownerCostShareLineItemChippingAcres, decimal grantAllocationAwardLandownerCostShareLineItemPruningAcres, decimal grantAllocationAwardLandownerCostShareLineItemThinningAcres, decimal grantAllocationAwardLandownerCostShareLineItemMasticationAcres, decimal grantAllocationAwardLandownerCostShareLineItemGrazingAcres, decimal grantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres, decimal grantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres, decimal grantAllocationAwardLandownerCostShareLineItemHandPileAcres, decimal grantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres, decimal grantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres, decimal grantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres, decimal grantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres, decimal grantAllocationAwardLandownerCostShareLineItemSlashAcres, string grantAllocationAwardLandownerCostShareLineItemNotes, decimal grantAllocationAwardLandownerCostShareLineItemAllocatedAmount, decimal grantAllocationAwardLandownerCostShareLineItemTotalCost) : this()
        {
            this.GrantAllocationAwardLandownerCostShareLineItemID = grantAllocationAwardLandownerCostShareLineItemID;
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.ProjectID = projectID;
            this.LandownerCostShareLineItemStatusID = landownerCostShareLineItemStatusID;
            this.GrantAllocationAwardLandownerCostShareLineItemStartDate = grantAllocationAwardLandownerCostShareLineItemStartDate;
            this.GrantAllocationAwardLandownerCostShareLineItemEndDate = grantAllocationAwardLandownerCostShareLineItemEndDate;
            this.GrantAllocationAwardLandownerCostShareLineItemFootprintAcres = grantAllocationAwardLandownerCostShareLineItemFootprintAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemChippingAcres = grantAllocationAwardLandownerCostShareLineItemChippingAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemPruningAcres = grantAllocationAwardLandownerCostShareLineItemPruningAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemThinningAcres = grantAllocationAwardLandownerCostShareLineItemThinningAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemMasticationAcres = grantAllocationAwardLandownerCostShareLineItemMasticationAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemGrazingAcres = grantAllocationAwardLandownerCostShareLineItemGrazingAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres = grantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres = grantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemHandPileAcres = grantAllocationAwardLandownerCostShareLineItemHandPileAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres = grantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres = grantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres = grantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres = grantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemSlashAcres = grantAllocationAwardLandownerCostShareLineItemSlashAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemNotes = grantAllocationAwardLandownerCostShareLineItemNotes;
            this.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount = grantAllocationAwardLandownerCostShareLineItemAllocatedAmount;
            this.GrantAllocationAwardLandownerCostShareLineItemTotalCost = grantAllocationAwardLandownerCostShareLineItemTotalCost;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardLandownerCostShareLineItem(int projectID, int landownerCostShareLineItemStatusID, decimal grantAllocationAwardLandownerCostShareLineItemFootprintAcres, decimal grantAllocationAwardLandownerCostShareLineItemChippingAcres, decimal grantAllocationAwardLandownerCostShareLineItemPruningAcres, decimal grantAllocationAwardLandownerCostShareLineItemThinningAcres, decimal grantAllocationAwardLandownerCostShareLineItemMasticationAcres, decimal grantAllocationAwardLandownerCostShareLineItemGrazingAcres, decimal grantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres, decimal grantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres, decimal grantAllocationAwardLandownerCostShareLineItemHandPileAcres, decimal grantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres, decimal grantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres, decimal grantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres, decimal grantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres, decimal grantAllocationAwardLandownerCostShareLineItemSlashAcres, decimal grantAllocationAwardLandownerCostShareLineItemAllocatedAmount, decimal grantAllocationAwardLandownerCostShareLineItemTotalCost) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardLandownerCostShareLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.LandownerCostShareLineItemStatusID = landownerCostShareLineItemStatusID;
            this.GrantAllocationAwardLandownerCostShareLineItemFootprintAcres = grantAllocationAwardLandownerCostShareLineItemFootprintAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemChippingAcres = grantAllocationAwardLandownerCostShareLineItemChippingAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemPruningAcres = grantAllocationAwardLandownerCostShareLineItemPruningAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemThinningAcres = grantAllocationAwardLandownerCostShareLineItemThinningAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemMasticationAcres = grantAllocationAwardLandownerCostShareLineItemMasticationAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemGrazingAcres = grantAllocationAwardLandownerCostShareLineItemGrazingAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres = grantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres = grantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemHandPileAcres = grantAllocationAwardLandownerCostShareLineItemHandPileAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres = grantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres = grantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres = grantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres = grantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemSlashAcres = grantAllocationAwardLandownerCostShareLineItemSlashAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount = grantAllocationAwardLandownerCostShareLineItemAllocatedAmount;
            this.GrantAllocationAwardLandownerCostShareLineItemTotalCost = grantAllocationAwardLandownerCostShareLineItemTotalCost;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationAwardLandownerCostShareLineItem(Project project, LandownerCostShareLineItemStatus landownerCostShareLineItemStatus, decimal grantAllocationAwardLandownerCostShareLineItemFootprintAcres, decimal grantAllocationAwardLandownerCostShareLineItemChippingAcres, decimal grantAllocationAwardLandownerCostShareLineItemPruningAcres, decimal grantAllocationAwardLandownerCostShareLineItemThinningAcres, decimal grantAllocationAwardLandownerCostShareLineItemMasticationAcres, decimal grantAllocationAwardLandownerCostShareLineItemGrazingAcres, decimal grantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres, decimal grantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres, decimal grantAllocationAwardLandownerCostShareLineItemHandPileAcres, decimal grantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres, decimal grantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres, decimal grantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres, decimal grantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres, decimal grantAllocationAwardLandownerCostShareLineItemSlashAcres, decimal grantAllocationAwardLandownerCostShareLineItemAllocatedAmount, decimal grantAllocationAwardLandownerCostShareLineItemTotalCost) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardLandownerCostShareLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.GrantAllocationAwardLandownerCostShareLineItems.Add(this);
            this.LandownerCostShareLineItemStatusID = landownerCostShareLineItemStatus.LandownerCostShareLineItemStatusID;
            this.GrantAllocationAwardLandownerCostShareLineItemFootprintAcres = grantAllocationAwardLandownerCostShareLineItemFootprintAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemChippingAcres = grantAllocationAwardLandownerCostShareLineItemChippingAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemPruningAcres = grantAllocationAwardLandownerCostShareLineItemPruningAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemThinningAcres = grantAllocationAwardLandownerCostShareLineItemThinningAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemMasticationAcres = grantAllocationAwardLandownerCostShareLineItemMasticationAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemGrazingAcres = grantAllocationAwardLandownerCostShareLineItemGrazingAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres = grantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres = grantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemHandPileAcres = grantAllocationAwardLandownerCostShareLineItemHandPileAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres = grantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres = grantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres = grantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres = grantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemSlashAcres = grantAllocationAwardLandownerCostShareLineItemSlashAcres;
            this.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount = grantAllocationAwardLandownerCostShareLineItemAllocatedAmount;
            this.GrantAllocationAwardLandownerCostShareLineItemTotalCost = grantAllocationAwardLandownerCostShareLineItemTotalCost;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationAwardLandownerCostShareLineItem CreateNewBlank(Project project, LandownerCostShareLineItemStatus landownerCostShareLineItemStatus)
        {
            return new GrantAllocationAwardLandownerCostShareLineItem(project, landownerCostShareLineItemStatus, default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal), default(decimal));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationAwardLandownerCostShareLineItem).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationAwardLandownerCostShareLineItems.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationAwardLandownerCostShareLineItemID { get; set; }
        public int? GrantAllocationAwardID { get; set; }
        public int ProjectID { get; set; }
        public int LandownerCostShareLineItemStatusID { get; set; }
        public DateTime? GrantAllocationAwardLandownerCostShareLineItemStartDate { get; set; }
        public DateTime? GrantAllocationAwardLandownerCostShareLineItemEndDate { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemFootprintAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemChippingAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemPruningAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemThinningAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemMasticationAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemGrazingAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemHandPileAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemSlashAcres { get; set; }
        public string GrantAllocationAwardLandownerCostShareLineItemNotes { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount { get; set; }
        public decimal GrantAllocationAwardLandownerCostShareLineItemTotalCost { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationAwardLandownerCostShareLineItemID; } set { GrantAllocationAwardLandownerCostShareLineItemID = value; } }

        public virtual GrantAllocationAward GrantAllocationAward { get; set; }
        public virtual Project Project { get; set; }
        public LandownerCostShareLineItemStatus LandownerCostShareLineItemStatus { get { return LandownerCostShareLineItemStatus.AllLookupDictionary[LandownerCostShareLineItemStatusID]; } }

        public static class FieldLengths
        {
            public const int GrantAllocationAwardLandownerCostShareLineItemNotes = 2000;
        }
    }
}