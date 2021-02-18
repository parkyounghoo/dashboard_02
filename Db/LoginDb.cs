using KPC_Monitoring.Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KPC_Monitoring.Db
{
    public class LoginDb
    {
        /// <summary>
        /// 로그인 사용자 체크
        /// </summary>
        /// <param name="userId">사용자 Id</param>
        /// <param name="userPw">사용자 Pw</param>
        public static bool LoginCheck(string userId, string userPw)
        {
            string pw = CommonController.SHA256Hash(userPw);
            string queryString = "select * from MONITORING_USER where USER_ID = '" + userId + "' and USER_PW = '" + pw + "'";
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = new SqlCommand(queryString, sqlConn);
                _SqlDataAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 패스워드 변경
        /// </summary>
        /// <param name="userId">사용자 ID</param>
        /// <param name="OldPw">기존 Pw</param>
        /// <param name="NewPw">변경될 Pw</param>
        /// <returns></returns>
        public static bool PasswordChange(string userId, string OldPw, string NewPw)
        {
            try
            {
                string Olepw = CommonController.SHA256Hash(OldPw);
                string Newpw = CommonController.SHA256Hash(NewPw);
                string queryString = "update MONITORING_USER set USER_PW = '" + Newpw + "' where USER_ID = '" + userId + "' and USER_PW = '" + Olepw + "'";

                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand(queryString, sqlConn);
                    sqlComm.Connection.Open();
                    sqlComm.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void LoginDateUpdate(string userId)
        {
            try
            {
                string queryString = "update MONITORING_USER set LOGIN_DATE = GETDATE() where USER_ID = '" + userId + "'";
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand(queryString, sqlConn);
                    sqlComm.Connection.Open();
                    sqlComm.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataSet getUserMenu(string userId)
        {
            string queryString = "EXEC PROC_USER_MENU_LIST '" + userId + "'";
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