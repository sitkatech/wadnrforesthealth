//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerPclBoundaryLine]
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
    public partial class vGeoServerPclBoundaryLine
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerPclBoundaryLine()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerPclBoundaryLine(int pclBoundaryLineID, int primaryKey, int priorityLandscapeID) : this()
        {
            this.PclBoundaryLineID = pclBoundaryLineID;
            this.PrimaryKey = primaryKey;
            this.PriorityLandscapeID = priorityLandscapeID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerPclBoundaryLine(vGeoServerPclBoundaryLine vGeoServerPclBoundaryLine) : this()
        {
            this.PclBoundaryLineID = vGeoServerPclBoundaryLine.PclBoundaryLineID;
            this.PrimaryKey = vGeoServerPclBoundaryLine.PrimaryKey;
            this.PriorityLandscapeID = vGeoServerPclBoundaryLine.PriorityLandscapeID;
            CallAfterConstructor(vGeoServerPclBoundaryLine);
        }

        partial void CallAfterConstructor(vGeoServerPclBoundaryLine vGeoServerPclBoundaryLine);

        public int PclBoundaryLineID { get; set; }
        public int PrimaryKey { get; set; }
        public int PriorityLandscapeID { get; set; }
    }
}