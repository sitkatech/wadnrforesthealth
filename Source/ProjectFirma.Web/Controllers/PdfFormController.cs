﻿/*-----------------------------------------------------------------------
<copyright file="AccountController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Controllers
{
    public class PdfFormController : FirmaBaseController
    {

        private string BlankCostSharePdfFilePath()
        {
            return Server.MapPath(Url.Content("~/Content/documents/CostShareBlankFormFields.pdf"));
        }

        private string BlankOutputFileName()
        {
            return "CostShareAgreementBlank.pdf";
        }

        [AnonymousUnclassifiedFeature]
        public FileContentResult BlankCostShareAgreementPdf()
        {
            string blankCostSharePdfFilePath = BlankCostSharePdfFilePath();
            var outputFileName = BlankOutputFileName();

            var binaryContentsOfOutputPdfFile = System.IO.File.ReadAllBytes(blankCostSharePdfFilePath);
            return File(binaryContentsOfOutputPdfFile, "application/pdf", outputFileName);
            
        }

        [CostSharePDFFilledInFeature]
        public FileContentResult CostShareAgreementPdf(ProjectPersonPrimaryKey projectPersonPrimaryKey)
        {
            string blankCostSharePdfFilePath = BlankCostSharePdfFilePath();
            byte[] binaryContentsOfOutputPdfFile;
            string outputFileName;

            ProjectPerson landownerProjectPerson = projectPersonPrimaryKey.EntityObject;
            Person landownerPerson = landownerProjectPerson.Person;

            Check.Ensure(landownerProjectPerson.ProjectPersonRelationshipType == ProjectPersonRelationshipType.PrivateLandowner, $"Only expecting Landowner contacts here. {landownerProjectPerson.Person.FullNameFirstLast} is a {landownerProjectPerson.ProjectPersonRelationshipType} on Project {landownerProjectPerson.Project.DisplayName}.");

            using (var outputPdfFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".pdf"))
            {
                PdfDocument pdf = new PdfDocument(new PdfReader(blankCostSharePdfFilePath), new PdfWriter(outputPdfFile.FileInfo.FullName));
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
                IDictionary<String, PdfFormField> fields = form.GetFormFields();

                var landownerName = landownerPerson.FullNameFirstLast;
                outputFileName = $"CostShareAgreement-{landownerName.Replace(" ", "")}.pdf";

                fields.TryGetValue("Names", out var nameToSet);
                nameToSet?.SetValue(MakeEmptyStringForNullString(landownerName));

                fields.TryGetValue("Address1", out var address1);
                address1?.SetValue(MakeEmptyStringForNullString(landownerPerson.PersonAddress));

                fields.TryGetValue("Address2", out var address2);
                address2?.SetValue(MakeEmptyStringForNullString(string.Empty));

                fields.TryGetValue("PhoneNumber", out var phoneToSet);
                phoneToSet?.SetValue(MakeEmptyStringForNullString(landownerPerson.Phone));

                fields.TryGetValue("Email", out var emailToSet);
                emailToSet?.SetValue(MakeEmptyStringForNullString(landownerPerson.Email));

                form.FlattenFields();
                pdf.Close();

                binaryContentsOfOutputPdfFile = System.IO.File.ReadAllBytes(outputPdfFile.FileInfo.FullName);
            }

            return File(binaryContentsOfOutputPdfFile, "application/pdf", outputFileName);
        }

        private static string MakeEmptyStringForNullString(string inputString)
        {
            return inputString == null ? string.Empty : inputString;
        }


    }
}
