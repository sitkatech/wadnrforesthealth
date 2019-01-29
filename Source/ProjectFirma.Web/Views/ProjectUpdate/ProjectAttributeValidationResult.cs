using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class ProjectAttributeValidationResult
    {
        
        private readonly List<string> _warningMessages;

        public ProjectAttributeValidationResult(Models.ProjectUpdate projectUpdate)
        {

            var potentialWarnings = new List<string>();
            new CustomAttributesViewModel(projectUpdate).GetValidationResults(out potentialWarnings);
            _warningMessages = potentialWarnings;
        }

        public List<string> GetWarningMessages()
        {
            return _warningMessages;
        }

        public bool IsValid => !_warningMessages.Any();
    }
}