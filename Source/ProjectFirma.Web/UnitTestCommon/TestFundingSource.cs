﻿/*-----------------------------------------------------------------------
<copyright file="TestFundingSource.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public static class TestFundingSource
        {
            public static FundingSource Create()
            {
                var organization = TestOrganization.Create();
                var fundingSource = FundingSource.CreateNewBlank(organization);
                fundingSource.IsActive = true;
                return fundingSource;
            }

            public static FundingSource Create(Organization organization, string fundingSourceName)
            {
                var fundingSource = new FundingSource(organization, string.Format("{0}{1}", organization.OrganizationName, fundingSourceName), true);
                return fundingSource;
            }

            public static FundingSource CreateWithoutChangingName(Organization organization, string fundingSourceName)
            {
                var fundingSource = new FundingSource(organization, fundingSourceName, true);
                return fundingSource;
            }

            public static FundingSource Create(DatabaseEntities dbContext)
            {
                var organization = TestOrganization.Insert(dbContext);
                string testFundingSourceName = TestFramework.MakeTestName("Test Funding Source");
                var fundingSource = new FundingSource(organization, testFundingSourceName, true);

                dbContext.FundingSources.Add(fundingSource);
                return fundingSource;
            }

            public static FundingSource Insert(DatabaseEntities dbContext)
            {
                var fundingSource = Create(dbContext);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                return fundingSource;
            }
        }
    }
}