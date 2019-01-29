//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerProjectLocationDetailed]
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
    public partial class vGeoServerProjectLocationDetailed
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerProjectLocationDetailed()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerProjectLocationDetailed(int projectID, int primaryKey, int projectLocationID, string projectName, string annotation) : this()
        {
            this.ProjectID = projectID;
            this.PrimaryKey = primaryKey;
            this.ProjectLocationID = projectLocationID;
            this.ProjectName = projectName;
            this.Annotation = annotation;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerProjectLocationDetailed(vGeoServerProjectLocationDetailed vGeoServerProjectLocationDetailed) : this()
        {
            this.ProjectID = vGeoServerProjectLocationDetailed.ProjectID;
            this.PrimaryKey = vGeoServerProjectLocationDetailed.PrimaryKey;
            this.ProjectLocationID = vGeoServerProjectLocationDetailed.ProjectLocationID;
            this.ProjectName = vGeoServerProjectLocationDetailed.ProjectName;
            this.Annotation = vGeoServerProjectLocationDetailed.Annotation;
            CallAfterConstructor(vGeoServerProjectLocationDetailed);
        }

        partial void CallAfterConstructor(vGeoServerProjectLocationDetailed vGeoServerProjectLocationDetailed);

        public int ProjectID { get; set; }
        public int PrimaryKey { get; set; }
        public int ProjectLocationID { get; set; }
        public string ProjectName { get; set; }
        public string Annotation { get; set; }
    }
}