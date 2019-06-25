/*-----------------------------------------------------------------------
<copyright file="EditGrantAllocationAwardContractorInvoiceItemViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel.DataAnnotations;
using System.Web;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class EditGrantAllocationAwardContractorInvoiceItemViewModel : FormViewModel
    {
        public int GrantAllocationAwardContractorInvoiceID { get; set; }
        public int GrantAllocationAwardID { get; set; }


        [StringLength(Models.GrantAllocationAwardContractorInvoice.FieldLengths.GrantAllocationAwardContractorInvoiceDescription)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceDescription)]
        [Required]
        public string Description { get; set; }

        [StringLength(GrantAllocationAwardContractorInvoice.FieldLengths.GrantAllocationAwardContractorInvoiceNumber)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceNumber)]
        [Required]
        public string InvoiceNumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceDate)]
        [Required]
        public DateTime Date { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceType)]
        [Required]
        public int TypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceForemanHours)]
        public decimal? ForemanHours { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceForemanRate)]
        public Money? ForemanRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceLaborHours)]
        public decimal? LaborHours { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceLaborRate)]
        public Money? LaborRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceGrappleHours)]
        public decimal? GrappleHours { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceGrappleRate)]
        public Money? GrappleRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceMasticationHours)]
        public decimal? MasticationHours { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceMasticationRate)]
        public Money? MasticationRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceTotal)]
        public Money? Total { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceTaxRate)]
        public decimal? TaxRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceAcresReported)]
        public decimal? AcresReported { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardContractorInvoiceDocumentUpload)]
        [WADNRFileExtensions(FileResourceMimeTypeEnum.PDF, FileResourceMimeTypeEnum.ExcelXLSX, FileResourceMimeTypeEnum.xExcelXLSX, FileResourceMimeTypeEnum.ExcelXLS, FileResourceMimeTypeEnum.PowerpointPPT, FileResourceMimeTypeEnum.PowerpointPPTX, FileResourceMimeTypeEnum.WordDOC, FileResourceMimeTypeEnum.WordDOCX, FileResourceMimeTypeEnum.TXT, FileResourceMimeTypeEnum.JPEG, FileResourceMimeTypeEnum.PNG)]
        public HttpPostedFileBase FileResourceData { get; set; }

        [StringLength(GrantAllocationAwardContractorInvoice.FieldLengths.GrantAllocationAwardContractorInvoiceNotes)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardTravelNotes)]
        public string Notes { get; set; }

        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationAwardContractorInvoiceItemViewModel()
        {
            Date = DateTime.Today;
            //Default to Hourly Type
            TypeID = GrantAllocationAwardContractorInvoiceType.Hourly.GrantAllocationAwardContractorInvoiceTypeID;
        }

        public EditGrantAllocationAwardContractorInvoiceItemViewModel(Models.GrantAllocationAwardContractorInvoice grantAllocationAwardContractorInvoice)
        {
            GrantAllocationAwardContractorInvoiceID = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceID;
            GrantAllocationAwardID = grantAllocationAwardContractorInvoice.GrantAllocationAwardID;

            Description = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceDescription;
            InvoiceNumber = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceNumber;
            if (grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceDate == DateTime.MinValue)
            {
                Date = DateTime.Today;
            }
            else
            {
                Date = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceDate;
            }

            TypeID = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceTypeID;
            switch (TypeID)
            {
                case (int)GrantAllocationAwardContractorInvoiceTypeEnum.Hourly:
                    ForemanHours = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceForemanHours ?? 0m;
                    ForemanRate = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceForemanRate ?? 0m;
                    LaborHours = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceLaborHours ?? 0m;
                    LaborRate = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceLaborRate ?? 0m;
                    GrappleHours = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceGrappleHours ?? 0m;
                    GrappleRate = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceGrappleRate ?? 0m;
                    MasticationHours = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceMasticationHours ?? 0m;
                    MasticationRate = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceMasticationRate ?? 0m;
                    Total = 0m;
                    break;
                case (int)GrantAllocationAwardContractorInvoiceTypeEnum.Other:
                    ForemanHours = null;
                    ForemanRate =  null;
                    LaborHours = null;
                    LaborRate = null;
                    GrappleHours = null;
                    GrappleRate = null;
                    MasticationHours = null;
                    MasticationRate = null;
                    Total = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceTotal ?? 0m;
                    break;
                default:
                    throw new Exception($"Unhandled TypeID {TypeID} for GrantAllocationAwardContractorInvoiceTypeEnum in EditGrantAllocationAwardContractorInvoiceItemViewModel");
            }

            TaxRate = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceTaxRate;
            AcresReported = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceAcresReported;
            //FileResourceID = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceFileResourceID;
            
            Notes = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceNotes;
        }



        public void UpdateModel(Models.GrantAllocationAwardContractorInvoice grantAllocationAwardContractorInvoice, Person currentPerson)
        {
            grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceID = GrantAllocationAwardContractorInvoiceID;
            grantAllocationAwardContractorInvoice.GrantAllocationAwardID = GrantAllocationAwardID;

            grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceDescription = Description;
            grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceNumber = InvoiceNumber;
            grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceDate = Date;
            grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceTypeID = TypeID;
            switch (TypeID)
            {
                case (int)GrantAllocationAwardContractorInvoiceTypeEnum.Hourly:
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceForemanHours = ForemanHours;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceForemanRate = ForemanRate;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceLaborHours = LaborHours;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceLaborRate = LaborRate;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceGrappleHours = GrappleHours;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceGrappleRate = GrappleRate;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceMasticationHours = MasticationHours;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceMasticationRate = MasticationRate;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceTotal = null;
                    break;
                case (int)GrantAllocationAwardContractorInvoiceTypeEnum.Other:
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceForemanHours = null;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceForemanRate = null;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceLaborHours = null;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceLaborRate = null;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceGrappleHours = null;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceGrappleRate = null;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceMasticationHours = null;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceMasticationRate = null;
                    grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceTotal = Total;
                    break;
                default:
                    throw new Exception($"Unhandled TypeID {TypeID} for GrantAllocationAwardContractorInvoiceTypeEnum in EditGrantAllocationAwardContractorInvoiceItemViewModel");
            }

            grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceTaxRate = TaxRate;
            grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceAcresReported = AcresReported;

            if (FileResourceData != null)
            {
                var currentFileResource = grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceFileResource;
                grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceFileResource = null;
                // Delete old file, if present
                if (currentFileResource != null)
                {
                    HttpRequestStorage.DatabaseEntities.SaveChanges();
                    HttpRequestStorage.DatabaseEntities.FileResources.DeleteFileResource(currentFileResource);
                }
                grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceFileResource = FileResource.CreateNewFromHttpPostedFileAndSave(FileResourceData, currentPerson);
            }

            grantAllocationAwardContractorInvoice.GrantAllocationAwardContractorInvoiceNotes = Notes;

        }
    }

}