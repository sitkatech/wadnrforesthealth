//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerProjectTreatmentArea]
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
    public partial class vGeoServerProjectTreatmentArea
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerProjectTreatmentArea()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerProjectTreatmentArea(int treatmentID, int primaryKey, int projectID, string projectName, string fhtProjectNumber) : this()
        {
            this.TreatmentID = treatmentID;
            this.PrimaryKey = primaryKey;
            this.ProjectID = projectID;
            this.ProjectName = projectName;
            this.FhtProjectNumber = fhtProjectNumber;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerProjectTreatmentArea(vGeoServerProjectTreatmentArea vGeoServerProjectTreatmentArea) : this()
        {
            this.TreatmentID = vGeoServerProjectTreatmentArea.TreatmentID;
            this.PrimaryKey = vGeoServerProjectTreatmentArea.PrimaryKey;
            this.ProjectID = vGeoServerProjectTreatmentArea.ProjectID;
            this.ProjectName = vGeoServerProjectTreatmentArea.ProjectName;
            this.FhtProjectNumber = vGeoServerProjectTreatmentArea.FhtProjectNumber;
            CallAfterConstructor(vGeoServerProjectTreatmentArea);
        }

        partial void CallAfterConstructor(vGeoServerProjectTreatmentArea vGeoServerProjectTreatmentArea);

        public int TreatmentID { get; set; }
        public int PrimaryKey { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string FhtProjectNumber { get; set; }
    }
}