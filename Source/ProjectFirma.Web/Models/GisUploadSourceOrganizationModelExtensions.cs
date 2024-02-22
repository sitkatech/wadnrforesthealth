using System.Linq;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static class GisUploadSourceOrganizationModelExtensions
    {
        public static void UpdateMappingColumnValue(this GisUploadSourceOrganization gisUploadSourceOrganization, int fieldDefinitionID, string newColumnMapping)
        {
            //need to check for existing mapping and remove or just get out of this
            if (string.IsNullOrEmpty(newColumnMapping))
            {
                var mappingsForFieldDefinition = HttpRequestStorage.DatabaseEntities.GisDefaultMappings.Where(x =>
                    x.FieldDefinitionID == fieldDefinitionID && x.GisUploadSourceOrganizationID ==
                    gisUploadSourceOrganization.GisUploadSourceOrganizationID);

                if (mappingsForFieldDefinition.Any())
                {
                    HttpRequestStorage.DatabaseEntities.GisDefaultMappings.RemoveRange(mappingsForFieldDefinition);
                }

                return;
            }

            var defaultMapping = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == fieldDefinitionID);

            if (defaultMapping == null)
            {
                var newMapping = new GisDefaultMapping(gisUploadSourceOrganization.GisUploadSourceOrganizationID, fieldDefinitionID, newColumnMapping);
                HttpRequestStorage.DatabaseEntities.GisDefaultMappings.Add(newMapping);
            }
            else
            {
                defaultMapping.GisDefaultMappingColumnName = newColumnMapping;
            }
        }
    }
}