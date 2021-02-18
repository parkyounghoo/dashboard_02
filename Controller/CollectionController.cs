using KPC_Monitoring.Db;
using KPC_Monitoring.Model;
using Open_Api_Collection_Module.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPC_Monitoring.Controller
{
    public class CollectionController
    {
        public DataSet ApiList(string userId)
        {
            CollectionModuleDb db = new CollectionModuleDb();

            return db.getApiList(userId);
        }

        public List<TableModel> TablePanelToTableModel(GridView grid)
        {
            List<TableModel> tableList = new List<TableModel>();

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                GridViewRow row = grid.Rows[i];

                TableModel model = new TableModel();
                model.ColumnType = "VARCHAR";
                model.ColumnName = row.Cells[0].Text;
                model.ColumnSize = row.Cells[1].Text;
                model.ColumnDescription = row.Cells[2].Text;

                tableList.Add(model);
            }

            return tableList;
        }

        public string PanelCreateTable(List<TableModel> list, string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE " + tableName + "\n");
            sb.Append("(\n");
            for (int i = 0; i < list.Count; i++)
            {
                TableModel model = list[i];
                if (model.ColumnName != "")
                {
                    if (i == 0)
                    {
                        sb.Append(model.ColumnName + " " + model.ColumnType + "(" + model.ColumnSize + ")\n");
                    }
                    else
                    {
                        sb.Append("," + model.ColumnName + " " + model.ColumnType + "(" + model.ColumnSize + ")\n");
                    }
                }
            }
            sb.Append(",BASE_DATE VARCHAR(10)\n");
            sb.Append(")");

            return sb.ToString();
        }

        public string PanelCreateProc(List<TableModel> list, string procName, string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE PROC " + procName + "\n");
            sb.Append("(\n");
            for (int i = 0; i < list.Count; i++)
            {
                TableModel model = list[i];
                if (model.ColumnName != "")
                {
                    if (i == 0)
                    {
                        sb.Append("@" + model.ColumnName + " " + model.ColumnType + "(" + model.ColumnSize + ")\n");
                    }
                    else
                    {
                        sb.Append(",@" + model.ColumnName + " " + model.ColumnType + "(" + model.ColumnSize + ")\n");
                    }
                }
            }
            sb.Append(",@BASE_DATE VARCHAR(10)\n");
            sb.Append(")\n");
            sb.Append("AS\n");
            sb.Append("BEGIN\n");
            sb.Append("DECLARE @CNT INT\n");
            sb.Append("SELECT\n");
            sb.Append("@CNT = COUNT(*)\n");
            sb.Append("FROM " + tableName + "\n");
            sb.Append("WHERE \n");
            for (int j = 0; j < list.Count; j++)
            {
                TableModel model = list[j];
                if (model.ColumnName != "")
                {
                    if (j == 0)
                    {
                        sb.Append(model.ColumnName + " = @" + model.ColumnName + "\n");
                    }
                    else
                    {
                        sb.Append("AND " + model.ColumnName + " = @" + model.ColumnName + "\n");
                    }
                }
            }
            sb.Append("IF(@CNT = 0)\n");
            sb.Append("BEGIN\n");
            sb.Append("INSERT INTO " + tableName + "\n");
            sb.Append("VALUES\n");
            sb.Append("(\n");
            for (int k = 0; k < list.Count; k++)
            {
                TableModel model = list[k];
                if (model.ColumnName != "")
                {
                    if (k == 0)
                    {
                        sb.Append("@" + model.ColumnName + "\n");
                    }
                    else
                    {
                        sb.Append(",@" + model.ColumnName + "\n");
                    }
                }
            }
            sb.Append(",@BASE_DATE\n");
            sb.Append(")\n");
            sb.Append("END\n");
            sb.Append("END\n");

            return sb.ToString();
        }

        public DataSet CollectionTable(string tableName, string sDate, string eDate, bool allCheck)
        {
            CollectionModuleDb db = new CollectionModuleDb();

            return db.getCollectionTable(tableName, sDate, eDate, allCheck);
        }

        public void ApiListToExcelDownload(Page page, List<API_Model> list, string filePath, string userName, string sDate, string eDate, bool allCheck)
        {
            for (int i = 0; i < list.Count; i++)
            {
                getApiExcel(CollectionTable(list[i].TABLE_NAME, sDate, eDate, allCheck), list[i].FILE_PATH, list[i].FILE_NAME);
            }

            using (var zip = new Ionic.Zip.ZipFile())
            {
                string zipfileName = userName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".zip";
                foreach (API_Model model in list)
                {
                    string encodingFileName = Encoding.GetEncoding("IBM437").GetString(Encoding.Default.GetBytes(model.FILE_NAME));
                    zip.AddEntry(encodingFileName, File.ReadAllBytes(Path.Combine(model.FILE_PATH, model.FILE_NAME)));
                }
                zip.Save(Path.Combine(filePath, zipfileName));

                FileDownload(page, filePath, zipfileName);
            }
        }

        public void FileDownload(Page page, string filePath, string fileName)
        {
            HttpResponse response = page.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("content-disposition",
                            "attachment; filename=" + fileName + ";");
            response.TransmitFile(Path.Combine(filePath,fileName));

            response.Flush();
            response.End();
        }

        public void getApiExcel(DataSet ds, string filePath, string fileName)
        {
            FileStream fs = new FileStream(Path.Combine(filePath, fileName), FileMode.Create, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            {
                string line = string.Join(",", ds.Tables[0].Columns.Cast<object>());
                sw.WriteLine(line);

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    line = "";
                    for (int k = 0; k < ds.Tables[0].Columns.Count; k++)
                    {
                        if (k == 0)
                        {
                            line += DateTimeStringFormat(item.ItemArray[k].ToString());
                        }
                        else
                        {
                            line += "," + DateTimeStringFormat(item.ItemArray[k].ToString());
                        }
                    }
                    sw.WriteLine(line);
                }

                sw.Close();
            }
            fs.Close();
        }

        public string DateTimeStringFormat(string val)
        {
            DateTime resut = new DateTime();
            if (DateTime.TryParse(val, out resut))
            {
                return resut.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                return val.Replace(",","");
            }
        }

        public void setCollectionModule(API_Model model, List<TableModel> list, out string errorMessage)
        {
            CollectionModuleDb db = new CollectionModuleDb();
            if (db.getTableList(model.TABLE_NAME))
            {
                //con 객체 얻기
                SqlConnection sqlConn = db.GetDbConnection();
                //tran 객체 생성
                SqlTransaction tran = sqlConn.BeginTransaction();
                try
                {
                    //API Insert
                    db.InsertApi(sqlConn, tran, model);
                    //테이블 생성
                    db.CreateApiTable(sqlConn, tran, model);
                    //프로시저 생성
                    db.CreateApiProc(sqlConn, tran, model);
                    //테이블 항목설명 저장
                    db.InsertApiDescription(sqlConn, tran, list, model.TABLE_NAME);
                    //최초 api 조회, 저장
                    db.InsertApiList(sqlConn, tran, apiRequest(model, list), model.TABLE_NAME, list);

                    tran.Commit();

                    errorMessage = "성공";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    errorMessage = ex.Message;
                }
                finally
                {
                    sqlConn.Dispose();
                }
            }
            else
            {
                errorMessage = "같은 테이블명이 존재 합니다.";
            }
        }

        public Dictionary<string, List<string>> apiRequest(API_Model model, List<TableModel> tableModelList)
        {
            //주소 생성
            string url = model.API_URL;

            //xml, json
            string result = "";
            //Api List
            Dictionary<string, List<string>> list = new Dictionary<string, List<string>>();
            if (model.API_Response == "JSON")
            {
                JsonParser json = new JsonParser();
                result = json.getJson(url);

                list = json.getJsonList(result, tableModelList);
            }
            else
            {
                XmlParser xml = new XmlParser();
                result = xml.getXml(url);

                list = xml.getXmlList(result, tableModelList);
            }

            return list;
        }
    }
}