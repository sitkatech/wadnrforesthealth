using System.Collections.Generic;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit Invoice")]
    public class InvoiceEditAsAdminFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<Invoice>
    {
        private readonly FirmaFeatureWithContextImpl<Invoice> _firmaFeatureWithContextImpl;

        public InvoiceEditAsAdminFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward})
        {
            _firmaFeatureWithContextImpl = new FirmaFeatureWithContextImpl<Invoice>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, Invoice contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, Invoice contextModelObject)
        {
            bool userHasPermission = HasPermissionByPerson(person);
            if (userHasPermission)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You do not have access to Invoice {contextModelObject.InvoiceID} - {contextModelObject.InvoiceIdentifyingName}");
        }
    }
}