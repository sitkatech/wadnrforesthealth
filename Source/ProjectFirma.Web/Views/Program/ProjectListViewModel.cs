using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFirma.Web.Views.Program
{
    public class ProjectListViewModel
    {
        public Models.Project Project { get; set; }
        public int? ProjectImportBlockListID { get; set; }
        public bool ExistsOnImportBlockList => ProjectImportBlockListID.HasValue && ProjectImportBlockListID.Value != 0;
    }
}