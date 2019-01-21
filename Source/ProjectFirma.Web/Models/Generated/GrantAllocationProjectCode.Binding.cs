//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationProjectCode]
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
    [Table("[dbo].[GrantAllocationProjectCode]")]
    public partial class GrantAllocationProjectCode : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationProjectCode()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationProjectCode(int grantAllocationProjectCodeID, int grantAllocationID, int projectCodeID) : this()
        {
            this.GrantAllocationProjectCodeID = grantAllocationProjectCodeID;
            this.GrantAllocationID = grantAllocationID;
            this.ProjectCodeID = projectCodeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationProjectCode(int grantAllocationID, int projectCodeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationProjectCodeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationID = grantAllocationID;
            this.ProjectCodeID = projectCodeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationProjectCode(GrantAllocation grantAllocation, ProjectCode projectCode) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationProjectCodeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationID = grantAllocation.GrantAllocationID;
            this.GrantAllocation = grantAllocation;
            grantAllocation.GrantAllocationProjectCodes.Add(this);
            this.ProjectCodeID = projectCode.ProjectCodeID;
            this.ProjectCode = projectCode;
            projectCode.GrantAllocationProjectCodes.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationProjectCode CreateNewBlank(GrantAllocation grantAllocation, ProjectCode projectCode)
        {
            return new GrantAllocationProjectCode(grantAllocation, projectCode);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationProjectCode).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            dbContext.GrantAllocationProjectCodes.Remove(this);
        }

        [Key]
        public int GrantAllocationProjectCodeID { get; set; }
        public int GrantAllocationID { get; set; }
        public int ProjectCodeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationProjectCodeID; } set { GrantAllocationProjectCodeID = value; } }

        public virtual GrantAllocation GrantAllocation { get; set; }
        public virtual ProjectCode ProjectCode { get; set; }

        public static class FieldLengths
        {

        }
    }
}