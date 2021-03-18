using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class FileResourceModelExtensions
    {
        public static int MaxUploadFileSizeInBytes = FirmaWebConfiguration.MaximumAllowedUploadFileSize;

        public static string MaxFileSizeHumanReadable
        {
            get { return $"{MaxUploadFileSizeInBytes / (1024 ^ 2):0.0} KB"; }
        }

        public static string GetMaxFileSizeHumanReadable(int maxFileSizeInBytes)
        {
            return $"{maxFileSizeInBytes / (1024 ^ 2):0.0} KB";
        }

        /// <summary>
        /// Prepare the file bytes for going into the database
        /// </summary>
        /// <param name="httpPostedFileBase"></param>
        /// <returns></returns>
        private static byte[] ConvertHttpPostedFileToByteArray(HttpPostedFileBase httpPostedFileBase)
        {
            byte[] fileResourceData;
            using (var binaryReader = new BinaryReader(httpPostedFileBase.InputStream))
            {
                fileResourceData = binaryReader.ReadBytes(httpPostedFileBase.ContentLength);
                binaryReader.Close();
            }
            return fileResourceData;
        }

        public static FileResourceMimeType GetFileResourceMimeTypeForFile(HttpPostedFileBase file)
        {
            var fileResourceMimeTypeForFile = FileResourceMimeType.All.SingleOrDefault(mt => mt.FileResourceMimeTypeContentTypeName == file.ContentType);
            Check.RequireNotNull(fileResourceMimeTypeForFile, $"Unhandled MIME type: {file.ContentType}");
            return fileResourceMimeTypeForFile;
        }

        public static FileResource CreateNewFromHttpPostedFileAndSave(HttpPostedFileBase httpPostedFileBase, Person currentPerson)
        {
            var fileResource = CreateNewFromHttpPostedFile(httpPostedFileBase, currentPerson);
            HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            return fileResource;
        }

        //Only public for unit testing
        public static FileResource CreateNewFromHttpPostedFile(HttpPostedFileBase httpPostedFileBase, Person currentPerson)
        {
            //public FileResource(int fileResourceMimeTypeID, string originalBaseFilename, string originalFileExtension, Guid fileResourceGUID, byte[] fileResourceData, int createPersonID, DateTime createDate) : this()
            var originalFilenameInfo = new FileInfo(httpPostedFileBase.FileName);
            var baseFilenameWithoutExtension = originalFilenameInfo.Name.Remove(originalFilenameInfo.Name.Length - originalFilenameInfo.Extension.Length, originalFilenameInfo.Extension.Length);
            var fileResourceData = ConvertHttpPostedFileToByteArray(httpPostedFileBase);
            var fileResourceMimeTypeID = GetFileResourceMimeTypeForFile(httpPostedFileBase).FileResourceMimeTypeID;
            var fileResourceInfo = new FileResource(fileResourceMimeTypeID, baseFilenameWithoutExtension, originalFilenameInfo.Extension, Guid.NewGuid(), fileResourceData,currentPerson.PersonID, DateTime.Now);
            return fileResourceInfo;
        }

        public static void ValidateFileSize(HttpPostedFileBase httpPostedFileBase, List<ValidationResult> errors, string propertyName)
        {
            if (httpPostedFileBase.ContentLength > MaxUploadFileSizeInBytes)
            {
                var formattedUploadSize = $"~{(httpPostedFileBase.ContentLength / 1000).ToGroupedNumeric()} KB";
                errors.Add(new ValidationResult(
                    $"File is too large - must be less than {MaxFileSizeHumanReadable} [Provided file was {formattedUploadSize}]", new[] { propertyName }));
            }
        }

        public static void ValidateFileSize(HttpPostedFileBase httpPostedFileBase, List<ValidationResult> errors, string propertyName, int maxFileSize)
        {
            if (httpPostedFileBase.ContentLength > maxFileSize)
            {
                var maxFileSizeHumanReadable = GetMaxFileSizeHumanReadable(maxFileSize);
                var formattedUploadSize = $"~{(httpPostedFileBase.ContentLength / 1000).ToGroupedNumeric()} KB";
                errors.Add(new ValidationResult(
                    $"File is too large - must be less than {maxFileSizeHumanReadable} [Provided file was {formattedUploadSize}]", new[] { propertyName }));
            }
        }

        public static readonly Regex FileResourceUrlRegEx =
            new Regex(@"FileResource\/DisplayResource\/(?<fileResourceGuidCapture>[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Based on a string that has embedded file resource URLs in it, parse out the URLs and look up the corresponding FileResourceInfo stuff
        /// Made public for testing purposes.
        /// </summary>
        public static List<Guid> FindAllFileResourceGuidsFromStringContainingFileResourceUrls(string textWithReferences)
        {
            if (String.IsNullOrWhiteSpace(textWithReferences))
            {
                return new List<Guid>();
            }
            var guidCaptures = FileResourceUrlRegEx.Matches(textWithReferences).Cast<Match>().Select(x => x.Groups["fileResourceGuidCapture"].Value).ToList();
            var theseGuids = guidCaptures.Select(x => new Guid(x)).Distinct().ToList();
            return theseGuids;
        }
    }
}