//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SocrataDataMartRawJsonImportTableType]
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
    public abstract partial class SocrataDataMartRawJsonImportTableType : IHavePrimaryKey
    {
        public static readonly SocrataDataMartRawJsonImportTableTypeVendor Vendor = SocrataDataMartRawJsonImportTableTypeVendor.Instance;
        public static readonly SocrataDataMartRawJsonImportTableTypePayment Payment = SocrataDataMartRawJsonImportTableTypePayment.Instance;
        public static readonly SocrataDataMartRawJsonImportTableTypePumpkinSpice PumpkinSpice = SocrataDataMartRawJsonImportTableTypePumpkinSpice.Instance;

        public static readonly List<SocrataDataMartRawJsonImportTableType> All;
        public static readonly ReadOnlyDictionary<int, SocrataDataMartRawJsonImportTableType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static SocrataDataMartRawJsonImportTableType()
        {
            All = new List<SocrataDataMartRawJsonImportTableType> { Vendor, Payment, PumpkinSpice };
            AllLookupDictionary = new ReadOnlyDictionary<int, SocrataDataMartRawJsonImportTableType>(All.ToDictionary(x => x.SocrataDataMartRawJsonImportTableTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected SocrataDataMartRawJsonImportTableType(int socrataDataMartRawJsonImportTableTypeID, string socrataDataMartRawJsonImportTableTypeName)
        {
            SocrataDataMartRawJsonImportTableTypeID = socrataDataMartRawJsonImportTableTypeID;
            SocrataDataMartRawJsonImportTableTypeName = socrataDataMartRawJsonImportTableTypeName;
        }

        [Key]
        public int SocrataDataMartRawJsonImportTableTypeID { get; private set; }
        public string SocrataDataMartRawJsonImportTableTypeName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return SocrataDataMartRawJsonImportTableTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(SocrataDataMartRawJsonImportTableType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.SocrataDataMartRawJsonImportTableTypeID == SocrataDataMartRawJsonImportTableTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as SocrataDataMartRawJsonImportTableType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return SocrataDataMartRawJsonImportTableTypeID;
        }

        public static bool operator ==(SocrataDataMartRawJsonImportTableType left, SocrataDataMartRawJsonImportTableType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SocrataDataMartRawJsonImportTableType left, SocrataDataMartRawJsonImportTableType right)
        {
            return !Equals(left, right);
        }

        public SocrataDataMartRawJsonImportTableTypeEnum ToEnum { get { return (SocrataDataMartRawJsonImportTableTypeEnum)GetHashCode(); } }

        public static SocrataDataMartRawJsonImportTableType ToType(int enumValue)
        {
            return ToType((SocrataDataMartRawJsonImportTableTypeEnum)enumValue);
        }

        public static SocrataDataMartRawJsonImportTableType ToType(SocrataDataMartRawJsonImportTableTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case SocrataDataMartRawJsonImportTableTypeEnum.Payment:
                    return Payment;
                case SocrataDataMartRawJsonImportTableTypeEnum.PumpkinSpice:
                    return PumpkinSpice;
                case SocrataDataMartRawJsonImportTableTypeEnum.Vendor:
                    return Vendor;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum SocrataDataMartRawJsonImportTableTypeEnum
    {
        Vendor = 1,
        Payment = 2,
        PumpkinSpice = 3
    }

    public partial class SocrataDataMartRawJsonImportTableTypeVendor : SocrataDataMartRawJsonImportTableType
    {
        private SocrataDataMartRawJsonImportTableTypeVendor(int socrataDataMartRawJsonImportTableTypeID, string socrataDataMartRawJsonImportTableTypeName) : base(socrataDataMartRawJsonImportTableTypeID, socrataDataMartRawJsonImportTableTypeName) {}
        public static readonly SocrataDataMartRawJsonImportTableTypeVendor Instance = new SocrataDataMartRawJsonImportTableTypeVendor(1, @"Vendor");
    }

    public partial class SocrataDataMartRawJsonImportTableTypePayment : SocrataDataMartRawJsonImportTableType
    {
        private SocrataDataMartRawJsonImportTableTypePayment(int socrataDataMartRawJsonImportTableTypeID, string socrataDataMartRawJsonImportTableTypeName) : base(socrataDataMartRawJsonImportTableTypeID, socrataDataMartRawJsonImportTableTypeName) {}
        public static readonly SocrataDataMartRawJsonImportTableTypePayment Instance = new SocrataDataMartRawJsonImportTableTypePayment(2, @"Payment");
    }

    public partial class SocrataDataMartRawJsonImportTableTypePumpkinSpice : SocrataDataMartRawJsonImportTableType
    {
        private SocrataDataMartRawJsonImportTableTypePumpkinSpice(int socrataDataMartRawJsonImportTableTypeID, string socrataDataMartRawJsonImportTableTypeName) : base(socrataDataMartRawJsonImportTableTypeID, socrataDataMartRawJsonImportTableTypeName) {}
        public static readonly SocrataDataMartRawJsonImportTableTypePumpkinSpice Instance = new SocrataDataMartRawJsonImportTableTypePumpkinSpice(3, @"PumpkinSpice");
    }
}