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
                var invoice =
                    new Invoice(TestFramework.MakeTestName("RequestorName", Invoice.FieldLengths.RequestorName),
                        DateTime.Now, testPerson, InvoiceApprovalStatus.Approved, false, InvoiceMatchAmountType.N_A,
                        InvoiceStatus.Pending);

                return invoice;
            }

        }
    }
}