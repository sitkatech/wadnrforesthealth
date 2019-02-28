//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DeploymentEnvironment
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class DeploymentEnvironmentPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<DeploymentEnvironment>
    {
        public DeploymentEnvironmentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DeploymentEnvironmentPrimaryKey(DeploymentEnvironment deploymentEnvironment) : base(deploymentEnvironment){}

        public static implicit operator DeploymentEnvironmentPrimaryKey(int primaryKeyValue)
        {
            return new DeploymentEnvironmentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator DeploymentEnvironmentPrimaryKey(DeploymentEnvironment deploymentEnvironment)
        {
            return new DeploymentEnvironmentPrimaryKey(deploymentEnvironment);
        }
    }
}