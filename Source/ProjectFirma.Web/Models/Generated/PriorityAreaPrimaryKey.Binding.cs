//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PriorityArea
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PriorityAreaPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PriorityArea>
    {
        public PriorityAreaPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PriorityAreaPrimaryKey(PriorityArea priorityArea) : base(priorityArea){}

        public static implicit operator PriorityAreaPrimaryKey(int primaryKeyValue)
        {
            return new PriorityAreaPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PriorityAreaPrimaryKey(PriorityArea priorityArea)
        {
            return new PriorityAreaPrimaryKey(priorityArea);
        }
    }
}