using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFirma.Web.Models
{
    public class FindYourForesterGridObject
    {

        public int ForesterWorkUnitID { get; set; }
        public string ForesterWorkUnitName { get; set; }
        public int ForesterRoleID { get; set; }
        public string ForesterRoleName { get; set; }
        public string ForesterRoleDisplayName { get; set; }
        public int? PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public FindYourForesterGridObject(int foresterWorkUnitID, string foresterWorkUnitName, int foresterRoleID, string foresterRoleName, string foresterRoleDisplayName, int? personID, string firstName, string lastName, string email, string phone)
        {
            this.ForesterWorkUnitID = foresterWorkUnitID;
            this.ForesterWorkUnitName = foresterWorkUnitName;
            this.ForesterRoleID = foresterRoleID;
            this.ForesterRoleName = foresterRoleName;
            this.ForesterRoleDisplayName = foresterRoleDisplayName;
            this.PersonID = personID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
        }

        public FindYourForesterGridObject(ForesterWorkUnit foresterWorkUnit)
        {
            this.ForesterWorkUnitName = foresterWorkUnit.ForesterWorkUnitName;
            this.ForesterRoleName = foresterWorkUnit.ForesterRole.ForesterRoleName;
            this.ForesterRoleDisplayName = foresterWorkUnit.ForesterRole.ForesterRoleDisplayName;
            if (foresterWorkUnit.PersonID.HasValue)
            {
                this.FirstName = foresterWorkUnit.Person.FirstName;
                this.LastName = foresterWorkUnit.Person.LastName;
                this.Email = foresterWorkUnit.Person.Email;
                this.Phone = foresterWorkUnit.Person.Phone;
            }

        }


        //public FindYourForesterGridObject(vGeoServerFindYourForester vGeoServerFindYourForester)
        //{
        //    this.PrimaryKey = vGeoServerFindYourForester.PrimaryKey;
        //    this.ForesterWorkUnitID = vGeoServerFindYourForester.ForesterWorkUnitID;
        //    this.ForesterWorkUnitName = vGeoServerFindYourForester.ForesterWorkUnitName;
        //    this.ForesterRoleID = vGeoServerFindYourForester.ForesterRoleID;
        //    this.ForesterRoleName = vGeoServerFindYourForester.ForesterRoleName;
        //    this.ForesterRoleDisplayName = vGeoServerFindYourForester.ForesterRoleDisplayName;
        //    this.PersonID = vGeoServerFindYourForester.PersonID;
        //    this.FirstName = vGeoServerFindYourForester.FirstName;
        //    this.LastName = vGeoServerFindYourForester.LastName;
        //    this.Email = vGeoServerFindYourForester.Email;
        //    this.Phone = vGeoServerFindYourForester.Phone;
        //}


    }
}