//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerPclWildfireResponseBenefit]
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
    public partial class vGeoServerPclWildfireResponseBenefit
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerPclWildfireResponseBenefit()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerPclWildfireResponseBenefit(int pclWildfireResponseBenefitID, int primaryKey, int priorityLandscapeID, string color) : this()
        {
            this.PclWildfireResponseBenefitID = pclWildfireResponseBenefitID;
            this.PrimaryKey = primaryKey;
            this.PriorityLandscapeID = priorityLandscapeID;
            this.Color = color;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerPclWildfireResponseBenefit(vGeoServerPclWildfireResponseBenefit vGeoServerPclWildfireResponseBenefit) : this()
        {
            this.PclWildfireResponseBenefitID = vGeoServerPclWildfireResponseBenefit.PclWildfireResponseBenefitID;
            this.PrimaryKey = vGeoServerPclWildfireResponseBenefit.PrimaryKey;
            this.PriorityLandscapeID = vGeoServerPclWildfireResponseBenefit.PriorityLandscapeID;
            this.Color = vGeoServerPclWildfireResponseBenefit.Color;
            CallAfterConstructor(vGeoServerPclWildfireResponseBenefit);
        }

        partial void CallAfterConstructor(vGeoServerPclWildfireResponseBenefit vGeoServerPclWildfireResponseBenefit);

        public int PclWildfireResponseBenefitID { get; set; }
        public int PrimaryKey { get; set; }
        public int PriorityLandscapeID { get; set; }
        public string Color { get; set; }
    }
}