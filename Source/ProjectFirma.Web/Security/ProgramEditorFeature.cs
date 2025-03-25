using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit {0} GIS Import Mappings", FieldDefinitionEnum.Program)]
    public class ProgramEditorFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Program>
    {
        private readonly FirmaFeatureWithContextImpl<Program> _firmaFeatureWithContextImpl;

        public ProgramEditorFeature() : base(new List<Role> { Role.EsaAdmin, Role.Admin, Role.CanEditProgram })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<Program>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, Program contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, Program contextModelObject)
        {
            if (!HasPermissionByPerson(person))
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to edit GIS import mappings for {contextModelObject.DisplayName}");
            }

            if (person != null && person.HasRole(Role.CanEditProgram) && !person.ProgramPeople.Select(x => x.ProgramID).Contains(contextModelObject.ProgramID))
            {
                return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to edit GIS import mappings for {contextModelObject.DisplayName}");
            }


            return PermissionCheckResult.MakeSuccessPermissionCheckResult();
        }
    }
}