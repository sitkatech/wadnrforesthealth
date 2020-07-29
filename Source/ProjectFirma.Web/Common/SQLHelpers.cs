using System.Data.SqlClient;

namespace ProjectFirma.Web.Common
{
    public class SqlHelpers
    {
        public static SqlConnection CreateAndOpenSqlConnection()
        {
            var db = new UnitTestCommon.ProjectFirmaSqlDatabase();
            var sqlConnection = db.CreateConnection();
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}