using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class GisUploadAttemptStaging
    {
      

        public static List<string> ImportIntoSqlTempTable(FileInfo gdbFile, GisUploadAttempt gisUploadAttempt, out string importTableName)
        {
            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(FirmaWebConfiguration.Ogr2OgrExecutable,
                Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                FirmaWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);

            var featureClassNames = OgrInfoCommandLineRunner.GetFeatureClassNamesFromFileGdb(new FileInfo(FirmaWebConfiguration.OgrInfoExecutable), gdbFile, Ogr2OgrCommandLineRunner.DefaultTimeOut);
            var destinationTableName = $"gisImport{gisUploadAttempt.GisUploadAttemptID}";   
            ogr2OgrCommandLineRunner.ImportFileGdbToSql(gdbFile, false,
                destinationTableName);
            importTableName = destinationTableName;
            return featureClassNames;
        }
    }
}