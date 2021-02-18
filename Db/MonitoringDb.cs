using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KPC_Monitoring.Db
{
    public class MonitoringDb
    {
        public DataSet getContact_Data(string date)
        {
            string queryString = "EXEC PROC_CONTACT_DATA '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getETL_Data(string date)
        {
            string queryString = "EXEC PROC_ETL_DATA '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getPrivate_Data(string date)
        {
            string queryString = "EXEC PROC_PRIVATE_DATA '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getDomain_Data(string date)
        {
            string queryString = "EXEC PROC_DOMAIN_DATA '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getDomain_Data_Total(string date)
        {
            string queryString = "EXEC PROC_DOMAIN_DATA_TOTAL '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getDomain_Data_Chart(string date, string domainName, string gubun)
        {
            string queryString = "EXEC PROC_DOMAIN_CHART '" + date + "','"+ domainName + "','" + gubun + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getDomain_Data_EDU(string date)
        {
            string queryString = "EXEC PROC_DOMAIN_DATA_EDU '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getDomain_Data_PROD(string date)
        {
            string queryString = "EXEC PROC_DOMAIN_DATA_PROD '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getDomain_Data_PRIV(string date)
        {
            string queryString = "EXEC PROC_DOMAIN_DATA_PRIV '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getPrivate_Data_D(string date)
        {
            string queryString = "EXEC PROC_PRIVATE_DATA_D '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getETL_Data_D(string date)
        {
            string queryString = "EXEC PROC_ETL_DATA_D '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getContact_Data_D(string date)
        {
            string queryString = "EXEC PROC_CONTACT_DATA_D '" + date + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                return ds;
            }
        }

        public DataSet getSche_Status(string dbName, string tableName, string date)
        {
            string queryString = "EXEC PROC_SCHE_STATUS '" + dbName + "', '" + tableName + "', '" + date + "'";
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