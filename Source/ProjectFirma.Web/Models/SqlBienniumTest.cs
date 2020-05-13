using System;
using System.Data;
using System.Data.SqlClient;
using log4net.Repository.Hierarchy;
using LtInfo.Common.DesignByContract;
using NUnit.Framework;

namespace ProjectFirma.Web.Models
{
    /// <summary>
    /// Tests the SQL Biennium function(s)
    /// </summary>
    [TestFixture]
    public class SqlBienniumTest
    {
        [Test]
        public void GetCurrentBienniumReturnsExpectedResults()
        {
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("1/1/2017"))  == 2015, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("6/27/2017")) == 2015, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("7/1/2017"))  == 2017, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("1/1/2018"))  == 2017, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("6/27/2018")) == 2017, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("7/1/2018"))  == 2017, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("1/1/2019"))  == 2017, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("6/27/2019")) == 2017, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("7/1/2019")) ==  2019, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("12/1/2019")) == 2019, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("1/1/2020")) == 2019, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("7/1/2020")) == 2019, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("7/1/2021")) == 2021, "Bad Biennium Fiscal year");
            Check.Assert(CallSqlGetFiscalYearBienniumForDate(DateTime.Parse("12/1/2021")) == 2021, "Bad Biennium Fiscal year");
        }

        private int CallSqlGetFiscalYearBienniumForDate(DateTime dateTimeToCheck)
        {
            using (SqlConnection sqlConnection = CreateAndOpenSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.fGetFiscalYearBienniumForDate(@dateToCheck)", sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@dateToCheck", dateTimeToCheck.ToShortDateString());
                    int biennium = (int)cmd.ExecuteScalar();
                    return biennium;
                }
            }
        }

        public static SqlConnection CreateAndOpenSqlConnection()
        {
            var db = new UnitTestCommon.ProjectFirmaSqlDatabase();
            var sqlConnection = db.CreateConnection();
            sqlConnection.Open();
            return sqlConnection;
        }

    }
}