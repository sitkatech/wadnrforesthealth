using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class CountiesValidationResult
    {
        private readonly List<string> _warningMessages;

        public CountiesValidationResult(bool isIncomplete)
        {           
            _warningMessages = new List<string>();

            if (isIncomplete)
            {
                _warningMessages.Add($"Select at least one {Models.FieldDefinition.County.FieldDefinitionDisplayName}, or if {Models.FieldDefinition.County.FieldDefinitionDisplayName} information is unavailable/irrelevant provide explanatory information in the Notes section.");
            }
        }
        public CountiesValidationResult(string customErrorMessage)
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