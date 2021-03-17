//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vLoaStageGrantAllocationByProgramIndexProjectCode]
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
    public partial class vLoaStageGrantAllocationByProgramIndexProjectCode
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vLoaStageGrantAllocationByProgramIndexProjectCode()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vLoaStageGrantAllocationByProgramIndexProjectCode(int loaStageID, int? grantAllocationID, int? grantID) : this()
        {
            this.LoaStageID = loaStageID;
            this.GrantAllocationID = grantAllocationID;
            this.GrantID = grantID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vLoaStageGrantAllocationByProgramIndexProjectCode(vLoaStageGrantAllocationByProgramIndexProjectCode vLoaStageGrantAllocationByProgramIndexProjectCode) : this()
        {
            this.LoaStageID = vLoaStageGrantAllocationByProgramIndexProjectCode.LoaStageID;
            this.GrantAllocationID = vLoaStageGrantAllocationByProgramIndexProjectCode.GrantAllocationID;
            this.GrantID = vLoaStageGrantAllocationByProgramIndexProjectCode.GrantID;
            CallAfterConstructor(vLoaStageGrantAllocationByProgramIndexProjectCode);
        }

        partial void CallAfterConstructor(vLoaStageGrantAllocationByProgramIndexProjectCode vLoaStageGrantAllocationByProgramIndexProjectCode);

        public int LoaStageID { get; set; }
        public int? GrantAllocationID { get; set; }
        public int? GrantID { get; set; }
    }
}