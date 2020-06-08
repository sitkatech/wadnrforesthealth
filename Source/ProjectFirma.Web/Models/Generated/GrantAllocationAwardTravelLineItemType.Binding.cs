//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardTravelLineItemType]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public abstract partial class GrantAllocationAwardTravelLineItemType : IHavePrimaryKey
    {
        public static readonly GrantAllocationAwardTravelLineItemTypeTransportation Transportation = GrantAllocationAwardTravelLineItemTypeTransportation.Instance;
        public static readonly GrantAllocationAwardTravelLineItemTypeOther Other = GrantAllocationAwardTravelLineItemTypeOther.Instance;

        public static readonly List<GrantAllocationAwardTravelLineItemType> All;
        public static readonly ReadOnlyDictionary<int, GrantAllocationAwardTravelLineItemType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static GrantAllocationAwardTravelLineItemType()
        {
            All = new List<GrantAllocationAwardTravelLineItemType> { Transportation, Other };
            AllLookupDictionary = new ReadOnlyDictionary<int, GrantAllocationAwardTravelLineItemType>(All.ToDictionary(x => x.GrantAllocationAwardTravelLineItemTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected GrantAllocationAwardTravelLineItemType(int grantAllocationAwardTravelLineItemTypeID, string grantAllocationAwardTravelLineItemTypeName, string grantAllocationAwardTravelLineItemTypeDisplayName)
        {
            GrantAllocationAwardTravelLineItemTypeID = grantAllocationAwardTravelLineItemTypeID;
            GrantAllocationAwardTravelLineItemTypeName = grantAllocationAwardTravelLineItemTypeName;
            GrantAllocationAwardTravelLineItemTypeDisplayName = grantAllocationAwardTravelLineItemTypeDisplayName;
        }

        [Key]
        public int GrantAllocationAwardTravelLineItemTypeID { get; private set; }
        public string GrantAllocationAwardTravelLineItemTypeName { get; private set; }
        public string GrantAllocationAwardTravelLineItemTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationAwardTravelLineItemTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(GrantAllocationAwardTravelLineItemType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.GrantAllocationAwardTravelLineItemTypeID == GrantAllocationAwardTravelLineItemTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as GrantAllocationAwardTravelLineItemType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return GrantAllocationAwardTravelLineItemTypeID;
        }

        public static bool operator ==(GrantAllocationAwardTravelLineItemType left, GrantAllocationAwardTravelLineItemType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GrantAllocationAwardTravelLineItemType left, GrantAllocationAwardTravelLineItemType right)
        {
            return !Equals(left, right);
        }

        public GrantAllocationAwardTravelLineItemTypeEnum ToEnum { get { return (GrantAllocationAwardTravelLineItemTypeEnum)GetHashCode(); } }

        public static GrantAllocationAwardTravelLineItemType ToType(int enumValue)
        {
            return ToType((GrantAllocationAwardTravelLineItemTypeEnum)enumValue);
        }

        public static GrantAllocationAwardTravelLineItemType ToType(GrantAllocationAwardTravelLineItemTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case GrantAllocationAwardTravelLineItemTypeEnum.Other:
                    return Other;
                case GrantAllocationAwardTravelLineItemTypeEnum.Transportation:
                    return Transportation;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum GrantAllocationAwardTravelLineItemTypeEnum
    {
        Transportation = 1,
        Other = 2
    }

    public partial class GrantAllocationAwardTravelLineItemTypeTransportation : GrantAllocationAwardTravelLineItemType
    {
        private GrantAllocationAwardTravelLineItemTypeTransportation(int grantAllocationAwardTravelLineItemTypeID, string grantAllocationAwardTravelLineItemTypeName, string grantAllocationAwardTravelLineItemTypeDisplayName) : base(grantAllocationAwardTravelLineItemTypeID, grantAllocationAwardTravelLineItemTypeName, grantAllocationAwardTravelLineItemTypeDisplayName) {}
        public static readonly GrantAllocationAwardTravelLineItemTypeTransportation Instance = new GrantAllocationAwardTravelLineItemTypeTransportation(1, @"Transportation", @"Transportation");
    }

    public partial class GrantAllocationAwardTravelLineItemTypeOther : GrantAllocationAwardTravelLineItemType
    {
        private GrantAllocationAwardTravelLineItemTypeOther(int grantAllocationAwardTravelLineItemTypeID, string grantAllocationAwardTravelLineItemTypeName, string grantAllocationAwardTravelLineItemTypeDisplayName) : base(grantAllocationAwardTravelLineItemTypeID, grantAllocationAwardTravelLineItemTypeName, grantAllocationAwardTravelLineItemTypeDisplayName) {}
        public static readonly GrantAllocationAwardTravelLineItemTypeOther Instance = new GrantAllocationAwardTravelLineItemTypeOther(2, @"Other", @"Other");
    }
}