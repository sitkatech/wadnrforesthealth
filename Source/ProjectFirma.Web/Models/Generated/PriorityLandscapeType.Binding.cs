//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PriorityLandscapeType]
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
    public abstract partial class PriorityLandscapeType : IHavePrimaryKey
    {
        public static readonly PriorityLandscapeTypeEast East = PriorityLandscapeTypeEast.Instance;
        public static readonly PriorityLandscapeTypeWest West = PriorityLandscapeTypeWest.Instance;

        public static readonly List<PriorityLandscapeType> All;
        public static readonly ReadOnlyDictionary<int, PriorityLandscapeType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static PriorityLandscapeType()
        {
            All = new List<PriorityLandscapeType> { East, West };
            AllLookupDictionary = new ReadOnlyDictionary<int, PriorityLandscapeType>(All.ToDictionary(x => x.PriorityLandscapeTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected PriorityLandscapeType(int priorityLandscapeTypeID, string priorityLandscapeTypeName, string priorityLandscapeTypeDisplayName, string priorityLandscapeTypeMapLayerColor)
        {
            PriorityLandscapeTypeID = priorityLandscapeTypeID;
            PriorityLandscapeTypeName = priorityLandscapeTypeName;
            PriorityLandscapeTypeDisplayName = priorityLandscapeTypeDisplayName;
            PriorityLandscapeTypeMapLayerColor = priorityLandscapeTypeMapLayerColor;
        }

        [Key]
        public int PriorityLandscapeTypeID { get; private set; }
        public string PriorityLandscapeTypeName { get; private set; }
        public string PriorityLandscapeTypeDisplayName { get; private set; }
        public string PriorityLandscapeTypeMapLayerColor { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return PriorityLandscapeTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(PriorityLandscapeType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.PriorityLandscapeTypeID == PriorityLandscapeTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as PriorityLandscapeType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return PriorityLandscapeTypeID;
        }

        public static bool operator ==(PriorityLandscapeType left, PriorityLandscapeType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PriorityLandscapeType left, PriorityLandscapeType right)
        {
            return !Equals(left, right);
        }

        public PriorityLandscapeTypeEnum ToEnum { get { return (PriorityLandscapeTypeEnum)GetHashCode(); } }

        public static PriorityLandscapeType ToType(int enumValue)
        {
            return ToType((PriorityLandscapeTypeEnum)enumValue);
        }

        public static PriorityLandscapeType ToType(PriorityLandscapeTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case PriorityLandscapeTypeEnum.East:
                    return East;
                case PriorityLandscapeTypeEnum.West:
                    return West;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum PriorityLandscapeTypeEnum
    {
        East = 1,
        West = 2
    }

    public partial class PriorityLandscapeTypeEast : PriorityLandscapeType
    {
        private PriorityLandscapeTypeEast(int priorityLandscapeTypeID, string priorityLandscapeTypeName, string priorityLandscapeTypeDisplayName, string priorityLandscapeTypeMapLayerColor) : base(priorityLandscapeTypeID, priorityLandscapeTypeName, priorityLandscapeTypeDisplayName, priorityLandscapeTypeMapLayerColor) {}
        public static readonly PriorityLandscapeTypeEast Instance = new PriorityLandscapeTypeEast(1, @"East", @"20-Year Forest Health Strategic Plan: Eastern Washington Priority Landscape", @"#59ACFF");
    }

    public partial class PriorityLandscapeTypeWest : PriorityLandscapeType
    {
        private PriorityLandscapeTypeWest(int priorityLandscapeTypeID, string priorityLandscapeTypeName, string priorityLandscapeTypeDisplayName, string priorityLandscapeTypeMapLayerColor) : base(priorityLandscapeTypeID, priorityLandscapeTypeName, priorityLandscapeTypeDisplayName, priorityLandscapeTypeMapLayerColor) {}
        public static readonly PriorityLandscapeTypeWest Instance = new PriorityLandscapeTypeWest(2, @"West", @"West", @"#4cc28f");
    }
}