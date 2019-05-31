using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("InteractionEventID: {InteractionEventID} - InteractionEventTitle: {InteractionEventTitle}")]
    public class InteractionEventApiJson
    {
        public int InteractionEventID { get; set; }
        public int InteractionEventTypeID { get; set; }
        public string InteractionEventTypeName { get; set; }
        public int StaffPersonID { get; set; }
        public string StaffPersonName { get; set; }
        public string InteractionEventTitle { get; set; }
        public string InteractionEventDescription { get; set; }
        public DateTime InteractionEventDate { get; set; }
        public DbGeometry InteractionEventLocationSimple;

        // For use by model binder
        public InteractionEventApiJson()
        {
        }

        public InteractionEventApiJson(InteractionEvent interactionEvent)
        {
            InteractionEventID = interactionEvent.InteractionEventID;
            InteractionEventTypeID = interactionEvent.InteractionEventTypeID;
            InteractionEventTypeName = interactionEvent.InteractionEventType.InteractionEventTypeName;
            StaffPersonID = interactionEvent.StaffPersonID;
            StaffPersonName = interactionEvent.StaffPerson.FullNameFirstLastAndOrgShortName;
            InteractionEventTitle = interactionEvent.InteractionEventTitle;
            InteractionEventDescription = interactionEvent.InteractionEventDescription;
            InteractionEventDate = interactionEvent.InteractionEventDate;
            // Hopefully this serializes OK, but it's currently untested
            InteractionEventLocationSimple = interactionEvent.InteractionEventLocationSimple;
        }

        public static List<InteractionEventApiJson> MakeInteractionEventApiJsonsFromInteractionEvents(List<InteractionEvent> interactionEvents, bool doAlphaSort = true)
        {
            var outgoingInteractionEvents = interactionEvents;
            if (doAlphaSort)
            {
                outgoingInteractionEvents = outgoingInteractionEvents.OrderBy(ia => ia.InteractionEventTitle).ToList();
            }
            return outgoingInteractionEvents.Select(ia => new InteractionEventApiJson(ia)).ToList();
        }
    }
}