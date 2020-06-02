using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class PriorityLandscapesValidationResult
    {
        private readonly List<string> _warningMessages;

        public PriorityLandscapesValidationResult(bool isIncomplete)
        {           
            _warningMessages = new List<string>();

            if (isIncomplete)
            {
                _warningMessages.Add($"Select at least one priority landscape, or if priority landscape information is unavailable/irrelevant provide explanatory information in the Notes section.");
            }
        }
        public PriorityLandscapesValidationResult(string customErrorMessage)
        {
            _warningMessages = new List<string> { customErrorMessage };
        }

        public List<string> GetWarningMessages()
        {     
            return _warningMessages;
        }

        public bool IsValid => !_warningMessages.Any();
    }
}