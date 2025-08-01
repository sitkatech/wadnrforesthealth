//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantStatus]
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
    public abstract partial class FundSourceStatus : IHavePrimaryKey
    {
        public static readonly FundSourceStatusActive Active = FundSourceStatusActive.Instance;
        public static readonly FundSourceStatusPending Pending = FundSourceStatusPending.Instance;
        public static readonly FundSourceStatusPlanned Planned = FundSourceStatusPlanned.Instance;
        public static readonly FundSourceStatusCloseout Closeout = FundSourceStatusCloseout.Instance;

        public static readonly List<FundSourceStatus> All;
        public static readonly ReadOnlyDictionary<int, FundSourceStatus> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static FundSourceStatus()
        {
            All = new List<FundSourceStatus> { Active, Pending, Planned, Closeout };
            AllLookupDictionary = new ReadOnlyDictionary<int, FundSourceStatus>(All.ToDictionary(x => x.GrantStatusID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected FundSourceStatus(int grantStatusID, string grantStatusName)
        {
            GrantStatusID = grantStatusID;
            GrantStatusName = grantStatusName;
        }

        [Key]
        public int GrantStatusID { get; private set; }
        public string GrantStatusName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantStatusID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(FundSourceStatus other)
        {
            if (other == null)
            {
                return false;
            }
            return other.GrantStatusID == GrantStatusID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as FundSourceStatus);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return GrantStatusID;
        }

        public static bool operator ==(FundSourceStatus left, FundSourceStatus right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FundSourceStatus left, FundSourceStatus right)
        {
            return !Equals(left, right);
        }

        public GrantStatusEnum ToEnum { get { return (GrantStatusEnum)GetHashCode(); } }

        public static FundSourceStatus ToType(int enumValue)
        {
            return ToType((GrantStatusEnum)enumValue);
        }

        public static FundSourceStatus ToType(GrantStatusEnum enumValue)
        {
            switch (enumValue)
            {
                case GrantStatusEnum.Active:
                    return Active;
                case GrantStatusEnum.Closeout:
                    return Closeout;
                case GrantStatusEnum.Pending:
                    return Pending;
                case GrantStatusEnum.Planned:
                    return Planned;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum GrantStatusEnum
    {
        Active = 1,
        Pending = 2,
        Planned = 3,
        Closeout = 4
    }

    public partial class FundSourceStatusActive : FundSourceStatus
    {
        private FundSourceStatusActive(int grantStatusID, string grantStatusName) : base(grantStatusID, grantStatusName) {}
        public static readonly FundSourceStatusActive Instance = new FundSourceStatusActive(1, @"Active");
    }

    public partial class FundSourceStatusPending : FundSourceStatus
    {
        private FundSourceStatusPending(int grantStatusID, string grantStatusName) : base(grantStatusID, grantStatusName) {}
        public static readonly FundSourceStatusPending Instance = new FundSourceStatusPending(2, @"Pending");
    }

    public partial class FundSourceStatusPlanned : FundSourceStatus
    {
        private FundSourceStatusPlanned(int grantStatusID, string grantStatusName) : base(grantStatusID, grantStatusName) {}
        public static readonly FundSourceStatusPlanned Instance = new FundSourceStatusPlanned(3, @"Planned");
    }

    public partial class FundSourceStatusCloseout : FundSourceStatus
    {
        private FundSourceStatusCloseout(int grantStatusID, string grantStatusName) : base(grantStatusID, grantStatusName) {}
        public static readonly FundSourceStatusCloseout Instance = new FundSourceStatusCloseout(4, @"Closeout");
    }
}