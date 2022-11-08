/*-----------------------------------------------------------------------
<copyright file="ProjectLocationDetailViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FocusArea
{
    public class EditLocationViewModel : FormViewModel, IValidatableObject
    {
        [Required, DisplayName("GIS File to Upload"), SitkaFileExtensions("zip")]
        public HttpPostedFileBase FileResourceData { get; set; }

        public void UpdateModel(Models.FocusArea focusArea)
        {
            using (var disposableTempFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".gdb.zip"))
            {
                var gdbFile = disposableTempFile.FileInfo;
                FileResourceData.SaveAs(gdbFile.FullName);
                HttpRequestStorage.DatabaseEntities.FocusAreaLocationStagings.RemoveRange(focusArea.FocusAreaLocationStagings.ToList());
                focusArea.FocusAreaLocationStagings.Clear();
                FocusAreaLocationStaging.CreateFocusAreaLocationStagingListFromGdb(gdbFile, focusArea);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            FileResource.ValidateFileSize(FileResourceData, errors, GeneralUtility.NameOf(() => FileResourceData));

            using (var disposableTempFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".gdb.zip"))
            {
                var gdbFile = disposableTempFile.FileInfo;
                FileResourceData.SaveAs(gdbFile.FullName);

                var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(FirmaWebConfiguration.Ogr2OgrExecutable,
                    Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                    FirmaWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);

                List<string> featureClassNames = null;
                try
                {
                    featureClassNames = OgrInfoCommandLineRunner.GetFeatureClassNamesFromFileGdb(new FileInfo(FirmaWebConfiguration.OgrInfoExecutable),
                        gdbFile,
                        Ogr2OgrCommandLineRunner.DefaultTimeOut);
                }
                catch (Exception e)
                {
                    errors.Add(new ValidationResult("There was a problem uploading your file geodatabase. Verify it meets the requirements and is not corrupt."));
                    SitkaLogger.Instance.LogDetailedErrorMessage(e);
                }

                if (featureClassNames != null)
                {
                    var featureClasses = featureClassNames.ToDictionary(x => x,
                        x =>
                        {
                            try
                            {
                                var geoJson = ogr2OgrCommandLineRunner.ImportFileGdbToGeoJson(gdbFile, x, false);
                                return JsonTools.DeserializeObject<FeatureCollection>(geoJson);
                            }
                            catch (Exception e)
                            {
                                errors.Add(new ValidationResult($"There was a problem processing the Feature Class \"{x}\"."));
                                SitkaLogger.Instance.LogDetailedErrorMessage(e);
                                return null;
                            }
                        }).Where(x => x.Value != null && FocusAreaLocationStaging.IsUsableFeatureCollectionGeoJson(x.Value));

                    if (!featureClasses.Any())
                    {
                        errors.Add(new ValidationResult("There are no usable Feature Classes in the uploaded file. Feature Classes must contain Polygon and/or Multi-Polygon features."));
                    }
                }
            }

            return errors;
        }
    }
}
