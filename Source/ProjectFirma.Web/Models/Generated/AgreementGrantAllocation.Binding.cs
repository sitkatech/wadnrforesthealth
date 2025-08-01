//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[AgreementGrantAllocation]
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
    // Table [dbo].[AgreementGrantAllocation] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[AgreementGrantAllocation]")]
    public partial class AgreementGrantAllocation : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected AgreementGrantAllocation()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public AgreementGrantAllocation(int agreementGrantAllocationID, int agreementID, int grantAllocationID) : this()
        {
            this.AgreementGrantAllocationID = agreementGrantAllocationID;
            this.AgreementID = agreementID;
            this.GrantAllocationID = grantAllocationID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public AgreementGrantAllocation(int agreementID, int grantAllocationID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.AgreementGrantAllocationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.AgreementID = agreementID;
            this.GrantAllocationID = grantAllocationID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public AgreementGrantAllocation(Agreement agreement, FundSourceAllocation fundSourceAllocation) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.AgreementGrantAllocationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.AgreementID = agreement.AgreementID;
            this.Agreement = agreement;
            agreement.AgreementGrantAllocations.Add(this);
            this.GrantAllocationID = fundSourceAllocation.GrantAllocationID;
            this.FundSourceAllocation = fundSourceAllocation;
            fundSourceAllocation.AgreementGrantAllocations.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static AgreementGrantAllocation CreateNewBlank(Agreement agreement, FundSourceAllocation fundSourceAllocation)
        {
            return new AgreementGrantAllocation(agreement, fundSourceAllocation);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(AgreementGrantAllocation).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.AgreementGrantAllocations.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int AgreementGrantAllocationID { get; set; }
        public int AgreementID { get; set; }
        public int GrantAllocationID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return AgreementGrantAllocationID; } set { AgreementGrantAllocationID = value; } }

        public virtual Agreement Agreement { get; set; }
        public virtual FundSourceAllocation FundSourceAllocation { get; set; }

        public static class FieldLengths
        {

        }
    }
}