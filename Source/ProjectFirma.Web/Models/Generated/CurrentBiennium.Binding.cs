//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CurrentBiennium]
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
    // Table [dbo].[CurrentBiennium] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[CurrentBiennium]")]
    public partial class CurrentBiennium : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected CurrentBiennium()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public CurrentBiennium(int currentBienniumID, int currentBienniumFiscalYear) : this()
        {
            this.CurrentBienniumID = currentBienniumID;
            this.CurrentBienniumFiscalYear = currentBienniumFiscalYear;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public CurrentBiennium(int currentBienniumFiscalYear) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.CurrentBienniumID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.CurrentBienniumFiscalYear = currentBienniumFiscalYear;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static CurrentBiennium CreateNewBlank()
        {
            return new CurrentBiennium(default(int));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(CurrentBiennium).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.CurrentBiennia.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int CurrentBienniumID { get; set; }
        public int CurrentBienniumFiscalYear { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return CurrentBienniumID; } set { CurrentBienniumID = value; } }



        public static class FieldLengths
        {

        }
    }
}