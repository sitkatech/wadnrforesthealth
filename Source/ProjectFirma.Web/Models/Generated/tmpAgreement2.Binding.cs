//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[tmpAgreement2]
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
    // Table [dbo].[tmpAgreement2] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[tmpAgreement2]")]
    public partial class tmpAgreement2 : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected tmpAgreement2()
        {
            this.Agreements = new HashSet<Agreement>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public tmpAgreement2(int tmpAgreement2ID, string agreementType, string agreementNumber, string organization, string agreementTitle, DateTime startDate, DateTime endDate, decimal? agreementAmount, decimal? expendedMoney, decimal? balanceMoney, decimal? firstBillDueOn, string notes) : this()
        {
            this.TmpAgreement2ID = tmpAgreement2ID;
            this.AgreementType = agreementType;
            this.AgreementNumber = agreementNumber;
            this.Organization = organization;
            this.AgreementTitle = agreementTitle;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.AgreementAmount = agreementAmount;
            this.ExpendedMoney = expendedMoney;
            this.BalanceMoney = balanceMoney;
            this.FirstBillDueOn = firstBillDueOn;
            this.Notes = notes;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public tmpAgreement2(string agreementType, string agreementNumber, string organization, string agreementTitle, DateTime startDate, DateTime endDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TmpAgreement2ID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.AgreementType = agreementType;
            this.AgreementNumber = agreementNumber;
            this.Organization = organization;
            this.AgreementTitle = agreementTitle;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static tmpAgreement2 CreateNewBlank()
        {
            return new tmpAgreement2(default(string), default(string), default(string), default(string), default(DateTime), default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return Agreements.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(tmpAgreement2).Name, typeof(Agreement).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.tmpAgreement2s.Remove(this);
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

            foreach(var x in Agreements.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int TmpAgreement2ID { get; set; }
        public string AgreementType { get; set; }
        public string AgreementNumber { get; set; }
        public string Organization { get; set; }
        public string AgreementTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? AgreementAmount { get; set; }
        public decimal? ExpendedMoney { get; set; }
        public decimal? BalanceMoney { get; set; }
        public decimal? FirstBillDueOn { get; set; }
        public string Notes { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TmpAgreement2ID; } set { TmpAgreement2ID = value; } }

        public virtual ICollection<Agreement> Agreements { get; set; }

        public static class FieldLengths
        {
            public const int AgreementType = 50;
            public const int AgreementNumber = 50;
            public const int Organization = 100;
            public const int AgreementTitle = 100;
        }
    }
}