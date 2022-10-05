using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LtInfo.Common.ExcelWorkbookUtilities;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Vendor;

namespace ProjectFirma.Web.Controllers
{
    public class VendorController : FirmaBaseController
    {
        [AnonymousUnclassifiedFeature]
        public JsonResult FindVendor(string term)
        {

            var vendorsFound = HttpRequestStorage.DatabaseEntities.Vendors
                .GetVendorFindResultsForVendorNameAndStatewideVendorNumber(term).Take(20);
            var vendorsFound2 = vendorsFound.Select(p => new ListItem(p.VendorNameAndStatewideVendorNumberWithSuffix, p.VendorID.ToString(CultureInfo.InvariantCulture))).ToList();

            return Json(vendorsFound2.Select(v => new { label = v.Text, value = v.Text, actualValue = v.Value }), JsonRequestBehavior.AllowGet);//use JSON structure for jquerys autocomplete functionality

        }

        [VendorViewFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.Vendor);
            var viewData = new IndexViewData(CurrentPerson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [VendorViewFeature]
        public GridJsonNetJObjectResult<Vendor> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var vendors = HttpRequestStorage.DatabaseEntities.Vendors.Where(x =>x.VendorStatus == "A").OrderBy(x => x.VendorName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Vendor>(vendors, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [VendorViewFeature]
        public ExcelResult VendorsExcelDownloadImpl()
        {

            var vendors = HttpRequestStorage.DatabaseEntities.Vendors.ToList();
            var workbookTitle = FieldDefinition.Vendor.GetFieldDefinitionLabelPluralized();
            var workSheets = new List<IExcelWorkbookSheetDescriptor>();

            // Agreements
            var vendorExcelSpec = new VendorExcelSpec();
            var vendorWorkSheet = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.Vendor.GetFieldDefinitionLabelPluralized()}", vendorExcelSpec, vendors);
            workSheets.Add(vendorWorkSheet);

            // Overall excel file
            var wbm = new ExcelWorkbookMaker(workSheets);
            var excelWorkbook = wbm.ToXLWorkbook();

            return new ExcelResult(excelWorkbook, workbookTitle);
        }



    }
}