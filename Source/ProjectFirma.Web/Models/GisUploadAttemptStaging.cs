using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class GisUploadAttemptStaging
    {

        public static List<string> ValidExtensions = new List<string> { ".shx", ".dbf", ".shp", ".prj" };

        public const string GISImportTableName = "gisimport";

        public const string GeomName = "Shape";
        public const string FIDName = "GisImportFeatureID";
        public static List<string> ImportGdbIntoSqlTempTable(FileInfo gdbFile)
        {
            var connectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(FirmaWebConfiguration.Ogr2OgrExecutable,
                Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                FirmaWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);
            var featureClassNames = OgrInfoCommandLineRunner.GetFeatureClassNamesFromFileGdb(new FileInfo(FirmaWebConfiguration.OgrInfoExecutable), gdbFile, Ogr2OgrCommandLineRunner.DefaultTimeOut);
            ogr2OgrCommandLineRunner.ImportFileGdbToSql(gdbFile, false,
                GISImportTableName, GeomName, FIDName, connectionString);
            return featureClassNames;
        }


        public static List<string> ImportShapefileIntoSqlTempTable(string shapeFilePath)
        {
            var connectionString = FirmaWebConfiguration.DatabaseConnectionString;
            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(FirmaWebConfiguration.Ogr2OgrExecutable,
                Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                FirmaWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);
            var featureClassNames = OgrInfoCommandLineRunner.GetFeatureClassNamesFromShapefile(new FileInfo(FirmaWebConfiguration.OgrInfoExecutable), shapeFilePath, Ogr2OgrCommandLineRunner.DefaultTimeOut);
            ogr2OgrCommandLineRunner.ImportPolyFromShapefile(shapeFilePath, false,
                GISImportTableName, GeomName, FIDName, connectionString);
            return featureClassNames;
        }


        public static string GetGisFileRootDirectory()
        {
            return FirmaWebConfiguration.ShapeFileRootDirectory;
        }

        public static string GisUploadAttemptDirectory(GisUploadAttempt gisUploadAttempt)
        {
            return Path.Combine(GetGisFileRootDirectory(), gisUploadAttempt.GisUploadAttemptID.ToString());
        }

        public static void SetupDirectory(GisUploadAttempt gisUploadAttempt)
        {
            if (!System.IO.Directory.Exists(GetGisFileRootDirectory()))
                System.IO.Directory.CreateDirectory(GetGisFileRootDirectory());
            if (!System.IO.Directory.Exists(GisUploadAttemptDirectory(gisUploadAttempt)))
                System.IO.Directory.CreateDirectory(GisUploadAttemptDirectory(gisUploadAttempt));
            var fileInfos = new DirectoryInfo(GisUploadAttemptDirectory(gisUploadAttempt)).GetFiles().ToList();
            fileInfos.ForEach(f => f.Delete());
        }

        public static string UnzipAndSaveFileToDiskIfShapefile(HttpPostedFileBase httpPostedFileBase,
            GisUploadAttempt gisUploadAttempt, ref bool shapeFileSuccessfullyExtractedToDisk)
        {
            ZipFile zipFile = null;
            var shapeFilePath = string.Empty;
            var zipFailure = false;
            try
            {
                zipFile = new ZipFile(httpPostedFileBase.InputStream);
            }
            catch (Exception)
            {
                zipFailure = true;
            }


            if (!zipFailure && zipFile != null)
            {
                GisUploadAttemptStaging.SetupDirectory(gisUploadAttempt);
                var extensionsFound = new List<string>();
                var shapeFilePathCreated = false;
                foreach (ZipEntry zipEntry in zipFile)
                {
                    if (!zipEntry.IsFile)
                        continue;

                    var extension = Path.GetExtension(zipEntry.Name);

                    if (ValidExtensions.Any(e => e == extension))
                    {
                        if (extensionsFound.All(e => e != extension))
                            extensionsFound.Add(extension);

                        var shapefileNameWithExtension = Path.GetFileName(zipEntry.Name);
                        if (shapefileNameWithExtension == null)
                            continue;

                        // this file is a "keeper", extract it and write it to disk

                        var fullFilePath = Path.Combine(GisUploadAttemptStaging.GisUploadAttemptDirectory(gisUploadAttempt),
                            shapefileNameWithExtension);
                        if (extension.Equals(".shp"))
                        {
                            shapeFilePath = fullFilePath;
                            shapeFilePathCreated = true;
                        }

                        var buffer = new byte[4096]; // 4K is optimum
                        var zipStream = zipFile.GetInputStream(zipEntry);

                        // unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size of the file, but does not waste memory
                        using (var streamWriter = System.IO.File.Create(fullFilePath))
                        {
                            StreamUtils.Copy(zipStream, streamWriter, buffer);
                        }
                    }
                }

                shapeFileSuccessfullyExtractedToDisk = extensionsFound.Count == ValidExtensions.Count && shapeFilePathCreated;
            }

            return shapeFilePath;
        }

    }
}