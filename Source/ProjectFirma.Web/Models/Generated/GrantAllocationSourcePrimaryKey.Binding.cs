//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.GrantAllocationSource
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationSourcePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<GrantAllocationSource>
    {
        public GrantAllocationSourcePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public GrantAllocationSourcePrimaryKey(GrantAllocationSource grantAllocationSource) : base(grantAllocationSource){}

        public static implicit operator GrantAllocationSourcePrimaryKey(int primaryKeyValue)
        {
            return new GrantAllocationSourcePrimaryKey(primaryKeyValue);
        }

        public static implicit operator GrantAllocationSourcePrimaryKey(GrantAllocationSource grantAllocationSource)
        {
            return new GrantAllocationSourcePrimaryKey(grantAllocationSource);
        }
    }
}