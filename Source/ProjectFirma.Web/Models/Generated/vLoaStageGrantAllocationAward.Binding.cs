//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vLoaStageGrantAllocationAward]
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
    public partial class vLoaStageGrantAllocationAward
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vLoaStageGrantAllocationAward()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vLoaStageGrantAllocationAward(int loaStageID, int grantAllocationAwardID, int grantAllocationID, int grantID, bool isNortheast, bool? isSoutheast, string programIndex, string projectCode) : this()
        {
            this.LoaStageID = loaStageID;
            this.GrantAllocationAwardID = grantAllocationAwardID;
            this.GrantAllocationID = grantAllocationID;
            this.GrantID = grantID;
            this.IsNortheast = isNortheast;
            this.IsSoutheast = isSoutheast;
            this.ProgramIndex = programIndex;
            this.ProjectCode = projectCode;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vLoaStageGrantAllocationAward(vLoaStageGrantAllocationAward vLoaStageGrantAllocationAward) : this()
        {
            this.LoaStageID = vLoaStageGrantAllocationAward.LoaStageID;
            this.GrantAllocationAwardID = vLoaStageGrantAllocationAward.GrantAllocationAwardID;
            this.GrantAllocationID = vLoaStageGrantAllocationAward.GrantAllocationID;
            this.GrantID = vLoaStageGrantAllocationAward.GrantID;
            this.IsNortheast = vLoaStageGrantAllocationAward.IsNortheast;
            this.IsSoutheast = vLoaStageGrantAllocationAward.IsSoutheast;
            this.ProgramIndex = vLoaStageGrantAllocationAward.ProgramIndex;
            this.ProjectCode = vLoaStageGrantAllocationAward.ProjectCode;
            CallAfterConstructor(vLoaStageGrantAllocationAward);
        }

        partial void CallAfterConstructor(vLoaStageGrantAllocationAward vLoaStageGrantAllocationAward);

        public int LoaStageID { get; set; }
        public int GrantAllocationAwardID { get; set; }
        public int GrantAllocationID { get; set; }
        public int GrantID { get; set; }
        public bool IsNortheast { get; set; }
        public bool? IsSoutheast { get; set; }
        public string ProgramIndex { get; set; }
        public string ProjectCode { get; set; }
    }
}