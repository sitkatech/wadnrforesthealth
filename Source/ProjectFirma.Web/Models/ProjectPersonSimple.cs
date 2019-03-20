namespace ProjectFirma.Web.Models
{
    public class ProjectPersonSimple
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public ProjectPersonSimple()
        {
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public ProjectPersonSimple(ProjectPerson projectPerson)
            : this()
        {
            PersonID = projectPerson.PersonID;
            PersonName = projectPerson.Person.FullNameFirstLast;
            ProjectPersonRelationshipTypeID = projectPerson.ProjectPersonRelationshipTypeID;
        }

        public ProjectPersonSimple(ProjectPersonUpdate projectPersonUpdate)
        {
            PersonID = projectPersonUpdate.PersonID;
            PersonName = projectPersonUpdate.Person.FullNameFirstLast;
            ProjectPersonRelationshipTypeID = projectPersonUpdate.ProjectPersonRelationshipTypeID;
        }

        // todo
        //public ProjectPersonSimple(ProjectPersonUpdate projectPerson)
        //{
        //    PersonID = projectPerson.PersonID;
        //    PersonName = projectPerson.Person.PersonName;
        //    RelationshipTypeID = projectPerson.RelationshipTypeID;
        //}

        public ProjectPersonSimple(int personID, int projectPersonRelationshipTypeID)
        {
            PersonID = personID;
            ProjectPersonRelationshipTypeID = projectPersonRelationshipTypeID;
        }

        public int PersonID { get; set; }
        public int ProjectPersonRelationshipTypeID { get; set; }
        public string PersonName { get; private set; }

        
        public ProjectPersonUpdate ToProjectPersonUpdate(ProjectUpdateBatch projectUpdateBatch)
        {
            return new ProjectPersonUpdate(projectUpdateBatch.ProjectUpdateBatchID, PersonID, ProjectPersonRelationshipTypeID);
        }
    }
}