//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationProgramIndexProjectCode]
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
    // Table [dbo].[GrantAllocationProgramIndexProjectCode] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationProgramIndexProjectCode]")]
    public partial class GrantAllocationProgramIndexProjectCode : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationProgramIndexProjectCode()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationProgramIndexProjectCode(int grantAllocationProgramIndexProjectCodeID, int grantAllocationID, int programIndexID, int? projectCodeID) : this()
        {
            this.GrantAllocationProgramIndexProjectCodeID = grantAllocationProgramIndexProjectCodeID;
            this.GrantAllocationID = grantAllocationID;
            this.ProgramIndexID = programIndexID;
            this.ProjectCodeID = projectCodeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationProgramIndexProjectCode(int grantAllocationID, int programIndexID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationProgramIndexProjectCodeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationID = grantAllocationID;
            this.ProgramIndexID = programIndexID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationProgramIndexProjectCode(FundSourceAllocation fundSourceAllocation, ProgramIndex programIndex) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationProgramIndexProjectCodeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationID = fundSourceAllocation.GrantAllocationID;
            this.FundSourceAllocation = fundSourceAllocation;
            fundSourceAllocation.GrantAllocationProgramIndexProjectCodes.Add(this);
            this.ProgramIndexID = programIndex.ProgramIndexID;
            this.ProgramIndex = programIndex;
            programIndex.GrantAllocationProgramIndexProjectCodes.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationProgramIndexProjectCode CreateNewBlank(FundSourceAllocation fundSourceAllocation, ProgramIndex programIndex)
        {
            return new GrantAllocationProgramIndexProjectCode(fundSourceAllocation, programIndex);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationProgramIndexProjectCode).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationProgramIndexProjectCodes.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationProgramIndexProjectCodeID { get; set; }
        public int GrantAllocationID { get; set; }
        public int ProgramIndexID { get; set; }
        public int? ProjectCodeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationProgramIndexProjectCodeID; } set { GrantAllocationProgramIndexProjectCodeID = value; } }

        public virtual FundSourceAllocation FundSourceAllocation { get; set; }
        public virtual ProgramIndex ProgramIndex { get; set; }
        public virtual ProjectCode ProjectCode { get; set; }

        public static class FieldLengths
        {

        }
    }
}