using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.FocusArea
{
    public class ApproveUploadGisViewModel : FormViewModel, IValidatableObject
    {
        [DisplayName("Focus Area Location"), Required]
        public string FocusAreaLocationWkt { get; set; }

        public void UpdateModel(Models.FocusArea focusArea)
        {
            focusArea.FocusAreaLocation = DbGeometry.FromText(FocusAreaLocationWkt, FirmaWebConfiguration.GeoSpatialReferenceID);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            try
            {
                DbGeometry.FromText(FocusAreaLocationWkt, FirmaWebConfiguration.GeoSpatialReferenceID);
            }
            catch
            {
                errors.Add(new SitkaValidationResult<ApproveUploadGisViewModel, string>(
                    "Unable to deserialize Focus Area Location. Make sure the Focus Area Location is valid Well-Known Text (WKT).",
                    x => x.FocusAreaLocationWkt));
            }

            return errors;
        }
    }
}
