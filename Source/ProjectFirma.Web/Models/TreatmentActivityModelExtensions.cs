/*-----------------------------------------------------------------------
<copyright file="TreatmentActivityModelExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class TreatmentActivityModelExtensions
    {
        public static string GetStatusDisplayName(this Models.TreatmentActivity treatmentActivity)
        {
            return treatmentActivity.TreatmentActivityStatus.TreatmentActivityStatusDisplayName;
        }

        public static string GetProjectName(this Models.TreatmentActivity treatmentActivity)
        {
            return treatmentActivity.Project.ProjectName;
        }

        public static string GetProjectFocusAreaName(this Models.TreatmentActivity treatmentActivity)
        {
            
            return treatmentActivity.Project.FocusArea?.FocusAreaName ?? "Focus Area Not Set";
        }

        public static string GetContactName(this Models.TreatmentActivity treatmentActivity)
        {
            return treatmentActivity.TreatmentActivityContact.FullNameLastFirst ?? "Contact Not Set";
        }

        public static string GetProjectRegions(this Models.TreatmentActivity treatmentActivity)
        {            
            var result = treatmentActivity.Project.GetProjectRegions().OrderBy(x => x.RegionName).Select(x => x.RegionName).Distinct();
            return string.Join(", ",result);
        }

        public static string GetContactUrl(this Models.TreatmentActivity treatmentActivity)
        {
            return SitkaRoute<UserController>.BuildUrlFromExpression(uc => uc.Detail(treatmentActivity.TreatmentActivityContactID));
        }

        public static string GetProjectDetailUrl(this Models.TreatmentActivity treatmentActivity)
        {
            return SitkaRoute<ProjectController>.BuildUrlFromExpression(pc => pc.Detail(treatmentActivity.ProjectID));
        }
        public static string GetProjectFocusAreaUrl(this Models.TreatmentActivity treatmentActivity)
        {
            string result = SitkaRoute<FocusAreaController>.BuildUrlFromExpression(fac => fac.Detail(treatmentActivity.Project.FocusAreaID));        
            return result;
        }
        
        public static HtmlString GetContactText(this Models.TreatmentActivity treatmentActivity)
        {
            HtmlString returnValue = new HtmlString(string.Empty);
            if (treatmentActivity.TreatmentActivityContactID == null)
            {
                returnValue = "".ToHTMLFormattedString();
            }
            else
            {
                returnValue = UrlTemplate.MakeHrefString(treatmentActivity.GetContactUrl(),
                    treatmentActivity.GetContactName());
            }

            return returnValue;
        }

        public static HtmlString GetFocusAreaText(this Models.TreatmentActivity treatmentActivity)
        {
            HtmlString returnValue = UrlTemplate.MakeHrefString(treatmentActivity.GetProjectFocusAreaUrl(), treatmentActivity.GetProjectFocusAreaName());
            return returnValue;
        }

        public static string GetProgramIndexName(this Models.TreatmentActivity treatmentActivity)
        {
            return treatmentActivity.ProgramIndex == null ? "" : treatmentActivity.ProgramIndex.ProgramIndexCode;
        }

        public static string GetProjectCodeName(this Models.TreatmentActivity treatmentActivity)
        {
            return treatmentActivity.ProjectCode == null ? "" : treatmentActivity.ProjectCode.ProjectCodeAbbrev;
        }

        public static string GetProjectTypeName(this Models.TreatmentActivity treatmentActivity)
        {
            return treatmentActivity.Project.ProjectType == null ? "": treatmentActivity.Project.ProjectType.ProjectTypeName;
        }

        public static string GetDeleteTreatmentActivityUrl(this Models.TreatmentActivity treatmentActivity)
        {
            return SitkaRoute<TreatmentActivityController>.BuildUrlFromExpression(pc => pc.DeleteTreatmentActivity(treatmentActivity));
        }

        public static string GetEditTreatmentActivityUrl(this Models.TreatmentActivity treatmentActivity)
        {
            return SitkaRoute<TreatmentActivityController>.BuildUrlFromExpression(pc => pc.EditTreatmentActivity(treatmentActivity.PrimaryKey));
        }


    }
}