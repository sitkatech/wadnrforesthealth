//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PclWildfireResponseBenefit]
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
    // Table [dbo].[PclWildfireResponseBenefit] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[PclWildfireResponseBenefit]")]
    public partial class PclWildfireResponseBenefit : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected PclWildfireResponseBenefit()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public PclWildfireResponseBenefit(int pclWildfireResponseBenefitID, int priorityLandscapeID, DbGeometry feature, string color) : this()
        {
            this.PclWildfireResponseBenefitID = pclWildfireResponseBenefitID;
            this.PriorityLandscapeID = priorityLandscapeID;
            this.Feature = feature;
            this.Color = color;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public PclWildfireResponseBenefit(int priorityLandscapeID, DbGeometry feature, string color) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PclWildfireResponseBenefitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PriorityLandscapeID = priorityLandscapeID;
            this.Feature = feature;
            this.Color = color;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public PclWildfireResponseBenefit(PriorityLandscape priorityLandscape, DbGeometry feature, string color) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PclWildfireResponseBenefitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.PriorityLandscapeID = priorityLandscape.PriorityLandscapeID;
            this.PriorityLandscape = priorityLandscape;
            priorityLandscape.PclWildfireResponseBenefits.Add(this);
            this.Feature = feature;
            this.Color = color;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static PclWildfireResponseBenefit CreateNewBlank(PriorityLandscape priorityLandscape)
        {
            return new PclWildfireResponseBenefit(priorityLandscape, default(DbGeometry), default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(PclWildfireResponseBenefit).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.PclWildfireResponseBenefits.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int PclWildfireResponseBenefitID { get; set; }
        public int PriorityLandscapeID { get; set; }
        public DbGeometry Feature { get; set; }
        public string Color { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PclWildfireResponseBenefitID; } set { PclWildfireResponseBenefitID = value; } }

        public virtual PriorityLandscape PriorityLandscape { get; set; }

        public static class FieldLengths
        {
            public const int Color = 20;
        }
    }
}