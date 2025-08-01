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
using System.Data.Entity;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.UnitTestCommon;
using LtInfo.Common.DesignByContract;
using NUnit.Framework;

namespace ProjectFirma.Web.Models
{
    /// <summary>
    /// To test Audit Logging, we need to create, modify, and delete records, exercising as many field types as we can.
    /// It is most important that Audit Logging does not cause a crash. It is also important that Audit Logging 
    /// make proper Audit Logs.
    /// 
    /// I've covered a lot of ground here, but the following classes don't have tests written for them, and could arguably use them.
    /// I've just run out of steam on this effort for now:
    /// 
    /// ProjectClassification
    /// 
    /// -- SLG
    /// </summary>
    [TestFixture]
    public class AuditLogTest : FirmaTestWithContext
    {
        [Test]
        public void GetAuditDescriptionStringIfAnyTest()
        {
            // Arrange
            var dbContext = HttpRequestStorage.DatabaseEntities;
            var objectContext = dbContext.GetObjectContext();
            var testProject = TestFramework.TestProject.Insert(dbContext);
            var originalProjectTypeName = testProject.ProjectType.ProjectTypeName;
            // Act
            var newProjectType = TestFramework.TestProjectType.Create(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            testProject.ProjectTypeID = newProjectType.ProjectTypeID;

            var changeTracker = dbContext.ChangeTracker;
            changeTracker.DetectChanges();
            var modifiedEntries = changeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();
            var dbEntry = modifiedEntries.First();
            var result = AuditLog.GetAuditDescriptionStringIfAnyForProperty(objectContext, dbEntry, "ProjectTypeID", AuditLogEventType.Modified);

            // Assert
            Assert.That(result, Is.EqualTo($"ProjectType: {originalProjectTypeName} changed to {newProjectType.ProjectTypeName}"));
        }

        [Test]
        public void GetAuditDescriptionStringIfAnyTest2()
        {
            // Arrange
            var dbContext = HttpRequestStorage.DatabaseEntities;
            var objectContext = dbContext.GetObjectContext();
            var testProject = TestFramework.TestProject.Insert(dbContext);
            var originalProjectStageName = ProjectStage.Planned.ProjectStageDisplayName;
            // Act
            var newProjectStage = ProjectStage.Cancelled;
            testProject.ProjectStageID = newProjectStage.ProjectStageID;

            var changeTracker = dbContext.ChangeTracker;
            changeTracker.DetectChanges();
            var modifiedEntries = changeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();
            var dbEntry = modifiedEntries.First();
            var result = AuditLog.GetAuditDescriptionStringIfAnyForProperty(objectContext, dbEntry, "ProjectStageID", AuditLogEventType.Modified);

            // Assert
            Assert.That(result, Is.EqualTo($"Project Stage: {originalProjectStageName} changed to {newProjectStage.ProjectStageDisplayName}"));
        }

        [Test]
        public void GetDeletedEntityAuditDescriptionStringTest()
        {
            // Arrange
            var dbContext = HttpRequestStorage.DatabaseEntities;
            var objectContext = dbContext.GetObjectContext();
            var testProject = TestFramework.TestProject.Insert(dbContext);
            // Act
            testProject.DeleteFull(dbContext);

            var changeTracker = dbContext.ChangeTracker;
            changeTracker.DetectChanges();
            var deletedEntries = changeTracker.Entries().Where(e => e.State == EntityState.Deleted).ToList();
            var dbEntry = deletedEntries.First();
            var objectStateEntry = objectContext.ObjectStateManager.GetObjectStateEntry(dbEntry.Entity);
            var objectByKey = objectContext.GetObjectByKey(objectStateEntry.EntityKey);
            var auditableEntityDeleted = (IAuditableEntity) objectByKey;
            var result = auditableEntityDeleted.AuditDescriptionString;

            // Assert
            Assert.That(result, Is.EqualTo(testProject.AuditDescriptionString));
        }

        public void TestGrantAllocationAuditLogging()
        {
            // Get an arbitrary real-word person to do these actions
            var firmaUser = HttpRequestStorage.DatabaseEntities.People.First();

            // Create audit logging
            // --------------------

            // Make a test grant allocation and save it
            var dbContext = HttpRequestStorage.DatabaseEntities;
            var testGrantAllocation = TestFramework.TestFundSourceAllocation.Create(dbContext);
            var testOrganization = TestFramework.TestOrganization.Create(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this GrantAllocation
            System.Diagnostics.Trace.WriteLine($"Looking for {FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} named \"{testGrantAllocation.GrantAllocationName}\" in Audit Log database entries.");
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.OriginalValue.Contains(testGrantAllocation.GrantAllocationName)));

            // Change audit logging
            // --------------------

            // Make changes to the grant allocation
            var newGrantAllocationName = TestFramework.MakeTestName("New Grant Allocation Name");
            testGrantAllocation.GrantAllocationName = newGrantAllocationName;
            testGrantAllocation.Organization = testOrganization;
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this NEW GrantAllocation name
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.NewValue.Contains(newGrantAllocationName)));

            // Delete audit logging
            // --------------------

            testGrantAllocation.DeleteFull(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);
            // Check that the audit log mentions this GrantAllocation name as deleted
            Check.Assert(
                HttpRequestStorage.DatabaseEntities.AuditLogs.SingleOrDefault(
                    al => al.TableName == "GrantAllocation" && al.AuditLogEventTypeID == AuditLogEventType.Deleted.AuditLogEventTypeID && al.RecordID == testGrantAllocation.GrantAllocationID) != null,
                "Could not find deleted Grant Allocation record");
        }

        [Test]
        public void TestProjectTypeAuditLogging()
        {
            // This test can become sluggish and fail when the table is very full, which suggests the table likely needs to be trimmed regularly.
            AuditLog.ClearAuditLogTable();

            // Get an arbitrary real-word person to do these actions
            var firmaUser = HttpRequestStorage.DatabaseEntities.People.First();

            // Create audit logging
            // --------------------

            var dbContext = HttpRequestStorage.DatabaseEntities;
            var testProjectType = TestFramework.TestProjectType.Create(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this ProjectType
            System.Diagnostics.Trace.WriteLine($"Looking for ProjectType named \"{testProjectType.ProjectTypeName}\" in Audit Log database entries.");
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.OriginalValue.Contains(testProjectType.ProjectTypeName)));

            // Change audit logging
            // --------------------

            var newProjectTypeName = TestFramework.MakeTestName("New ProjectTypeName", ProjectType.FieldLengths.ProjectTypeName);
            testProjectType.ProjectTypeName = newProjectTypeName;
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this NEW name
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.NewValue.Contains(newProjectTypeName)));

            // Delete audit logging
            // --------------------

            testProjectType.DeleteFull(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);
            // Check that the audit log mentions this ProjectTypeName as deleted
            Check.Assert(
                HttpRequestStorage.DatabaseEntities.AuditLogs.SingleOrDefault(
                    al => al.TableName == "ProjectType" && al.AuditLogEventTypeID == AuditLogEventType.Deleted.AuditLogEventTypeID && al.RecordID == testProjectType.ProjectTypeID) != null,
                "Could not find deleted ProjectType record");
        }

        [Test]
        public void TestProjectAuditLogging()
        {
            // This test can become sluggish and fail when the table is very full, which suggests the table likely needs to be trimmed regularly.
            AuditLog.ClearAuditLogTable();

            // Get an arbitrary real-word person to do these actions
            var firmaUser = HttpRequestStorage.DatabaseEntities.People.First();

            // Create audit logging
            // --------------------

            // Make a test object and save it
            var dbContext = HttpRequestStorage.DatabaseEntities;

            var testProject = TestFramework.TestProject.Create(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this object
            System.Diagnostics.Trace.WriteLine($"Looking for {FieldDefinition.Project.GetFieldDefinitionLabel()} named \"{testProject.ProjectName}\" in Audit Log database entries.");
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.OriginalValue.Contains(testProject.ProjectName)));

            // Change audit logging
            // --------------------

            // Make changes to the original object
            var newProjectName = TestFramework.MakeTestName($"New {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Name", Project.FieldLengths.ProjectName);
            testProject.ProjectName = newProjectName;
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this NEW name
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.NewValue.Contains(newProjectName)));

            // Delete audit logging
            // --------------------

            // Stash for later; we'll need to clean these up
            var testRegion = testProject.FocusArea.DNRUplandRegion;
            var testFocusArea = testProject.FocusArea;

            testProject.DeleteFull(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);
            // Check that the audit log mentions this Project name as deleted
            Check.Assert(
                HttpRequestStorage.DatabaseEntities.AuditLogs.SingleOrDefault(
                    al => al.TableName == "Project" && al.AuditLogEventTypeID == AuditLogEventType.Deleted.AuditLogEventTypeID && al.RecordID == testProject.ProjectID) != null,
                $"Could not find deleted {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} record");

            // Cleanup
            testRegion.DeleteFull(dbContext);
            testFocusArea.DeleteFull(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);
        }

        [Test]
        public void TestTaxonomyTrunkAuditLogging()
        {
            // This test can become sluggish and fail when the table is very full, which suggests the table likely needs to be trimmed regularly.
            AuditLog.ClearAuditLogTable();

            // Get an arbitrary real-word person to do these actions
            var firmaUser = HttpRequestStorage.DatabaseEntities.People.First();

            // Create audit logging
            // --------------------

            // Make a test object and save it
            var dbContext = HttpRequestStorage.DatabaseEntities;

            var testTaxonomyTrunk = TestFramework.TestTaxonomyTrunk.Create(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this object
            System.Diagnostics.Trace.WriteLine(string.Format("Looking for TaxonomyTrunk named \"{0}\" in Audit Log database entries.", testTaxonomyTrunk.TaxonomyTrunkName));
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.OriginalValue.Contains(testTaxonomyTrunk.TaxonomyTrunkName)));

            // Change audit logging
            // --------------------

            // Make changes to the original object
            var newTaxonomyTrunkName = TestFramework.MakeTestName("New TaxonomyTrunk Name", TaxonomyTrunk.FieldLengths.TaxonomyTrunkName);
            testTaxonomyTrunk.TaxonomyTrunkName = newTaxonomyTrunkName;
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this NEW name
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.NewValue.Contains(newTaxonomyTrunkName)));

            // Delete audit logging
            // --------------------

            testTaxonomyTrunk.DeleteFull(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);
            // Check that the audit log mentions this TaxonomyTrunk name as deleted
            Check.Assert(
                HttpRequestStorage.DatabaseEntities.AuditLogs.SingleOrDefault(
                    al => al.TableName == "TaxonomyTrunk" && al.AuditLogEventTypeID == AuditLogEventType.Deleted.AuditLogEventTypeID && al.RecordID == testTaxonomyTrunk.TaxonomyTrunkID) != null,
                "Could not find deleted TaxonomyTrunk record");
        }

        [Test]
        public void TestOrganizationAuditLogging()
        {
            // This test can become sluggish and fail when the table is very full, which suggests the table likely needs to be trimmed regularly.
            AuditLog.ClearAuditLogTable();

            // Get an arbitrary real-word person to do these actions
            var firmaUser = HttpRequestStorage.DatabaseEntities.People.First();

            // Create audit logging
            // --------------------

            // Make a test object and save it
            var dbContext = HttpRequestStorage.DatabaseEntities;

            var testOrganization = TestFramework.TestOrganization.Create(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this object
            System.Diagnostics.Trace.WriteLine($"Looking for Organization named \"{testOrganization.OrganizationName}\" in Audit Log database entries.");
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.OriginalValue.Contains(testOrganization.OrganizationName)));

            // Change audit logging
            // --------------------

            // Make changes to the original object
            var newOrganizationName = TestFramework.MakeTestName("New Organization Name", Organization.FieldLengths.OrganizationName);
            testOrganization.OrganizationName = newOrganizationName;
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this NEW name
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.NewValue.Contains(newOrganizationName)));

            // Delete audit logging
            // --------------------

            testOrganization.DeleteFull(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);
            // Check that the audit log mentions this Organization name as deleted
            Check.Assert(
                HttpRequestStorage.DatabaseEntities.AuditLogs.SingleOrDefault(
                    al => al.TableName == "Organization" && al.AuditLogEventTypeID == AuditLogEventType.Deleted.AuditLogEventTypeID && al.RecordID == testOrganization.OrganizationID) != null,
                "Could not find deleted Organization record");
        }

        [Test]
        public void TestTaxonomyBranchAuditLogging()
        {
            // This test can become sluggish and fail when the table is very full, which suggests the table likely needs to be trimmed regularly.
            AuditLog.ClearAuditLogTable();

            // Get an arbitrary real-word person to do these actions
            var firmaUser = HttpRequestStorage.DatabaseEntities.People.First();

            // Create audit logging
            // --------------------

            // Make a test object and save it
            var dbContext = HttpRequestStorage.DatabaseEntities;

            var testTaxonomyBranch = TestFramework.TestTaxonomyBranch.Create(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this object
            System.Diagnostics.Trace.WriteLine(string.Format("Looking for TaxonomyBranch named \"{0}\" in Audit Log database entries.", testTaxonomyBranch.TaxonomyBranchName));
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.OriginalValue.Contains(testTaxonomyBranch.TaxonomyBranchName)));

            // Change audit logging
            // --------------------

            // Make changes to the original object
            var newTaxonomyBranchName = TestFramework.MakeTestName("New TaxonomyBranch Name", TaxonomyBranch.FieldLengths.TaxonomyBranchName);
            testTaxonomyBranch.TaxonomyBranchName = newTaxonomyBranchName;
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this NEW name
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.NewValue.Contains(newTaxonomyBranchName)));

            // Delete audit logging
            // --------------------

            testTaxonomyBranch.DeleteFull(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);
            // Check that the audit log mentions this TaxonomyBranch name as deleted
            Check.Assert(
                HttpRequestStorage.DatabaseEntities.AuditLogs.SingleOrDefault(
                    al => al.TableName == "TaxonomyBranch" && al.AuditLogEventTypeID == AuditLogEventType.Deleted.AuditLogEventTypeID && al.RecordID == testTaxonomyBranch.TaxonomyBranchID) != null,
                "Could not find deleted TaxonomyBranch record");
        }

        [Test]
        public void TestProjectNoteAuditLogging()
        {
            // This test can become sluggish and fail when the table is very full, which suggests the table likely needs to be trimmed regularly.
            AuditLog.ClearAuditLogTable();

            // Get an arbitrary real-word person to do these actions
            var firmaUser = HttpRequestStorage.DatabaseEntities.People.First();

            // Create audit logging
            // --------------------

            // Make a test object and save it
            var dbContext = HttpRequestStorage.DatabaseEntities;

            var testProjectNote = TestFramework.TestProjectNote.Create(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this object
            System.Diagnostics.Trace.WriteLine(string.Format("Looking for ProjectNote \"{0}\" in Audit Log database entries.", testProjectNote.Note));
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.OriginalValue.Contains(testProjectNote.Note)));

            // Change audit logging
            // --------------------

            // Make changes to the original object
            var newProjectNoteName = TestFramework.MakeTestName("New ProjectNote", ProjectNote.FieldLengths.Note);
            testProjectNote.Note = newProjectNoteName;
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this NEW name
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.NewValue.Contains(newProjectNoteName)));

            // Delete audit logging
            // --------------------

            testProjectNote.DeleteFull(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);
            // Check that the audit log mentions this ProjectNote name as deleted
            Check.Assert(
                HttpRequestStorage.DatabaseEntities.AuditLogs.SingleOrDefault(
                    al => al.TableName == "ProjectNote" && al.AuditLogEventTypeID == AuditLogEventType.Deleted.AuditLogEventTypeID && al.RecordID == testProjectNote.ProjectNoteID) != null,
                "Could not find deleted ProjectNote record");
        }

        [Test]
        public void TestClassificationAuditLogging()
        {
            // This test can become sluggish and fail when the table is very full, which suggests the table likely needs to be trimmed regularly.
            AuditLog.ClearAuditLogTable();

            // Get an arbitrary real-word person to do these actions
            var firmaUser = HttpRequestStorage.DatabaseEntities.People.First();

            // Create audit logging
            // --------------------

            // Make a test object and save it
            var dbContext = HttpRequestStorage.DatabaseEntities;

            var testClassification = TestFramework.TestClassification.Create(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this object
            System.Diagnostics.Trace.WriteLine($"Looking for Classification \"{testClassification.DisplayName}\" in Audit Log database entries.");
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.OriginalValue.Contains(testClassification.DisplayName)));

            // Change audit logging
            // --------------------

            // Make changes to the original object
            var newClassificationName = TestFramework.MakeTestName("New Classification", Classification.FieldLengths.DisplayName);
            var newClassificationDescription = TestFramework.MakeTestName("New ClassificationDescription");
            testClassification.DisplayName = newClassificationName;
            testClassification.ClassificationDescription = newClassificationDescription;
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);

            // Check that the audit log mentions this NEW name
            Check.Assert(HttpRequestStorage.DatabaseEntities.AuditLogs.Any(al => al.NewValue.Contains(newClassificationName)));

            // Delete audit logging
            // --------------------

            testClassification.DeleteFull(dbContext);
            HttpRequestStorage.DatabaseEntities.SaveChanges(firmaUser);
            // Check that the audit log mentions this Classification name as deleted
            Check.Assert(
                HttpRequestStorage.DatabaseEntities.AuditLogs.SingleOrDefault(
                    al => al.TableName == "Classification" && al.AuditLogEventTypeID == AuditLogEventType.Deleted.AuditLogEventTypeID && al.RecordID == testClassification.ClassificationID) !=
                null,
                "Could not find deleted Classification record");
        }

    }
}
