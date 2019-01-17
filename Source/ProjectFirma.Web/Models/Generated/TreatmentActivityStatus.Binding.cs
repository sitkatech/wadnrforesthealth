//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentActivityStatus]
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
    public abstract partial class TreatmentActivityStatus : IHavePrimaryKey
    {
        public static readonly TreatmentActivityStatusPlanned Planned = TreatmentActivityStatusPlanned.Instance;
        public static readonly TreatmentActivityStatusCompleted Completed = TreatmentActivityStatusCompleted.Instance;

        public static readonly List<TreatmentActivityStatus> All;
        public static readonly ReadOnlyDictionary<int, TreatmentActivityStatus> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static TreatmentActivityStatus()
        {
            All = new List<TreatmentActivityStatus> { Planned, Completed };
            AllLookupDictionary = new ReadOnlyDictionary<int, TreatmentActivityStatus>(All.ToDictionary(x => x.TreatmentActivityStatusID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected TreatmentActivityStatus(int treatmentActivityStatusID, string treatmentActivityStatusName, string treatmentActivityStatusDisplayName)
        {
            TreatmentActivityStatusID = treatmentActivityStatusID;
            TreatmentActivityStatusName = treatmentActivityStatusName;
            TreatmentActivityStatusDisplayName = treatmentActivityStatusDisplayName;
        }

        [Key]
        public int TreatmentActivityStatusID { get; private set; }
        public string TreatmentActivityStatusName { get; private set; }
        public string TreatmentActivityStatusDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentActivityStatusID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(TreatmentActivityStatus other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TreatmentActivityStatusID == TreatmentActivityStatusID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as TreatmentActivityStatus);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return TreatmentActivityStatusID;
        }

        public static bool operator ==(TreatmentActivityStatus left, TreatmentActivityStatus right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TreatmentActivityStatus left, TreatmentActivityStatus right)
        {
            return !Equals(left, right);
        }

        public TreatmentActivityStatusEnum ToEnum { get { return (TreatmentActivityStatusEnum)GetHashCode(); } }

        public static TreatmentActivityStatus ToType(int enumValue)
        {
            return ToType((TreatmentActivityStatusEnum)enumValue);
        }

        public static TreatmentActivityStatus ToType(TreatmentActivityStatusEnum enumValue)
        {
            switch (enumValue)
            {
                case TreatmentActivityStatusEnum.Completed:
                    return Completed;
                case TreatmentActivityStatusEnum.Planned:
                    return Planned;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum TreatmentActivityStatusEnum
    {
        Planned = 1,
        Completed = 2
    }

    public partial class TreatmentActivityStatusPlanned : TreatmentActivityStatus
    {
        private TreatmentActivityStatusPlanned(int treatmentActivityStatusID, string treatmentActivityStatusName, string treatmentActivityStatusDisplayName) : base(treatmentActivityStatusID, treatmentActivityStatusName, treatmentActivityStatusDisplayName) {}
        public static readonly TreatmentActivityStatusPlanned Instance = new TreatmentActivityStatusPlanned(1, @"Planned", @"Planned");
    }

    public partial class TreatmentActivityStatusCompleted : TreatmentActivityStatus
    {
        private TreatmentActivityStatusCompleted(int treatmentActivityStatusID, string treatmentActivityStatusName, string treatmentActivityStatusDisplayName) : base(treatmentActivityStatusID, treatmentActivityStatusName, treatmentActivityStatusDisplayName) {}
        public static readonly TreatmentActivityStatusCompleted Instance = new TreatmentActivityStatusCompleted(2, @"Completed", @"Completed");
    }
}