//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerPclVectorRanked]
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
    public partial class vGeoServerPclVectorRanked
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerPclVectorRanked()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerPclVectorRanked(int pclVectorRankedID, int primaryKey, int priorityLandscapeID, string color) : this()
        {
            this.PclVectorRankedID = pclVectorRankedID;
            this.PrimaryKey = primaryKey;
            this.PriorityLandscapeID = priorityLandscapeID;
            this.Color = color;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerPclVectorRanked(vGeoServerPclVectorRanked vGeoServerPclVectorRanked) : this()
        {
            this.PclVectorRankedID = vGeoServerPclVectorRanked.PclVectorRankedID;
            this.PrimaryKey = vGeoServerPclVectorRanked.PrimaryKey;
            this.PriorityLandscapeID = vGeoServerPclVectorRanked.PriorityLandscapeID;
            this.Color = vGeoServerPclVectorRanked.Color;
            CallAfterConstructor(vGeoServerPclVectorRanked);
        }

        partial void CallAfterConstructor(vGeoServerPclVectorRanked vGeoServerPclVectorRanked);

        public int PclVectorRankedID { get; set; }
        public int PrimaryKey { get; set; }
        public int PriorityLandscapeID { get; set; }
        public string Color { get; set; }
    }
}