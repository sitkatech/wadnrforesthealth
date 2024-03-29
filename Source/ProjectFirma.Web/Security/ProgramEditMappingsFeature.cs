using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit {0} GIS Import Mappings", FieldDefinitionEnum.Program)]
    public class ProgramEditMappingsFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GisUploadSourceOrganization>
    {
        private readonly FirmaFeatureWithContextImpl<GisUploadSourceOrganization> _firmaFeatureWithContextImpl;

        public ProgramEditMappingsFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanEditProgram })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<GisUploadSourceOrganization>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, GisUploadSourceOrganization contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, GisUploadSourceOrganization contextModelObject)
        {
            if (!HasPermissionByPerson(person))
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to edit GIS import mappings for {contextModelObject.Program.DisplayName}");
            }

            if (person != null && person.HasRole(Role.CanEditProgram) && !person.ProgramPeople.Select(x => x.ProgramID).Contains(contextModelObject.ProgramID))
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to edit GIS import mappings for {contextModelObject.Program.DisplayName}");
            }


            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}