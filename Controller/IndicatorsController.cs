using KPC_Monitoring.Db;
using KPC_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace KPC_Monitoring.Controller
{
    public class IndicatorsController
    {
        #region 
        public MONI_TAB_1 setMonitoringController(string date)
        {
            IndicatorsDb db = new IndicatorsDb();

            DataSet ds = db.getMonitoringCount(date);

            MONI_TAB_1 model = new MONI_TAB_1();
            //1. 전체 테이블 카운트
            model.tableCount = ds.Tables[0].Rows[0]["TABLE_COUNT"].ToString();
            //2. 검색일자 성공 카운트
            model.complateY = ds.Tables[1].Rows[0]["COMPLATE_COUNT"].ToString();
            //3. 검색일자 실패 카운트
            model.complateN = ds.Tables[2].Rows[0]["COMPLATE_COUNT"].ToString();
            //4. 전체 테이블 정보
            Int32 cnt = 0;
            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[3].Rows[i];
                cnt += db.getTableDataCount(dr["TABLE_NAME"].ToString());
            }
            model.dataCount = cnt.ToString();

            return model;
        }
        public List<MONI_Model> getMonitoringList(string date)
        {
            IndicatorsDb db = new IndicatorsDb();

            DataSet ds = db.getMonitoringList(date);

            return getMONI_ModelList(ds);
        }

        private List<MONI_Model> getMONI_ModelList(DataSet ds)
        {
            List<MONI_Model> list = new List<MONI_Model>();

            int tot = ds.Tables[0].Rows.Count;
            int cnt = ds.Tables[0].Rows.Count / 4;
            int remainder = ds.Tables[0].Rows.Count % 4;

            if (remainder != 0)
            {
                cnt++;
            }

            for (int i = 0; i < cnt; i++)
            {
                MONI_Model model = new MONI_Model();

                model.TABLE_1 = (i * 4) >= tot ? "" : ds.Tables[0].Rows[i * 4]["TABLE_NAME"].ToString();
                model.TABLE_1_YN = (i * 4) >= tot ? "" : CompleteRgbChange(ds.Tables[0].Rows[i * 4]["COMPLATE_YN"].ToString());
                model.TABLE_2 = (i * 4) + 1 >= tot ? "" : ds.Tables[0].Rows[(i * 4) + 1]["TABLE_NAME"].ToString();
                model.TABLE_2_YN = (i * 4) + 1 >= tot ? "" : CompleteRgbChange(ds.Tables[0].Rows[(i * 4) + 1]["COMPLATE_YN"].ToString());
                model.TABLE_3 = (i * 4) + 2 >= tot ? "" : ds.Tables[0].Rows[(i * 4) + 2]["TABLE_NAME"].ToString();
                model.TABLE_3_YN = (i * 4) + 2 >= tot ? "" : CompleteRgbChange(ds.Tables[0].Rows[(i * 4) + 2]["COMPLATE_YN"].ToString());
                model.TABLE_4 = (i * 4) + 3 >= tot ? "" : ds.Tables[0].Rows[(i * 4) + 3]["TABLE_NAME"].ToString();
                model.TABLE_4_YN = (i * 4) + 3 >= tot ? "" : CompleteRgbChange(ds.Tables[0].Rows[(i * 4) + 3]["COMPLATE_YN"].ToString());

                list.Add(model);
            }

            return list;
        }

        private string CompleteRgbChange(string complete)
        {
            string rgbColor = "background-color: ";
            if (complete == "Y")
            {
                //blue
                rgbColor += "rgb(0, 168, 243);";
            }
            else if (complete == "S")
            {
                //yellow
                rgbColor += "rgb(255, 127, 39);";
            }
            else if (complete == "G")
            {
                //gray
                rgbColor += "rgb(108, 117, 125);";
            }
            else
            {
                //red
                rgbColor += "rgb(215, 0, 15);";
            }

            return rgbColor;
        }
        public List<BATCH_LIST_Model> getApiListController(string date)
        {
            IndicatorsDb db = new IndicatorsDb();

            DataSet ds = db.getApiList(date);

            return setGridImages(ds);
        }

        private List<BATCH_LIST_Model> setGridImages(DataSet ds)
        {
            List<BATCH_LIST_Model> list = new List<BATCH_LIST_Model>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                BATCH_LIST_Model model = new BATCH_LIST_Model();
                model.API_NAME = dr["API_NAME"].ToString();
                model.API_Purpose = dr["API_Purpose"].ToString();
                model.API_Data_Collection = dr["API_Data_Collection"].ToString();
                model.API_Data_Service = dr["API_Data_Service"].ToString();
                model.TABLE_NAME = dr["TABLE_NAME"].ToString();
                model.API_Status = getImageUrl(dr["COMPLATE_YN"].ToString());
                model.ERROR_LOG = dr["COMPLATE_YN"].ToString() == "Y" ? "성공" : dr["ERROR_LOG"].ToString();

                list.Add(model);
            }

            return list;
        }

        private string getImageUrl(string status)
        {
            //if (status == "Y")
            //{
            //    return blue;
            //}
            //else if (status == "N")
            //{
            //    return red;
            //}
            //else
            //{
            //    return orange;
            //}
            return "";
        }

        public string getTableTotalCount(string date, string morrisName, string element)
        {
            IndicatorsDb db = new IndicatorsDb();

            return setScriptText(morrisName, element, db.getTableTotalCount(date));
        }

        public string getTableDaliyCount(string date, string morrisName, string element)
        {
            IndicatorsDb db = new IndicatorsDb();

            return setScriptText(morrisName, element, db.getTableDaliyCount(date));
        }

        public string setScriptText(string morrisName, string element, DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type='text/javascript'>");
            sb.Append("     new Morris." + morrisName + "({");
            sb.Append("         element: '" + element + "',");
            sb.Append("         data: [");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("         { Name: '" + ds.Tables[0].Rows[i]["NAME"] + "', Count: " + ds.Tables[0].Rows[i]["CNT"] + " },");
            }

            sb.Append("         ],");
            sb.Append("         xkey: 'Name',");
            sb.Append("         ykeys: ['Count'],");
            sb.Append("         labels: ['건수'],");
            sb.Append("         hideHover: 'auto',");
            sb.Append("         resize: true,");
            sb.Append("         gridTextSize: 8,");
            sb.Append("         gridTextColor: 'white',");
            sb.Append("     });");
            sb.Append("</script>");

            return sb.ToString();
        }

        public DataSet CalendarData(string tableName, string date)
        {
            IndicatorsDb db = new IndicatorsDb();

            return db.getCalendarData(tableName, date);
        }
        #endregion
    }
}