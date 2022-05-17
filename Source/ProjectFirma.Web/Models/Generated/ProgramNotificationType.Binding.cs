//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProgramNotificationType]
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
    // Table [dbo].[ProgramNotificationType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProgramNotificationType]")]
    public partial class ProgramNotificationType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProgramNotificationType()
        {
            this.ProgramNotificationConfigurations = new HashSet<ProgramNotificationConfiguration>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProgramNotificationType(int programNotificationTypeID, string programNotificationTypeName, string programNotificationTypeDisplayName) : this()
        {
            this.ProgramNotificationTypeID = programNotificationTypeID;
            this.ProgramNotificationTypeName = programNotificationTypeName;
            this.ProgramNotificationTypeDisplayName = programNotificationTypeDisplayName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProgramNotificationType(string programNotificationTypeName, string programNotificationTypeDisplayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProgramNotificationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProgramNotificationTypeName = programNotificationTypeName;
            this.ProgramNotificationTypeDisplayName = programNotificationTypeDisplayName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProgramNotificationType CreateNewBlank()
        {
            return new ProgramNotificationType(default(string), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ProgramNotificationConfigurations.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(ProgramNotificationConfigurations.Any())
            {
                dependentObjects.Add(typeof(ProgramNotificationConfiguration).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProgramNotificationType).Name, typeof(ProgramNotificationConfiguration).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProgramNotificationTypes.Remove(this);
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

            foreach(var x in ProgramNotificationConfigurations.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ProgramNotificationTypeID { get; set; }
        public string ProgramNotificationTypeName { get; set; }
        public string ProgramNotificationTypeDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProgramNotificationTypeID; } set { ProgramNotificationTypeID = value; } }

        public virtual ICollection<ProgramNotificationConfiguration> ProgramNotificationConfigurations { get; set; }

        public static class FieldLengths
        {
            public const int ProgramNotificationTypeName = 100;
            public const int ProgramNotificationTypeDisplayName = 100;
        }
    }
}