//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[InteractionEvent]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    // Table [dbo].[InteractionEvent] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[InteractionEvent]")]
    public partial class InteractionEvent : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected InteractionEvent()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public InteractionEvent(int interactionEventID, int interactionEventTypeID, int staffPersonID, string interactionEventTitle, string interactionEventDescription, DateTime interactionEventDate) : this()
        {
            this.InteractionEventID = interactionEventID;
            this.InteractionEventTypeID = interactionEventTypeID;
            this.StaffPersonID = staffPersonID;
            this.InteractionEventTitle = interactionEventTitle;
            this.InteractionEventDescription = interactionEventDescription;
            this.InteractionEventDate = interactionEventDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public InteractionEvent(int interactionEventTypeID, int staffPersonID, string interactionEventTitle, DateTime interactionEventDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InteractionEventID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.InteractionEventTypeID = interactionEventTypeID;
            this.StaffPersonID = staffPersonID;
            this.InteractionEventTitle = interactionEventTitle;
            this.InteractionEventDate = interactionEventDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public InteractionEvent(InteractionEventType interactionEventType, Person staffPerson, string interactionEventTitle, DateTime interactionEventDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.InteractionEventID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.InteractionEventTypeID = interactionEventType.InteractionEventTypeID;
            this.InteractionEventType = interactionEventType;
            interactionEventType.InteractionEvents.Add(this);
            this.StaffPersonID = staffPerson.PersonID;
            this.StaffPerson = staffPerson;
            staffPerson.InteractionEventsWhereYouAreTheStaffPerson.Add(this);
            this.InteractionEventTitle = interactionEventTitle;
            this.InteractionEventDate = interactionEventDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static InteractionEvent CreateNewBlank(InteractionEventType interactionEventType, Person staffPerson)
        {
            return new InteractionEvent(interactionEventType, staffPerson, default(string), default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return false;
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(InteractionEvent).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.InteractionEvents.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int InteractionEventID { get; set; }
        public int InteractionEventTypeID { get; set; }
        public int StaffPersonID { get; set; }
        public string InteractionEventTitle { get; set; }
        public string InteractionEventDescription { get; set; }
        public DateTime InteractionEventDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return InteractionEventID; } set { InteractionEventID = value; } }

        public virtual InteractionEventType InteractionEventType { get; set; }
        public virtual Person StaffPerson { get; set; }

        public static class FieldLengths
        {
            public const int InteractionEventTitle = 255;
            public const int InteractionEventDescription = 3000;
        }
    }
}