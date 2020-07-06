using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using log4net;
using log4net.Repository.Hierarchy;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class CurrentBiennium
    {
        protected static readonly ILog Logger = LogManager.GetLogger(typeof(CurrentBiennium));

        public static int GetBienniumFiscalYearForDate(DateTime dateToEvaluate)
        {
            Logger.Info($"Starting GetBienniumFiscalYearForDate");
            string fGetFiscalYearBienniumForDate = "dbo.fGetFiscalYearBienniumForDate";
            int bienniumFiscalYear;
            using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $"Select {fGetFiscalYearBienniumForDate}('{dateToEvaluate}')";
                cmd.Connection = sqlConnection;

                object executeScalarResult = cmd.ExecuteScalar();
                bienniumFiscalYear = System.Convert.ToInt32(executeScalarResult);
            }
            Logger.Info($"Ending GetBienniumFiscalYearForDate");
            return bienniumFiscalYear;
        }

        public static int GetCurrentBienniumFiscalYearFromDatabase()
        {
            Logger.Info($"Starting Getting Current Biennium from DB");
            return GetBienniumFiscalYearForDate(DateTime.Now);
        }
    }
}