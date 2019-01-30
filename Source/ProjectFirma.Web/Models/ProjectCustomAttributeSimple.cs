using System;
using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Views.Shared.ProjectControls;

namespace ProjectFirma.Web.Models
{
    public class ProjectCustomAttributeSimple
    {
        public int ProjectCustomAttributeTypeID { get; set; }
        public IList<string> ProjectCustomAttributeValues { get; set; }
        public string ProjectCustomAttributeValueDisplayString { get; set; }

        /*
         * The below constructor is needed for binding as part of the viewmodel
         */
        public ProjectCustomAttributeSimple() { }

        public ProjectCustomAttributeSimple(IProjectCustomAttribute projectCustomAttribute)
        {
            ProjectCustomAttributeTypeID = projectCustomAttribute.ProjectCustomAttributeTypeID;
            ProjectCustomAttributeValues = projectCustomAttribute.Values
                .Select(y =>
                    y.IProjectCustomAttribute.ProjectCustomAttributeType.ProjectCustomAttributeDataType ==
                    ProjectCustomAttributeDataType.DateTime
                        ? DateTime.Parse(y.AttributeValue).ToShortDateString()
                        : y.AttributeValue)
                .ToList();
            if (ProjectCustomAttributeTypeID == ProjectCustomAttributeDataType.DateTime.ProjectCustomAttributeDataTypeID)
            {
                ProjectCustomAttributeValueDisplayString = ProjectAttributes.StringToDateStringStatic(projectCustomAttribute.Values.Single().AttributeValue);
            }
            else
            {
                ProjectCustomAttributeValueDisplayString =
                    string.Join(", ", projectCustomAttribute.Values.Select(x => x.AttributeValue).OrderBy(x => x));
            }
        }
    }
}
