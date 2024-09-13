using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class ProjectLocationTest : DatabaseDirectAccessTestFixtureBase
    {
        /// <summary>
        /// If this unit test find any bad project locations, they can be fixed by running the SQL commented out in the test below.
        /// </summary>
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

            // The following SQL can be used to fix the bad project locations found by the test above.
            //drop table if exists #tmpA
            //    select p.ProjectID,
            //p.ProjectName, 
            //p.ProjectLocationPoint.STDistance(l.ProjectLocationGeometry.STCentroid()) as Distance,
            //p.ProjectLocationPoint, l.ProjectLocationGeometry, l.ProjectLocationGeometry.STCentroid() as BetterPoint
            //into #tmpA
            //    from dbo.Project p
            //join(
            //    SELECT l.ProjectID, geometry::UnionAggregate(l.ProjectLocationGeometry) as ProjectLocationGeometry
            //from dbo.vGeoServerProjectLocationDetailed l
            //    group by l.ProjectID
            //    ) l on p.ProjectID = l.ProjectID
            //where p.ProjectLocationPoint.STIntersects(l.ProjectLocationGeometry) = 0
            //and p.ProjectLocationPoint.STDistance(l.ProjectLocationGeometry.STCentroid())
            //    > 0.001

            //update dbo.Project
            //set ProjectLocationPoint = BetterPoint
            //from #tmpA x
            //join dbo.Project p on x.ProjectID = p.ProjectID
            //where p.CreateGisUploadAttemptID is not null
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