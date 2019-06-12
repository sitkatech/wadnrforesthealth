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
        public class TestInvoiceLineItem
        {
            public static InvoiceLineItem Create(DatabaseEntities dbContext, CostType costType)
            {
                var invoice = TestInvoice.Create();
                var grantAllocation = TestGrantAllocation.Create();
                
                var testInvoiceLineItem = new InvoiceLineItem(invoice, grantAllocation, costType, 1m); //InvoiceLineItem.CreateNewBlank(invoice, grantAllocation, costType);

                string testInvoiceLineItemNote = TestFramework.MakeTestName($"Test Invoice Line Item for CostType: {costType.CostTypeDisplayName}" );
                testInvoiceLineItem.InvoiceLineItemNote = testInvoiceLineItemNote;

                dbContext.InvoiceLineItems.Add(testInvoiceLineItem);
                return testInvoiceLineItem;
            }
        }
    }
}