//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Tenant]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public abstract partial class Tenant : IHavePrimaryKey
    {
        public static readonly TenantWashingtonDepartmentOfNaturalResources WashingtonDepartmentOfNaturalResources = TenantWashingtonDepartmentOfNaturalResources.Instance;

        public static readonly List<Tenant> All;
        public static readonly ReadOnlyDictionary<int, Tenant> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static Tenant()
        {
            All = new List<Tenant> { WashingtonDepartmentOfNaturalResources };
            AllLookupDictionary = new ReadOnlyDictionary<int, Tenant>(All.ToDictionary(x => x.TenantID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected Tenant(int tenantID, string tenantName, string canonicalHostNameLocal, string canonicalHostNameQa, string canonicalHostNameProd, DateTime reportingYearStartDate, bool useFiscalYears, bool usesTechnicalAssistanceParameters)
        {
            TenantID = tenantID;
            TenantName = tenantName;
            CanonicalHostNameLocal = canonicalHostNameLocal;
            CanonicalHostNameQa = canonicalHostNameQa;
            CanonicalHostNameProd = canonicalHostNameProd;
            ReportingYearStartDate = reportingYearStartDate;
            UseFiscalYears = useFiscalYears;
            UsesTechnicalAssistanceParameters = usesTechnicalAssistanceParameters;
        }

        [Key]
        public int TenantID { get; private set; }
        public string TenantName { get; private set; }
        public string CanonicalHostNameLocal { get; private set; }
        public string CanonicalHostNameQa { get; private set; }
        public string CanonicalHostNameProd { get; private set; }
        public DateTime ReportingYearStartDate { get; private set; }
        public bool UseFiscalYears { get; private set; }
        public bool UsesTechnicalAssistanceParameters { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return TenantID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(Tenant other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TenantID == TenantID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as Tenant);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return TenantID;
        }

        public static bool operator ==(Tenant left, Tenant right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Tenant left, Tenant right)
        {
            return !Equals(left, right);
        }

        public TenantEnum ToEnum { get { return (TenantEnum)GetHashCode(); } }

        public static Tenant ToType(int enumValue)
        {
            return ToType((TenantEnum)enumValue);
        }

        public static Tenant ToType(TenantEnum enumValue)
        {
            switch (enumValue)
            {
                case TenantEnum.WashingtonDepartmentOfNaturalResources:
                    return WashingtonDepartmentOfNaturalResources;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum TenantEnum
    {
        WashingtonDepartmentOfNaturalResources = 10
    }

    public partial class TenantWashingtonDepartmentOfNaturalResources : Tenant
    {
        private TenantWashingtonDepartmentOfNaturalResources(int tenantID, string tenantName, string canonicalHostNameLocal, string canonicalHostNameQa, string canonicalHostNameProd, DateTime reportingYearStartDate, bool useFiscalYears, bool usesTechnicalAssistanceParameters) : base(tenantID, tenantName, canonicalHostNameLocal, canonicalHostNameQa, canonicalHostNameProd, reportingYearStartDate, useFiscalYears, usesTechnicalAssistanceParameters) {}
        public static readonly TenantWashingtonDepartmentOfNaturalResources Instance = new TenantWashingtonDepartmentOfNaturalResources(10, @"WashingtonDepartmentOfNaturalResources", @"wadnrforesthealth.localhost.projectfirma.com", @"wadnrforesthealth.qa.projectfirma.com", @"foresthealthtracker.dnr.wa.gov", DateTime.Parse("01/01/1990"), false, false);
    }
}