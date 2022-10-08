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
                var testPerson = TestPerson.Create();
                var testIPR = TestInvoicePaymentRequest.Create();
                var invoice =
                    //new Invoice(TestFramework.MakeTestName("RequestorName", Invoice.FieldLengths.RequestorName),
                    //    DateTime.Now, testPerson, InvoiceApprovalStatus.Approved, false, InvoiceMatchAmountType.N_A,
                    //    InvoiceStatus.Pending);
                    //TODO 10/7/22 TK - support IPR test creation
                    new Invoice(DateTime.Now, InvoiceApprovalStatus.Approved.InvoiceApprovalStatusID,
                        (int)InvoiceMatchAmountTypeEnum.DNR, InvoiceStatus.Paid.InvoiceStatusID, testIPR);

                return invoice;
            }

        }
    }
}