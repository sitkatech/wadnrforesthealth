//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProgramIndexProjectCode]
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
    // Table [dbo].[ProgramIndexProjectCode] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProgramIndexProjectCode]")]
    public partial class ProgramIndexProjectCode : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProgramIndexProjectCode()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProgramIndexProjectCode(int programIndexProjectCodeID, int programIndexID, int projectCodeID) : this()
        {
            this.ProgramIndexProjectCodeID = programIndexProjectCodeID;
            this.ProgramIndexID = programIndexID;
            this.ProjectCodeID = projectCodeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProgramIndexProjectCode(int programIndexID, int projectCodeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProgramIndexProjectCodeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProgramIndexID = programIndexID;
            this.ProjectCodeID = projectCodeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProgramIndexProjectCode(ProgramIndex programIndex, ProjectCode projectCode) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProgramIndexProjectCodeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProgramIndexID = programIndex.ProgramIndexID;
            this.ProgramIndex = programIndex;
            programIndex.ProgramIndexProjectCodes.Add(this);
            this.ProjectCodeID = projectCode.ProjectCodeID;
            this.ProjectCode = projectCode;
            projectCode.ProgramIndexProjectCodes.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProgramIndexProjectCode CreateNewBlank(ProgramIndex programIndex, ProjectCode projectCode)
        {
            return new ProgramIndexProjectCode(programIndex, projectCode);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProgramIndexProjectCode).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProgramIndexProjectCodes.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProgramIndexProjectCodeID { get; set; }
        public int ProgramIndexID { get; set; }
        public int ProjectCodeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProgramIndexProjectCodeID; } set { ProgramIndexProjectCodeID = value; } }

        public virtual ProgramIndex ProgramIndex { get; set; }
        public virtual ProjectCode ProjectCode { get; set; }

        public static class FieldLengths
        {

        }
    }
}