using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Delete Agreement")]
    public class AgreementDeleteFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Agreement>
    {
        private readonly FirmaFeatureWithContextImpl<Agreement> _firmaFeatureWithContextImpl;

        public AgreementDeleteFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin })
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<Agreement>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, Agreement contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, Agreement contextModelObject)
        {
            var hasPermissionByPerson = HasPermissionByPerson(person);
            if (!hasPermissionByPerson)
            {
                //TODO use agreement title
                return new PermissionCheckResult(
                    $"You don't have permission to delete {"AgreementTitleGoesHere"}");
            }
            return new PermissionCheckResult();
        }
    }
}