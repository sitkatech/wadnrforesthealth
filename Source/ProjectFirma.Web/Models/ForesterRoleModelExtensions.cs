using System.Collections.Generic;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public static class ForesterRoleModelExtensions
    {
        public static FieldDefinition GetFieldDefinition(this ForesterRole foresterRole)
        {
            var fieldDefinition = FieldDefinition.All.SingleOrDefault(x => x.FieldDefinitionName == foresterRole.ForesterRoleName);
            return fieldDefinition;

        }
        

    }
}