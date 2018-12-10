//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ContractorTimeActivity]
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
    [Table("[dbo].[ContractorTimeActivity]")]
    public partial class ContractorTimeActivity : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ContractorTimeActivity()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ContractorTimeActivity(int contractorTimeActivityID, int projectID, int fundingSourceID, decimal contractorTimeActivityAcreage, decimal contractorTimeActivityHours, decimal contractorTimeActivityRate, DateTime contractorTimeActivityStartDate, DateTime? contractorTimeActivityEndDate, string contractorTimeActivityNotes) : this()
        {
            this.ContractorTimeActivityID = contractorTimeActivityID;
            this.ProjectID = projectID;
            this.FundingSourceID = fundingSourceID;
            this.ContractorTimeActivityAcreage = contractorTimeActivityAcreage;
            this.ContractorTimeActivityHours = contractorTimeActivityHours;
            this.ContractorTimeActivityRate = contractorTimeActivityRate;
            this.ContractorTimeActivityStartDate = contractorTimeActivityStartDate;
            this.ContractorTimeActivityEndDate = contractorTimeActivityEndDate;
            this.ContractorTimeActivityNotes = contractorTimeActivityNotes;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ContractorTimeActivity(int projectID, int fundingSourceID, decimal contractorTimeActivityAcreage, decimal contractorTimeActivityHours, decimal contractorTimeActivityRate, DateTime contractorTimeActivityStartDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ContractorTimeActivityID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.FundingSourceID = fundingSourceID;
            this.ContractorTimeActivityAcreage = contractorTimeActivityAcreage;
            this.ContractorTimeActivityHours = contractorTimeActivityHours;
            this.ContractorTimeActivityRate = contractorTimeActivityRate;
            this.ContractorTimeActivityStartDate = contractorTimeActivityStartDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ContractorTimeActivity(Project project, FundingSource fundingSource, decimal contractorTimeActivityAcreage, decimal contractorTimeActivityHours, decimal contractorTimeActivityRate, DateTime contractorTimeActivityStartDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ContractorTimeActivityID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ContractorTimeActivities.Add(this);
            this.FundingSourceID = fundingSource.FundingSourceID;
            this.FundingSource = fundingSource;
            fundingSource.ContractorTimeActivities.Add(this);
            this.ContractorTimeActivityAcreage = contractorTimeActivityAcreage;
            this.ContractorTimeActivityHours = contractorTimeActivityHours;
            this.ContractorTimeActivityRate = contractorTimeActivityRate;
            this.ContractorTimeActivityStartDate = contractorTimeActivityStartDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ContractorTimeActivity CreateNewBlank(Project project, FundingSource fundingSource)
        {
            return new ContractorTimeActivity(project, fundingSource, default(decimal), default(decimal), default(decimal), default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ContractorTimeActivity).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(HttpRequestStorage.DatabaseEntities);
            dbContext.AllContractorTimeActivities.Remove(this);
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

        }

        [Key]
        public int ContractorTimeActivityID { get; set; }
        public int TenantID { get; private set; }
        public int ProjectID { get; set; }
        public int FundingSourceID { get; set; }
        public decimal ContractorTimeActivityAcreage { get; set; }
        public decimal ContractorTimeActivityHours { get; set; }
        public decimal ContractorTimeActivityRate { get; set; }
        public DateTime ContractorTimeActivityStartDate { get; set; }
        public DateTime? ContractorTimeActivityEndDate { get; set; }
        public string ContractorTimeActivityNotes { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ContractorTimeActivityID; } set { ContractorTimeActivityID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual Project Project { get; set; }
        public virtual FundingSource FundingSource { get; set; }

        public static class FieldLengths
        {

        }
    }
}