using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFirma.Web.Views.Program
{
    public class ProjectImportBlockListViewModel
    {
        public int ProgramID { get; set; }
        public int? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectGisIdentifier { get; set; }
    }
}