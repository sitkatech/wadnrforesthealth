﻿/*-----------------------------------------------------------------------
<copyright file="FileResourceController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
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
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;
using LtInfo.Common;
using LtInfo.Common.MvcResults;

namespace ProjectFirma.Web.Controllers
{
    public class FileResourceController : FirmaBaseController
    {
        [AnonymousUnclassifiedFeature]
        [CrossAreaRoute]
        public ActionResult DisplayResource(string fileResourceGuidAsString)
        {
            Guid fileResourceGuid;
            var isStringAGuid = Guid.TryParse(fileResourceGuidAsString, out fileResourceGuid);
            if (isStringAGuid)
            {
                var fileResource = HttpRequestStorage.DatabaseEntities.FileResources.SingleOrDefault(x => x.FileResourceGUID == fileResourceGuid);

                return DisplayResourceImpl(fileResourceGuidAsString, fileResource);
            }
            // Unhappy path - return an HTTP 404
            // ---------------------------------
            var message = $"File Resource {fileResourceGuidAsString} Not Found in database. It may have been deleted.";
            throw new HttpException(404, message);
        }

        private ActionResult DisplayResourceImpl(string fileResourcePrimaryKey, FileResource fileResource)
        {
            if (fileResource == null)
            {
                var message = $"File Resource {fileResourcePrimaryKey} Not Found in database. It may have been deleted.";
                throw new HttpException(404, message);
            }

            switch (fileResource.FileResourceMimeType.ToEnum)
            {
                case FileResourceMimeTypeEnum.ExcelXLS:
                case FileResourceMimeTypeEnum.ExcelXLSX:
                case FileResourceMimeTypeEnum.xExcelXLSX:
                    return new ExcelResult(new MemoryStream(fileResource.FileResourceData), fileResource.OriginalCompleteFileName);
                case FileResourceMimeTypeEnum.PDF:
                    return new PdfResult(fileResource);
                case FileResourceMimeTypeEnum.WordDOCX:
                case FileResourceMimeTypeEnum.WordDOC:
                case FileResourceMimeTypeEnum.PowerpointPPTX:
                case FileResourceMimeTypeEnum.PowerpointPPT:
                case FileResourceMimeTypeEnum.CSS:
                case FileResourceMimeTypeEnum.TXT:
                case FileResourceMimeTypeEnum.ZIP:
                case FileResourceMimeTypeEnum.XZIP:
                    return new FileResourceResult(fileResource.OriginalCompleteFileName, fileResource.FileResourceData, fileResource.FileResourceMimeType);
                case FileResourceMimeTypeEnum.XPNG:
                case FileResourceMimeTypeEnum.PNG:
                case FileResourceMimeTypeEnum.TIFF:
                case FileResourceMimeTypeEnum.BMP:
                case FileResourceMimeTypeEnum.GIF:
                case FileResourceMimeTypeEnum.JPEG:
                case FileResourceMimeTypeEnum.PJPEG:
                    return File(fileResource.FileResourceData, fileResource.FileResourceMimeType.FileResourceMimeTypeName);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [LoggedInUnclassifiedFeature]
        [CrossAreaRoute]
        public ActionResult DisplayResourceByID(FileResourcePrimaryKey fileResourcePrimaryKey)
        {
            var fileResource = fileResourcePrimaryKey.EntityObject;
            return DisplayResourceImpl(fileResourcePrimaryKey.PrimaryKeyValue.ToString(), fileResource);
        }

        [AnonymousUnclassifiedFeature]
        [CrossAreaRoute]
        public ActionResult GetFileResourceResized(string fileResourceGuidAsString, int maxWidth, int maxHeight)
        {
            Guid fileResourceGuid;
            var isStringAGuid = Guid.TryParse(fileResourceGuidAsString, out fileResourceGuid);
            if (isStringAGuid)
            {
                var fileResource = HttpRequestStorage.DatabaseEntities.FileResources.SingleOrDefault(x => x.FileResourceGUID == fileResourceGuid);
                if (fileResource != null)
                {
                    // Happy path - return the resource
                    // ---------------------------------
                    switch (fileResource.FileResourceMimeType.ToEnum)
                    {
                        case FileResourceMimeTypeEnum.ExcelXLS:
                        case FileResourceMimeTypeEnum.ExcelXLSX:
                        case FileResourceMimeTypeEnum.xExcelXLSX:
                        case FileResourceMimeTypeEnum.PDF:
                        case FileResourceMimeTypeEnum.WordDOCX:
                        case FileResourceMimeTypeEnum.WordDOC:
                        case FileResourceMimeTypeEnum.PowerpointPPTX:
                        case FileResourceMimeTypeEnum.PowerpointPPT:
                            throw new ArgumentOutOfRangeException($"Not supported mime type {fileResource.FileResourceMimeType.FileResourceMimeTypeDisplayName}");
                        case FileResourceMimeTypeEnum.XPNG:
                        case FileResourceMimeTypeEnum.PNG:
                        case FileResourceMimeTypeEnum.TIFF:
                        case FileResourceMimeTypeEnum.BMP:
                        case FileResourceMimeTypeEnum.GIF:
                        case FileResourceMimeTypeEnum.JPEG:
                        case FileResourceMimeTypeEnum.PJPEG:
                            using (var scaledImage = ImageHelper.ScaleImage(fileResource.FileResourceData, maxWidth, maxHeight))
                            {
                                using (var ms = new MemoryStream())
                                {
                                    scaledImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                    return File(ms.ToArray(), FileResourceMimeType.PNG.FileResourceMimeTypeName);
                                }
                            }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            // Unhappy path - return an HTTP 404
            // ---------------------------------
            var message = $"File Resource {fileResourceGuidAsString} Not Found in database. It may have been deleted.";
            throw new HttpException(404, message);
        }

        /// <summary>
        /// Dummy fake HTTP "GET" for <see cref="CkEditorUploadFileResource(FirmaPagePrimaryKey, CkEditorImageUploadViewModel)"/>
        /// </summary>
        /// <returns></returns>
        [CrossAreaRoute]
        [HttpGet]
        [FirmaPageManageFeature]
        public ContentResult CkEditorUploadFileResource(FirmaPagePrimaryKey firmaPagePrimaryKey)
        {
            return Content(String.Empty);
        }

        [CrossAreaRoute]
        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        [FirmaPageManageFeature]
        public ContentResult CkEditorUploadFileResource(FirmaPagePrimaryKey firmaPagePrimaryKey, CkEditorImageUploadViewModel viewModel)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.upload, CurrentPerson);
            var firmaPage = firmaPagePrimaryKey.EntityObject;
            var ppImage = new FirmaPageImage(firmaPage, fileResource);
            HttpRequestStorage.DatabaseEntities.FirmaPageImages.Add(ppImage);
            return Content(viewModel.GetCkEditorJavascriptContentToReturn(fileResource));
        }

        /// <summary>
        /// Dummy fake HTTP "GET" for <see cref="CkEditorUploadFileResource(FirmaPagePrimaryKey, CkEditorImageUploadViewModel)"/>
        /// </summary>
        /// <returns></returns>
        [CrossAreaRoute]
        [HttpGet]
        [FirmaPageManageFeature]
        public ContentResult CkEditorUploadFileResourceForFirmaPage(FirmaPagePrimaryKey firmaPagePrimaryKey)
        {
            return Content(String.Empty);
        }


        [CrossAreaRoute]
        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        [FirmaPageManageFeature]
        public ContentResult CkEditorUploadFileResourceForFirmaPage(FirmaPagePrimaryKey firmaPagePrimaryKey, CkEditorImageUploadViewModel viewModel)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.upload, CurrentPerson);
            var firmaPage = firmaPagePrimaryKey.EntityObject;
            var ppImage = new FirmaPageImage(firmaPage, fileResource);
            HttpRequestStorage.DatabaseEntities.FirmaPageImages.Add(ppImage);
            return Content(viewModel.GetCkEditorJavascriptContentToReturn(fileResource));
        }

        /// <summary>
        /// Dummy fake HTTP "GET" for <see cref="CkEditorUploadFileResourceForFieldDefinition(FieldDefinitionPrimaryKey, CkEditorImageUploadViewModel)"/>
        /// </summary>
        /// <returns></returns>
        [CrossAreaRoute]
        [HttpGet]
        [FieldDefinitionManageFeature]
        public ContentResult CkEditorUploadFileResourceForFieldDefinition(FieldDefinitionPrimaryKey fieldDefinitionPrimaryKey)
        {
            return Content(String.Empty);
        }

        [CrossAreaRoute]
        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        [FieldDefinitionManageFeature]
        public ContentResult CkEditorUploadFileResourceForFieldDefinition(FieldDefinitionPrimaryKey fieldDefinitionPrimaryKey, CkEditorImageUploadViewModel viewModel)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.upload, CurrentPerson);
            var fieldDefinition = fieldDefinitionPrimaryKey.EntityObject;
            var image = new FieldDefinitionDataImage(fieldDefinition.GetFieldDefinitionData().FieldDefinitionDataID, fileResource.FileResourceID);
            HttpRequestStorage.DatabaseEntities.FieldDefinitionDataImages.Add(image);
            return Content(viewModel.GetCkEditorJavascriptContentToReturn(fileResource));
        }

        public class CkEditorImageUploadViewModel
        {
            // ReSharper disable InconsistentNaming
            public string CKEditorFuncNum { get; set; }
            public HttpPostedFileBase upload { get; set; }
            // ReSharper restore InconsistentNaming

            public string GetCkEditorJavascriptContentToReturn(FileResource fileResource)
            {
                var ckEditorJavascriptContentToReturn = $@"
<script language=""javascript"" type=""text/javascript"">
    // <![CDATA[
    window.parent.CKEDITOR.tools.callFunction({CKEditorFuncNum}, {fileResource.FileResourceUrl.ToJS()});
    // ]]>
</script>";
                return ckEditorJavascriptContentToReturn;
            }
        }


        /// <summary>
        /// Dummy fake HTTP "GET" for <see cref="CkEditorUploadFileResourceForCustomPage(CustomPagePrimaryKey, CkEditorImageUploadViewModel)"/>
        /// </summary>
        /// <returns></returns>
        [CrossAreaRoute]
        [HttpGet]
        [CustomPageManageFeature]
        public ContentResult CkEditorUploadFileResourceForCustomPage(CustomPagePrimaryKey customPagePrimaryKey)
        {
            return Content(String.Empty);
        }

        [CrossAreaRoute]
        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        [CustomPageManageFeature]
        public ContentResult CkEditorUploadFileResourceForCustomPage(CustomPagePrimaryKey customPagePrimaryKey, CkEditorImageUploadViewModel viewModel)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.upload, CurrentPerson);
            var customPage = customPagePrimaryKey.EntityObject;
            var ppImage = new CustomPageImage(customPage, fileResource);
            HttpRequestStorage.DatabaseEntities.CustomPageImages.Add(ppImage);
            return Content(viewModel.GetCkEditorJavascriptContentToReturn(fileResource));
        }


        /// <summary>
        /// Dummy fake HTTP "GET" for <see cref="CkEditorUploadFileResourceForDnrUplandRegionPage(DNRUplandRegionPrimaryKey, CkEditorImageUploadViewModel)"/>
        /// </summary>
        /// <returns></returns>
        [CrossAreaRoute]
        [HttpGet]
        [CustomPageManageFeature]
        public ContentResult CkEditorUploadFileResourceForDnrUplandRegionPage(DNRUplandRegionPrimaryKey regionPrimaryKey)
        {
            return Content(String.Empty);
        }

        [CrossAreaRoute]
        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        [CustomPageManageFeature]
        public ContentResult CkEditorUploadFileResourceForDnrUplandRegionPage(DNRUplandRegionPrimaryKey regionPrimaryKey, CkEditorImageUploadViewModel viewModel)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(viewModel.upload, CurrentPerson);
            var region = regionPrimaryKey.EntityObject;
            var regionContentImage = new DNRUplandRegionContentImage(region, fileResource);
            HttpRequestStorage.DatabaseEntities.DNRUplandRegionContentImages.Add(regionContentImage);
            return Content(viewModel.GetCkEditorJavascriptContentToReturn(fileResource));
        }
    }
}
