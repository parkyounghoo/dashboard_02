using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KPC_Monitoring.Db
{
    public class UserDb
    {
        public DataSet getUserList()
        {
            string queryString = "EXEC PROC_USER_LIST";
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