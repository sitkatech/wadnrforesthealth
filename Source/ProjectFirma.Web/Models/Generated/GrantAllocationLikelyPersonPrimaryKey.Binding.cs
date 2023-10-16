//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationLikelyPerson
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationLikelyPersonPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationLikelyPerson>
    {
        public GrantAllocationLikelyPersonPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationLikelyPersonPrimaryKey(GrantAllocationLikelyPerson grantAllocationLikelyPerson) : base(grantAllocationLikelyPerson){}

        public static implicit operator GrantAllocationLikelyPersonPrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationLikelyPersonPrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationLikelyPersonPrimaryKey(GrantAllocationLikelyPerson grantAllocationLikelyPerson)
        {
            return new GrantAllocationLikelyPersonPrimaryKey(grantAllocationLikelyPerson);
        }
    }
}