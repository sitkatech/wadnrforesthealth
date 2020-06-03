//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerDNRUplandRegion]
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
    public partial class vGeoServerDNRUplandRegion
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerDNRUplandRegion()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerDNRUplandRegion(int dNRUplandRegionID, int primaryKey, string dNRUplandRegionName) : this()
        {
            this.DNRUplandRegionID = dNRUplandRegionID;
            this.PrimaryKey = primaryKey;
            this.DNRUplandRegionName = dNRUplandRegionName;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerDNRUplandRegion(vGeoServerDNRUplandRegion vGeoServerDNRUplandRegion) : this()
        {
            this.DNRUplandRegionID = vGeoServerDNRUplandRegion.DNRUplandRegionID;
            this.PrimaryKey = vGeoServerDNRUplandRegion.PrimaryKey;
            this.DNRUplandRegionName = vGeoServerDNRUplandRegion.DNRUplandRegionName;
            CallAfterConstructor(vGeoServerDNRUplandRegion);
        }

        partial void CallAfterConstructor(vGeoServerDNRUplandRegion vGeoServerDNRUplandRegion);

        public int DNRUplandRegionID { get; set; }
        public int PrimaryKey { get; set; }
        public string DNRUplandRegionName { get; set; }
    }
}