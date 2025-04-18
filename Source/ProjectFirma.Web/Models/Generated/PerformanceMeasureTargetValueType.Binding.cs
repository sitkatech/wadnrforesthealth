//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PerformanceMeasureTargetValueType]
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
    public abstract partial class PerformanceMeasureTargetValueType : IHavePrimaryKey
    {
        public static readonly PerformanceMeasureTargetValueTypeNoTarget NoTarget = PerformanceMeasureTargetValueTypeNoTarget.Instance;
        public static readonly PerformanceMeasureTargetValueTypeOverallTarget OverallTarget = PerformanceMeasureTargetValueTypeOverallTarget.Instance;
        public static readonly PerformanceMeasureTargetValueTypeTargetPerReportingPeriod TargetPerReportingPeriod = PerformanceMeasureTargetValueTypeTargetPerReportingPeriod.Instance;

        public static readonly List<PerformanceMeasureTargetValueType> All;
        public static readonly ReadOnlyDictionary<int, PerformanceMeasureTargetValueType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static PerformanceMeasureTargetValueType()
        {
            All = new List<PerformanceMeasureTargetValueType> { NoTarget, OverallTarget, TargetPerReportingPeriod };
            AllLookupDictionary = new ReadOnlyDictionary<int, PerformanceMeasureTargetValueType>(All.ToDictionary(x => x.PerformanceMeasureTargetValueTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected PerformanceMeasureTargetValueType(int performanceMeasureTargetValueTypeID, string performanceMeasureTargetValueTypeName, string performanceMeasureTargetValueTypeDisplayName)
        {
            PerformanceMeasureTargetValueTypeID = performanceMeasureTargetValueTypeID;
            PerformanceMeasureTargetValueTypeName = performanceMeasureTargetValueTypeName;
            PerformanceMeasureTargetValueTypeDisplayName = performanceMeasureTargetValueTypeDisplayName;
        }

        [Key]
        public int PerformanceMeasureTargetValueTypeID { get; private set; }
        public string PerformanceMeasureTargetValueTypeName { get; private set; }
        public string PerformanceMeasureTargetValueTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return PerformanceMeasureTargetValueTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(PerformanceMeasureTargetValueType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.PerformanceMeasureTargetValueTypeID == PerformanceMeasureTargetValueTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as PerformanceMeasureTargetValueType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return PerformanceMeasureTargetValueTypeID;
        }

        public static bool operator ==(PerformanceMeasureTargetValueType left, PerformanceMeasureTargetValueType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PerformanceMeasureTargetValueType left, PerformanceMeasureTargetValueType right)
        {
            return !Equals(left, right);
        }

        public PerformanceMeasureTargetValueTypeEnum ToEnum { get { return (PerformanceMeasureTargetValueTypeEnum)GetHashCode(); } }

        public static PerformanceMeasureTargetValueType ToType(int enumValue)
        {
            return ToType((PerformanceMeasureTargetValueTypeEnum)enumValue);
        }

        public static PerformanceMeasureTargetValueType ToType(PerformanceMeasureTargetValueTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case PerformanceMeasureTargetValueTypeEnum.NoTarget:
                    return NoTarget;
                case PerformanceMeasureTargetValueTypeEnum.OverallTarget:
                    return OverallTarget;
                case PerformanceMeasureTargetValueTypeEnum.TargetPerReportingPeriod:
                    return TargetPerReportingPeriod;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum PerformanceMeasureTargetValueTypeEnum
    {
        NoTarget = 1,
        OverallTarget = 2,
        TargetPerReportingPeriod = 3
    }

    public partial class PerformanceMeasureTargetValueTypeNoTarget : PerformanceMeasureTargetValueType
    {
        private PerformanceMeasureTargetValueTypeNoTarget(int performanceMeasureTargetValueTypeID, string performanceMeasureTargetValueTypeName, string performanceMeasureTargetValueTypeDisplayName) : base(performanceMeasureTargetValueTypeID, performanceMeasureTargetValueTypeName, performanceMeasureTargetValueTypeDisplayName) {}
        public static readonly PerformanceMeasureTargetValueTypeNoTarget Instance = new PerformanceMeasureTargetValueTypeNoTarget(1, @"NoTarget", @"No Target");
    }

    public partial class PerformanceMeasureTargetValueTypeOverallTarget : PerformanceMeasureTargetValueType
    {
        private PerformanceMeasureTargetValueTypeOverallTarget(int performanceMeasureTargetValueTypeID, string performanceMeasureTargetValueTypeName, string performanceMeasureTargetValueTypeDisplayName) : base(performanceMeasureTargetValueTypeID, performanceMeasureTargetValueTypeName, performanceMeasureTargetValueTypeDisplayName) {}
        public static readonly PerformanceMeasureTargetValueTypeOverallTarget Instance = new PerformanceMeasureTargetValueTypeOverallTarget(2, @"OverallTarget", @"Overall Target");
    }

    public partial class PerformanceMeasureTargetValueTypeTargetPerReportingPeriod : PerformanceMeasureTargetValueType
    {
        private PerformanceMeasureTargetValueTypeTargetPerReportingPeriod(int performanceMeasureTargetValueTypeID, string performanceMeasureTargetValueTypeName, string performanceMeasureTargetValueTypeDisplayName) : base(performanceMeasureTargetValueTypeID, performanceMeasureTargetValueTypeName, performanceMeasureTargetValueTypeDisplayName) {}
        public static readonly PerformanceMeasureTargetValueTypeTargetPerReportingPeriod Instance = new PerformanceMeasureTargetValueTypeTargetPerReportingPeriod(3, @"TargetPerReportingPeriod", @"Target per Reporting Period");
    }
}