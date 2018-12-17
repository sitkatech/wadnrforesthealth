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
            RelationshipTypeID = projectPerson.ProjectPersonRelationshipTypeID;
        }

        // todo
        //public ProjectPersonSimple(ProjectPersonUpdate projectPerson)
        //{
        //    PersonID = projectPerson.PersonID;
        //    PersonName = projectPerson.Person.PersonName;
        //    RelationshipTypeID = projectPerson.RelationshipTypeID;
        //}

        public ProjectPersonSimple(int organizationID, int relationshipTypeID)
        {
            PersonID = organizationID;
            RelationshipTypeID = relationshipTypeID;
        }

        public int PersonID { get; set; }
        public int RelationshipTypeID { get; set; }
        public string PersonName { get; private set; }

        // todo
        //public ProjectPersonUpdate ToProjectPersonUpdate(ProjectUpdateBatch projectUpdateBatch)
        //{
        //    return new ProjectPersonUpdate(projectUpdateBatch.ProjectUpdateBatchID, PersonID, RelationshipTypeID);
        //}
    }
}