/*-----------------------------------------------------------------------
<copyright file="AccountController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
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
using System.Web.Mvc;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using LtInfo.Common;
using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class PdfFormController : FirmaBaseController
    {

        [AnonymousUnclassifiedFeature]
        public FileContentResult DoStuffWithPdf()
        {
            // TODO: Permissions checks?

            //string filePath = Server.MapPath(Url.Content("~/Content/Images/Image.jpg"));
            string blankCostSharePdfFilePath = Server.MapPath(Url.Content("~/Content/documents/CostShareBlankFormFields.pdf"));
            //string pdfInputFilename = "c:\\temp\\SampleHealth.pdf";
            //string pdfOutputFilename = "c:\\temp\\SampleHealthOutput2.pdf";

            byte[] binaryContentsOfOutputPdfFile;
            using (var outputPdfFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".pdf"))
            {
                string outputFileName = outputPdfFile.FileInfo.FullName;

                PdfDocument pdf = new PdfDocument(new PdfReader(blankCostSharePdfFilePath), new PdfWriter(outputFileName));
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
                IDictionary<String, PdfFormField> fields = form.GetFormFields();

                fields.TryGetValue("Names", out var nameToSet);
                nameToSet.SetValue("Name Goes Here");

                fields.TryGetValue("Address1", out var address1);
                address1.SetValue("Address 1 goes here");

                fields.TryGetValue("Address2", out var address2);
                address2.SetValue("Address 2 goes here");

                fields.TryGetValue("PhoneNumber", out var phoneToSet);
                phoneToSet.SetValue("Phone # goes here");

                fields.TryGetValue("Email", out var emailToSet);
                emailToSet.SetValue("Email goes here");

                form.FlattenFields();
                pdf.Close();

                binaryContentsOfOutputPdfFile = System.IO.File.ReadAllBytes(outputPdfFile.FileInfo.FullName);

            }

            return File(binaryContentsOfOutputPdfFile, "application/pdf", "CostShareSample.pdf");

        }

    }
}
