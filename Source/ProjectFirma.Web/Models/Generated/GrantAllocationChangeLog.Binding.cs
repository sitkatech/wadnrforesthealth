//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationChangeLog]
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
    // Table [dbo].[GrantAllocationChangeLog] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationChangeLog]")]
    public partial class GrantAllocationChangeLog : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationChangeLog()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationChangeLog(int grantAllocationChangeLogID, int grantAllocationID, decimal? grantAllocationAmountOldValue, decimal? grantAllocationAmountNewValue, string grantAllocationAmountNote, int changePersonID, DateTime changeDate) : this()
        {
            this.GrantAllocationChangeLogID = grantAllocationChangeLogID;
            this.GrantAllocationID = grantAllocationID;
            this.GrantAllocationAmountOldValue = grantAllocationAmountOldValue;
            this.GrantAllocationAmountNewValue = grantAllocationAmountNewValue;
            this.GrantAllocationAmountNote = grantAllocationAmountNote;
            this.ChangePersonID = changePersonID;
            this.ChangeDate = changeDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationChangeLog(int grantAllocationID, int changePersonID, DateTime changeDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationChangeLogID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationID = grantAllocationID;
            this.ChangePersonID = changePersonID;
            this.ChangeDate = changeDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationChangeLog(FundSourceAllocation fundSourceAllocation, Person changePerson, DateTime changeDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationChangeLogID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationID = fundSourceAllocation.GrantAllocationID;
            this.FundSourceAllocation = fundSourceAllocation;
            fundSourceAllocation.GrantAllocationChangeLogs.Add(this);
            this.ChangePersonID = changePerson.PersonID;
            this.ChangePerson = changePerson;
            changePerson.GrantAllocationChangeLogsWhereYouAreTheChangePerson.Add(this);
            this.ChangeDate = changeDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationChangeLog CreateNewBlank(FundSourceAllocation fundSourceAllocation, Person changePerson)
        {
            return new GrantAllocationChangeLog(fundSourceAllocation, changePerson, default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationChangeLog).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationChangeLogs.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationChangeLogID { get; set; }
        public int GrantAllocationID { get; set; }
        public decimal? GrantAllocationAmountOldValue { get; set; }
        public decimal? GrantAllocationAmountNewValue { get; set; }
        public string GrantAllocationAmountNote { get; set; }
        public int ChangePersonID { get; set; }
        public DateTime ChangeDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationChangeLogID; } set { GrantAllocationChangeLogID = value; } }

        public virtual FundSourceAllocation FundSourceAllocation { get; set; }
        public virtual Person ChangePerson { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationAmountNote = 2000;
        }
    }
}