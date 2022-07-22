//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ForesterRole]
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
    public abstract partial class ForesterRole : IHavePrimaryKey
    {
        public static readonly ForesterRoleServiceForester ServiceForester = ForesterRoleServiceForester.Instance;
        public static readonly ForesterRoleServiceForestrySpecialist ServiceForestrySpecialist = ForesterRoleServiceForestrySpecialist.Instance;
        public static readonly ForesterRoleForestPracticesForester ForestPracticesForester = ForesterRoleForestPracticesForester.Instance;
        public static readonly ForesterRoleStewardshipBiologist StewardshipBiologist = ForesterRoleStewardshipBiologist.Instance;
        public static readonly ForesterRoleUrbanForestryTechnician UrbanForestryTechnician = ForesterRoleUrbanForestryTechnician.Instance;
        public static readonly ForesterRoleCommunityWildfirePreparednessSpecialist CommunityWildfirePreparednessSpecialist = ForesterRoleCommunityWildfirePreparednessSpecialist.Instance;
        public static readonly ForesterRoleRegulationAssistanceForester RegulationAssistanceForester = ForesterRoleRegulationAssistanceForester.Instance;

        public static readonly List<ForesterRole> All;
        public static readonly ReadOnlyDictionary<int, ForesterRole> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ForesterRole()
        {
            All = new List<ForesterRole> { ServiceForester, ServiceForestrySpecialist, ForestPracticesForester, StewardshipBiologist, UrbanForestryTechnician, CommunityWildfirePreparednessSpecialist, RegulationAssistanceForester };
            AllLookupDictionary = new ReadOnlyDictionary<int, ForesterRole>(All.ToDictionary(x => x.ForesterRoleID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ForesterRole(int foresterRoleID, string foresterRoleDisplayName, string foresterRoleName)
        {
            ForesterRoleID = foresterRoleID;
            ForesterRoleDisplayName = foresterRoleDisplayName;
            ForesterRoleName = foresterRoleName;
        }

        [Key]
        public int ForesterRoleID { get; private set; }
        public string ForesterRoleDisplayName { get; private set; }
        public string ForesterRoleName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return ForesterRoleID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ForesterRole other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ForesterRoleID == ForesterRoleID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ForesterRole);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ForesterRoleID;
        }

        public static bool operator ==(ForesterRole left, ForesterRole right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ForesterRole left, ForesterRole right)
        {
            return !Equals(left, right);
        }

        public ForesterRoleEnum ToEnum { get { return (ForesterRoleEnum)GetHashCode(); } }

        public static ForesterRole ToType(int enumValue)
        {
            return ToType((ForesterRoleEnum)enumValue);
        }

        public static ForesterRole ToType(ForesterRoleEnum enumValue)
        {
            switch (enumValue)
            {
                case ForesterRoleEnum.CommunityWildfirePreparednessSpecialist:
                    return CommunityWildfirePreparednessSpecialist;
                case ForesterRoleEnum.ForestPracticesForester:
                    return ForestPracticesForester;
                case ForesterRoleEnum.RegulationAssistanceForester:
                    return RegulationAssistanceForester;
                case ForesterRoleEnum.ServiceForester:
                    return ServiceForester;
                case ForesterRoleEnum.ServiceForestrySpecialist:
                    return ServiceForestrySpecialist;
                case ForesterRoleEnum.StewardshipBiologist:
                    return StewardshipBiologist;
                case ForesterRoleEnum.UrbanForestryTechnician:
                    return UrbanForestryTechnician;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ForesterRoleEnum
    {
        ServiceForester = 1,
        ServiceForestrySpecialist = 2,
        ForestPracticesForester = 3,
        StewardshipBiologist = 4,
        UrbanForestryTechnician = 5,
        CommunityWildfirePreparednessSpecialist = 6,
        RegulationAssistanceForester = 7
    }

    public partial class ForesterRoleServiceForester : ForesterRole
    {
        private ForesterRoleServiceForester(int foresterRoleID, string foresterRoleDisplayName, string foresterRoleName) : base(foresterRoleID, foresterRoleDisplayName, foresterRoleName) {}
        public static readonly ForesterRoleServiceForester Instance = new ForesterRoleServiceForester(1, @"Service Forester", @"ServiceForester");
    }

    public partial class ForesterRoleServiceForestrySpecialist : ForesterRole
    {
        private ForesterRoleServiceForestrySpecialist(int foresterRoleID, string foresterRoleDisplayName, string foresterRoleName) : base(foresterRoleID, foresterRoleDisplayName, foresterRoleName) {}
        public static readonly ForesterRoleServiceForestrySpecialist Instance = new ForesterRoleServiceForestrySpecialist(2, @"Service Forestry Specialist", @"ServiceForestrySpecialist");
    }

    public partial class ForesterRoleForestPracticesForester : ForesterRole
    {
        private ForesterRoleForestPracticesForester(int foresterRoleID, string foresterRoleDisplayName, string foresterRoleName) : base(foresterRoleID, foresterRoleDisplayName, foresterRoleName) {}
        public static readonly ForesterRoleForestPracticesForester Instance = new ForesterRoleForestPracticesForester(3, @"Forest Practices Forester", @"ForestPracticesForester");
    }

    public partial class ForesterRoleStewardshipBiologist : ForesterRole
    {
        private ForesterRoleStewardshipBiologist(int foresterRoleID, string foresterRoleDisplayName, string foresterRoleName) : base(foresterRoleID, foresterRoleDisplayName, foresterRoleName) {}
        public static readonly ForesterRoleStewardshipBiologist Instance = new ForesterRoleStewardshipBiologist(4, @"Stewardship Biologist", @"StewardshipBiologist");
    }

    public partial class ForesterRoleUrbanForestryTechnician : ForesterRole
    {
        private ForesterRoleUrbanForestryTechnician(int foresterRoleID, string foresterRoleDisplayName, string foresterRoleName) : base(foresterRoleID, foresterRoleDisplayName, foresterRoleName) {}
        public static readonly ForesterRoleUrbanForestryTechnician Instance = new ForesterRoleUrbanForestryTechnician(5, @"Urban Forestry Technician", @"UrbanForestryTechnician");
    }

    public partial class ForesterRoleCommunityWildfirePreparednessSpecialist : ForesterRole
    {
        private ForesterRoleCommunityWildfirePreparednessSpecialist(int foresterRoleID, string foresterRoleDisplayName, string foresterRoleName) : base(foresterRoleID, foresterRoleDisplayName, foresterRoleName) {}
        public static readonly ForesterRoleCommunityWildfirePreparednessSpecialist Instance = new ForesterRoleCommunityWildfirePreparednessSpecialist(6, @"Community Wildfire Preparedness Specialist", @"CommunityWildfirePreparednessSpecialist");
    }

    public partial class ForesterRoleRegulationAssistanceForester : ForesterRole
    {
        private ForesterRoleRegulationAssistanceForester(int foresterRoleID, string foresterRoleDisplayName, string foresterRoleName) : base(foresterRoleID, foresterRoleDisplayName, foresterRoleName) {}
        public static readonly ForesterRoleRegulationAssistanceForester Instance = new ForesterRoleRegulationAssistanceForester(7, @"Regulation Assistance Forester", @"RegulationAssistanceForester");
    }
}