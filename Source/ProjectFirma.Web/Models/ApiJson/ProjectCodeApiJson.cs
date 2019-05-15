using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This JSON class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("ProjectCodeID: {ProjectCodeID} - ProjectCodeName: {ProjectCodeName}")]
    public class ProjectCodeApiJson
    {
        public int ProjectCodeID { get; set; }
        public string ProjectCodeName { get; set; }
        public string ProjectCodeTitle { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }

        // For use by model binder
        public ProjectCodeApiJson()
        {
        }

        public ProjectCodeApiJson(ProjectCode projectCode)
        {
            ProjectCodeID = projectCode.ProjectCodeID;
            ProjectCodeName = projectCode.ProjectCodeName;
            ProjectCodeTitle = projectCode.ProjectCodeTitle;
            CreateDate = projectCode.CreateDate;
            ProjectStartDate = projectCode.ProjectStartDate;
            ProjectEndDate = projectCode.ProjectEndDate;
        }

        public static List<ProjectCodeApiJson> MakeProjectCodeApiJsonsFromProjectCodes(List<ProjectCode> projectCodes, bool doAlphaSort = true)
        {
            var outgoingProjectCodes = projectCodes;
            if (doAlphaSort)
            {
                outgoingProjectCodes = outgoingProjectCodes.OrderBy(pi => pi.ProjectCodeName).ToList();
            }
            return outgoingProjectCodes.Select(pi => new ProjectCodeApiJson(pi)).ToList();
        }
    }
}