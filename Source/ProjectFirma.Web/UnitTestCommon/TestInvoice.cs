using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public class TestInvoice
        {
            public static Invoice Create()
            {
                var testIPR = TestInvoicePaymentRequest.Create();
                var invoice = new Invoice(DateTime.Now, InvoiceApprovalStatus.Approved, InvoiceMatchAmountType.DNR, InvoiceStatus.Paid, testIPR);

                return invoice;
            }

        }
        
        public class TestInvoicePaymentRequest
        {
            public static InvoicePaymentRequest Create()
            {
                var project = TestProject.Create();
                var invoicePaymentRequest = InvoicePaymentRequest.CreateNewBlank(project);

                return invoicePaymentRequest;
            }

        }
    }
}