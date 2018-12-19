//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectPerson]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ProjectPerson GetProjectPerson(this IQueryable<ProjectPerson> projectPeople, int projectPersonID)
        {
            var projectPerson = projectPeople.SingleOrDefault(x => x.ProjectPersonID == projectPersonID);
            Check.RequireNotNullThrowNotFound(projectPerson, "ProjectPerson", projectPersonID);
            return projectPerson;
        }

        public static void DeleteProjectPerson(this List<int> projectPersonIDList)
        {
            if(projectPersonIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllProjectPeople.RemoveRange(HttpRequestStorage.DatabaseEntities.ProjectPeople.Where(x => projectPersonIDList.Contains(x.ProjectPersonID)));
            }
        }

        public static void DeleteProjectPerson(this ICollection<ProjectPerson> projectPeopleToDelete)
        {
            if(projectPeopleToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllProjectPeople.RemoveRange(projectPeopleToDelete);
            }
        }

        public static void DeleteProjectPerson(this int projectPersonID)
        {
            DeleteProjectPerson(new List<int> { projectPersonID });
        }

        public static void DeleteProjectPerson(this ProjectPerson projectPersonToDelete)
        {
            DeleteProjectPerson(new List<ProjectPerson> { projectPersonToDelete });
        }
    }
}