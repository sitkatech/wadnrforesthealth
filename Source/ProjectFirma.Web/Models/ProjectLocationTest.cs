using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class ProjectLocationTest : DatabaseDirectAccessTestFixtureBase
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void AreProjectLocationsNotBad()
        {
            const string sqlQueryString = @"
                        select  p.ProjectID,
                            p.ProjectName, 
                            p.ProjectLocationPoint.STDistance(l.ProjectLocationGeometry.STCentroid()) as Distance
                        from dbo.Project p
                        join (
                            SELECT l.ProjectID, geometry::UnionAggregate(l.ProjectLocationGeometry) as ProjectLocationGeometry
                            from dbo.vGeoServerProjectLocationDetailed l
                            group by l.ProjectID
                        ) l on p.ProjectID = l.ProjectID
                        where p.ProjectLocationPoint.STIntersects(l.ProjectLocationGeometry) = 0
                        and p.ProjectLocationPoint.STDistance(l.ProjectLocationGeometry.STCentroid())
                        > 0.001  
                        and p.CreateGisUploadAttemptID is not null
                        order by p.ProjectID";
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