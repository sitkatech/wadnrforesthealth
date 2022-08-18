using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Program
{
    public class EditProjectImportBlockListViewModel
    {
        public int ProjectImportBlockListID { get; set; } = ModelObjectHelpers.NotYetAssignedID;

        [DisplayName("Program ID")]
        public int ProgramID { get; set; }

        [DisplayName("Project ID")]
        public int? ProjectID { get; set; }

        [DisplayName("Project GIS Identifier")]
        public string ProjectGisIdentifier { get; set; }

        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("Notes")]
        [MaxLength(500)]
        [Required]
        public string Notes { get; set; }
    }
}