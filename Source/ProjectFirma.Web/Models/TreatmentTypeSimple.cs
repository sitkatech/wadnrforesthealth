namespace ProjectFirma.Web.Models
{
    public class TreatmentTypeSimple
    {
        public int TreatmentTypeID { get; set; }
        public string TreatmentTypeDisplayName { get; set; }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public TreatmentTypeSimple(TreatmentType treatmentType)
        {
            TreatmentTypeID = treatmentType.TreatmentTypeID;
            TreatmentTypeDisplayName = treatmentType.TreatmentTypeDisplayName;
        }
    }
}