//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerPodVectorRanked]
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
    public partial class vGeoServerPodVectorRanked
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerPodVectorRanked()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerPodVectorRanked(int podVectorRankedID, int primaryKey, int priorityLandscapeID, string color) : this()
        {
            this.PodVectorRankedID = podVectorRankedID;
            this.PrimaryKey = primaryKey;
            this.PriorityLandscapeID = priorityLandscapeID;
            this.Color = color;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerPodVectorRanked(vGeoServerPodVectorRanked vGeoServerPodVectorRanked) : this()
        {
            this.PodVectorRankedID = vGeoServerPodVectorRanked.PodVectorRankedID;
            this.PrimaryKey = vGeoServerPodVectorRanked.PrimaryKey;
            this.PriorityLandscapeID = vGeoServerPodVectorRanked.PriorityLandscapeID;
            this.Color = vGeoServerPodVectorRanked.Color;
            CallAfterConstructor(vGeoServerPodVectorRanked);
        }

        partial void CallAfterConstructor(vGeoServerPodVectorRanked vGeoServerPodVectorRanked);

        public int PodVectorRankedID { get; set; }
        public int PrimaryKey { get; set; }
        public int PriorityLandscapeID { get; set; }
        public string Color { get; set; }
    }
}