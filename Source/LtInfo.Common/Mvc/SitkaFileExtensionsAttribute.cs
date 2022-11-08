/*-----------------------------------------------------------------------
<copyright file="SitkaFileExtensionsAttribute.cs" company="Environmental Science Associates">
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LtInfo.Common.Mvc
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class SitkaFileExtensionsAttribute : ValidationAttribute, IClientValidatable
    {
        public List<string> ValidExtensions { get; set; }

        public SitkaFileExtensionsAttribute()
        {
        }

        public SitkaFileExtensionsAttribute(string fileExtensions)
        {
            ValidExtensions = fileExtensions.ToLower().Split('|').ToList();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                if (value.GetType() == typeof(HttpPostedFileBase))
                {
                    if (value is HttpPostedFileBase file)
                    {
                        var fileName = file.FileName.ToLower();
                        var isValidExtension = ValidExtensions.Any(fileName.EndsWith);
                        if (!isValidExtension)
                        {
                            string fileExtension = FileUtility.ExtensionFor(fileName);
                            string allowedExtensionsString = string.Join(", ", ValidExtensions);
                            string errorMessage = $"File extension \"{fileExtension}\" is invalid. Allowed extensions: {allowedExtensionsString}";
                            return new ValidationResult(errorMessage);
                        }
                    }

                }

                if (value.GetType() == typeof(List<HttpPostedFileBase>))
                {
                    if (value is List<HttpPostedFileBase> files && files[0] != null)
                    {
                        foreach (var file in files)
                        {
                            var fileName = file.FileName.ToLower();
                            var isValidExtension = ValidExtensions.Any(fileName.EndsWith);
                            if (!isValidExtension)
                            {
                                string fileExtension = FileUtility.ExtensionFor(fileName);
                                string allowedExtensionsString = string.Join(", ", ValidExtensions);
                                string errorMessage = $"File extension \"{fileExtension}\" is invalid. Allowed extensions: {allowedExtensionsString}";
                                return new ValidationResult(errorMessage);
                            }
                        }
                    }

                }
            }

            

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            if (!metadata.IsRequired)
                yield break;

            if (string.IsNullOrWhiteSpace(ErrorMessage))
            {
                ErrorMessage = "Uploaded file needs to be one of the following extensions: " + string.Join(", ", ValidExtensions);
            }
            var rule = new ModelClientFileExtensionValidationRule(ErrorMessage, ValidExtensions);
            yield return rule;
        }
    }
    
    public class ModelClientFileExtensionValidationRule : ModelClientValidationRule
    {
        public ModelClientFileExtensionValidationRule(string errorMessage, IEnumerable<string> fileExtensions)
        {
            ErrorMessage = errorMessage;
            ValidationType = "fileextensions";
            ValidationParameters.Add("fileextensions", string.Join(",", fileExtensions));
        }
    }
}
