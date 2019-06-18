//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardContractorInvoiceType]
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
    public abstract partial class GrantAllocationAwardContractorInvoiceType : IHavePrimaryKey
    {
        public static readonly GrantAllocationAwardContractorInvoiceTypeHourly Hourly = GrantAllocationAwardContractorInvoiceTypeHourly.Instance;
        public static readonly GrantAllocationAwardContractorInvoiceTypeOther Other = GrantAllocationAwardContractorInvoiceTypeOther.Instance;

        public static readonly List<GrantAllocationAwardContractorInvoiceType> All;
        public static readonly ReadOnlyDictionary<int, GrantAllocationAwardContractorInvoiceType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static GrantAllocationAwardContractorInvoiceType()
        {
            All = new List<GrantAllocationAwardContractorInvoiceType> { Hourly, Other };
            AllLookupDictionary = new ReadOnlyDictionary<int, GrantAllocationAwardContractorInvoiceType>(All.ToDictionary(x => x.GrantAllocationAwardContractorInvoiceTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected GrantAllocationAwardContractorInvoiceType(int grantAllocationAwardContractorInvoiceTypeID, string grantAllocationAwardContractorInvoiceTypeName, string grantAllocationAwardContractorInvoiceDisplayName)
        {
            GrantAllocationAwardContractorInvoiceTypeID = grantAllocationAwardContractorInvoiceTypeID;
            GrantAllocationAwardContractorInvoiceTypeName = grantAllocationAwardContractorInvoiceTypeName;
            GrantAllocationAwardContractorInvoiceDisplayName = grantAllocationAwardContractorInvoiceDisplayName;
        }

        [Key]
        public int GrantAllocationAwardContractorInvoiceTypeID { get; private set; }
        public string GrantAllocationAwardContractorInvoiceTypeName { get; private set; }
        public string GrantAllocationAwardContractorInvoiceDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationAwardContractorInvoiceTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(GrantAllocationAwardContractorInvoiceType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.GrantAllocationAwardContractorInvoiceTypeID == GrantAllocationAwardContractorInvoiceTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as GrantAllocationAwardContractorInvoiceType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return GrantAllocationAwardContractorInvoiceTypeID;
        }

        public static bool operator ==(GrantAllocationAwardContractorInvoiceType left, GrantAllocationAwardContractorInvoiceType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GrantAllocationAwardContractorInvoiceType left, GrantAllocationAwardContractorInvoiceType right)
        {
            return !Equals(left, right);
        }

        public GrantAllocationAwardContractorInvoiceTypeEnum ToEnum { get { return (GrantAllocationAwardContractorInvoiceTypeEnum)GetHashCode(); } }

        public static GrantAllocationAwardContractorInvoiceType ToType(int enumValue)
        {
            return ToType((GrantAllocationAwardContractorInvoiceTypeEnum)enumValue);
        }

        public static GrantAllocationAwardContractorInvoiceType ToType(GrantAllocationAwardContractorInvoiceTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case GrantAllocationAwardContractorInvoiceTypeEnum.Hourly:
                    return Hourly;
                case GrantAllocationAwardContractorInvoiceTypeEnum.Other:
                    return Other;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum GrantAllocationAwardContractorInvoiceTypeEnum
    {
        Hourly = 1,
        Other = 2
    }

    public partial class GrantAllocationAwardContractorInvoiceTypeHourly : GrantAllocationAwardContractorInvoiceType
    {
        private GrantAllocationAwardContractorInvoiceTypeHourly(int grantAllocationAwardContractorInvoiceTypeID, string grantAllocationAwardContractorInvoiceTypeName, string grantAllocationAwardContractorInvoiceDisplayName) : base(grantAllocationAwardContractorInvoiceTypeID, grantAllocationAwardContractorInvoiceTypeName, grantAllocationAwardContractorInvoiceDisplayName) {}
        public static readonly GrantAllocationAwardContractorInvoiceTypeHourly Instance = new GrantAllocationAwardContractorInvoiceTypeHourly(1, @"Hourly", @"Hourly");
    }

    public partial class GrantAllocationAwardContractorInvoiceTypeOther : GrantAllocationAwardContractorInvoiceType
    {
        private GrantAllocationAwardContractorInvoiceTypeOther(int grantAllocationAwardContractorInvoiceTypeID, string grantAllocationAwardContractorInvoiceTypeName, string grantAllocationAwardContractorInvoiceDisplayName) : base(grantAllocationAwardContractorInvoiceTypeID, grantAllocationAwardContractorInvoiceTypeName, grantAllocationAwardContractorInvoiceDisplayName) {}
        public static readonly GrantAllocationAwardContractorInvoiceTypeOther Instance = new GrantAllocationAwardContractorInvoiceTypeOther(2, @"Other", @"Other");
    }
}