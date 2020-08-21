//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGrantAllocationRequestFundingSource]
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
    public abstract partial class ProjectGrantAllocationRequestFundingSource : IHavePrimaryKey
    {
        public static readonly ProjectGrantAllocationRequestFundingSourceFederal Federal = ProjectGrantAllocationRequestFundingSourceFederal.Instance;
        public static readonly ProjectGrantAllocationRequestFundingSourceState State = ProjectGrantAllocationRequestFundingSourceState.Instance;
        public static readonly ProjectGrantAllocationRequestFundingSourcePrivate Private = ProjectGrantAllocationRequestFundingSourcePrivate.Instance;
        public static readonly ProjectGrantAllocationRequestFundingSourceOther Other = ProjectGrantAllocationRequestFundingSourceOther.Instance;

        public static readonly List<ProjectGrantAllocationRequestFundingSource> All;
        public static readonly ReadOnlyDictionary<int, ProjectGrantAllocationRequestFundingSource> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ProjectGrantAllocationRequestFundingSource()
        {
            All = new List<ProjectGrantAllocationRequestFundingSource> { Federal, State, Private, Other };
            AllLookupDictionary = new ReadOnlyDictionary<int, ProjectGrantAllocationRequestFundingSource>(All.ToDictionary(x => x.ProjectGrantAllocationRequestFundingSourceID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ProjectGrantAllocationRequestFundingSource(int projectGrantAllocationRequestFundingSourceID, string projectGrantAllocationRequestFundingSourceName, string projectGrantAllocationRequestFundingSourceDisplayName)
        {
            ProjectGrantAllocationRequestFundingSourceID = projectGrantAllocationRequestFundingSourceID;
            ProjectGrantAllocationRequestFundingSourceName = projectGrantAllocationRequestFundingSourceName;
            ProjectGrantAllocationRequestFundingSourceDisplayName = projectGrantAllocationRequestFundingSourceDisplayName;
        }

        [Key]
        public int ProjectGrantAllocationRequestFundingSourceID { get; private set; }
        public string ProjectGrantAllocationRequestFundingSourceName { get; private set; }
        public string ProjectGrantAllocationRequestFundingSourceDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectGrantAllocationRequestFundingSourceID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ProjectGrantAllocationRequestFundingSource other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ProjectGrantAllocationRequestFundingSourceID == ProjectGrantAllocationRequestFundingSourceID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ProjectGrantAllocationRequestFundingSource);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ProjectGrantAllocationRequestFundingSourceID;
        }

        public static bool operator ==(ProjectGrantAllocationRequestFundingSource left, ProjectGrantAllocationRequestFundingSource right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProjectGrantAllocationRequestFundingSource left, ProjectGrantAllocationRequestFundingSource right)
        {
            return !Equals(left, right);
        }

        public ProjectGrantAllocationRequestFundingSourceEnum ToEnum { get { return (ProjectGrantAllocationRequestFundingSourceEnum)GetHashCode(); } }

        public static ProjectGrantAllocationRequestFundingSource ToType(int enumValue)
        {
            return ToType((ProjectGrantAllocationRequestFundingSourceEnum)enumValue);
        }

        public static ProjectGrantAllocationRequestFundingSource ToType(ProjectGrantAllocationRequestFundingSourceEnum enumValue)
        {
            switch (enumValue)
            {
                case ProjectGrantAllocationRequestFundingSourceEnum.Federal:
                    return Federal;
                case ProjectGrantAllocationRequestFundingSourceEnum.Other:
                    return Other;
                case ProjectGrantAllocationRequestFundingSourceEnum.Private:
                    return Private;
                case ProjectGrantAllocationRequestFundingSourceEnum.State:
                    return State;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ProjectGrantAllocationRequestFundingSourceEnum
    {
        Federal = 1,
        State = 2,
        Private = 3,
        Other = 4
    }

    public partial class ProjectGrantAllocationRequestFundingSourceFederal : ProjectGrantAllocationRequestFundingSource
    {
        private ProjectGrantAllocationRequestFundingSourceFederal(int projectGrantAllocationRequestFundingSourceID, string projectGrantAllocationRequestFundingSourceName, string projectGrantAllocationRequestFundingSourceDisplayName) : base(projectGrantAllocationRequestFundingSourceID, projectGrantAllocationRequestFundingSourceName, projectGrantAllocationRequestFundingSourceDisplayName) {}
        public static readonly ProjectGrantAllocationRequestFundingSourceFederal Instance = new ProjectGrantAllocationRequestFundingSourceFederal(1, @"Federal", @"Federal");
    }

    public partial class ProjectGrantAllocationRequestFundingSourceState : ProjectGrantAllocationRequestFundingSource
    {
        private ProjectGrantAllocationRequestFundingSourceState(int projectGrantAllocationRequestFundingSourceID, string projectGrantAllocationRequestFundingSourceName, string projectGrantAllocationRequestFundingSourceDisplayName) : base(projectGrantAllocationRequestFundingSourceID, projectGrantAllocationRequestFundingSourceName, projectGrantAllocationRequestFundingSourceDisplayName) {}
        public static readonly ProjectGrantAllocationRequestFundingSourceState Instance = new ProjectGrantAllocationRequestFundingSourceState(2, @"State", @"State");
    }

    public partial class ProjectGrantAllocationRequestFundingSourcePrivate : ProjectGrantAllocationRequestFundingSource
    {
        private ProjectGrantAllocationRequestFundingSourcePrivate(int projectGrantAllocationRequestFundingSourceID, string projectGrantAllocationRequestFundingSourceName, string projectGrantAllocationRequestFundingSourceDisplayName) : base(projectGrantAllocationRequestFundingSourceID, projectGrantAllocationRequestFundingSourceName, projectGrantAllocationRequestFundingSourceDisplayName) {}
        public static readonly ProjectGrantAllocationRequestFundingSourcePrivate Instance = new ProjectGrantAllocationRequestFundingSourcePrivate(3, @"Private", @"Private");
    }

    public partial class ProjectGrantAllocationRequestFundingSourceOther : ProjectGrantAllocationRequestFundingSource
    {
        private ProjectGrantAllocationRequestFundingSourceOther(int projectGrantAllocationRequestFundingSourceID, string projectGrantAllocationRequestFundingSourceName, string projectGrantAllocationRequestFundingSourceDisplayName) : base(projectGrantAllocationRequestFundingSourceID, projectGrantAllocationRequestFundingSourceName, projectGrantAllocationRequestFundingSourceDisplayName) {}
        public static readonly ProjectGrantAllocationRequestFundingSourceOther Instance = new ProjectGrantAllocationRequestFundingSourceOther(4, @"Other", @"Other");
    }
}