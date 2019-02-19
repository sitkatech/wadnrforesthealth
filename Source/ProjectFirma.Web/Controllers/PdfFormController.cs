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
using System.Linq;
using System.Web.Mvc;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Controllers
{
    public class PdfFormController : FirmaBaseController
    {

        [AnonymousUnclassifiedFeature]
        public FileContentResult BlankCostShareAgreementPdf()
        {
            string blankCostSharePdfFilePath = BlankCostSharePdfFilePath();
            byte[] binaryContentsOfOutputPdfFile;
            var outputFileName = BlankOutputFileName();

            binaryContentsOfOutputPdfFile = System.IO.File.ReadAllBytes(blankCostSharePdfFilePath);
            return File(binaryContentsOfOutputPdfFile, "application/pdf", outputFileName);
            
        }

        private string BlankCostSharePdfFilePath()
        {
            return Server.MapPath(Url.Content("~/Content/documents/CostShareBlankFormFields.pdf"));
        }

        private string BlankOutputFileName()
        {
            return "CostShareAgreementBlank.pdf";
        }

        [AnonymousUnclassifiedFeature]
        //public FileContentResult CostShareAgreementPdf(ProjectPrimaryKey projectPrimaryKey)
        //{
        public FileContentResult CostShareAgreementPdf(ProjectPersonPrimaryKey projectPersonPrimaryKey)
        {
            // TODO: Permissions checks?

            string blankCostSharePdfFilePath = BlankCostSharePdfFilePath();
            byte[] binaryContentsOfOutputPdfFile;
            var outputFileName = BlankOutputFileName();

            ProjectPerson landownerProjectPerson = projectPersonPrimaryKey.EntityObject;
            Person landownerPerson = landownerProjectPerson.Person;

            Check.Ensure(landownerProjectPerson.ProjectPersonRelationshipType == ProjectPersonRelationshipType.Landowner, "Only expecting Landowner contacts here");

            using (var outputPdfFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".pdf"))
            {

                PdfDocument pdf = new PdfDocument(new PdfReader(blankCostSharePdfFilePath), new PdfWriter(outputPdfFile.FileInfo.FullName));
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
                IDictionary<String, PdfFormField> fields = form.GetFormFields();

                var landownerName = landownerPerson.FullNameFirstLast;
                outputFileName = $"CostShareAgreement-{landownerName.Replace(" ", "")}.pdf";

                fields.TryGetValue("Names", out var nameToSet);
                nameToSet.SetValue(landownerName);

                fields.TryGetValue("Address1", out var address1);
                address1.SetValue(landownerPerson.PersonAddress);

                fields.TryGetValue("Address2", out var address2);
                address2.SetValue(string.Empty);

                fields.TryGetValue("PhoneNumber", out var phoneToSet);
                phoneToSet.SetValue(landownerPerson.Phone);

                fields.TryGetValue("Email", out var emailToSet);
                emailToSet.SetValue(landownerPerson.Email);

                form.FlattenFields();
                pdf.Close();

                binaryContentsOfOutputPdfFile = System.IO.File.ReadAllBytes(outputPdfFile.FileInfo.FullName);

            }

            return File(binaryContentsOfOutputPdfFile, "application/pdf", outputFileName);

        }

    }
}
