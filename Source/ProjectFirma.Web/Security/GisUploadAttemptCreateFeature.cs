using System.Collections.Generic;
using LtInfo.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Security
{
    [SecurityFeatureDescription("Edit GisUploadAttempt")]
    public class GisUploadAttemptCreateFeature : FirmaFeatureWithContext, IFirmaBaseFeatureWithContext<GisUploadAttempt>
    {
        public GisUploadAttemptCreateFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.ProjectSteward })
        {
            
        }

        public PermissionCheckResult HasPermission(Person person, GisUploadAttempt contextModelObject)
        {
            if (contextModelObject.GisUploadAttemptCreatePerson.PersonID == person.PersonID)
            {
                return PermissionCheckResult.MakeSuccessPermissionCheckResult();
            }

            return PermissionCheckResult.MakeFailurePermissionCheckResult($"You don't have permission to edit this upload attempt");
        }

        public void DemandPermission(Person person, GisUploadAttempt contextModelObject)
        {
            var permissionCheckResult = HasPermission(person, contextModelObject);
            if (!permissionCheckResult.HasPermission)
            {
                throw new SitkaRecordNotAuthorizedException(permissionCheckResult.PermissionDeniedMessage);
            }
        }
    }
}