using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class PriorityAreasValidationResult
    {
        private readonly List<string> _warningMessages;

        public PriorityAreasValidationResult(bool isIncomplete)
        {           
            _warningMessages = new List<string>();

            if (isIncomplete)
            {
                _warningMessages.Add($"Select at least one priority area, or if priority area information is unavailable/irrelevant provide explanatory information in the Notes section.");
            }
        }
        public PriorityAreasValidationResult(string customErrorMessage)
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