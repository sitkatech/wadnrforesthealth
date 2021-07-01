//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vLoaStageGrantAllocation]
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
    public partial class vLoaStageGrantAllocation
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vLoaStageGrantAllocation()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vLoaStageGrantAllocation(int loaStageID, int? grantID, int? grantAllocationID, bool isNortheast, bool? isSoutheast, string programIndex, string projectCode) : this()
        {
            this.LoaStageID = loaStageID;
            this.GrantID = grantID;
            this.GrantAllocationID = grantAllocationID;
            this.IsNortheast = isNortheast;
            this.IsSoutheast = isSoutheast;
            this.ProgramIndex = programIndex;
            this.ProjectCode = projectCode;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vLoaStageGrantAllocation(vLoaStageGrantAllocation vLoaStageGrantAllocation) : this()
        {
            this.LoaStageID = vLoaStageGrantAllocation.LoaStageID;
            this.GrantID = vLoaStageGrantAllocation.GrantID;
            this.GrantAllocationID = vLoaStageGrantAllocation.GrantAllocationID;
            this.IsNortheast = vLoaStageGrantAllocation.IsNortheast;
            this.IsSoutheast = vLoaStageGrantAllocation.IsSoutheast;
            this.ProgramIndex = vLoaStageGrantAllocation.ProgramIndex;
            this.ProjectCode = vLoaStageGrantAllocation.ProjectCode;
            CallAfterConstructor(vLoaStageGrantAllocation);
        }

        partial void CallAfterConstructor(vLoaStageGrantAllocation vLoaStageGrantAllocation);

        public int LoaStageID { get; set; }
        public int? GrantID { get; set; }
        public int? GrantAllocationID { get; set; }
        public bool IsNortheast { get; set; }
        public bool? IsSoutheast { get; set; }
        public string ProgramIndex { get; set; }
        public string ProjectCode { get; set; }
    }
}