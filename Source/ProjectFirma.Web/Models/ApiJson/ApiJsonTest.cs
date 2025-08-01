/*-----------------------------------------------------------------------
<copyright file="AuditLogTest.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
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
using System.Linq.Expressions;
using System.Net;
using NUnit.Framework;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// All of these are separate tests because I'm concerned one or more may become slow, and I want to make it easier to disable
    /// individual URL tests separately. -- SLG 5/14/2019
    /// </summary>
    [TestFixture]
    public class ApiJsonTest : FirmaTestWithContext
    {
        [SetUp]
        public void SetupToRestoreRouteTable()
        {
            // We want to put the RouteTable back to something sensible, in case a prior test (e.g. RouteTableBuilderTest) has trashed it
            RouteTableBuilder.ClearRoutes();
            RouteTableBuilder.Build(FirmaBaseController.AllControllerActionMethods, null, Global.AreasDictionary);
        }

        [Test]
        public void TestProjectJsonApi()
        {
            var projectJsonUrl = SitkaRoute<ProjectController>.BuildAbsoluteUrlHttpsFromExpression(c => c.ProjectJsonApi());
            var webClient = new WebClient();
            var jsonContent = webClient.DownloadString(projectJsonUrl);

            Assert.IsNotEmpty(jsonContent, $"Got nothing at all back from URL {projectJsonUrl}");
        }

        // Tammy doesn't want to see these, commented out for now.
        
        //[Test]
        //public void TestProjectCodeJsonApi()
        //{
        //    var projectCodeJsonUrl = SitkaRoute<ProjectCodeController>.BuildAbsoluteUrlFromExpression(c => c.ProjectCodeJsonApi());
        //    var webClient = new WebClient();
        //    var jsonContent = webClient.DownloadString(projectCodeJsonUrl);

        //    Assert.IsNotEmpty(jsonContent, $"Got nothing at all back from URL {projectCodeJsonUrl}");
        //}

        //[Test]
        //public void TestProgramIndexJsonApi()
        //{
        //    var programIndexJsonUrl = SitkaRoute<ProgramIndexController>.BuildAbsoluteUrlFromExpression(c => c.ProgramIndexJsonApi());
        //    var webClient = new WebClient();
        //    var jsonContent = webClient.DownloadString(programIndexJsonUrl);

        //    Assert.IsNotEmpty(jsonContent, $"Got nothing at all back from URL {programIndexJsonUrl}");
        //}

        [Test]
        public void TestInteractionEventJsonApi()
        {
            var interactionEventJsonUrl = SitkaRoute<InteractionEventController>.BuildAbsoluteUrlHttpsFromExpression(c => c.InteractionEventJsonApi());
            var webClient = new WebClient();
            var jsonContent = webClient.DownloadString(interactionEventJsonUrl);

            Assert.IsNotEmpty(jsonContent, $"Got nothing at all back from URL {interactionEventJsonUrl}");
        }

        [Test]
        public void TestInvoiceJsonApi()
        {
            var invoiceJsonUrl = SitkaRoute<InvoiceController>.BuildAbsoluteUrlHttpsFromExpression(c => c.InvoiceJsonApi());
            var webClient = new WebClient();
            var jsonContent = webClient.DownloadString(invoiceJsonUrl);

            Assert.IsNotEmpty(jsonContent, $"Got nothing at all back from URL {invoiceJsonUrl}");
        }



        [Test]
        public void TestAgreementJsonApi()
        {
            var agreementJsonUrl = SitkaRoute<AgreementController>.BuildAbsoluteUrlHttpsFromExpression(c => c.AgreementJsonApi());
            Console.WriteLine($"Attempting to retrieve JSON URL: {agreementJsonUrl}");
            var webClient = new WebClient();
            string jsonContent = null;
            // Having some trouble with bad SSL handshakes - but only on Sprague, so I'm adding some debugging to hopefully clarify. -- SLG 1/9/2020
            try
            {
                jsonContent = webClient.DownloadString(agreementJsonUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Problem retrieving {agreementJsonUrl}: {e.Message}");
                throw;
            }
            
            //Assert.IsNotEmpty(jsonContent, $"Got nothing at all back from URL {agreementJsonUrl}");
        }

        [Test]
        public void TestGrantJsonApi()
        {
            var grantJsonUrl = SitkaRoute<FundSourceController>.BuildAbsoluteUrlHttpsFromExpression(c => c.GrantJsonApi());
            var webClient = new WebClient();
            var jsonContent = webClient.DownloadString(grantJsonUrl);

            Assert.IsNotEmpty(jsonContent, $"Got nothing at all back from URL {grantJsonUrl}");
        }

        [Test]
        public void TestGrantStatusJsonApi()
        {
            var grantStatusJsonUrl = SitkaRoute<FundSourceController>.BuildAbsoluteUrlHttpsFromExpression(c => c.GrantStatusJsonApi());
            var webClient = new WebClient();
            var jsonContent = webClient.DownloadString(grantStatusJsonUrl);

            Assert.IsNotEmpty(jsonContent, $"Got nothing at all back from URL {grantStatusJsonUrl}");
        }

        [Test]
        public void TestGrantAllocationJsonApi()
        {
            var grantAllocationJsonUrl = SitkaRoute<GrantAllocationController>.BuildAbsoluteUrlHttpsFromExpression(c => c.GrantAllocationJsonApi());
            var webClient = new WebClient();
            var jsonContent = webClient.DownloadString(grantAllocationJsonUrl);

            Assert.IsNotEmpty(jsonContent, $"Got nothing at all back from URL {grantAllocationJsonUrl}");
        }

        [Test]
        public void TestGrantAllocationProgramIndexProjectCodeJsonApi()
        {
            var grantAllocationProgramIndexProjectCodeJsonUrl = SitkaRoute<GrantAllocationProgramIndexProjectCodeController>.BuildAbsoluteUrlHttpsFromExpression(c => c.GrantAllocationProgramIndexProjectCodeJsonApi());
            var webClient = new WebClient();
            var jsonContent = webClient.DownloadString(grantAllocationProgramIndexProjectCodeJsonUrl);

            Assert.IsNotEmpty(jsonContent, $"Got nothing at all back from URL {grantAllocationProgramIndexProjectCodeJsonUrl}");
        }



    }
}
