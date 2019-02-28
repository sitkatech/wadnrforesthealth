//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DeploymentEnvironment]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static DeploymentEnvironment GetDeploymentEnvironment(this IQueryable<DeploymentEnvironment> deploymentEnvironments, int deploymentEnvironmentID)
        {
            var deploymentEnvironment = deploymentEnvironments.SingleOrDefault(x => x.DeploymentEnvironmentID == deploymentEnvironmentID);
            Check.RequireNotNullThrowNotFound(deploymentEnvironment, "DeploymentEnvironment", deploymentEnvironmentID);
            return deploymentEnvironment;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteDeploymentEnvironment(this IQueryable<DeploymentEnvironment> deploymentEnvironments, List<int> deploymentEnvironmentIDList)
        {
            if(deploymentEnvironmentIDList.Any())
            {
                var deploymentEnvironmentsInSourceCollectionToDelete = deploymentEnvironments.Where(x => deploymentEnvironmentIDList.Contains(x.DeploymentEnvironmentID));
                foreach (var deploymentEnvironmentToDelete in deploymentEnvironmentsInSourceCollectionToDelete.ToList())
                {
                    deploymentEnvironmentToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteDeploymentEnvironment(this IQueryable<DeploymentEnvironment> deploymentEnvironments, ICollection<DeploymentEnvironment> deploymentEnvironmentsToDelete)
        {
            if(deploymentEnvironmentsToDelete.Any())
            {
                var deploymentEnvironmentIDList = deploymentEnvironmentsToDelete.Select(x => x.DeploymentEnvironmentID).ToList();
                var deploymentEnvironmentsToDeleteFromSourceList = deploymentEnvironments.Where(x => deploymentEnvironmentIDList.Contains(x.DeploymentEnvironmentID)).ToList();

                foreach (var deploymentEnvironmentToDelete in deploymentEnvironmentsToDeleteFromSourceList)
                {
                    deploymentEnvironmentToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteDeploymentEnvironment(this IQueryable<DeploymentEnvironment> deploymentEnvironments, int deploymentEnvironmentID)
        {
            DeleteDeploymentEnvironment(deploymentEnvironments, new List<int> { deploymentEnvironmentID });
        }

        public static void DeleteDeploymentEnvironment(this IQueryable<DeploymentEnvironment> deploymentEnvironments, DeploymentEnvironment deploymentEnvironmentToDelete)
        {
            DeleteDeploymentEnvironment(deploymentEnvironments, new List<DeploymentEnvironment> { deploymentEnvironmentToDelete });
        }
    }
}