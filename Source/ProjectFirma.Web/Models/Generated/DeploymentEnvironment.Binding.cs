//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DeploymentEnvironment]
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
    // Table [dbo].[DeploymentEnvironment] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[DeploymentEnvironment]")]
    public partial class DeploymentEnvironment : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected DeploymentEnvironment()
        {
            this.PersonEnvironmentCredentials = new HashSet<PersonEnvironmentCredential>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public DeploymentEnvironment(int deploymentEnvironmentID, string deploymentEnvironmentName) : this()
        {
            this.DeploymentEnvironmentID = deploymentEnvironmentID;
            this.DeploymentEnvironmentName = deploymentEnvironmentName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public DeploymentEnvironment(string deploymentEnvironmentName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DeploymentEnvironmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.DeploymentEnvironmentName = deploymentEnvironmentName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static DeploymentEnvironment CreateNewBlank()
        {
            return new DeploymentEnvironment(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return PersonEnvironmentCredentials.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(DeploymentEnvironment).Name, typeof(PersonEnvironmentCredential).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.DeploymentEnvironments.Remove(this);
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

            foreach(var x in PersonEnvironmentCredentials.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int DeploymentEnvironmentID { get; set; }
        public string DeploymentEnvironmentName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return DeploymentEnvironmentID; } set { DeploymentEnvironmentID = value; } }

        public virtual ICollection<PersonEnvironmentCredential> PersonEnvironmentCredentials { get; set; }

        public static class FieldLengths
        {
            public const int DeploymentEnvironmentName = 10;
        }
    }
}