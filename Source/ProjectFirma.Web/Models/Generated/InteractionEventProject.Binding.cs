//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[InteractionEventProject]
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
    // Table [dbo].[InteractionEventProject] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[InteractionEventProject]")]
    public partial class InteractionEventProject : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected InteractionEventProject()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public InteractionEventProject(int interactionEventProjectID, int interactionEventID, int projectID) : this()
        {
            this.InteractionEventProjectID = interactionEventProjectID;
            this.InteractionEventID = interactionEventID;
            this.ProjectID = projectID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public InteractionEventProject(int interactionEventID, int projectID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InteractionEventProjectID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.InteractionEventID = interactionEventID;
            this.ProjectID = projectID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public InteractionEventProject(InteractionEvent interactionEvent, Project project) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InteractionEventProjectID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.InteractionEventID = interactionEvent.InteractionEventID;
            this.InteractionEvent = interactionEvent;
            interactionEvent.InteractionEventProjects.Add(this);
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.InteractionEventProjects.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static InteractionEventProject CreateNewBlank(InteractionEvent interactionEvent, Project project)
        {
            return new InteractionEventProject(interactionEvent, project);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(InteractionEventProject).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.InteractionEventProjects.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int InteractionEventProjectID { get; set; }
        public int InteractionEventID { get; set; }
        public int ProjectID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return InteractionEventProjectID; } set { InteractionEventProjectID = value; } }

        public virtual InteractionEvent InteractionEvent { get; set; }
        public virtual Project Project { get; set; }

        public static class FieldLengths
        {

        }
    }
}