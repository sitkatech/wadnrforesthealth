using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;

namespace ProjectFirma.Web.Models
{
    public partial class TreatmentGroup
    {
        public List<Treatment> Treatments { get; set; }

        public ProjectLocation ProjectLocation
        {
            get;
            set;
        }


        public TreatmentGroup(ProjectLocation projectLocation)
        {
            Check.Require(projectLocation.ProjectLocationTypeID == (int)ProjectLocationTypeEnum.TreatmentArea, "Your Project Location needs to be a Treatment Area");
            Treatments = projectLocation.Treatments.ToList();
            ProjectLocation = projectLocation;
        }

    }
}