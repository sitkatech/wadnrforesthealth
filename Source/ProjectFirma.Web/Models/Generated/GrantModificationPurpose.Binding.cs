//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantModificationPurpose]
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
    public abstract partial class GrantModificationPurpose : IHavePrimaryKey
    {
        public static readonly GrantModificationPurposeChangeinPerformancePeriod ChangeinPerformancePeriod = GrantModificationPurposeChangeinPerformancePeriod.Instance;
        public static readonly GrantModificationPurposeAdministrativeChanges AdministrativeChanges = GrantModificationPurposeAdministrativeChanges.Instance;
        public static readonly GrantModificationPurposeChangeinFunding ChangeinFunding = GrantModificationPurposeChangeinFunding.Instance;
        public static readonly GrantModificationPurposeOther Other = GrantModificationPurposeOther.Instance;
        public static readonly GrantModificationPurposeInitialAward InitialAward = GrantModificationPurposeInitialAward.Instance;

        public static readonly List<GrantModificationPurpose> All;
        public static readonly ReadOnlyDictionary<int, GrantModificationPurpose> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static GrantModificationPurpose()
        {
            All = new List<GrantModificationPurpose> { ChangeinPerformancePeriod, AdministrativeChanges, ChangeinFunding, Other, InitialAward };
            AllLookupDictionary = new ReadOnlyDictionary<int, GrantModificationPurpose>(All.ToDictionary(x => x.GrantModificationPurposeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected GrantModificationPurpose(int grantModificationPurposeID, string grantModificationPurposeName)
        {
            GrantModificationPurposeID = grantModificationPurposeID;
            GrantModificationPurposeName = grantModificationPurposeName;
        }

        [Key]
        public int GrantModificationPurposeID { get; private set; }
        public string GrantModificationPurposeName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantModificationPurposeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(GrantModificationPurpose other)
        {
            if (other == null)
            {
                return false;
            }
            return other.GrantModificationPurposeID == GrantModificationPurposeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as GrantModificationPurpose);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return GrantModificationPurposeID;
        }

        public static bool operator ==(GrantModificationPurpose left, GrantModificationPurpose right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GrantModificationPurpose left, GrantModificationPurpose right)
        {
            return !Equals(left, right);
        }

        public GrantModificationPurposeEnum ToEnum { get { return (GrantModificationPurposeEnum)GetHashCode(); } }

        public static GrantModificationPurpose ToType(int enumValue)
        {
            return ToType((GrantModificationPurposeEnum)enumValue);
        }

        public static GrantModificationPurpose ToType(GrantModificationPurposeEnum enumValue)
        {
            switch (enumValue)
            {
                case GrantModificationPurposeEnum.AdministrativeChanges:
                    return AdministrativeChanges;
                case GrantModificationPurposeEnum.ChangeinFunding:
                    return ChangeinFunding;
                case GrantModificationPurposeEnum.ChangeinPerformancePeriod:
                    return ChangeinPerformancePeriod;
                case GrantModificationPurposeEnum.InitialAward:
                    return InitialAward;
                case GrantModificationPurposeEnum.Other:
                    return Other;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum GrantModificationPurposeEnum
    {
        ChangeinPerformancePeriod = 1,
        AdministrativeChanges = 2,
        ChangeinFunding = 3,
        Other = 4,
        InitialAward = 5
    }

    public partial class GrantModificationPurposeChangeinPerformancePeriod : GrantModificationPurpose
    {
        private GrantModificationPurposeChangeinPerformancePeriod(int grantModificationPurposeID, string grantModificationPurposeName) : base(grantModificationPurposeID, grantModificationPurposeName) {}
        public static readonly GrantModificationPurposeChangeinPerformancePeriod Instance = new GrantModificationPurposeChangeinPerformancePeriod(1, @"Change in Performance Period");
    }

    public partial class GrantModificationPurposeAdministrativeChanges : GrantModificationPurpose
    {
        private GrantModificationPurposeAdministrativeChanges(int grantModificationPurposeID, string grantModificationPurposeName) : base(grantModificationPurposeID, grantModificationPurposeName) {}
        public static readonly GrantModificationPurposeAdministrativeChanges Instance = new GrantModificationPurposeAdministrativeChanges(2, @"Administrative Changes");
    }

    public partial class GrantModificationPurposeChangeinFunding : GrantModificationPurpose
    {
        private GrantModificationPurposeChangeinFunding(int grantModificationPurposeID, string grantModificationPurposeName) : base(grantModificationPurposeID, grantModificationPurposeName) {}
        public static readonly GrantModificationPurposeChangeinFunding Instance = new GrantModificationPurposeChangeinFunding(3, @"Change in Funding");
    }

    public partial class GrantModificationPurposeOther : GrantModificationPurpose
    {
        private GrantModificationPurposeOther(int grantModificationPurposeID, string grantModificationPurposeName) : base(grantModificationPurposeID, grantModificationPurposeName) {}
        public static readonly GrantModificationPurposeOther Instance = new GrantModificationPurposeOther(4, @"Other");
    }

    public partial class GrantModificationPurposeInitialAward : GrantModificationPurpose
    {
        private GrantModificationPurposeInitialAward(int grantModificationPurposeID, string grantModificationPurposeName) : base(grantModificationPurposeID, grantModificationPurposeName) {}
        public static readonly GrantModificationPurposeInitialAward Instance = new GrantModificationPurposeInitialAward(5, @"Initial Award");
    }
}