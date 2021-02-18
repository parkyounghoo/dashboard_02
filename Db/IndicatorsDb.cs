using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace KPC_Monitoring.Db
{
    public class IndicatorsDb
    {
        public DataSet getMonitoringList(string date)
        {
            string queryString = "EXEC MONITORING_TAB_4 '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getApiList(string date)
        {
            string queryString = "EXEC API_BATCH_LIST_STATUS '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getMonitoringCount(string date)
        {
            string queryString = "EXEC MONITORING_TAB_1 '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public Int32 getTableDataCount(string tableName)
        {
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                sqlConn.Open();
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandText = "SELECT COUNT(*) FROM " + tableName;
                Int32 count = (Int32)sqlComm.ExecuteScalar();

                return count;
            }
        }

        public DataSet getTableTotalCount(string date)
        {
            string queryString = "EXEC MONITORING_TAB_2 '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getTableDaliyCount(string date)
        {
            string queryString = "EXEC MONITORING_TAB_3 '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getCalendarData(string tableName, string date)
        {
            string queryString = "EXEC MONITORING_CALENDAR '" + date + "', '" + tableName + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }
    }
}