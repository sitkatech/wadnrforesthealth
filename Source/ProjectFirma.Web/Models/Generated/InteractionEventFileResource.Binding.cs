//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[InteractionEventFileResource]
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
    // Table [dbo].[InteractionEventFileResource] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[InteractionEventFileResource]")]
    public partial class InteractionEventFileResource : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected InteractionEventFileResource()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public InteractionEventFileResource(int interactionEventFileResourceID, int interactionEventID, int fileResourceID, string displayName, string description) : this()
        {
            this.InteractionEventFileResourceID = interactionEventFileResourceID;
            this.InteractionEventID = interactionEventID;
            this.FileResourceID = fileResourceID;
            this.DisplayName = displayName;
            this.Description = description;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public InteractionEventFileResource(int interactionEventID, int fileResourceID, string displayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InteractionEventFileResourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.InteractionEventID = interactionEventID;
            this.FileResourceID = fileResourceID;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public InteractionEventFileResource(InteractionEvent interactionEvent, FileResource fileResource, string displayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InteractionEventFileResourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.InteractionEventID = interactionEvent.InteractionEventID;
            this.InteractionEvent = interactionEvent;
            interactionEvent.InteractionEventFileResources.Add(this);
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static InteractionEventFileResource CreateNewBlank(InteractionEvent interactionEvent, FileResource fileResource)
        {
            return new InteractionEventFileResource(interactionEvent, fileResource, default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(InteractionEventFileResource).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.InteractionEventFileResources.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int InteractionEventFileResourceID { get; set; }
        public int InteractionEventID { get; set; }
        public int FileResourceID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return InteractionEventFileResourceID; } set { InteractionEventFileResourceID = value; } }

        public virtual InteractionEvent InteractionEvent { get; set; }
        public virtual FileResource FileResource { get; set; }

        public static class FieldLengths
        {
            public const int DisplayName = 200;
            public const int Description = 1000;
        }
    }
}