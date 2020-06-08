//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ClassificationPerformanceMeasure]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ClassificationPerformanceMeasure GetClassificationPerformanceMeasure(this IQueryable<ClassificationPerformanceMeasure> classificationPerformanceMeasures, int classificationPerformanceMeasureID)
        {
            var classificationPerformanceMeasure = classificationPerformanceMeasures.SingleOrDefault(x => x.ClassificationPerformanceMeasureID == classificationPerformanceMeasureID);
            Check.RequireNotNullThrowNotFound(classificationPerformanceMeasure, "ClassificationPerformanceMeasure", classificationPerformanceMeasureID);
            return classificationPerformanceMeasure;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteClassificationPerformanceMeasure(this IQueryable<ClassificationPerformanceMeasure> classificationPerformanceMeasures, List<int> classificationPerformanceMeasureIDList)
        {
            if(classificationPerformanceMeasureIDList.Any())
            {
                var classificationPerformanceMeasuresInSourceCollectionToDelete = classificationPerformanceMeasures.Where(x => classificationPerformanceMeasureIDList.Contains(x.ClassificationPerformanceMeasureID));
                foreach (var classificationPerformanceMeasureToDelete in classificationPerformanceMeasuresInSourceCollectionToDelete.ToList())
                {
                    classificationPerformanceMeasureToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteClassificationPerformanceMeasure(this IQueryable<ClassificationPerformanceMeasure> classificationPerformanceMeasures, ICollection<ClassificationPerformanceMeasure> classificationPerformanceMeasuresToDelete)
        {
            if(classificationPerformanceMeasuresToDelete.Any())
            {
                var classificationPerformanceMeasureIDList = classificationPerformanceMeasuresToDelete.Select(x => x.ClassificationPerformanceMeasureID).ToList();
                var classificationPerformanceMeasuresToDeleteFromSourceList = classificationPerformanceMeasures.Where(x => classificationPerformanceMeasureIDList.Contains(x.ClassificationPerformanceMeasureID)).ToList();

                foreach (var classificationPerformanceMeasureToDelete in classificationPerformanceMeasuresToDeleteFromSourceList)
                {
                    classificationPerformanceMeasureToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteClassificationPerformanceMeasure(this IQueryable<ClassificationPerformanceMeasure> classificationPerformanceMeasures, int classificationPerformanceMeasureID)
        {
            DeleteClassificationPerformanceMeasure(classificationPerformanceMeasures, new List<int> { classificationPerformanceMeasureID });
        }

        public static void DeleteClassificationPerformanceMeasure(this IQueryable<ClassificationPerformanceMeasure> classificationPerformanceMeasures, ClassificationPerformanceMeasure classificationPerformanceMeasureToDelete)
        {
            DeleteClassificationPerformanceMeasure(classificationPerformanceMeasures, new List<ClassificationPerformanceMeasure> { classificationPerformanceMeasureToDelete });
        }
    }
}