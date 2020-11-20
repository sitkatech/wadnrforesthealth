using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Models
{
    public partial class Program : IAuditableEntity
    {
        public const string ProgramUnknown = "(Unknown or Unspecified Organization)";
        public string AuditDescriptionString => ProgramName;

        public string DisplayName => IsUnknown ? "Unknown or unspecified" : $"{ProgramName}{(!String.IsNullOrWhiteSpace(ProgramShortName) ? $" ({ProgramShortName})" : String.Empty)}{(!ProgramIsActive ? " (Inactive)" : String.Empty)}";

        public bool IsUnknown => !String.IsNullOrWhiteSpace(ProgramName) && ProgramName.Equals(ProgramUnknown, StringComparison.InvariantCultureIgnoreCase);

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