//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerPriorityArea]
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
    public partial class vGeoServerPriorityArea
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerPriorityArea()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerPriorityArea(int priorityAreaID, int primaryKey, string priorityAreaName) : this()
        {
            this.PriorityAreaID = priorityAreaID;
            this.PrimaryKey = primaryKey;
            this.PriorityAreaName = priorityAreaName;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerPriorityArea(vGeoServerPriorityArea vGeoServerPriorityArea) : this()
        {
            this.PriorityAreaID = vGeoServerPriorityArea.PriorityAreaID;
            this.PrimaryKey = vGeoServerPriorityArea.PrimaryKey;
            this.PriorityAreaName = vGeoServerPriorityArea.PriorityAreaName;
            CallAfterConstructor(vGeoServerPriorityArea);
        }

        partial void CallAfterConstructor(vGeoServerPriorityArea vGeoServerPriorityArea);

        public int PriorityAreaID { get; set; }
        public int PrimaryKey { get; set; }
        public string PriorityAreaName { get; set; }
    }
}