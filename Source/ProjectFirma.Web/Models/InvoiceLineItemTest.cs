using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using LtInfo.Common.Models;
using NUnit.Framework;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class InvoiceLineItemTest : FirmaTestWithContext
    {
        [Test]
        public void CheckForValidInsertOfInvoiceLineItemForAllValidCostTypes()
        {
            var lineItemCostTypes = CostType.GetLineItemCostTypes();
            foreach (var costType in lineItemCostTypes)
            {
                var item = TestFramework.TestInvoiceLineItem.Create(HttpRequestStorage.DatabaseEntities, costType);
                Assert.IsNotNull(item, "should get item");
                Assert.DoesNotThrow(() => HttpRequestStorage.DatabaseEntities.SaveChanges(), "Should be able to create for these types");
                Assert.That(ModelObjectHelpers.IsRealPrimaryKeyValue(item.PrimaryKey), "has primary key in database");
            }

        }
        [Test]
        public void GivenBadCostTypeWhenInvoiceSavedThenFails()
        {
            var invalidCostTypeItems = CostType.All.Except(CostType.GetLineItemCostTypes());
            foreach (var costType in invalidCostTypeItems)
            {
                var item = TestFramework.TestInvoiceLineItem.Create(HttpRequestStorage.DatabaseEntities, costType);
                Assert.IsNotNull(item, "should get item");
                Assert.Catch(() => HttpRequestStorage.DatabaseEntities.SaveChanges(), "Database save should catch on constraint and fail");
                Assert.That(!ModelObjectHelpers.IsRealPrimaryKeyValue(item.PrimaryKey), "does not have primary key in database");
            }
        }
    }
}