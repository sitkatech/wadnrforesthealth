//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerProjectLocationSimple]
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
    public partial class vGeoServerProjectLocationSimple
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerProjectLocationSimple()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerProjectLocationSimple(int projectID, int primaryKey, string projectName) : this()
        {
            this.ProjectID = projectID;
            this.PrimaryKey = primaryKey;
            this.ProjectName = projectName;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerProjectLocationSimple(vGeoServerProjectLocationSimple vGeoServerProjectLocationSimple) : this()
        {
            this.ProjectID = vGeoServerProjectLocationSimple.ProjectID;
            this.PrimaryKey = vGeoServerProjectLocationSimple.PrimaryKey;
            this.ProjectName = vGeoServerProjectLocationSimple.ProjectName;
            CallAfterConstructor(vGeoServerProjectLocationSimple);
        }

        partial void CallAfterConstructor(vGeoServerProjectLocationSimple vGeoServerProjectLocationSimple);

        public int ProjectID { get; set; }
        public int PrimaryKey { get; set; }
        public string ProjectName { get; set; }
    }
}