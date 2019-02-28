//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DeploymentEnvironment]
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
    public abstract partial class DeploymentEnvironment : IHavePrimaryKey
    {
        public static readonly DeploymentEnvironmentLocal Local = DeploymentEnvironmentLocal.Instance;
        public static readonly DeploymentEnvironmentQA QA = DeploymentEnvironmentQA.Instance;
        public static readonly DeploymentEnvironmentProd Prod = DeploymentEnvironmentProd.Instance;

        public static readonly List<DeploymentEnvironment> All;
        public static readonly ReadOnlyDictionary<int, DeploymentEnvironment> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static DeploymentEnvironment()
        {
            All = new List<DeploymentEnvironment> { Local, QA, Prod };
            AllLookupDictionary = new ReadOnlyDictionary<int, DeploymentEnvironment>(All.ToDictionary(x => x.DeploymentEnvironmentID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected DeploymentEnvironment(int deploymentEnvironmentID, string deploymentEnvironmentName)
        {
            DeploymentEnvironmentID = deploymentEnvironmentID;
            DeploymentEnvironmentName = deploymentEnvironmentName;
        }

        [Key]
        public int DeploymentEnvironmentID { get; private set; }
        public string DeploymentEnvironmentName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return DeploymentEnvironmentID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(DeploymentEnvironment other)
        {
            if (other == null)
            {
                return false;
            }
            return other.DeploymentEnvironmentID == DeploymentEnvironmentID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as DeploymentEnvironment);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return DeploymentEnvironmentID;
        }

        public static bool operator ==(DeploymentEnvironment left, DeploymentEnvironment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DeploymentEnvironment left, DeploymentEnvironment right)
        {
            return !Equals(left, right);
        }

        public DeploymentEnvironmentEnum ToEnum { get { return (DeploymentEnvironmentEnum)GetHashCode(); } }

        public static DeploymentEnvironment ToType(int enumValue)
        {
            return ToType((DeploymentEnvironmentEnum)enumValue);
        }

        public static DeploymentEnvironment ToType(DeploymentEnvironmentEnum enumValue)
        {
            switch (enumValue)
            {
                case DeploymentEnvironmentEnum.Local:
                    return Local;
                case DeploymentEnvironmentEnum.Prod:
                    return Prod;
                case DeploymentEnvironmentEnum.QA:
                    return QA;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum DeploymentEnvironmentEnum
    {
        Local = 1,
        QA = 2,
        Prod = 3
    }

    public partial class DeploymentEnvironmentLocal : DeploymentEnvironment
    {
        private DeploymentEnvironmentLocal(int deploymentEnvironmentID, string deploymentEnvironmentName) : base(deploymentEnvironmentID, deploymentEnvironmentName) {}
        public static readonly DeploymentEnvironmentLocal Instance = new DeploymentEnvironmentLocal(1, @"Local");
    }

    public partial class DeploymentEnvironmentQA : DeploymentEnvironment
    {
        private DeploymentEnvironmentQA(int deploymentEnvironmentID, string deploymentEnvironmentName) : base(deploymentEnvironmentID, deploymentEnvironmentName) {}
        public static readonly DeploymentEnvironmentQA Instance = new DeploymentEnvironmentQA(2, @"QA");
    }

    public partial class DeploymentEnvironmentProd : DeploymentEnvironment
    {
        private DeploymentEnvironmentProd(int deploymentEnvironmentID, string deploymentEnvironmentName) : base(deploymentEnvironmentID, deploymentEnvironmentName) {}
        public static readonly DeploymentEnvironmentProd Instance = new DeploymentEnvironmentProd(3, @"Prod");
    }
}