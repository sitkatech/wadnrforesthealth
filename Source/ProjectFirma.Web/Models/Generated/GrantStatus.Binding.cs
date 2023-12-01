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
    public abstract partial class GrantStatus : IHavePrimaryKey
    {
        public static readonly GrantStatusActive Active = GrantStatusActive.Instance;
        public static readonly GrantStatusPending Pending = GrantStatusPending.Instance;
        public static readonly GrantStatusPlanned Planned = GrantStatusPlanned.Instance;
        public static readonly GrantStatusCloseout Closeout = GrantStatusCloseout.Instance;

        public static readonly List<GrantStatus> All;
        public static readonly ReadOnlyDictionary<int, GrantStatus> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static GrantStatus()
        {
            All = new List<GrantStatus> { Active, Pending, Planned, Closeout };
            AllLookupDictionary = new ReadOnlyDictionary<int, GrantStatus>(All.ToDictionary(x => x.GrantStatusID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected GrantStatus(int grantStatusID, string grantStatusName)
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
        public bool Equals(GrantStatus other)
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
            return Equals(obj as GrantStatus);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return GrantStatusID;
        }

        public static bool operator ==(GrantStatus left, GrantStatus right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GrantStatus left, GrantStatus right)
        {
            return !Equals(left, right);
        }

        public GrantStatusEnum ToEnum { get { return (GrantStatusEnum)GetHashCode(); } }

        public static GrantStatus ToType(int enumValue)
        {
            return ToType((GrantStatusEnum)enumValue);
        }

        public static GrantStatus ToType(GrantStatusEnum enumValue)
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

    public partial class GrantStatusActive : GrantStatus
    {
        private GrantStatusActive(int grantStatusID, string grantStatusName) : base(grantStatusID, grantStatusName) {}
        public static readonly GrantStatusActive Instance = new GrantStatusActive(1, @"Active");
    }

    public partial class GrantStatusPending : GrantStatus
    {
        private GrantStatusPending(int grantStatusID, string grantStatusName) : base(grantStatusID, grantStatusName) {}
        public static readonly GrantStatusPending Instance = new GrantStatusPending(2, @"Pending");
    }

    public partial class GrantStatusPlanned : GrantStatus
    {
        private GrantStatusPlanned(int grantStatusID, string grantStatusName) : base(grantStatusID, grantStatusName) {}
        public static readonly GrantStatusPlanned Instance = new GrantStatusPlanned(3, @"Planned");
    }

    public partial class GrantStatusCloseout : GrantStatus
    {
        private GrantStatusCloseout(int grantStatusID, string grantStatusName) : base(grantStatusID, grantStatusName) {}
        public static readonly GrantStatusCloseout Instance = new GrantStatusCloseout(4, @"Closeout");
    }
}