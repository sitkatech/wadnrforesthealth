//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentType]
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
    public abstract partial class TreatmentType : IHavePrimaryKey
    {
        public static readonly TreatmentTypeChipping Chipping = TreatmentTypeChipping.Instance;
        public static readonly TreatmentTypePruning Pruning = TreatmentTypePruning.Instance;
        public static readonly TreatmentTypeThinning Thinning = TreatmentTypeThinning.Instance;
        public static readonly TreatmentTypeMastication Mastication = TreatmentTypeMastication.Instance;
        public static readonly TreatmentTypeGrazing Grazing = TreatmentTypeGrazing.Instance;
        public static readonly TreatmentTypeLopAndScatter LopAndScatter = TreatmentTypeLopAndScatter.Instance;
        public static readonly TreatmentTypeBiomassRemoval BiomassRemoval = TreatmentTypeBiomassRemoval.Instance;
        public static readonly TreatmentTypeHandPile HandPile = TreatmentTypeHandPile.Instance;
        public static readonly TreatmentTypeBroadcastBurn BroadcastBurn = TreatmentTypeBroadcastBurn.Instance;
        public static readonly TreatmentTypeHandPileBurn HandPileBurn = TreatmentTypeHandPileBurn.Instance;
        public static readonly TreatmentTypeMachinePileBurn MachinePileBurn = TreatmentTypeMachinePileBurn.Instance;
        public static readonly TreatmentTypeSlash Slash = TreatmentTypeSlash.Instance;
        public static readonly TreatmentTypeOther Other = TreatmentTypeOther.Instance;
        public static readonly TreatmentTypeJackpotBurn JackpotBurn = TreatmentTypeJackpotBurn.Instance;
        public static readonly TreatmentTypeMachinePile MachinePile = TreatmentTypeMachinePile.Instance;

        public static readonly List<TreatmentType> All;
        public static readonly ReadOnlyDictionary<int, TreatmentType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static TreatmentType()
        {
            All = new List<TreatmentType> { Chipping, Pruning, Thinning, Mastication, Grazing, LopAndScatter, BiomassRemoval, HandPile, BroadcastBurn, HandPileBurn, MachinePileBurn, Slash, Other, JackpotBurn, MachinePile };
            AllLookupDictionary = new ReadOnlyDictionary<int, TreatmentType>(All.ToDictionary(x => x.TreatmentTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected TreatmentType(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName)
        {
            TreatmentTypeID = treatmentTypeID;
            TreatmentTypeName = treatmentTypeName;
            TreatmentTypeDisplayName = treatmentTypeDisplayName;
        }

        [Key]
        public int TreatmentTypeID { get; private set; }
        public string TreatmentTypeName { get; private set; }
        public string TreatmentTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(TreatmentType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TreatmentTypeID == TreatmentTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as TreatmentType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return TreatmentTypeID;
        }

        public static bool operator ==(TreatmentType left, TreatmentType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TreatmentType left, TreatmentType right)
        {
            return !Equals(left, right);
        }

        public TreatmentTypeEnum ToEnum { get { return (TreatmentTypeEnum)GetHashCode(); } }

        public static TreatmentType ToType(int enumValue)
        {
            return ToType((TreatmentTypeEnum)enumValue);
        }

        public static TreatmentType ToType(TreatmentTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case TreatmentTypeEnum.BiomassRemoval:
                    return BiomassRemoval;
                case TreatmentTypeEnum.BroadcastBurn:
                    return BroadcastBurn;
                case TreatmentTypeEnum.Chipping:
                    return Chipping;
                case TreatmentTypeEnum.Grazing:
                    return Grazing;
                case TreatmentTypeEnum.HandPile:
                    return HandPile;
                case TreatmentTypeEnum.HandPileBurn:
                    return HandPileBurn;
                case TreatmentTypeEnum.JackpotBurn:
                    return JackpotBurn;
                case TreatmentTypeEnum.LopAndScatter:
                    return LopAndScatter;
                case TreatmentTypeEnum.MachinePile:
                    return MachinePile;
                case TreatmentTypeEnum.MachinePileBurn:
                    return MachinePileBurn;
                case TreatmentTypeEnum.Mastication:
                    return Mastication;
                case TreatmentTypeEnum.Other:
                    return Other;
                case TreatmentTypeEnum.Pruning:
                    return Pruning;
                case TreatmentTypeEnum.Slash:
                    return Slash;
                case TreatmentTypeEnum.Thinning:
                    return Thinning;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum TreatmentTypeEnum
    {
        Chipping = 1,
        Pruning = 2,
        Thinning = 3,
        Mastication = 4,
        Grazing = 5,
        LopAndScatter = 6,
        BiomassRemoval = 7,
        HandPile = 8,
        BroadcastBurn = 9,
        HandPileBurn = 10,
        MachinePileBurn = 11,
        Slash = 12,
        Other = 13,
        JackpotBurn = 14,
        MachinePile = 15
    }

    public partial class TreatmentTypeChipping : TreatmentType
    {
        private TreatmentTypeChipping(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeChipping Instance = new TreatmentTypeChipping(1, @"Chipping", @"Chipping");
    }

    public partial class TreatmentTypePruning : TreatmentType
    {
        private TreatmentTypePruning(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypePruning Instance = new TreatmentTypePruning(2, @"Pruning", @"Pruning");
    }

    public partial class TreatmentTypeThinning : TreatmentType
    {
        private TreatmentTypeThinning(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeThinning Instance = new TreatmentTypeThinning(3, @"Thinning", @"Thinning");
    }

    public partial class TreatmentTypeMastication : TreatmentType
    {
        private TreatmentTypeMastication(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeMastication Instance = new TreatmentTypeMastication(4, @"Mastication", @"Mastication");
    }

    public partial class TreatmentTypeGrazing : TreatmentType
    {
        private TreatmentTypeGrazing(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeGrazing Instance = new TreatmentTypeGrazing(5, @"Grazing", @"Grazing");
    }

    public partial class TreatmentTypeLopAndScatter : TreatmentType
    {
        private TreatmentTypeLopAndScatter(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeLopAndScatter Instance = new TreatmentTypeLopAndScatter(6, @"LopAndScatter", @"Lop and Scatter");
    }

    public partial class TreatmentTypeBiomassRemoval : TreatmentType
    {
        private TreatmentTypeBiomassRemoval(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeBiomassRemoval Instance = new TreatmentTypeBiomassRemoval(7, @"BiomassRemoval", @"Biomass Removal");
    }

    public partial class TreatmentTypeHandPile : TreatmentType
    {
        private TreatmentTypeHandPile(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeHandPile Instance = new TreatmentTypeHandPile(8, @"HandPile", @"Hand Pile");
    }

    public partial class TreatmentTypeBroadcastBurn : TreatmentType
    {
        private TreatmentTypeBroadcastBurn(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeBroadcastBurn Instance = new TreatmentTypeBroadcastBurn(9, @"BroadcastBurn", @"Broadcast Burn");
    }

    public partial class TreatmentTypeHandPileBurn : TreatmentType
    {
        private TreatmentTypeHandPileBurn(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeHandPileBurn Instance = new TreatmentTypeHandPileBurn(10, @"HandPileBurn", @"Hand Pile Burn");
    }

    public partial class TreatmentTypeMachinePileBurn : TreatmentType
    {
        private TreatmentTypeMachinePileBurn(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeMachinePileBurn Instance = new TreatmentTypeMachinePileBurn(11, @"MachinePileBurn", @"Machine Pile Burn");
    }

    public partial class TreatmentTypeSlash : TreatmentType
    {
        private TreatmentTypeSlash(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeSlash Instance = new TreatmentTypeSlash(12, @"Slash", @"Slash");
    }

    public partial class TreatmentTypeOther : TreatmentType
    {
        private TreatmentTypeOther(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeOther Instance = new TreatmentTypeOther(13, @"Other", @"Other");
    }

    public partial class TreatmentTypeJackpotBurn : TreatmentType
    {
        private TreatmentTypeJackpotBurn(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeJackpotBurn Instance = new TreatmentTypeJackpotBurn(14, @"JackpotBurn", @"Jackpot Burn");
    }

    public partial class TreatmentTypeMachinePile : TreatmentType
    {
        private TreatmentTypeMachinePile(int treatmentTypeID, string treatmentTypeName, string treatmentTypeDisplayName) : base(treatmentTypeID, treatmentTypeName, treatmentTypeDisplayName) {}
        public static readonly TreatmentTypeMachinePile Instance = new TreatmentTypeMachinePile(15, @"MachinePile", @"Machine Pile");
    }
}