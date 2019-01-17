//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Grant]
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
    // Table [dbo].[Grant] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Grant]")]
    public partial class Grant : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Grant()
        {
            this.GrantAllocations = new HashSet<GrantAllocation>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Grant(int grantID, string grantNumber, DateTime? startDate, DateTime? endDate, int? programIndex, string projectCode, string conditionsAndRequirements, string complianceNotes, decimal? awardedFunds) : this()
        {
            this.GrantID = grantID;
            this.GrantNumber = grantNumber;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.ProgramIndex = programIndex;
            this.ProjectCode = projectCode;
            this.ConditionsAndRequirements = conditionsAndRequirements;
            this.ComplianceNotes = complianceNotes;
            this.AwardedFunds = awardedFunds;
        }



        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Grant CreateNewBlank()
        {
            return new Grant();
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantAllocations.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Grant).Name, typeof(GrantAllocation).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.Grants.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in GrantAllocations.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantID { get; set; }
        public string GrantNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ProgramIndex { get; set; }
        public string ProjectCode { get; set; }
        public string ConditionsAndRequirements { get; set; }
        public string ComplianceNotes { get; set; }
        public decimal? AwardedFunds { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantID; } set { GrantID = value; } }

        public virtual ICollection<GrantAllocation> GrantAllocations { get; set; }

        public static class FieldLengths
        {
            public const int GrantNumber = 30;
            public const int ProjectCode = 100;
        }
    }
}