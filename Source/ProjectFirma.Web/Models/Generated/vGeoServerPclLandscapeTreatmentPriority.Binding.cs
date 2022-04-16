//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerPclLandscapeTreatmentPriority]
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class vGeoServerPclLandscapeTreatmentPriority
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerPclLandscapeTreatmentPriority()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerPclLandscapeTreatmentPriority(int pclLandscapeTreatmentPriorityID, int primaryKey, int priorityLandscapeID, string color) : this()
        {
            this.PclLandscapeTreatmentPriorityID = pclLandscapeTreatmentPriorityID;
            this.PrimaryKey = primaryKey;
            this.PriorityLandscapeID = priorityLandscapeID;
            this.Color = color;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerPclLandscapeTreatmentPriority(vGeoServerPclLandscapeTreatmentPriority vGeoServerPclLandscapeTreatmentPriority) : this()
        {
            this.PclLandscapeTreatmentPriorityID = vGeoServerPclLandscapeTreatmentPriority.PclLandscapeTreatmentPriorityID;
            this.PrimaryKey = vGeoServerPclLandscapeTreatmentPriority.PrimaryKey;
            this.PriorityLandscapeID = vGeoServerPclLandscapeTreatmentPriority.PriorityLandscapeID;
            this.Color = vGeoServerPclLandscapeTreatmentPriority.Color;
            CallAfterConstructor(vGeoServerPclLandscapeTreatmentPriority);
        }

        partial void CallAfterConstructor(vGeoServerPclLandscapeTreatmentPriority vGeoServerPclLandscapeTreatmentPriority);

        public int PclLandscapeTreatmentPriorityID { get; set; }
        public int PrimaryKey { get; set; }
        public int PriorityLandscapeID { get; set; }
        public string Color { get; set; }
    }
}