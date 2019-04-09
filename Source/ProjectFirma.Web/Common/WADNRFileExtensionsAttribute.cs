using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Common
{
    public class WADNRFileExtensionsAttribute : SitkaFileExtensionsAttribute
    {
        public WADNRFileExtensionsAttribute(string fileExtensions) : base(fileExtensions)
        {
        }

        public WADNRFileExtensionsAttribute(params FileResourceMimeTypeEnum[] allowedMimeTypes)
        {
            List<int> allowedFileResourceMimeTypeIDs = allowedMimeTypes.Select(x => (int)x).ToList();
            List<string> fileExtensionsToReturn = new List<string>();

            foreach (var currentFileResourceFileExtension in HttpRequestStorage.DatabaseEntities.FileResourceMimeTypeFileExtensions)
            {
                int currentFileResourceMimeTypeID = currentFileResourceFileExtension.FileResourceMimeTypeID;
                if (allowedFileResourceMimeTypeIDs.Contains(currentFileResourceMimeTypeID))
                {
                    fileExtensionsToReturn.Add(currentFileResourceFileExtension.FileResourceMimeTypeFileExtensionText);
                }
            }

            ValidExtensions = fileExtensionsToReturn;
        }

    }
}