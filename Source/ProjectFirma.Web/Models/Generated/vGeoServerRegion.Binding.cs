//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerRegion]
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class vGeoServerRegion
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerRegion()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerRegion(int regionID, int primaryKey, string regionName) : this()
        {
            this.RegionID = regionID;
            this.PrimaryKey = primaryKey;
            this.RegionName = regionName;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerRegion(vGeoServerRegion vGeoServerRegion) : this()
        {
            this.RegionID = vGeoServerRegion.RegionID;
            this.PrimaryKey = vGeoServerRegion.PrimaryKey;
            this.RegionName = vGeoServerRegion.RegionName;
            CallAfterConstructor(vGeoServerRegion);
        }

        partial void CallAfterConstructor(vGeoServerRegion vGeoServerRegion);

        public int RegionID { get; set; }
        public int PrimaryKey { get; set; }
        public string RegionName { get; set; }
    }
}