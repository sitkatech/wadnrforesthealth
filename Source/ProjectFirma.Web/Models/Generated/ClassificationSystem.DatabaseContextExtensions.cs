//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ClassificationSystem]
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
        public static ClassificationSystem GetClassificationSystem(this IQueryable<ClassificationSystem> classificationSystems, int classificationSystemID)
        {
            var classificationSystem = classificationSystems.SingleOrDefault(x => x.ClassificationSystemID == classificationSystemID);
            Check.RequireNotNullThrowNotFound(classificationSystem, "ClassificationSystem", classificationSystemID);
            return classificationSystem;
        }

        public static void DeleteClassificationSystem(this IQueryable<ClassificationSystem> classificationSystems, List<int> classificationSystemIDList)
        {
            if(classificationSystemIDList.Any())
            {
                classificationSystems.Where(x => classificationSystemIDList.Contains(x.ClassificationSystemID)).Delete();
            }
        }

        public static void DeleteClassificationSystem(this IQueryable<ClassificationSystem> classificationSystems, ICollection<ClassificationSystem> classificationSystemsToDelete)
        {
            if(classificationSystemsToDelete.Any())
            {
                var classificationSystemIDList = classificationSystemsToDelete.Select(x => x.ClassificationSystemID).ToList();
                classificationSystems.Where(x => classificationSystemIDList.Contains(x.ClassificationSystemID)).Delete();
            }
        }

        public static void DeleteClassificationSystem(this IQueryable<ClassificationSystem> classificationSystems, int classificationSystemID)
        {
            DeleteClassificationSystem(classificationSystems, new List<int> { classificationSystemID });
        }

        public static void DeleteClassificationSystem(this IQueryable<ClassificationSystem> classificationSystems, ClassificationSystem classificationSystemToDelete)
        {
            DeleteClassificationSystem(classificationSystems, new List<ClassificationSystem> { classificationSystemToDelete });
        }
    }
}