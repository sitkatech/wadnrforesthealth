//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PriorityLandscapeType
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class PriorityLandscapeTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PriorityLandscapeType>
    {
        public PriorityLandscapeTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PriorityLandscapeTypePrimaryKey(PriorityLandscapeType priorityLandscapeType) : base(priorityLandscapeType){}

        public static implicit operator PriorityLandscapeTypePrimaryKey(int primaryKeyValue)
        {
            return new PriorityLandscapeTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator PriorityLandscapeTypePrimaryKey(PriorityLandscapeType priorityLandscapeType)
        {
            return new PriorityLandscapeTypePrimaryKey(priorityLandscapeType);
        }
    }
}