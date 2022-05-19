using System.Collections.Generic;
using System.Web.Mvc;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Program
{
    public class EditProgramNotificationConfigurationViewData : FirmaViewData
    {

        public IEnumerable<SelectListItem> RecurrenceIntervals { get; }

        public IEnumerable<SelectListItem> ProgramNotificationTypes { get; }

        public EditProgramNotificationConfigurationViewData(Person currentPerson, IEnumerable<SelectListItem> recurrenceIntervals, IEnumerable<SelectListItem> programNotificationTypes ) : base(currentPerson)
        {
            RecurrenceIntervals = recurrenceIntervals;
            ProgramNotificationTypes = programNotificationTypes;
        }
    }
}
