//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerFindYourForester]
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class vGeoServerFindYourForester
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerFindYourForester()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerFindYourForester(int primaryKey, string foresterWorkUnitName, int foresterRoleID, string foresterRoleName, string foresterRoleDisplayName, int? personID, string firstName, string lastName, string email, string phone) : this()
        {
            this.PrimaryKey = primaryKey;
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

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerFindYourForester(vGeoServerFindYourForester vGeoServerFindYourForester) : this()
        {
            this.PrimaryKey = vGeoServerFindYourForester.PrimaryKey;
            this.ForesterWorkUnitName = vGeoServerFindYourForester.ForesterWorkUnitName;
            this.ForesterRoleID = vGeoServerFindYourForester.ForesterRoleID;
            this.ForesterRoleName = vGeoServerFindYourForester.ForesterRoleName;
            this.ForesterRoleDisplayName = vGeoServerFindYourForester.ForesterRoleDisplayName;
            this.PersonID = vGeoServerFindYourForester.PersonID;
            this.FirstName = vGeoServerFindYourForester.FirstName;
            this.LastName = vGeoServerFindYourForester.LastName;
            this.Email = vGeoServerFindYourForester.Email;
            this.Phone = vGeoServerFindYourForester.Phone;
            CallAfterConstructor(vGeoServerFindYourForester);
        }

        partial void CallAfterConstructor(vGeoServerFindYourForester vGeoServerFindYourForester);

        public int PrimaryKey { get; set; }
        public string ForesterWorkUnitName { get; set; }
        public int ForesterRoleID { get; set; }
        public string ForesterRoleName { get; set; }
        public string ForesterRoleDisplayName { get; set; }
        public int? PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}