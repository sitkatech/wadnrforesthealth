using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.Controllers
{
    [TestFixture]
    public class ProjectControllerTest : DatabaseDirectAccessTestFixtureBase
    {
        /// <summary>
        ///  **** If this unit test fails, that means a new FK to dbo.Project was added.
        /// Make sure this FK/table is added to the delete statements in the stored procedure 'dbo.pBulkDeleteProjects'
        /// </summary>
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void FlagChangesToForeignKeysReferencingProjectTableForDevelopersToUpdateBulkDeleteProjectsStoredProcedure()
        {
            const string sqlQueryString = @"
                        SELECT  
                            tab1.name AS [table],
                            col1.name AS [column],
                            obj.name AS FK_NAME
                        FROM sys.foreign_key_columns fkc
                        INNER JOIN sys.objects obj
                            ON obj.object_id = fkc.constraint_object_id
                        INNER JOIN sys.tables tab1
                            ON tab1.object_id = fkc.parent_object_id
                        INNER JOIN sys.columns col1
                            ON col1.column_id = parent_column_id AND col1.object_id = tab1.object_id
						INNER JOIN sys.tables tab2
							ON tab2.object_id = fkc.referenced_object_id
						INNER JOIN sys.schemas sch
							ON tab1.schema_id = sch.schema_id
						WHERE
							tab2.name = 'Project' and sch.name = 'dbo'
                        ORDER BY tab1.name;";
            var result = ExecAdHocSql(sqlQueryString);
            Approvals.Verify(result.TableToHumanReadableString());
        }

        /// <summary>
        ///  **** If this unit test fails, that means a new FK to dbo.ProjectUpdateBatch was added.
        /// Make sure this FK/table is added to the delete statements in the stored procedure 'dbo.pBulkDeleteProjects'
        /// </summary>
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void FlagChangesToForeignKeysReferencingProjectUpdateBatchTableForDevelopersToUpdateBulkDeleteProjectsStoredProcedure()
        {
            const string sqlQueryString = @"
                        SELECT  
                            tab1.name AS [table],
                            col1.name AS [column],
                            obj.name AS FK_NAME
                        FROM sys.foreign_key_columns fkc
                        INNER JOIN sys.objects obj
                            ON obj.object_id = fkc.constraint_object_id
                        INNER JOIN sys.tables tab1
                            ON tab1.object_id = fkc.parent_object_id
                        INNER JOIN sys.columns col1
                            ON col1.column_id = parent_column_id AND col1.object_id = tab1.object_id
						INNER JOIN sys.tables tab2
							ON tab2.object_id = fkc.referenced_object_id
						INNER JOIN sys.schemas sch
							ON tab1.schema_id = sch.schema_id
						WHERE
							tab2.name = 'ProjectUpdateBatch' and sch.name = 'dbo'
                        ORDER BY tab1.name;";
            var result = ExecAdHocSql(sqlQueryString);
            Approvals.Verify(result.TableToHumanReadableString());
        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            BaseFixtureSetup();
        }

        [TestFixtureTearDown]
        public void TestFixtureTeardown()
        {
            BaseFixtureTeardown();
        }

    }
}
