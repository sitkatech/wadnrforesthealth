//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Agreement]
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
    // Table [dbo].[Agreement] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Agreement]")]
    public partial class Agreement : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Agreement()
        {
            this.AgreementProjectCodes = new HashSet<AgreementProjectCode>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Agreement(int agreementID, int? tmpAgreementID, int? agreementTypeID, string agreementNumber, DateTime? startDate, DateTime? endDate, decimal? agreementAmount, decimal? expendedAmount, decimal? balanceAmount, int? regionID, DateTime? firstBillDueOn, string notes, string agreementTitle, int organizationID, int grantID, int? agreementStatusID) : this()
        {
            this.AgreementID = agreementID;
            this.TmpAgreementID = tmpAgreementID;
            this.AgreementTypeID = agreementTypeID;
            this.AgreementNumber = agreementNumber;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.AgreementAmount = agreementAmount;
            this.ExpendedAmount = expendedAmount;
            this.BalanceAmount = balanceAmount;
            this.RegionID = regionID;
            this.FirstBillDueOn = firstBillDueOn;
            this.Notes = notes;
            this.AgreementTitle = agreementTitle;
            this.OrganizationID = organizationID;
            this.GrantID = grantID;
            this.AgreementStatusID = agreementStatusID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Agreement(string agreementTitle, int organizationID, int grantID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.AgreementID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.AgreementTitle = agreementTitle;
            this.OrganizationID = organizationID;
            this.GrantID = grantID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Agreement(string agreementTitle, Organization organization, Grant grant) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.AgreementID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.AgreementTitle = agreementTitle;
            this.OrganizationID = organization.OrganizationID;
            this.Organization = organization;
            organization.Agreements.Add(this);
            this.GrantID = grant.GrantID;
            this.Grant = grant;
            grant.Agreements.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Agreement CreateNewBlank(Organization organization, Grant grant)
        {
            return new Agreement(default(string), organization, grant);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return AgreementProjectCodes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Agreement).Name, typeof(AgreementProjectCode).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.Agreements.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in AgreementProjectCodes.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int AgreementID { get; set; }
        public int? TmpAgreementID { get; set; }
        public int? AgreementTypeID { get; set; }
        public string AgreementNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? AgreementAmount { get; set; }
        public decimal? ExpendedAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public int? RegionID { get; set; }
        public DateTime? FirstBillDueOn { get; set; }
        public string Notes { get; set; }
        public string AgreementTitle { get; set; }
        public int OrganizationID { get; set; }
        public int GrantID { get; set; }
        public int? AgreementStatusID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return AgreementID; } set { AgreementID = value; } }

        public virtual ICollection<AgreementProjectCode> AgreementProjectCodes { get; set; }
        public virtual AgreementType AgreementType { get; set; }
        public virtual Region Region { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Grant Grant { get; set; }
        public virtual AgreementStatus AgreementStatus { get; set; }

        public static class FieldLengths
        {
            public const int AgreementNumber = 100;
            public const int AgreementTitle = 256;
        }
    }
}