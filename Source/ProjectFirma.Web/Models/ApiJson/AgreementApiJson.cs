using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("AgreementID: {AgreementID} - AgreementNumber: {AgreementNumber}")]
    public class AgreementApiJson
    {
        // There is some selective denormalization here to assist WADNR's comprehension (AgreementTypeName, AgreementStatusName, etc.)
        public int AgreementID { get; set; }
        public int AgreementTypeID { get; set; }
        public string AgreementTypeName { get; set; }
        public string AgreementNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? AgreementAmount { get; set; }
        public decimal? ExpendedAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public int? RegionID { get; set; }
        public string RegionName { get; set; }
        public DateTime? FirstBillDueOn { get; set; }
        public string Notes { get; set; }
        public string AgreementTitle { get; set; }
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public int? AgreementStatusID { get; set; }
        public string AgreementStatusName { get; set; }
        public int? AgreementFileResourceID { get; set; }

        // For use by model binder
        public AgreementApiJson()
        {
        }

        public AgreementApiJson(Agreement agreement)
        {
            AgreementID = agreement.AgreementID;
            AgreementTypeID = agreement.AgreementTypeID;
            AgreementTypeName = agreement.AgreementType.AgreementTypeName;
            AgreementNumber = agreement.AgreementNumber;
            StartDate = agreement.StartDate;
            EndDate = agreement.EndDate;
            AgreementAmount = agreement.AgreementAmount;
            ExpendedAmount = agreement.ExpendedAmount;
            BalanceAmount = agreement.BalanceAmount;
            RegionID = agreement.DNRUplandRegionID;
            RegionName = agreement.DNRUplandRegion?.DNRUplandRegionName;
            FirstBillDueOn = agreement.FirstBillDueOn;
            Notes = agreement.Notes;
            AgreementTitle = agreement.AgreementTitle;
            OrganizationID = agreement.OrganizationID;
            OrganizationName = agreement.Organization?.OrganizationName;
            AgreementStatusID = agreement.AgreementStatusID;
            AgreementStatusName = agreement.AgreementStatus?.AgreementStatusName;
            AgreementFileResourceID = agreement.AgreementFileResourceID;
        }

        public static List<AgreementApiJson> MakeAgreementApiJsonsFromAgreements(List<Agreement> agreements, bool doAlphaSort = true)
        {
            var outgoingAgreements = agreements;
            if (doAlphaSort)
            {
                outgoingAgreements = outgoingAgreements.OrderBy(a => a.AgreementNumber).ToList();
            }
            return outgoingAgreements.Select(a => new AgreementApiJson(a)).ToList();
        }
    }
}