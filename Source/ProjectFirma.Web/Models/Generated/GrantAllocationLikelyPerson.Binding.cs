//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationLikelyPerson]
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
    // Table [dbo].[GrantAllocationLikelyPerson] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationLikelyPerson]")]
    public partial class GrantAllocationLikelyPerson : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationLikelyPerson()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationLikelyPerson(int grantAllocationLikelyPersonID, int grantAllocationID, int personID) : this()
        {
            this.GrantAllocationLikelyPersonID = grantAllocationLikelyPersonID;
            this.GrantAllocationID = grantAllocationID;
            this.PersonID = personID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationLikelyPerson(int grantAllocationID, int personID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationLikelyPersonID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationID = grantAllocationID;
            this.PersonID = personID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationLikelyPerson(FundSourceAllocation fundSourceAllocation, Person person) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationLikelyPersonID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationID = fundSourceAllocation.GrantAllocationID;
            this.FundSourceAllocation = fundSourceAllocation;
            fundSourceAllocation.GrantAllocationLikelyPeople.Add(this);
            this.PersonID = person.PersonID;
            this.Person = person;
            person.GrantAllocationLikelyPeople.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationLikelyPerson CreateNewBlank(FundSourceAllocation fundSourceAllocation, Person person)
        {
            return new GrantAllocationLikelyPerson(fundSourceAllocation, person);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationLikelyPerson).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationLikelyPeople.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationLikelyPersonID { get; set; }
        public int GrantAllocationID { get; set; }
        public int PersonID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationLikelyPersonID; } set { GrantAllocationLikelyPersonID = value; } }

        public virtual FundSourceAllocation FundSourceAllocation { get; set; }
        public virtual Person Person { get; set; }

        public static class FieldLengths
        {

        }
    }
}