//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CostType]
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
    public abstract partial class CostType : IHavePrimaryKey
    {
        public static readonly CostTypeIndirectCosts IndirectCosts = CostTypeIndirectCosts.Instance;
        public static readonly CostTypeSupplies Supplies = CostTypeSupplies.Instance;
        public static readonly CostTypePersonnelAndBenefits PersonnelAndBenefits = CostTypePersonnelAndBenefits.Instance;
        public static readonly CostTypeTravel Travel = CostTypeTravel.Instance;
        public static readonly CostTypeContractual Contractual = CostTypeContractual.Instance;
        public static readonly CostTypeAgreements Agreements = CostTypeAgreements.Instance;
        public static readonly CostTypeEquipment Equipment = CostTypeEquipment.Instance;
        public static readonly CostTypeOther Other = CostTypeOther.Instance;

        public static readonly List<CostType> All;
        public static readonly ReadOnlyDictionary<int, CostType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static CostType()
        {
            All = new List<CostType> { IndirectCosts, Supplies, PersonnelAndBenefits, Travel, Contractual, Agreements, Equipment, Other };
            AllLookupDictionary = new ReadOnlyDictionary<int, CostType>(All.ToDictionary(x => x.CostTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected CostType(int costTypeID, string costTypeDisplayName, string costTypeName, bool isValidInvoiceLineItemCostType)
        {
            CostTypeID = costTypeID;
            CostTypeDisplayName = costTypeDisplayName;
            CostTypeName = costTypeName;
            IsValidInvoiceLineItemCostType = isValidInvoiceLineItemCostType;
        }

        [Key]
        public int CostTypeID { get; private set; }
        public string CostTypeDisplayName { get; private set; }
        public string CostTypeName { get; private set; }
        public bool IsValidInvoiceLineItemCostType { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return CostTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(CostType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.CostTypeID == CostTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as CostType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return CostTypeID;
        }

        public static bool operator ==(CostType left, CostType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CostType left, CostType right)
        {
            return !Equals(left, right);
        }

        public CostTypeEnum ToEnum { get { return (CostTypeEnum)GetHashCode(); } }

        public static CostType ToType(int enumValue)
        {
            return ToType((CostTypeEnum)enumValue);
        }

        public static CostType ToType(CostTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case CostTypeEnum.Agreements:
                    return Agreements;
                case CostTypeEnum.Contractual:
                    return Contractual;
                case CostTypeEnum.Equipment:
                    return Equipment;
                case CostTypeEnum.IndirectCosts:
                    return IndirectCosts;
                case CostTypeEnum.Other:
                    return Other;
                case CostTypeEnum.PersonnelAndBenefits:
                    return PersonnelAndBenefits;
                case CostTypeEnum.Supplies:
                    return Supplies;
                case CostTypeEnum.Travel:
                    return Travel;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum CostTypeEnum
    {
        IndirectCosts = 1,
        Supplies = 2,
        PersonnelAndBenefits = 3,
        Travel = 4,
        Contractual = 5,
        Agreements = 6,
        Equipment = 7,
        Other = 8
    }

    public partial class CostTypeIndirectCosts : CostType
    {
        private CostTypeIndirectCosts(int costTypeID, string costTypeDisplayName, string costTypeName, bool isValidInvoiceLineItemCostType) : base(costTypeID, costTypeDisplayName, costTypeName, isValidInvoiceLineItemCostType) {}
        public static readonly CostTypeIndirectCosts Instance = new CostTypeIndirectCosts(1, @"Indirect Costs", @"IndirectCosts", true);
    }

    public partial class CostTypeSupplies : CostType
    {
        private CostTypeSupplies(int costTypeID, string costTypeDisplayName, string costTypeName, bool isValidInvoiceLineItemCostType) : base(costTypeID, costTypeDisplayName, costTypeName, isValidInvoiceLineItemCostType) {}
        public static readonly CostTypeSupplies Instance = new CostTypeSupplies(2, @"Supplies", @"Supplies", true);
    }

    public partial class CostTypePersonnelAndBenefits : CostType
    {
        private CostTypePersonnelAndBenefits(int costTypeID, string costTypeDisplayName, string costTypeName, bool isValidInvoiceLineItemCostType) : base(costTypeID, costTypeDisplayName, costTypeName, isValidInvoiceLineItemCostType) {}
        public static readonly CostTypePersonnelAndBenefits Instance = new CostTypePersonnelAndBenefits(3, @"Personnel and Benefits", @"PersonnelAndBenefits", true);
    }

    public partial class CostTypeTravel : CostType
    {
        private CostTypeTravel(int costTypeID, string costTypeDisplayName, string costTypeName, bool isValidInvoiceLineItemCostType) : base(costTypeID, costTypeDisplayName, costTypeName, isValidInvoiceLineItemCostType) {}
        public static readonly CostTypeTravel Instance = new CostTypeTravel(4, @"Travel", @"Travel", true);
    }

    public partial class CostTypeContractual : CostType
    {
        private CostTypeContractual(int costTypeID, string costTypeDisplayName, string costTypeName, bool isValidInvoiceLineItemCostType) : base(costTypeID, costTypeDisplayName, costTypeName, isValidInvoiceLineItemCostType) {}
        public static readonly CostTypeContractual Instance = new CostTypeContractual(5, @"Contractual", @"Contractual", true);
    }

    public partial class CostTypeAgreements : CostType
    {
        private CostTypeAgreements(int costTypeID, string costTypeDisplayName, string costTypeName, bool isValidInvoiceLineItemCostType) : base(costTypeID, costTypeDisplayName, costTypeName, isValidInvoiceLineItemCostType) {}
        public static readonly CostTypeAgreements Instance = new CostTypeAgreements(6, @"Agreements", @"Agreements", false);
    }

    public partial class CostTypeEquipment : CostType
    {
        private CostTypeEquipment(int costTypeID, string costTypeDisplayName, string costTypeName, bool isValidInvoiceLineItemCostType) : base(costTypeID, costTypeDisplayName, costTypeName, isValidInvoiceLineItemCostType) {}
        public static readonly CostTypeEquipment Instance = new CostTypeEquipment(7, @"Equipment", @"Equipment", true);
    }

    public partial class CostTypeOther : CostType
    {
        private CostTypeOther(int costTypeID, string costTypeDisplayName, string costTypeName, bool isValidInvoiceLineItemCostType) : base(costTypeID, costTypeDisplayName, costTypeName, isValidInvoiceLineItemCostType) {}
        public static readonly CostTypeOther Instance = new CostTypeOther(8, @"Other", @"Other", true);
    }
}