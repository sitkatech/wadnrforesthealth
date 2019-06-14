//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardContractorInvoiceType]
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
    // Table [dbo].[GrantAllocationAwardContractorInvoiceType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationAwardContractorInvoiceType]")]
    public partial class GrantAllocationAwardContractorInvoiceType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationAwardContractorInvoiceType()
        {
            this.GrantAllocationAwardContractorInvoices = new HashSet<GrantAllocationAwardContractorInvoice>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationAwardContractorInvoiceType(int grantAllocationAwardContractorInvoiceTypeID, string grantAllocationAwardContractorInvoiceTypeName, string grantAllocationAwardContractorInvoiceDisplayName) : this()
        {
            this.GrantAllocationAwardContractorInvoiceTypeID = grantAllocationAwardContractorInvoiceTypeID;
            this.GrantAllocationAwardContractorInvoiceTypeName = grantAllocationAwardContractorInvoiceTypeName;
            this.GrantAllocationAwardContractorInvoiceDisplayName = grantAllocationAwardContractorInvoiceDisplayName;
        }



        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationAwardContractorInvoiceType CreateNewBlank()
        {
            return new GrantAllocationAwardContractorInvoiceType();
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantAllocationAwardContractorInvoices.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationAwardContractorInvoiceType).Name, typeof(GrantAllocationAwardContractorInvoice).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationAwardContractorInvoiceTypes.Remove(this);
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
        }

        [Key]
        public int GrantAllocationAwardContractorInvoiceTypeID { get; set; }
        public string GrantAllocationAwardContractorInvoiceTypeName { get; set; }
        public string GrantAllocationAwardContractorInvoiceDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationAwardContractorInvoiceTypeID; } set { GrantAllocationAwardContractorInvoiceTypeID = value; } }

        public virtual ICollection<GrantAllocationAwardContractorInvoice> GrantAllocationAwardContractorInvoices { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationAwardContractorInvoiceTypeName = 255;
            public const int GrantAllocationAwardContractorInvoiceDisplayName = 255;
        }
    }
}