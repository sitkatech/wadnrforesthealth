//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.tmpChildrenGrantsInGrantsTab
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class tmpChildrenGrantsInGrantsTabPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<tmpChildrenGrantsInGrantsTab>
    {
        public tmpChildrenGrantsInGrantsTabPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public tmpChildrenGrantsInGrantsTabPrimaryKey(tmpChildrenGrantsInGrantsTab tmpChildrenGrantsInGrantsTab) : base(tmpChildrenGrantsInGrantsTab){}

        public static implicit operator tmpChildrenGrantsInGrantsTabPrimaryKey(int primaryKeyValue)
        {
            return new tmpChildrenGrantsInGrantsTabPrimaryKey(primaryKeyValue);
        }

        public static implicit operator tmpChildrenGrantsInGrantsTabPrimaryKey(tmpChildrenGrantsInGrantsTab tmpChildrenGrantsInGrantsTab)
        {
            return new tmpChildrenGrantsInGrantsTabPrimaryKey(tmpChildrenGrantsInGrantsTab);
        }
    }
}