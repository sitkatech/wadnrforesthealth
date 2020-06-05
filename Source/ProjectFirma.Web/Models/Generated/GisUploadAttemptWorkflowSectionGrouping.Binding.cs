//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GisUploadAttemptWorkflowSectionGrouping]
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
    // Table [dbo].[GisUploadAttemptWorkflowSectionGrouping] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GisUploadAttemptWorkflowSectionGrouping]")]
    public partial class GisUploadAttemptWorkflowSectionGrouping : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GisUploadAttemptWorkflowSectionGrouping()
        {
            this.GisUploadAttemptWorkflowSections = new HashSet<GisUploadAttemptWorkflowSection>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GisUploadAttemptWorkflowSectionGrouping(int gisUploadAttemptWorkflowSectionGroupingID, string gisUploadAttemptWorkflowSectionGroupingName, string gisUploadAttemptWorkflowSectionGroupingDisplayName, int sortOrder) : this()
        {
            this.GisUploadAttemptWorkflowSectionGroupingID = gisUploadAttemptWorkflowSectionGroupingID;
            this.GisUploadAttemptWorkflowSectionGroupingName = gisUploadAttemptWorkflowSectionGroupingName;
            this.GisUploadAttemptWorkflowSectionGroupingDisplayName = gisUploadAttemptWorkflowSectionGroupingDisplayName;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GisUploadAttemptWorkflowSectionGrouping(string gisUploadAttemptWorkflowSectionGroupingName, string gisUploadAttemptWorkflowSectionGroupingDisplayName, int sortOrder) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GisUploadAttemptWorkflowSectionGroupingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GisUploadAttemptWorkflowSectionGroupingName = gisUploadAttemptWorkflowSectionGroupingName;
            this.GisUploadAttemptWorkflowSectionGroupingDisplayName = gisUploadAttemptWorkflowSectionGroupingDisplayName;
            this.SortOrder = sortOrder;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GisUploadAttemptWorkflowSectionGrouping CreateNewBlank()
        {
            return new GisUploadAttemptWorkflowSectionGrouping(default(string), default(string), default(int));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GisUploadAttemptWorkflowSections.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(GisUploadAttemptWorkflowSections.Any())
            {
                dependentObjects.Add(typeof(GisUploadAttemptWorkflowSection).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GisUploadAttemptWorkflowSectionGrouping).Name, typeof(GisUploadAttemptWorkflowSection).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GisUploadAttemptWorkflowSectionGroupings.Remove(this);
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

            foreach(var x in GisUploadAttemptWorkflowSections.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GisUploadAttemptWorkflowSectionGroupingID { get; set; }
        public string GisUploadAttemptWorkflowSectionGroupingName { get; set; }
        public string GisUploadAttemptWorkflowSectionGroupingDisplayName { get; set; }
        public int SortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GisUploadAttemptWorkflowSectionGroupingID; } set { GisUploadAttemptWorkflowSectionGroupingID = value; } }

        public virtual ICollection<GisUploadAttemptWorkflowSection> GisUploadAttemptWorkflowSections { get; set; }

        public static class FieldLengths
        {
            public const int GisUploadAttemptWorkflowSectionGroupingName = 50;
            public const int GisUploadAttemptWorkflowSectionGroupingDisplayName = 50;
        }
    }
}