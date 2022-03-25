using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Create a Grant Allocation", FieldDefinitionEnum.Application)]
    public class GrantAllocationCreateFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Organization>
    {
        private readonly FirmaFeatureWithContextImpl<Organization> _firmaFeatureWithContextImpl;
        public GrantAllocationCreateFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<Organization>(this);
            ActionFilter = _firmaFeatureWithContextImpl;

        }

        public PermissionCheckResult HasPermission(Person person, Organization contextModelObject)
        {
            if (person.HasAnyOfTheseRoles(new List<IRole>() {Role.Admin, Role.SitkaAdmin}))
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            // If person is project steward, they can only create grant allocations for their organization
            if (person.HasRole(Role.ProjectSteward) && person.OrganizationID == contextModelObject.OrganizationID)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You cannot create a {FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()}  for this {FieldDefinition.Organization.GetFieldDefinitionLabel()}, '{contextModelObject.DisplayName}'.");
        }

        public void DemandPermission(Person person, Organization contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}