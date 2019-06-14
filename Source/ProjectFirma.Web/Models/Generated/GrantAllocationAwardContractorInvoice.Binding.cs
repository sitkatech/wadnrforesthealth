//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardContractorInvoice]
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
    // Table [dbo].[GrantAllocationAwardContractorInvoice] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationAwardContractorInvoice]")]
    public partial class GrantAllocationAwardContractorInvoice : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationAwardContractorInvoice()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardContractorInvoice(int grantAllocationAwardContractorInvoiceID, int grantAllocationAwardID, string grantAllocationAwardContractorInvoiceDescription, string grantAllocationAwardContractorInvoiceNumber, DateTime grantAllocationAwardContractorInvoiceDate, int grantAllocationAwardContractorInvoiceTypeID, decimal? grantAllocationAwardContractorInvoiceForemanHours, decimal? grantAllocationAwardContractorInvoiceForemanRate, decimal? grantAllocationAwardContractorInvoiceLaborHours, decimal? grantAllocationAwardContractorInvoiceLaborRate, decimal? grantAllocationAwardContractorInvoiceGrappleHours, decimal? grantAllocationAwardContractorInvoiceGrappleRate, decimal? grantAllocationAwardContractorInvoiceMasticationHours, decimal? grantAllocationAwardContractorInvoiceMasticationRate, decimal? grantAllocationAwardContractorInvoiceTotal, decimal? grantAllocationAwardContractorInvoiceTaxRate, decimal? grantAllocationAwardContractorInvoiceAcresReported, string grantAllocationAwardContractorInvoiceNotes, int? grantAllocationAwardContractorInvoiceFileResourceID) : this()
        {
            this.GrantAllocationAwardContractorInvoiceID = grantAllocationAwardContractorInvoiceID;
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.GrantAllocationAwardContractorInvoiceDescription = grantAllocationAwardContractorInvoiceDescription;
            this.GrantAllocationAwardContractorInvoiceNumber = grantAllocationAwardContractorInvoiceNumber;
            this.GrantAllocationAwardContractorInvoiceDate = grantAllocationAwardContractorInvoiceDate;
            this.GrantAllocationAwardContractorInvoiceTypeID = grantAllocationAwardContractorInvoiceTypeID;
            this.GrantAllocationAwardContractorInvoiceForemanHours = grantAllocationAwardContractorInvoiceForemanHours;
            this.GrantAllocationAwardContractorInvoiceForemanRate = grantAllocationAwardContractorInvoiceForemanRate;
            this.GrantAllocationAwardContractorInvoiceLaborHours = grantAllocationAwardContractorInvoiceLaborHours;
            this.GrantAllocationAwardContractorInvoiceLaborRate = grantAllocationAwardContractorInvoiceLaborRate;
            this.GrantAllocationAwardContractorInvoiceGrappleHours = grantAllocationAwardContractorInvoiceGrappleHours;
            this.GrantAllocationAwardContractorInvoiceGrappleRate = grantAllocationAwardContractorInvoiceGrappleRate;
            this.GrantAllocationAwardContractorInvoiceMasticationHours = grantAllocationAwardContractorInvoiceMasticationHours;
            this.GrantAllocationAwardContractorInvoiceMasticationRate = grantAllocationAwardContractorInvoiceMasticationRate;
            this.GrantAllocationAwardContractorInvoiceTotal = grantAllocationAwardContractorInvoiceTotal;
            this.GrantAllocationAwardContractorInvoiceTaxRate = grantAllocationAwardContractorInvoiceTaxRate;
            this.GrantAllocationAwardContractorInvoiceAcresReported = grantAllocationAwardContractorInvoiceAcresReported;
            this.GrantAllocationAwardContractorInvoiceNotes = grantAllocationAwardContractorInvoiceNotes;
            this.GrantAllocationAwardContractorInvoiceFileResourceID = grantAllocationAwardContractorInvoiceFileResourceID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardContractorInvoice(int grantAllocationAwardID, string grantAllocationAwardContractorInvoiceDescription, string grantAllocationAwardContractorInvoiceNumber, DateTime grantAllocationAwardContractorInvoiceDate, int grantAllocationAwardContractorInvoiceTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardContractorInvoiceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.GrantAllocationAwardContractorInvoiceDescription = grantAllocationAwardContractorInvoiceDescription;
            this.GrantAllocationAwardContractorInvoiceNumber = grantAllocationAwardContractorInvoiceNumber;
            this.GrantAllocationAwardContractorInvoiceDate = grantAllocationAwardContractorInvoiceDate;
            this.GrantAllocationAwardContractorInvoiceTypeID = grantAllocationAwardContractorInvoiceTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationAwardContractorInvoice(GrantAllocationAward grantAllocationAward, string grantAllocationAwardContractorInvoiceDescription, string grantAllocationAwardContractorInvoiceNumber, DateTime grantAllocationAwardContractorInvoiceDate, GrantAllocationAwardContractorInvoiceType grantAllocationAwardContractorInvoiceType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationAwardContractorInvoiceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID;
            this.GrantAllocationAward = grantAllocationAward;
            grantAllocationAward.GrantAllocationAwardContractorInvoices.Add(this);
            this.GrantAllocationAwardContractorInvoiceDescription = grantAllocationAwardContractorInvoiceDescription;
            this.GrantAllocationAwardContractorInvoiceNumber = grantAllocationAwardContractorInvoiceNumber;
            this.GrantAllocationAwardContractorInvoiceDate = grantAllocationAwardContractorInvoiceDate;
            this.GrantAllocationAwardContractorInvoiceTypeID = grantAllocationAwardContractorInvoiceType.GrantAllocationAwardContractorInvoiceTypeID;
            this.GrantAllocationAwardContractorInvoiceType = grantAllocationAwardContractorInvoiceType;
            grantAllocationAwardContractorInvoiceType.GrantAllocationAwardContractorInvoices.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationAwardContractorInvoice CreateNewBlank(GrantAllocationAward grantAllocationAward, GrantAllocationAwardContractorInvoiceType grantAllocationAwardContractorInvoiceType)
        {
            return new GrantAllocationAwardContractorInvoice(grantAllocationAward, default(string), default(string), default(DateTime), grantAllocationAwardContractorInvoiceType);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationAwardContractorInvoice).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationAwardContractorInvoices.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationAwardContractorInvoiceID { get; set; }
        public int GrantAllocationAwardID { get; set; }
        public string GrantAllocationAwardContractorInvoiceDescription { get; set; }
        public string GrantAllocationAwardContractorInvoiceNumber { get; set; }
        public DateTime GrantAllocationAwardContractorInvoiceDate { get; set; }
        public int GrantAllocationAwardContractorInvoiceTypeID { get; set; }
        public decimal? GrantAllocationAwardContractorInvoiceForemanHours { get; set; }
        public decimal? GrantAllocationAwardContractorInvoiceForemanRate { get; set; }
        public decimal? GrantAllocationAwardContractorInvoiceLaborHours { get; set; }
        public decimal? GrantAllocationAwardContractorInvoiceLaborRate { get; set; }
        public decimal? GrantAllocationAwardContractorInvoiceGrappleHours { get; set; }
        public decimal? GrantAllocationAwardContractorInvoiceGrappleRate { get; set; }
        public decimal? GrantAllocationAwardContractorInvoiceMasticationHours { get; set; }
        public decimal? GrantAllocationAwardContractorInvoiceMasticationRate { get; set; }
        public decimal? GrantAllocationAwardContractorInvoiceTotal { get; set; }
        public decimal? GrantAllocationAwardContractorInvoiceTaxRate { get; set; }
        public decimal? GrantAllocationAwardContractorInvoiceAcresReported { get; set; }
        public string GrantAllocationAwardContractorInvoiceNotes { get; set; }
        public int? GrantAllocationAwardContractorInvoiceFileResourceID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationAwardContractorInvoiceID; } set { GrantAllocationAwardContractorInvoiceID = value; } }

        public virtual GrantAllocationAward GrantAllocationAward { get; set; }
        public virtual GrantAllocationAwardContractorInvoiceType GrantAllocationAwardContractorInvoiceType { get; set; }
        public virtual FileResource GrantAllocationAwardContractorInvoiceFileResource { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationAwardContractorInvoiceDescription = 255;
            public const int GrantAllocationAwardContractorInvoiceNumber = 255;
            public const int GrantAllocationAwardContractorInvoiceNotes = 2000;
        }
    }
}