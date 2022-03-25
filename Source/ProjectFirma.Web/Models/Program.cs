using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class Program : IAuditableEntity
    {
        public string AuditDescriptionString => ProgramName;

        public string InternalDisplayName =>  $"{ProgramName}{(!String.IsNullOrWhiteSpace(ProgramShortName) ? $" ({ProgramShortName})" : String.Empty)}{(!ProgramIsActive ? " (Inactive)" : String.Empty)}";
        public string DisplayName => $"{(IsDefaultProgramForImportOnly ? Organization.DisplayName : InternalDisplayName)}";

        public string ProgramNameDisplay => IsDefaultProgramForImportOnly ? "(default)" : ProgramName;

        public HtmlString GetDisplayNameAsUrl()
        {
            return UrlTemplate.MakeHrefString(SitkaRoute<ProgramController>.BuildUrlFromExpression(t => t.Detail(ProgramID)), DisplayName);
        }



        public static bool IsProgramNameUnique(IEnumerable<Program> programs, string programName, int currentProgramID, int currentOrganizationID)
        {
            var program =
                programs.SingleOrDefault(x => x.OrganizationID != currentOrganizationID && x.ProgramID != currentProgramID && String.Equals(x.ProgramName, programName, StringComparison.InvariantCultureIgnoreCase));
            return program == null;
        }

        public static bool IsProgramShortNameUniqueIfProvided(IEnumerable<Program> programs, string programShortName, int currentProgramID,int currentOrganizationID)
        {
            // Nulls don't trip the unique check
            if (programShortName == null)
            {
                return true;
            }
            var existingProgram =
                programs.SingleOrDefault(x => x.OrganizationID != currentOrganizationID && x.ProgramID != currentProgramID && String.Equals(x.ProgramShortName, programShortName, StringComparison.InvariantCultureIgnoreCase));
            return existingProgram == null;
        }

        public HtmlString PrimaryContactPersonWithOrgAsUrl => ProgramPrimaryContactPerson != null ? ProgramPrimaryContactPerson.GetFullNameFirstLastAndOrgAsUrl() : new HtmlString(ViewUtilities.NoneString);

        public HtmlString PrimaryContactPersonAsStringAndOrgAsUrl => ProgramPrimaryContactPerson != null ? ProgramPrimaryContactPerson.GetFullNameFirstLastAsStringAndOrgAsUrl() : new HtmlString(ViewUtilities.NoneString);
    }
}