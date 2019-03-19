/*-----------------------------------------------------------------------
<copyright file="Person.DatabaseContextExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static Person GetPersonByEmail(this IQueryable<Person> people, string email)
        {
            var peopleWithDesiredEmail = people.Where(x => x.Email == email).ToList();
            Check.Require(peopleWithDesiredEmail.Count <= 1, "Found more than one Person with email \"{email}\", emails should be unique in database.");
            var person = peopleWithDesiredEmail.SingleOrDefault();
            return person;
        }

        public static Person GetPersonByPersonUniqueIdentifier(this IQueryable<Person> people, string desiredPersonUniqueIdentifier)
        {
            Check.EnsureNotNull(desiredPersonUniqueIdentifier, "Must look for a particular PersonUniqueIdentifier, not null!");

            // Make sure the GUID we are looking up aligns with the environment (Local,QA, Prod) and authentication method (ADFS, SAW).
            var desiredDeploymentEnvironment = AuthenticatorHelper.GetDeploymentEnvironment();
            var desiredAuthenticator = AuthenticatorHelper.GetAuthenticator(desiredPersonUniqueIdentifier);

            var person = people.ToList().SingleOrDefault(p => IsMatchingPersonEnvironmentCredentials(p, desiredPersonUniqueIdentifier, desiredDeploymentEnvironment, desiredAuthenticator));

            return person;
        }

        private static bool IsMatchingPersonEnvironmentCredentials(Person person, string desiredPersonUniqueIdentifier, DeploymentEnvironment desiredDeploymentEnvironment, Authenticator desiredAuthenticator)
        {
            return person.PersonEnvironmentCredentials.ToList().Any(pec => pec.DeploymentEnvironment.DeploymentEnvironmentID == desiredDeploymentEnvironment.DeploymentEnvironmentID &&
                                                                           pec.Authenticator.AuthenticatorID == desiredAuthenticator.AuthenticatorID &&
                                                                           pec.PersonUniqueIdentifier == desiredPersonUniqueIdentifier);
        }

        public static Person GetPersonByWebServiceAccessToken(this IQueryable<Person> people, Guid webServiceAccessToken)
        {
            var person = people.SingleOrDefault(x => x.WebServiceAccessToken == webServiceAccessToken);
            return person;
        }

        public static List<Person> GetActivePeople(this IQueryable<Person> people)
        {
            return people.Where(x => x.IsActive).ToList().OrderBy(ht => ht.FullNameLastFirst).ToList();
        }

        public static List<Person> GetPeopleWhoReceiveNotifications(this IQueryable<Person> people)
        {
            return people.ToList().Where(x => x.ShouldReceiveNotifications()).OrderBy(ht => ht.FullNameLastFirst).ToList();
        }

        public static List<Person> GetPeopleWhoReceiveSupportEmails(this IQueryable<Person> people)
        {
            return people.ToList().Where(x => x.ReceiveSupportEmails && x.IsActive).OrderBy(ht => ht.FullNameLastFirst).ToList();
        }
    }
}
