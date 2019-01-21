//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.Region
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class RegionPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<Region>
    {
        public RegionPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public RegionPrimaryKey(Region region) : base(region){}

        public static implicit operator RegionPrimaryKey(int primaryKeyValue)
        {
            return new RegionPrimaryKey(primaryKeyValue);
        }

        public static implicit operator RegionPrimaryKey(Region region)
        {
            return new RegionPrimaryKey(region);
        }
    }
}