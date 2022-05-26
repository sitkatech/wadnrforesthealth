//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProgramNotificationSent]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    // Table [dbo].[ProgramNotificationSent] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProgramNotificationSent]")]
    public partial class ProgramNotificationSent : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProgramNotificationSent()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProgramNotificationSent(int programNotificationSendID, int programNotificationConfigurationID, int sentToPersonID, int projectID, DateTime programNotificationSentDate) : this()
        {
            this.ProgramNotificationSendID = programNotificationSendID;
            this.ProgramNotificationConfigurationID = programNotificationConfigurationID;
            this.SentToPersonID = sentToPersonID;
            this.ProjectID = projectID;
            this.ProgramNotificationSentDate = programNotificationSentDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProgramNotificationSent(int programNotificationConfigurationID, int sentToPersonID, int projectID, DateTime programNotificationSentDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProgramNotificationSendID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProgramNotificationConfigurationID = programNotificationConfigurationID;
            this.SentToPersonID = sentToPersonID;
            this.ProjectID = projectID;
            this.ProgramNotificationSentDate = programNotificationSentDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProgramNotificationSent(ProgramNotificationConfiguration programNotificationConfiguration, Person sentToPerson, Project project, DateTime programNotificationSentDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProgramNotificationSendID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProgramNotificationConfigurationID = programNotificationConfiguration.ProgramNotificationConfigurationID;
            this.ProgramNotificationConfiguration = programNotificationConfiguration;
            programNotificationConfiguration.ProgramNotificationSents.Add(this);
            this.SentToPersonID = sentToPerson.PersonID;
            this.SentToPerson = sentToPerson;
            sentToPerson.ProgramNotificationSentsWhereYouAreTheSentToPerson.Add(this);
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProgramNotificationSents.Add(this);
            this.ProgramNotificationSentDate = programNotificationSentDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProgramNotificationSent CreateNewBlank(ProgramNotificationConfiguration programNotificationConfiguration, Person sentToPerson, Project project)
        {
            return new ProgramNotificationSent(programNotificationConfiguration, sentToPerson, project, default(DateTime));
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
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProgramNotificationSent).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProgramNotificationSents.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProgramNotificationSendID { get; set; }
        public int ProgramNotificationConfigurationID { get; set; }
        public int SentToPersonID { get; set; }
        public int ProjectID { get; set; }
        public DateTime ProgramNotificationSentDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProgramNotificationSendID; } set { ProgramNotificationSendID = value; } }

        public virtual ProgramNotificationConfiguration ProgramNotificationConfiguration { get; set; }
        public virtual Person SentToPerson { get; set; }
        public virtual Project Project { get; set; }

        public static class FieldLengths
        {

        }
    }
}