using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;

namespace KPC_Monitoring.Controller
{
    public class CommonController
    {
        /// <summary>
        /// 메시지 box
        /// </summary>
        /// <param name="page">현재 페이지</param>
        /// <param name="message">띄울 메시지</param>
        public static void MessageBox(Page page, string message)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "Message Box", "<script language = 'javascript'>alert('" + message + "')</script>");
        }

        /// <summary>
        /// SHA256암호화
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SHA256Hash(string data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        public DataTable ExcelToDataSet(string filePath)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                DataTable dtExcelRecords = new DataTable();
                con.Open();
                DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                dAdapter.SelectCommand = cmd;
                dAdapter.Fill(dtExcelRecords);

                for (int i = 0; i < dtExcelRecords.Rows.Count; i++)
                {

                    DataRow dr = dtExcelRecords.Rows[i];
                    if (dr["ColumnName"].ToString().Trim() == "")
                    {
                        dtExcelRecords.Rows.RemoveAt(i);
                        i--;
                    }
                }

                return dtExcelRecords;
            }
        }

        public static bool CheckURLValid(string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult);
        }

        public static string RedirectUrl(string authority)
        {
            string url = "";
            if (authority == "A01")
            {
                url = "/View/Monitoring.aspx";
            }
            else if (authority == "A02")
            {
                url = "/View/Monitoring.aspx";
            }
            else if (authority == "A03")
            {
                url = "/View/CollectionModule.aspx";
            }

            return url;
        }

        //메뉴 셋팅
        public static bool setMenuList(string menu, string name)
        {
            string[] menuList = menu.Split('/');

            for (int i = 0; i < menuList.Length; i++)
            {
                if (menuList[i] == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}