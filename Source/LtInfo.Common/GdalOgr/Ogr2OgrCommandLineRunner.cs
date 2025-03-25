/*-----------------------------------------------------------------------
<copyright file="Ogr2OgrCommandLineRunner.cs" company="Environmental Science Associates">
Copyright (c) Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LtInfo.Common.DesignByContract;

namespace LtInfo.Common.GdalOgr
{
    /// <summary>
    /// Wrapper class for calling ogr2ogr.exe for the purpose of importing data from a File Geodatabase (.gdb) using the OpenFileGDB drivers in GDAL 1.11 and above
    /// </summary>
    public class Ogr2OgrCommandLineRunner
    {
        public const int DefaultCoordinateSystemId = 4326;
        public const int DefaultTimeOut = 110000;
        public const string OgrGeoJsonTableName = "OGRGeoJSON";

        private readonly FileInfo _ogr2OgrExecutable;
        private readonly int _coordinateSystemId;
        private readonly double _totalMilliseconds;
        private readonly DirectoryInfo _gdalDataPath;

        public Ogr2OgrCommandLineRunner(string pathToOgr2OgrExecutable, int coordinateSystemId, double totalMilliseconds)
        {
            _totalMilliseconds = totalMilliseconds;
            _ogr2OgrExecutable = new FileInfo(pathToOgr2OgrExecutable);
            _coordinateSystemId = coordinateSystemId;
            Check.RequireFileExists(_ogr2OgrExecutable, "Can't find ogr2ogr program in expected path. Is it installed?");
            Check.RequireNotNull(_ogr2OgrExecutable.Directory,
                $"ogr2ogr must be a full path including directory but was \"{_ogr2OgrExecutable.FullName}\"");
            // ReSharper disable once PossibleNullReferenceException
            _gdalDataPath = new DirectoryInfo(Path.Combine(_ogr2OgrExecutable.Directory.FullName, "gdal-data"));
            Check.RequireDirectoryExists(_gdalDataPath.FullName, "Can't find gdal-data directory needed for import with ogr2ogr");
        }

        /// <summary>
        /// Import GDB to SQL using GDAL Ogr2Ogr command line tool
        /// </summary>
        public void ImportFileGdbToMsSql(FileInfo inputGdbFile, string sourceLayerName, string destinationTableName, List<string> columnNameList, string connectionString)
        {
            Check.Require(inputGdbFile.FullName.ToLower().EndsWith(".gdb.zip"),
                $"Input filename for GDB input must end with .gdb.zip. Filename passed is {inputGdbFile.FullName}");
            Check.RequireFileExists(inputGdbFile, "Can't find input File GDB for import with ogr2ogr");

            var databaseConnectionString = $"MSSQL:{connectionString}";
            var commandLineArguments = BuildCommandLineArgumentsForFileGdbToMsSql(inputGdbFile, _gdalDataPath, databaseConnectionString, sourceLayerName, destinationTableName, columnNameList, _coordinateSystemId);
            ExecuteOgr2OgrCommand(commandLineArguments);
        }

        public string ImportFileGdbToGeoJson(FileInfo inputGdbFile, string sourceLayerName, bool explodeCollections)
        {            
            Check.Require(inputGdbFile.FullName.ToLower().EndsWith(".gdb.zip"),
                $"Input filename for GDB input must end with .gdb.zip. Filename passed is {inputGdbFile.FullName}");
            Check.RequireFileExists(inputGdbFile, "Can't find input File GDB for import with ogr2ogr");

            var commandLineArguments = BuildCommandLineArgumentsForFileGdbToGeoJson(inputGdbFile, _gdalDataPath, sourceLayerName, _coordinateSystemId, explodeCollections);
            var processUtilityResult = ExecuteOgr2OgrCommand(commandLineArguments);
            return processUtilityResult.StdOut;
        }

        public string ImportSqlToGeoJson(string sqlQuery, string connectionString, int coordinateSystemID)
        {
            var databaseConnectionString = $"MSSQL:{connectionString}";
            var commandLineArguments = BuildCommandLineArgumentsForSqlToGeoJson(sqlQuery, _gdalDataPath, coordinateSystemID, databaseConnectionString);
            var processUtilityResult = ExecuteOgr2OgrCommand(commandLineArguments);
            return processUtilityResult.StdOut;
        }

        public string ImportShapeFileToGeoJson(string shapeFilePath, string sourceLayerName, bool explodeCollections)
        {
            Check.Require(shapeFilePath.ToLower().EndsWith(".shp"), $"Input filename for shp input must end with .shp. Filename passed is {shapeFilePath}");

            var commandLineArguments = BuildCommandLineArgumentsForShapeFileToGeoJson(shapeFilePath, _gdalDataPath, sourceLayerName, _coordinateSystemId, explodeCollections);
            var processUtilityResult = ExecuteOgr2OgrCommand(commandLineArguments);
            return processUtilityResult.StdOut;
        }

        public string ImportFileGdbToSql(FileInfo inputGdbFile, bool explodeCollections, string destinationTableName, string geomName, string idName, string connectionString, string featureClassNameToImport)
        {
            Check.Require(inputGdbFile.FullName.ToLower().EndsWith(".gdb.zip"),
                $"Input filename for GDB input must end with .gdb.zip. Filename passed is {inputGdbFile.FullName}");
            Check.RequireFileExists(inputGdbFile, "Can't find input File GDB for import with ogr2ogr");

            var commandLineArguments = BuildCommandLineArgumentsForFileGdbToSql(inputGdbFile, _gdalDataPath, _coordinateSystemId, explodeCollections, destinationTableName, geomName, idName, connectionString, featureClassNameToImport);
            var processUtilityResult = ExecuteOgr2OgrCommand(commandLineArguments);
            return processUtilityResult.StdOut;
        }

        public string ImportPolyFromShapefile(string shapeFilePath, bool explodeCollections, string destinationTableName, string geomName, string idName, string connectionString)
        {
            Check.Require(shapeFilePath.ToLower().EndsWith(".shp"), $"Input filename for shp input must end with .shp. Filename passed is {shapeFilePath}");
            
            var commandLineArguments = BuildOgr2OgrCommandLineArgumentsForShapefileToSqlImport(shapeFilePath, _gdalDataPath, destinationTableName, _coordinateSystemId, geomName, idName, connectionString);
            var processUtilityResult = ExecuteOgr2OgrCommand(commandLineArguments);
            return processUtilityResult.StdOut;
        }

        private static List<string> BuildOgr2OgrCommandLineArgumentsForShapefileToSqlImport(string sourceShapeFilePath
            , DirectoryInfo gdalDataDirectoryInfo
            , string destinationTableName
            , int coordinateSystemID
            , string geomName
            , string fIDName
             ,   string connectionString)
        {
            var databaseConnectionString = $"MSSQL:{connectionString}";
            var commandLineArguments = new List<string>
            {
                "--config",
                "GDAL_DATA",
                gdalDataDirectoryInfo.FullName,
                "-overwrite",
                "-t_srs",
                GetMapProjection(coordinateSystemID),
                "-lco",
                "precision=NO",
                "-lco",
                $"GEOM_NAME={geomName}",
                "-lco",
                $"FID={fIDName}",
                "-f",
                "MSSQLSpatial",
                databaseConnectionString,
                sourceShapeFilePath,
                "-nln",
                destinationTableName,
                "-skipfailures"
            };
            return commandLineArguments;
        }

        public void ImportGeoJsonToMsSql(string geoJson, string connectionString, string destinationTableName, string sourceColumnName, string destinationColumnName, string extraColumns)
        {
            var databaseConnectionString = $"MSSQL:{connectionString}";
            using (var geoJsonFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".json"))
            {
                File.WriteAllText(geoJsonFile.FileInfo.FullName, geoJson);
                var commandLineArguments = BuildCommandLineArgumentsForGeoJsonToMsSql(geoJsonFile.FileInfo,
                    sourceColumnName, destinationTableName, destinationColumnName, _gdalDataPath, databaseConnectionString, _coordinateSystemId, extraColumns);
                ExecuteOgr2OgrCommand(commandLineArguments);
            }
        }

        /// <summary>
        /// Import GDB to SQL using GDAL Ogr2Ogr command line tool
        /// </summary>
        public void ImportArcGisQueryToMsSql(string arcGisQuery, string destinationTableName, string sourceColumnName, string destinationColumnName, string connectionString)
        {
            var databaseConnectionString = $"MSSQL:{connectionString}";
            var commandLineArguments = BuildCommandLineArgumentsForArgGisQueryToMsSql(arcGisQuery, _gdalDataPath, databaseConnectionString, destinationTableName, sourceColumnName, destinationColumnName, _coordinateSystemId);
            ExecuteOgr2OgrCommand(commandLineArguments);
        }

        public ProcessUtilityResult ExecuteOgr2OgrCommand(List<string> commandLineArguments)
        {

            var argumentsAsStringCombined = String.Join(" ", commandLineArguments.Select(ProcessUtility.EncodeArgumentForCommandLine).ToList());
            var fullProcessAndArgumentsString =
                $"{ProcessUtility.EncodeArgumentForCommandLine(_ogr2OgrExecutable.FullName)} {argumentsAsStringCombined}";
            var errorMessageString = $"Process Command Line:\r\n{fullProcessAndArgumentsString}\r\n\r\nProcess Working Directory: {_ogr2OgrExecutable.DirectoryName}";

            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(_ogr2OgrExecutable.DirectoryName, _ogr2OgrExecutable.FullName, commandLineArguments, true, Convert.ToInt32(_totalMilliseconds));
            if (processUtilityResult.ReturnCode != 0)
            {
                var argumentsAsString = String.Join(" ", commandLineArguments.Select(ProcessUtility.EncodeArgumentForCommandLine).ToList());
                var fullProcessAndArguments =
                    $"{ProcessUtility.EncodeArgumentForCommandLine(_ogr2OgrExecutable.FullName)} {argumentsAsString}";
                var errorMessage =
                    $"Process \"{_ogr2OgrExecutable.Name}\" returned with exit code {processUtilityResult}, expected exit code 0.\r\n\r\nStdErr and StdOut:\r\n{processUtilityResult.StdOutAndStdErr}\r\n\r\nProcess Command Line:\r\n{fullProcessAndArguments}\r\n\r\nProcess Working Directory: {_ogr2OgrExecutable.DirectoryName}";
                throw new Ogr2OgrCommandLineException(errorMessage);
            }
            return processUtilityResult;
        }

        /// <summary>
        /// Produces the command line arguments for ogr2ogr.exe to run the File Geodatabase import.
        /// <example>"C:\Program Files\GDAL\ogr2ogr.exe" -progress -append --config GDAL_DATA "C:\Program Files\GDAL\gdal-data" -t_srs "EPSG:4326" -f MSSQLSpatial "MSSQL:server=(local);database=Scratch;trusted_connection=yes" "C:\temp\GdalScratch\Sub_Actions_20131219.gdb" "Sub_Actions_Polygon_20131219" -nln MyTable</example>
        /// </summary>
        internal static List<string> BuildCommandLineArgumentsForFileGdbToMsSql(FileInfo inputGdbFile, DirectoryInfo gdalDataDirectoryInfo, string databaseConnectionString, string sourceLayerName, string targetTableName, List<string> columnNameList, int coordinateSystemId)
        {
            var reservedFields = new[] { "Ogr_Fid", "Ogr_Geometry" };
            var filteredColumnNameList = columnNameList.Where(x => reservedFields.All(y => !String.Equals(x, y, StringComparison.InvariantCultureIgnoreCase))).ToList();
            const string ogr2OgrColumnListSeparator = ",";
            Check.Require(filteredColumnNameList.All(x => !x.Contains(ogr2OgrColumnListSeparator)),
                $"Found column names with separator character \"{ogr2OgrColumnListSeparator}\", can't continue. Columns:{String.Join("\r\n", filteredColumnNameList)}");
            Check.Require(filteredColumnNameList.All(x => !Regex.IsMatch(x, @"\s")),
                $"Found column names with whitespace in them, can't continue. Columns:{String.Join("\r\n", filteredColumnNameList)}");

            var selectStatement =
                $"select {String.Join(ogr2OgrColumnListSeparator + " ", filteredColumnNameList)} from {sourceLayerName}";
            var commandLineArguments =  new List<string>
            {
                "-append",
                "-sql",
                selectStatement,
                "--config",
                "GDAL_DATA",
                gdalDataDirectoryInfo.FullName,
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                "-f",
                "MSSQLSpatial",
                databaseConnectionString,
                inputGdbFile.FullName,
                "-nln",
                targetTableName
            };

            return commandLineArguments;
        }

        /// <summary>
        /// Produces the command line arguments for ogr2ogr.exe to run the File Geodatabase import.
        /// <example>"C:\Program Files\GDAL\ogr2ogr.exe" -progress -append --config GDAL_DATA "C:\Program Files\GDAL\gdal-data" -t_srs "EPSG:4326" -f MSSQLSpatial "MSSQL:server=(local);database=Scratch;trusted_connection=yes" "C:\temp\GdalScratch\Sub_Actions_20131219.gdb" "Sub_Actions_Polygon_20131219" -nln MyTable</example>
        /// </summary>
        internal static List<string> BuildCommandLineArgumentsForArgGisQueryToMsSql(string arcGisQuery, DirectoryInfo gdalDataDirectoryInfo, string databaseConnectionString, string targetTableName, string sourceColumnName, string destinationColumnName, int coordinateSystemId)
        {
            var commandLineArguments =  new List<string>
            {
                "-append",
                "-sql",
                $"SELECT {sourceColumnName} AS {destinationColumnName} FROM {OgrGeoJsonTableName}",
                "--config",
                "GDAL_DATA",
                gdalDataDirectoryInfo.FullName,
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                "-f",
                "MSSQLSpatial",
                databaseConnectionString,
                $"\"{arcGisQuery}\"",
                "-nln",
                targetTableName
            };

            return commandLineArguments;
        }

        internal static List<string> BuildCommandLineArgumentsForGeoJsonToMsSql(FileInfo sourceGeoJsonFile, string sourceColumnName, string destinationTableName, string destinationColumnName, DirectoryInfo gdalDataDirectoryInfo, string databaseConnectionString, int coordinateSystemId, string extraColumns)
        {
            //c:\SVN\sitkatech\trunk\Corral\Build>"C:\Program Files\GDAL\ogr2ogr.exe" -preserve_fid --config GDAL_DATA "C:\\Program Files\\GDAL\\gdal-data" -t_srs EPSG:4326 -f MSSQLSpatial "MSSQL:server=localhost;database=tempdb;trusted_connection=yes" "C:\temp\geojson.json" -nln "TestTable"            

            var commandLineArguments = new List<string>
            {
                "-append",
                "-sql",
                $"SELECT {sourceColumnName} AS {destinationColumnName}{extraColumns} FROM {OgrGeoJsonTableName}",
                "--config",
                "GDAL_DATA",
                gdalDataDirectoryInfo.FullName,
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                "-explodecollections",
                "-f",
                "MSSQLSpatial",
                databaseConnectionString,
                sourceGeoJsonFile.FullName,
                "-nln",
                destinationTableName
            };

            return commandLineArguments;
        }

        /// <summary>
        /// Produces the command line arguments for ogr2ogr.exe to run the File Geodatabase import.
        /// <example>"C:\Program Files\GDAL\ogr2ogr.exe" -preserve_fid --config GDAL_DATA "C:\\Program Files\\GDAL\\gdal-data" -t_srs EPSG:4326 -f GeoJSON /dev/stdout "C:\\svn\\sitkatech\\trunk\\Corral\\Source\\ProjectFirma.Web\\Models\\GdalOgr\\SampleFileGeodatabase.gdb.zip" "somelayername"</example>
        /// </summary>
        internal static List<string> BuildCommandLineArgumentsForFileGdbToGeoJson(FileInfo inputGdbFile, DirectoryInfo gdalDataDirectoryInfo, string sourceLayerName, int coordinateSystemId, bool explodeCollections)
        {
            var commandLineArguments =  new List<string>
            {
                "--config",
                "GDAL_DATA",
                gdalDataDirectoryInfo.FullName,
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                explodeCollections ? "-explodecollections" : null,
                "-f",
                "GeoJSON",
                "/dev/stdout",
                inputGdbFile.FullName,
                $"\"{sourceLayerName}\"",
                "-dim",
                "2"
            };

            return commandLineArguments.Where(x => x != null).ToList();
        }

        /// <summary>
        /// Produces the command line arguments for ogr2ogr.exe to run the File Geodatabase import.
        /// <example>"C:\Program Files\GDAL\ogr2ogr.exe" -preserve_fid --config GDAL_DATA "C:\\Program Files\\GDAL\\gdal-data" -t_srs EPSG:4326 -f GeoJSON /dev/stdout "C:\\svn\\sitkatech\\trunk\\Corral\\Source\\ProjectFirma.Web\\Models\\GdalOgr\\SampleFileGeodatabase.gdb.zip" "somelayername"</example>
        /// </summary>
        internal static List<string> BuildCommandLineArgumentsForSqlToGeoJson(string sqlQuery, DirectoryInfo gdalDataDirectoryInfo, int coordinateSystemId, string connectionString)
        {

            var replacedConnectionString =
                connectionString.Replace("Trusted_Connection=True", "Trusted_Connection=Yes");

            var commandLineArguments = new List<string>
            {
                "--config",
                "GDAL_DATA",
                gdalDataDirectoryInfo.FullName,
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                "-f",
                "GeoJSON",
                "/dev/stdout",
                replacedConnectionString,
                "-sql",
                sqlQuery,
                "-dim",
                "2"
            };

            return commandLineArguments.Where(x => x != null).ToList();
        }

        /// <summary>
        /// Produces the command line arguments for ogr2ogr.exe to run the File Geodatabase import.
        /// <example>"C:\Program Files\GDAL\ogr2ogr.exe" -preserve_fid --config GDAL_DATA "C:\\Program Files\\GDAL\\gdal-data" -t_srs EPSG:4326 -f GeoJSON /dev/stdout "C:\\svn\\sitkatech\\trunk\\Corral\\Source\\ProjectFirma.Web\\Models\\GdalOgr\\SampleFileGeodatabase.gdb.zip" "somelayername"</example>
        /// </summary>
        internal static List<string> BuildCommandLineArgumentsForShapeFileToGeoJson(string shapeFilePath, DirectoryInfo gdalDataDirectoryInfo, string sourceLayerName, int coordinateSystemId, bool explodeCollections)
        {
            var commandLineArguments = new List<string>
            {
                "--config",
                "GDAL_DATA",
                gdalDataDirectoryInfo.FullName,
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                explodeCollections ? "-explodecollections" : null,
                "-f",
                "GeoJSON",
                "/dev/stdout",
                shapeFilePath,
                $"\"{sourceLayerName}\"",
                "-dim",
                "2"
            };

            return commandLineArguments.Where(x => x != null).ToList();
        }

        /// <summary>
        /// Produces the command line arguments for ogr2ogr.exe to run the File Geodatabase import.
        /// <example>"C:\Program Files\GDAL\ogr2ogr.exe" -preserve_fid --config GDAL_DATA "C:\\Program Files\\GDAL\\gdal-data" -t_srs EPSG:4326 -f GeoJSON /dev/stdout "C:\\svn\\sitkatech\\trunk\\Corral\\Source\\ProjectFirma.Web\\Models\\GdalOgr\\SampleFileGeodatabase.gdb.zip" "somelayername"</example>
        /// </summary>
        internal static List<string> BuildCommandLineArgumentsForFileGdbToSql(FileInfo inputGdbFile
            , DirectoryInfo gdalDataDirectoryInfo
            , int coordinateSystemId
            , bool explodeCollections
            , string destinationTableName
            , string geomName
            , string fIDName
            , string connectionString
            , string featureClassNameToImport)
        {
            var databaseConnectionString = $"MSSQL:{connectionString}";
            var commandLineArguments = new List<string>
            {
                "--config",
                "GDAL_DATA",
                gdalDataDirectoryInfo.FullName,
                "-overwrite",
                "-lco",
                "precision=NO",
                "-lco",
                $"GEOM_NAME={geomName}",
                "-lco",
                $"FID={fIDName}",
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                explodeCollections ? "-explodecollections" : null,
                "-f",
                "MSSQLSpatial",
                databaseConnectionString,
                inputGdbFile.FullName,
                "-nln",
                $"\"{destinationTableName}\""
                , "-skipfailures"
            };

            if (!string.IsNullOrEmpty(featureClassNameToImport))
            {
                commandLineArguments.Add(featureClassNameToImport);
            }

            return commandLineArguments.Where(x => x != null).ToList();
        }

        public static string GetMapProjection(int coordinateSystemId)
        {
            return $"EPSG:{coordinateSystemId}";
        }


        public static List<string> BuildCommandLineArgumentsForGeoJsonToFileGdb(string pathToSourceGeoJsonFile, int coordinateSystemId, string outputPath, string outputLayerName, bool update, string geometryType)
        {
            var commandLineArguments = new List<string>
            {
                update ? "-update" : null,
                "-s_srs",
                GetMapProjection(coordinateSystemId),
                "-a_srs",
                GetMapProjection(coordinateSystemId),
                "-f",
                "OpenFileGDB",
                outputPath,
                pathToSourceGeoJsonFile,
                "-nln",
                SanitizeStringForGdb(outputLayerName),
                "-nlt",
                geometryType,
                "-append",
            };

            return commandLineArguments.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        }

        public static string SanitizeStringForGdb(string str)
        {
            var arr = str.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray();
            return new string(arr).Replace(" ", "_");
        }


    }
}
