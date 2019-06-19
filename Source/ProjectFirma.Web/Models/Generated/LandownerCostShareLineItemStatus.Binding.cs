//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandownerCostShareLineItemStatus]
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
    public abstract partial class LandownerCostShareLineItemStatus : IHavePrimaryKey
    {
        public static readonly LandownerCostShareLineItemStatusPlanned Planned = LandownerCostShareLineItemStatusPlanned.Instance;
        public static readonly LandownerCostShareLineItemStatusCompleted Completed = LandownerCostShareLineItemStatusCompleted.Instance;
        public static readonly LandownerCostShareLineItemStatusCancelled Cancelled = LandownerCostShareLineItemStatusCancelled.Instance;

        public static readonly List<LandownerCostShareLineItemStatus> All;
        public static readonly ReadOnlyDictionary<int, LandownerCostShareLineItemStatus> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static LandownerCostShareLineItemStatus()
        {
            All = new List<LandownerCostShareLineItemStatus> { Planned, Completed, Cancelled };
            AllLookupDictionary = new ReadOnlyDictionary<int, LandownerCostShareLineItemStatus>(All.ToDictionary(x => x.LandownerCostShareLineItemStatusID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected LandownerCostShareLineItemStatus(int landownerCostShareLineItemStatusID, string landownerCostShareLineItemStatusName, string landownerCostShareLineItemStatusDisplayName)
        {
            LandownerCostShareLineItemStatusID = landownerCostShareLineItemStatusID;
            LandownerCostShareLineItemStatusName = landownerCostShareLineItemStatusName;
            LandownerCostShareLineItemStatusDisplayName = landownerCostShareLineItemStatusDisplayName;
        }

        [Key]
        public int LandownerCostShareLineItemStatusID { get; private set; }
        public string LandownerCostShareLineItemStatusName { get; private set; }
        public string LandownerCostShareLineItemStatusDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return LandownerCostShareLineItemStatusID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(LandownerCostShareLineItemStatus other)
        {
            if (other == null)
            {
                return false;
            }
            return other.LandownerCostShareLineItemStatusID == LandownerCostShareLineItemStatusID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as LandownerCostShareLineItemStatus);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return LandownerCostShareLineItemStatusID;
        }

        public static bool operator ==(LandownerCostShareLineItemStatus left, LandownerCostShareLineItemStatus right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LandownerCostShareLineItemStatus left, LandownerCostShareLineItemStatus right)
        {
            return !Equals(left, right);
        }

        public LandownerCostShareLineItemStatusEnum ToEnum { get { return (LandownerCostShareLineItemStatusEnum)GetHashCode(); } }

        public static LandownerCostShareLineItemStatus ToType(int enumValue)
        {
            return ToType((LandownerCostShareLineItemStatusEnum)enumValue);
        }

        public static LandownerCostShareLineItemStatus ToType(LandownerCostShareLineItemStatusEnum enumValue)
        {
            switch (enumValue)
            {
                case LandownerCostShareLineItemStatusEnum.Cancelled:
                    return Cancelled;
                case LandownerCostShareLineItemStatusEnum.Completed:
                    return Completed;
                case LandownerCostShareLineItemStatusEnum.Planned:
                    return Planned;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum LandownerCostShareLineItemStatusEnum
    {
        Planned = 1,
        Completed = 2,
        Cancelled = 3
    }

    public partial class LandownerCostShareLineItemStatusPlanned : LandownerCostShareLineItemStatus
    {
        private LandownerCostShareLineItemStatusPlanned(int landownerCostShareLineItemStatusID, string landownerCostShareLineItemStatusName, string landownerCostShareLineItemStatusDisplayName) : base(landownerCostShareLineItemStatusID, landownerCostShareLineItemStatusName, landownerCostShareLineItemStatusDisplayName) {}
        public static readonly LandownerCostShareLineItemStatusPlanned Instance = new LandownerCostShareLineItemStatusPlanned(1, @"Planned", @"Planned");
    }

    public partial class LandownerCostShareLineItemStatusCompleted : LandownerCostShareLineItemStatus
    {
        private LandownerCostShareLineItemStatusCompleted(int landownerCostShareLineItemStatusID, string landownerCostShareLineItemStatusName, string landownerCostShareLineItemStatusDisplayName) : base(landownerCostShareLineItemStatusID, landownerCostShareLineItemStatusName, landownerCostShareLineItemStatusDisplayName) {}
        public static readonly LandownerCostShareLineItemStatusCompleted Instance = new LandownerCostShareLineItemStatusCompleted(2, @"Completed", @"Completed");
    }

    public partial class LandownerCostShareLineItemStatusCancelled : LandownerCostShareLineItemStatus
    {
        private LandownerCostShareLineItemStatusCancelled(int landownerCostShareLineItemStatusID, string landownerCostShareLineItemStatusName, string landownerCostShareLineItemStatusDisplayName) : base(landownerCostShareLineItemStatusID, landownerCostShareLineItemStatusName, landownerCostShareLineItemStatusDisplayName) {}
        public static readonly LandownerCostShareLineItemStatusCancelled Instance = new LandownerCostShareLineItemStatusCancelled(3, @"Cancelled", @"Cancelled");
    }
}