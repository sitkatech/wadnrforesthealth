//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vLoaStageProjectGrantAllocation]
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
    public partial class vLoaStageProjectGrantAllocation
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vLoaStageProjectGrantAllocation()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vLoaStageProjectGrantAllocation(int projectID, decimal? matchAmount, decimal? payAmount, string projectStatus, int? grantAllocationID, DateTime? letterDate, DateTime? projectExpirationDate) : this()
        {
            this.ProjectID = projectID;
            this.MatchAmount = matchAmount;
            this.PayAmount = payAmount;
            this.ProjectStatus = projectStatus;
            this.GrantAllocationID = grantAllocationID;
            this.LetterDate = letterDate;
            this.ProjectExpirationDate = projectExpirationDate;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vLoaStageProjectGrantAllocation(vLoaStageProjectGrantAllocation vLoaStageProjectGrantAllocation) : this()
        {
            this.ProjectID = vLoaStageProjectGrantAllocation.ProjectID;
            this.MatchAmount = vLoaStageProjectGrantAllocation.MatchAmount;
            this.PayAmount = vLoaStageProjectGrantAllocation.PayAmount;
            this.ProjectStatus = vLoaStageProjectGrantAllocation.ProjectStatus;
            this.GrantAllocationID = vLoaStageProjectGrantAllocation.GrantAllocationID;
            this.LetterDate = vLoaStageProjectGrantAllocation.LetterDate;
            this.ProjectExpirationDate = vLoaStageProjectGrantAllocation.ProjectExpirationDate;
            CallAfterConstructor(vLoaStageProjectGrantAllocation);
        }

        partial void CallAfterConstructor(vLoaStageProjectGrantAllocation vLoaStageProjectGrantAllocation);

        public int ProjectID { get; set; }
        public decimal? MatchAmount { get; set; }
        public decimal? PayAmount { get; set; }
        public string ProjectStatus { get; set; }
        public int? GrantAllocationID { get; set; }
        public DateTime? LetterDate { get; set; }
        public DateTime? ProjectExpirationDate { get; set; }
    }
}