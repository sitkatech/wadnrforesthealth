//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.InvoiceLineItem
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class InvoiceLineItemPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<InvoiceLineItem>
    {
        public InvoiceLineItemPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public InvoiceLineItemPrimaryKey(InvoiceLineItem invoiceLineItem) : base(invoiceLineItem){}

        public static implicit operator InvoiceLineItemPrimaryKey(int primaryKeyValue)
        {
            return new InvoiceLineItemPrimaryKey(primaryKeyValue);
        }

        public static implicit operator InvoiceLineItemPrimaryKey(InvoiceLineItem invoiceLineItem)
        {
            return new InvoiceLineItemPrimaryKey(invoiceLineItem);
        }
    }
}