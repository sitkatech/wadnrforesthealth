using System;
using System.Collections.Generic;
using System.Data;
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


        /// <summary>
        ///     This maps the C# possibilities of null, empty, non-empty to the Sql server side, where a table valued parameter can *never* be null.
        ///     C# side:        Sql Server Side:
        ///     --------        ----------------
        ///     null       =>   empty
        ///     empty      =>   { null }               (list with a single "null" row in it)
        ///     non-empty  =>   non-empty
        ///     That way the Sql Server side code can still detect the three cases. We are using C# null to mean ignore the list.
        ///     The empty list => list with one null row should never get a hit so it should be the same as empty list in
        /// </summary>
        /// <param name="integerList"></param>
        /// <returns></returns>
        public static DataTable IntListToDataTable(List<int> integerList)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(Int32)));
            if (integerList != null)
            {
                // Eventually the table ends up in SQL as:
                // declare @p1 dbo.IDList
                // insert into @p1 values(2)
                // insert into @p1 values(3)
                // insert into @p1 values(4)
                // insert into @p1 values(5)
                // insert into @p1 values(6)
                // insert into @p1 values(7)
                // insert into @p1 values(10)
                // insert into @p1 values(11)
                integerList.ForEach(id => dataTable.Rows.Add(id));
            }
            return dataTable;
        }

    }
}