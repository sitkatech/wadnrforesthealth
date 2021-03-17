//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vSingularGrantAllocation]
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
    public partial class vSingularGrantAllocation
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vSingularGrantAllocation()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vSingularGrantAllocation(int grantID, int grantAllocationID) : this()
        {
            this.GrantID = grantID;
            this.GrantAllocationID = grantAllocationID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vSingularGrantAllocation(vSingularGrantAllocation vSingularGrantAllocation) : this()
        {
            this.GrantID = vSingularGrantAllocation.GrantID;
            this.GrantAllocationID = vSingularGrantAllocation.GrantAllocationID;
            CallAfterConstructor(vSingularGrantAllocation);
        }

        partial void CallAfterConstructor(vSingularGrantAllocation vSingularGrantAllocation);

        public int GrantID { get; set; }
        public int GrantAllocationID { get; set; }
    }
}